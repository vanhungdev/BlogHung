using Newtonsoft.Json;
using System.Text;
using System.Web;
using System.Net.Http.Headers;
using BlogHung.Infrastructure.Exceptions;
using BlogHung.Infrastructure.Logging;

namespace BlogHung.Infrastructure.Utilities
{
    public class CoreHttpClient : ICoreHttpClient
    {
        private readonly IHttpClientFactory _clientFactory;
        public CoreHttpClient(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="clientName"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string clientName, string uri) where T : class
        {
            var log = new StringBuilder();
            try
            {
                log.AppendLine($"Before call api url: {uri}");
                var clientFactory = _clientFactory.CreateClient(clientName);
                var client = await clientFactory.GetAsync(uri);
                log.AppendLine($"RequestUri: {client.RequestMessage.RequestUri.OriginalString}");

                if (client.IsSuccessStatusCode)
                {
                    var jsonString = await client.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<T>(jsonString);
                    log.AppendLine($"ResponseCode: {client.StatusCode}");
                    log.AppendLine($"Response: {JsonConvert.SerializeObject(result)}");

                    return result;
                }
                else
                {
                    var result = await client.Content.ReadAsStringAsync();
                    log.AppendLine($"ResponseCode: {client.StatusCode}");
                    log.AppendLine($"Response: {result}");

                    throw new HttpException("error_http_status_code");
                    //return default(T);
                }
            }
            catch (Exception ex)
            {
                log.AppendLine($"Exception: {ex}");
                throw new HttpException(ex.ToString());
            }
            finally
            {
                LoggingHelper.SetProperty("call_api_log: ", log.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="clientName"></param>
        /// <param name="uri"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string clientName, string uri, TimeSpan timeout) where T : class
        {
            var log = new StringBuilder();
            try
            {
                log.AppendLine($"Before call api url: {uri}");
                var clientFactory = _clientFactory.CreateClient(clientName);
                clientFactory.Timeout = timeout;
                var client = await clientFactory.GetAsync(uri);
                log.AppendLine($"RequestUri: {client.RequestMessage.RequestUri.OriginalString}");

                if (client.IsSuccessStatusCode)
                {
                    var jsonString = await client.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<T>(jsonString);
                    log.AppendLine($"ResponseCode: {client.StatusCode}");
                    log.AppendLine($"Response: {JsonConvert.SerializeObject(result)}");

                    return result;
                }
                else
                {
                    var result = await client.Content.ReadAsStringAsync();
                    log.AppendLine($"ResponseCode: {client.StatusCode}");
                    log.AppendLine($"Response: {result}");

                    throw new HttpException("error_http_status_code");
                    //return default(T);
                }
            }
            catch (Exception ex)
            {
                log.AppendLine($"Exception: {ex}");
                throw new HttpException(ex.ToString());
            }
            finally
            {
                LoggingHelper.SetProperty("call_api_log: ", log.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="clientName"></param>
        /// <param name="uri"></param>
        /// <param name="reqObj"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string clientName, string uri, object reqObj) where T : class
        {
            var log = new StringBuilder();
            try
            {
                var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(JsonConvert.SerializeObject(reqObj));
                string queryString = string.Join("&", dictionary.Select(x => string.Format("{0}={1}", x.Key, HttpUtility.UrlEncode(x.Value))));
                uri += string.Format("?{0}", queryString);

                log.AppendLine($"Before call api url: {uri}");
                var clientFactory = _clientFactory.CreateClient(clientName);
                var client = await clientFactory.GetAsync(uri);
                log.AppendLine($"RequestUri: {client.RequestMessage.RequestUri.OriginalString}");

                if (client.IsSuccessStatusCode)
                {
                    var jsonString = await client.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<T>(jsonString);
                    log.AppendLine($"ResponseCode: {client.StatusCode}");
                    log.AppendLine($"Response: {JsonConvert.SerializeObject(result)}");

                    return result;
                }
                else
                {
                    var result = await client.Content.ReadAsStringAsync();
                    log.AppendLine($"ResponseCode: {client.StatusCode}");
                    log.AppendLine($"Response: {result}");

                    throw new HttpException("error_http_status_code");
                    //return default(T);
                }
            }
            catch (Exception ex)
            {
                log.AppendLine($"Exception: {ex}");
                throw new HttpException(ex.ToString());
            }
            finally
            {
                LoggingHelper.SetProperty("call_api_log: ", log.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="clientName"></param>
        /// <param name="uri"></param>
        /// <param name="reqObj"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string clientName, string uri, object reqObj, TimeSpan timeout) where T : class
        {
            var log = new StringBuilder();
            try
            {
                var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(JsonConvert.SerializeObject(reqObj));
                string queryString = string.Join("&", dictionary.Select(x => string.Format("{0}={1}", x.Key, HttpUtility.UrlEncode(x.Value))));
                uri += string.Format("?{0}", queryString);

                log.AppendLine($"Before call api url: {uri}");
                var clientFactory = _clientFactory.CreateClient(clientName);
                clientFactory.Timeout = timeout;
                var client = await clientFactory.GetAsync(uri);
                log.AppendLine($"RequestUri: {client.RequestMessage.RequestUri.OriginalString}");

                if (client.IsSuccessStatusCode)
                {
                    var jsonString = await client.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<T>(jsonString);
                    log.AppendLine($"ResponseCode: {client.StatusCode}");
                    log.AppendLine($"Response: {JsonConvert.SerializeObject(result)}");

                    return result;
                }
                else
                {
                    var result = await client.Content.ReadAsStringAsync();
                    log.AppendLine($"ResponseCode: {client.StatusCode}");
                    log.AppendLine($"Response: {result}");

                    throw new HttpException("error_http_status_code");
                    //return default(T);
                }
            }
            catch (Exception ex)
            {
                log.AppendLine($"Exception: {ex}");
                throw new HttpException(ex.ToString());
            }
            finally
            {
                LoggingHelper.SetProperty("call_api_log: ", log.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="clientName"></param>
        /// <param name="uri"></param>
        /// <param name="reqObj"></param>
        /// <returns></returns>
        public async Task<T> PostAsync<T>(string clientName, string uri, object reqObj) where T : class
        {
            var log = new StringBuilder();
            try
            {
                var jsonString = JsonConvert.SerializeObject(reqObj);
                log.AppendLine(string.Concat($"Before call api url: {uri}", Environment.NewLine, $"Input: {jsonString}"));

                var clientFactory = _clientFactory.CreateClient(clientName);
                var data = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var client = await clientFactory.PostAsync(uri, data);
                log.AppendLine($"RequestUri: {client.RequestMessage.RequestUri.OriginalString}");

                if (client.IsSuccessStatusCode)
                {
                    var jsonStringResult = await client.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<T>(jsonStringResult);
                    log.AppendLine($"ResponseCode: {client.StatusCode}");
                    log.AppendLine($"Response: {JsonConvert.SerializeObject(result)}");

                    return result;
                }
                else
                {
                    var result = await client.Content.ReadAsStringAsync();
                    log.AppendLine($"ResponseCode: {client.StatusCode}");
                    log.AppendLine($"Response: {result}");

                    throw new HttpException("error_http_status_code");
                    //return default(T);
                }
            }
            catch (Exception ex)
            {
                log.AppendLine($"Exception: {ex}");
                throw new HttpException(ex.ToString());
            }
            finally
            {
                LoggingHelper.SetProperty("call_api_log: ", log.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="clientName"></param>
        /// <param name="uri"></param>
        /// <param name="reqObj"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public async Task<T> PostAsync<T>(string clientName, string uri, object reqObj, TimeSpan timeout) where T : class
        {
            var log = new StringBuilder();
            try
            {
                var jsonString = JsonConvert.SerializeObject(reqObj);
                log.AppendLine(string.Concat($"Before call api url: {uri}", Environment.NewLine, $"Input: {jsonString}"));

                var clientFactory = _clientFactory.CreateClient(clientName);
                clientFactory.Timeout = timeout;
                var data = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var client = await clientFactory.PostAsync(uri, data);
                log.AppendLine($"RequestUri: {client.RequestMessage.RequestUri.OriginalString}");

                if (client.IsSuccessStatusCode)
                {
                    var jsonStringResult = await client.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<T>(jsonStringResult);
                    log.AppendLine($"ResponseCode: {client.StatusCode}");
                    log.AppendLine($"Response: {JsonConvert.SerializeObject(result)}");

                    return result;
                }
                else
                {
                    var result = await client.Content.ReadAsStringAsync();
                    log.AppendLine($"ResponseCode: {client.StatusCode}");
                    log.AppendLine($"Response: {result}");

                    throw new HttpException("error_http_status_code");
                    //return default(T);
                }
            }
            catch (Exception ex)
            {
                log.AppendLine($"Exception: {ex}");
                throw new HttpException(ex.ToString());
            }
            finally
            {
                LoggingHelper.SetProperty("call_api_log: ", log.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="clientName"></param>
        /// <param name="uri"></param>
        /// <param name="reqObj"></param>
        /// <returns></returns>
        public async Task<T> PostAsyncRegisterChat<T>(string clientName, string uri, object reqObj) where T : class
        {
            var log = new StringBuilder();
            try
            {
                var jsonString = JsonConvert.SerializeObject(reqObj);
                log.AppendLine(string.Concat($"Before call api url: {uri}", Environment.NewLine, $"Input: {jsonString}"));

                var clientFactory = _clientFactory.CreateClient(clientName);
                var client = await clientFactory.PostAsync("http://14.225.192.203"+uri, null);
                log.AppendLine($"RequestUri: {client.RequestMessage.RequestUri.OriginalString}");

                if (client.IsSuccessStatusCode)
                {
                    var jsonStringResult = await client.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<T>(jsonStringResult);
                    log.AppendLine($"ResponseCode: {client.StatusCode}");
                    log.AppendLine($"Response: {JsonConvert.SerializeObject(result)}");

                    return result;
                }
                else
                {
                    var result = await client.Content.ReadAsStringAsync();
                    log.AppendLine($"ResponseCode: {client.StatusCode}");
                    log.AppendLine($"Response: {result}");

                    throw new HttpException("error_http_status_code");
                    //return default(T);
                }
            }
            catch (Exception ex)
            {
                log.AppendLine($"Exception: {ex}");
                throw new HttpException(ex.ToString());
            }
            finally
            {
                LoggingHelper.SetProperty("call_api_log: ", log.ToString());
            }
        }
        public async Task<T> PathAsync<T>(string clientName, string uri, object reqObj, string bearer) where T : class
        {

            var log = new StringBuilder();
            try
            {
                var jsonString = JsonConvert.SerializeObject(reqObj);
                log.AppendLine(string.Concat($"Before call api url: {uri}", Environment.NewLine, $"Input: {jsonString}"));
                var clientFactory = _clientFactory.CreateClient(clientName);
                clientFactory.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);
                var data = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var client = await clientFactory.PatchAsync("http://14.225.192.203" + uri, data);
                log.AppendLine($"RequestUri: {client.RequestMessage.RequestUri.OriginalString}");

                if (client.IsSuccessStatusCode)
                {
                    var jsonStringResult = await client.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<T>(jsonStringResult);
                    log.AppendLine($"ResponseCode: {client.StatusCode}");
                    log.AppendLine($"Response: {JsonConvert.SerializeObject(result)}");

                    return result;
                }
                else
                {
                    var result = await client.Content.ReadAsStringAsync();
                    log.AppendLine($"ResponseCode: {client.StatusCode}");
                    log.AppendLine($"Response: {result}");

                    throw new HttpException("error_http_status_code");
                    //return default(T);
                }
            }
            catch (Exception ex)
            {
                log.AppendLine($"Exception: {ex}");
                throw new HttpException(ex.ToString());
            }
            finally
            {
                LoggingHelper.SetProperty("call_api_log: ", log.ToString());
            }
        }
    }
}
