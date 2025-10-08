using CSGenio.core.di;
using CSGenio.framework;
using CSGenio.business;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http.Headers;
using MimeKit;
using System.Text.Json;
using Newtonsoft.Json;


namespace CSGenio.core.ai
{
    // Core service for interacting with the Chatbot API. Ensures endpoints are correctly formatted and requests are securely sent.
    public class ChatbotService : IChatbotService
    {
        private readonly HttpClient _httpClient;
        private readonly string _chatbotEndpointUrl;

        public ChatbotService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _chatbotEndpointUrl = Configuration.APIEndpoint?.TrimEnd('/');
        }

        public ChatbotService()
        {
            _httpClient = GenioDI.HttpFactory.CreateClient();
            _chatbotEndpointUrl = Configuration.APIEndpoint?.TrimEnd('/');
        }

        // Constructs the full endpoint URL by appending the specified path.
        private string BuildEndpoint(string path)
        {
            if (string.IsNullOrEmpty(_chatbotEndpointUrl))
            {
                throw new InvalidOperationException("ChatbotEndpointUrl is not configured.");
            }

            string targetUrl = _chatbotEndpointUrl;
            if (!string.IsNullOrEmpty(path))
            {
                path = path.TrimStart('/');
                targetUrl = $"{_chatbotEndpointUrl}/{path}";
            }
            return targetUrl;
        }

        /// <summary>
        /// Sends a JSON-based HTTP request to the Chatbot API.
        /// </summary>
        /// <param name="path">The API endpoint path</param>
        /// <param name="method">The HTTP method to use</param>
        /// <param name="content">The request content as a stream</param>
        /// <returns>The response content as a string</returns>
        public async Task<string> SendChatbotRequestAsync(string path, HttpMethod method, Stream content)
        {
            var request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(BuildEndpoint(path))
            };

            string jsonContent;

            using (StreamReader reader = new StreamReader(content))
                jsonContent = await reader.ReadToEndAsync();

            request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<HttpRequestMessage> BuildRequest(string path, HttpMethod method, Stream content) {
            var request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(BuildEndpoint(path))
            };

            string jsonContent;

            using (StreamReader reader = new StreamReader(content))
                jsonContent = await reader.ReadToEndAsync();

            request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            return request;

        }

        public MultipartFormDataContent BuildHttpRequestData(AgentRequestData requestData)
        {
            var form = new MultipartFormDataContent();
            form.Add(new StringContent(System.Text.Json.JsonSerializer.Serialize(requestData.JsonSchema)), "jsonSchema");
            form.Add(new StringContent(requestData.Prompt), "prompt");
            form.Add(new StringContent(requestData.SystemPrompt), "systemPrompt");
            form.Add(new StringContent(requestData.Project), "project");

            if (requestData is AgentRequestData requestDataWithFiles && requestDataWithFiles.Files != null)
            {
                foreach (DBFile file in requestDataWithFiles.Files)
                {
                    var fileContent = new ByteArrayContent(file.File);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(MimeTypes.GetMimeType(file.Name));
                    form.Add(fileContent, "filesToProcess", file.Name);
                }
            }

            return form;
        }

        public async Task<string> SendChatbotRequestAsync(HttpRequestMessage request)
        {
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        
        /// <summary>
        /// Sends a prompt to the Chatbot API and retrieves the response as a stream.
        /// </summary>
        /// <param name="requestData">The request data as a stream</param>
        /// <returns>The response stream from the API</returns>
        public async Task<Stream> GetChatbotStreamAsync(Stream requestData)
        {
            string jsonContent;
            using (StreamReader reader = new StreamReader(requestData))
                jsonContent = await reader.ReadToEndAsync();

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Post, BuildEndpoint("get-job-result"))
            {
                Content = content
            };

            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStreamAsync();
        }

        /// <summary>
        /// Sends a prompt to the Chatbot API and retrieves the response as a stream(formData Handling).
        /// </summary>
        /// <param name="fields">Formdata fields</param>
        /// <param name="files">Formdata files</param>
        /// <returns>The response stream from the API</returns>
        public async Task<Stream> GetChatbotStreamAsync(
            IEnumerable<KeyValuePair<string, string>> fields,
            IEnumerable<(string FileName, string ContentType, Stream Content)> files)
        {
            var boundary = Guid.NewGuid().ToString();
            var multipartContent = new MultipartFormDataContent(boundary);
            //Add fields
            foreach (var field in fields)
            {
                var stringContent = new StringContent(field.Value);
                multipartContent.Add(stringContent, $"\"{field.Key}\"");
            }
            //Add files
            foreach (var file in files)
            {
                var fileContent = new StreamContent(file.Content);
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(file.ContentType);
                multipartContent.Add(fileContent, file.FileName);
            }
            // Set the Content-Type header with the boundary
            multipartContent.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data")
            {
                Parameters = { new NameValueHeaderValue("boundary", boundary) }
            };
            var request = new HttpRequestMessage(HttpMethod.Post, BuildEndpoint("prompt/submit"))
            {
                Content = multipartContent
            };
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStreamAsync();
        }

        /// <summary>
        /// Calls a specific API endpoint on the Chatbot service and deserializes the response.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the response into.</typeparam>
        /// <param name="requestData">The request data to send.</param>
        /// <param name="endpointPath">The specific API path to call (e.g., "/function/json").</param>
        /// <returns>The deserialized response data.</returns>
        private async Task<T> CallChatbotApiAsync<T>(object requestData, string endpointPath)
        {
            if (string.IsNullOrWhiteSpace(_chatbotEndpointUrl))
            {
                throw new InvalidOperationException("Chatbot endpoint URL is not configured. Please check the client configuration.");
            }

            HttpContent content;
            if (requestData is HttpContent httpContent)
            {
                content = httpContent;
            }
            else
            {
                content = new StringContent(
                    JsonConvert.SerializeObject(requestData),
                    Encoding.UTF8,
                    "application/json");
            }

            var response = await _httpClient.PostAsync($"{_chatbotEndpointUrl}/{endpointPath}", content);
            response.EnsureSuccessStatusCode();

            string responseContent = await response.Content.ReadAsStringAsync();

            try
            {
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<T>>(responseContent);
                return apiResponse.Data;
            }
            catch (Newtonsoft.Json.JsonException jsonEx)
            {
                throw new InvalidOperationException("Failed to deserialize the response from the chatbot.", jsonEx);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while calling the chatbot function.", ex);
            }
        }

        /// <summary>
        /// Calls a specific function on the Chatbot API and deserializes the response.
        /// If files are provided, sends them as multipart/form-data; otherwise, sends JSON.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the response into</typeparam>
        /// <param name="requestData">The request data to send</param>
        /// <returns>The deserialized response data</returns>
        public Task<T> CallChatbotFunctionAsync<T>(AgentRequestData requestData)
        {
            HttpContent httpRequestData = BuildHttpRequestData(requestData);
            return CallChatbotApiAsync<T>(httpRequestData, "function/json");
        }

        /// <summary>
        /// Calls a specific function on the Chatbot API and deserializes the response.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the response into</typeparam>
        /// <param name="requestData">The request data to send</param>
        /// <returns>The deserialized response data</returns>
        [Obsolete("Use CallChatbotFunctionAsync<T>(AgentRequestData requestData) instead.")]
        public Task<T> CallChatbotFunctionAsync<T>(object requestData)
        {
            return CallChatbotApiAsync<T>(requestData, "function/json");
        }


        /// <summary>
        /// Calls an agent prompt on the chatbot API.
        /// </summary>
        public Task<T> CallChabotAgentPromptAsync<T>(AgentRequestData requestData)
        {
            // If the AgentContextData.UserPrompt comes filled it's because a message was sent via the input while on the Agent chat
            string path = string.IsNullOrEmpty(requestData.AgentContextData.UserPrompt) ? "prompt/structured" : "prompt/direct-agent-chat";
            var data = requestData.FlattenData();

            return CallChatbotApiAsync<T>(data, path);
        }

        /// <summary>
        /// Sends an HTTP request to retrieve a file from the chatbot service and returns the response stream.
        /// </summary>
        /// <param name="fileName">The name of the file to retrieve.</param>
        /// <param name="method">The HTTP method to use for the request.</param>
        /// <returns>
        /// A <see cref="Stream"/> containing the file content from the chatbot service.
        /// </returns>
        public async Task<Stream> GetChatbotFileAsync(string fileName, HttpMethod method)
        {
            var filePath = $"temp/{fileName}";
            var request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(BuildEndpoint(filePath))
            };
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStreamAsync();
        }
        
        // Represents the standard API response structure used by the Chatbot service.
        private class ApiResponse<T>
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public T Data { get; set; }
        }
    }
}
