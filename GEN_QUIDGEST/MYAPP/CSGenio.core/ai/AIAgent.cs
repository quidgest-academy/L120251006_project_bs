

using CSGenio.core.di;
using CSGenio.framework;
using CSGenio.business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json.Linq;
namespace CSGenio.core.ai;

/// <summary>
/// Abstract class representing an AI Agent that interacts with a chatbot service.
/// </summary>
/// <typeparam name="OutData">The type of data expected as the response from the chatbot.</typeparam>
public abstract class AiAgent
{
    /// <summary>
    /// Protected field holding the chatbot service instance.
    /// </summary>
    protected readonly IChatbotService _service;

    /// <summary>
    /// Constructor for the AiAgent, ensuring a valid IChatbotService instance.
    /// </summary>
    /// <param name="service">An implementation of IChatbotService to communicate with the chatbot.</param>
    /// <exception cref="ArgumentNullException">Thrown if the service is null.</exception>
    public AiAgent(IChatbotService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    /// <summary>
    /// Gets the system prompt that defines the behavior of the AI agent.
    /// Must be implemented by derived classes.
    /// </summary>
    public abstract string BuildSystemPrompt();

    /// <summary>
    /// Builds the user prompt sent to the AI agent
    /// Must be implemented by derived classes.
    /// </summary>
    public abstract string BuildUserPrompt();

    /// <summary>
    /// Gets the JSON schema used to format the chatbot's response.
    /// Must be implemented by derived classes.
    /// </summary>
    public abstract object JsonSchema { get; }

    public abstract string AGENT_ID { get; }

    public List<DBFile> Files { get; set; }

    /// <summary>
    /// Sends a prompt to the chatbot and retrieves a response asynchronously.
    /// </summary>
    /// <param name="prompt">The input message sent to the chatbot.</param>
    /// <param name="prompt">The user sending the request</param>
    /// <returns>A task representing the asynchronous operation, returning an instance of OutData.</returns>
    public async Task<OutData> GetResponseAsync<OutData>(User user)
    {

        try
        {
            var tags = new List<KeyValuePair<string, object>>() {
                new("agent", AGENT_ID)
            };
            GenioDI.MetricsOtlp.IncrementCounter("agent_call", 1, tags);
            using (GenioDI.MetricsOtlp.RecordTime("agent_load_time", tags, "ms", "Time to get response from agent"))
            {

                // Construct the request payload with necessary parameters
                AgentRequestData requestData = BuildRequestData();
                Log.Info($"User ${user.Name} called {AGENT_ID}"); 
                // Call the chatbot service asynchronously and return the result
                return await _service.CallChatbotFunctionAsync<OutData>(requestData)
                    .ConfigureAwait(false);            
            }
        }
        catch (Exception ex)
        {
            Log.Error($"Error in agent {AGENT_ID}: {ex.Message}");
            throw;
        }
    }

    public AgentRequestData BuildRequestData()
    {
        var requestData = new AgentRequestData(
                JsonSchema,
                BuildUserPrompt(),
                BuildSystemPrompt(),
                Configuration.Application.Name,
                Files,
                null
            );

        return requestData;
    }

    public AgentRequestData BuildRequestData(AgentContextData contextData)
    {
        var requestData = BuildRequestData();
        requestData.AgentContextData = contextData;

        return requestData;
    }

    /// <summary>
    /// Sends a structutred prompt to the chatbot and returns the jobId of that request
    /// </summary>
    /// <param name="requestData"> The request data </param>
    /// <returns></returns>
    public object GetAgentPromptJobId(AgentContextData contextData)
    {
        var requestData = BuildRequestData(contextData);

        var jobId = _service.CallChabotAgentPromptAsync<object>(requestData)
                        .GetAwaiter()
                        .GetResult();

        return jobId;
    }

    /// <summary>
    /// Sends a prompt to the chatbot and retrieves a response synchronously.
    /// </summary>
    /// <param name="prompt">The input message sent to the chatbot.</param>
    /// <param name="prompt">The user sending the request</param>
    /// <returns>An instance of OutData, parsed from the returned data</returns>
    public OutData GetResponse<OutData>(User user)
    {
        return GetResponseAsync<OutData>(user)
            .GetAwaiter()
            .GetResult();
    }
}

