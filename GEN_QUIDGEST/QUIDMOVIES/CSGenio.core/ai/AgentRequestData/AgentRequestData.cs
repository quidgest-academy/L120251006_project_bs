using CSGenio.business;
using System.Collections.Generic;

public class AgentRequestData
{
    public object JsonSchema { get; set; }
    public string Prompt { get; set; }
    public string SystemPrompt { get; set; }
    public string Project { get; set; }
    public List<DBFile> Files { get; set; }

    public AgentContextData AgentContextData { get; set; }

    public AgentRequestData(object jsonSchema, string prompt, string systemPrompt, string project, List<DBFile> files, AgentContextData agentContextData)
    {
        JsonSchema = jsonSchema;
        Prompt = prompt;
        SystemPrompt = systemPrompt;
        Project = project;
        Files = files;
        AgentContextData = agentContextData;
    }

    /// <summary>
    /// Flattens the request and context data into a single anonymous object,
    /// preparing it for serialization and transmission to the chatbot backend
    /// </summary>
    public object FlattenData()
    {
        return new
        {
            JsonSchema,
            Prompt,
            SystemPrompt,
            Project,
            Files,
            AgentContextData.Username,
            AgentContextData.AgentId,
            AgentContextData.FormId,
            AgentContextData.UserPrompt,
        };

    }
}

public class AgentContextData
{
    public string Username { get; set; }
    public string AgentId { get; set; }
    public string FormId { get; set; }
    public string UserPrompt { get; set; }
}