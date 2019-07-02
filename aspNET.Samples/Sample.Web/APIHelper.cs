using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Sample.Utilities
{
    public class APIHelper
    {

        private static readonly HttpClient client = new HttpClient(); 

        

        private static async Task<List<TypeOfValue>> BasicCallAsync<TypeOfValue>(string Url)
        {
            var content = await client.GetStringAsync(Url);
            return JsonConvert.DeserializeObject<List<TypeOfValue>>(content);
        }

        private static async Task<List<TypeOfValue>> CancellableCallAsync<TypeOfValue>(string Url, CancellationToken cancellationToken)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, Url))
            using (var response = await client.SendAsync(request, cancellationToken))
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<TypeOfValue>>(content);
            }
        }

        private static async Task<List<TypeOfValue>> CheckNetworkErrorCallAsync<TypeOfValue>(string Url, CancellationToken cancellationToken)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, Url))
            using (var response = await client.SendAsync(request, cancellationToken))
            {
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<TypeOfValue>>(content);
            }
        }

        public class ApiException : Exception
        {
            public int StatusCode { get; set; }

            public string Content { get; set; }
        }

        private static async Task<List<TypeOfValue>> CustomExceptionCallAsync<TypeOfValue>(string Url, CancellationToken cancellationToken)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, Url))
            using (var response = await client.SendAsync(request, cancellationToken))
            {
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode == false)
                    throw new ApiException { StatusCode = (int)response.StatusCode, Content = content };

                return JsonConvert.DeserializeObject<List<TypeOfValue>>(content);
            }
        }

        private static T DeserializeJsonFromStream<T>(Stream stream)
        {
            if (stream == null || stream.CanRead == false)
                return default(T);
            
            using (var sr = new StreamReader(stream))
            using (var jtr = new JsonTextReader(sr))
            {
                var jr = new JsonSerializer();
                var searchResult = jr.Deserialize<T>(jtr);
                return searchResult;
            }
        }

        private static async Task<string> StreamToStringAsync(Stream stream)
        {
            string content = null;

            if (stream != null)
            {
                using (var sr = new StreamReader(stream))
                {
                    content = await sr.ReadToEndAsync();
                }
            }
            
            return content;
        }

        private static async Task<List<TypeOfValue>> DeserializeFromStreamCallAsync<TypeOfValue>(string Url, CancellationToken cancellationToken)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, Url))
            using (var response = await client.SendAsync(request, cancellationToken))
            {
                var stream = await response.Content.ReadAsStreamAsync();

                if (response.IsSuccessStatusCode)
                    return DeserializeJsonFromStream<List<TypeOfValue>>(stream);

                var content = await StreamToStringAsync(stream);
                throw new ApiException { StatusCode = (int)response.StatusCode, Content = content };
            }
        }

        private static async Task<List<TypeOfValue>> DeserializeOptimizedFromStreamCallAsync<TypeOfValue>(string Url, CancellationToken cancellationToken)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, Url))
            using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
            {
                var stream = await response.Content.ReadAsStreamAsync();

                if (response.IsSuccessStatusCode)
                    return DeserializeJsonFromStream<List<TypeOfValue>>(stream);

                var content = await StreamToStringAsync(stream);
                throw new ApiException { StatusCode = (int)response.StatusCode, Content = content };
            }
        }

        private static async Task PostBasicAsync(string Url, object content, CancellationToken cancellationToken)
        {
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Post, Url))
            {
                var json = JsonConvert.SerializeObject(content);
                using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    request.Content = stringContent;

                    using (var response = await client
                        .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                        .ConfigureAwait(false))
                    {
                        response.EnsureSuccessStatusCode();
                    }
                }
            }
        }

        public static void SerializeJsonIntoStream(object value, Stream stream)
        {
            using (var sw = new StreamWriter(stream, new UTF8Encoding(false), 1024, true))
            using (var jtw = new JsonTextWriter(sw) { Formatting = Formatting.None })
            {
                var js = new JsonSerializer();
                js.Serialize(jtw, value);
                jtw.Flush();
            }
        }

        private static HttpContent CreateHttpContent(object content)
        {
            HttpContent httpContent = null;

            if (content != null)
            {
                var ms = new MemoryStream();
                SerializeJsonIntoStream(content, ms);
                ms.Seek(0, SeekOrigin.Begin);
                httpContent = new StreamContent(ms);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            return httpContent;
        }

        private static async Task PostStreamAsync(string Url, object content, CancellationToken cancellationToken)
        {
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Post, Url))
            using (var httpContent = CreateHttpContent(content))
            {
                request.Content = httpContent;

                using (var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false))
                {
                    response.EnsureSuccessStatusCode();
                }
            }
        }
    }
}
