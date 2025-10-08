using CSGenio.business;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CSGenio.core.ai
{
    public interface IChatbotService
    {
        /// <summary>
        /// Sends a request to the chatbot service with specified path, method and optional content
        /// </summary>
        Task<string> SendChatbotRequestAsync(string path, HttpMethod method, Stream content);
        Task<string> SendChatbotRequestAsync(HttpRequestMessage request);

        /// <summary>
        /// Gets a stream response from the chatbot service
        /// </summary>
        Task<Stream> GetChatbotStreamAsync(Stream requestData);
		
		/// <summary>
        /// Gets a stream response from the chatbot service(formdata handling)
        /// </summary
        Task<Stream> GetChatbotStreamAsync(
            IEnumerable<KeyValuePair<string, string>> fields,
            IEnumerable<(string FileName, string ContentType, Stream Content)> files);
			
        /// <summary>
        /// Gets the respective file from the chatbot server
        /// </summary
        Task<Stream> GetChatbotFileAsync(string fileName, HttpMethod method);
		
        /// <summary>
        /// Makes a function call to the chatbot service and returns the result of type T
        /// </summary>
        Task<T> CallChatbotFunctionAsync<T>(AgentRequestData requestData);

        /// <summary>
        /// Calls a specific function on the Chatbot API and deserializes the response.
        /// </summary>
        [Obsolete("Use CallChatbotFunctionAsync<T>(AgentRequestData requestData) instead.")]
        Task<T> CallChatbotFunctionAsync<T>(object requestData);

        /// <summary>
        /// Prepares a multipart HTTP request for an agent, including files if provided.
        /// </summary>
        MultipartFormDataContent BuildHttpRequestData(AgentRequestData requestData);

        /// <summary>
        /// TODO
        /// </summary>
        Task<HttpRequestMessage> BuildRequest(string path, HttpMethod method, Stream content);

        /// <summary>
        /// Makes a function call to the chatbot service and returns the result of type T
        /// </summary>

        /// <summary>
        /// Makes a function call to the chatbot service and returns the result of type T
        /// </summary>
        Task<T> CallChabotAgentPromptAsync<T>(AgentRequestData requestData);
    }
}