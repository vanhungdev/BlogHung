using System;
using System.Threading.Tasks;

namespace BlogHung.Infrastructure.Utilities
{
    public interface ICoreHttpClient
    {
        Task<T> GetAsync<T>(string clientName, string uri) where T : class;
        Task<T> GetAsync<T>(string clientName, string uri, TimeSpan timeout) where T : class;
        Task<T> GetAsync<T>(string clientName, string uri, object reqObj) where T : class;
        Task<T> GetAsync<T>(string clientName, string uri, object reqObj, TimeSpan timeout) where T : class;
        Task<T> PostAsync<T>(string clientName, string uri, object reqObj) where T : class;
        Task<T> PostAsyncRegisterChat<T>(string clientName, string uri, object reqObj) where T : class;
        Task<T> PostAsync<T>(string clientName, string uri, object reqObj, TimeSpan timeout) where T : class;
        Task<T> PathAsync<T>(string clientName, string uri, object reqObj, string bearer) where T : class;
    }
}
