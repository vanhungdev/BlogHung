using BlogHung.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BlogHung.Infrastructure.Utilities
{
    public static partial class CoreUtility
    {
        private static IHttpContextAccessor _httpContextAccessor;
        private static HttpContext _context => _httpContextAccessor.HttpContext;
        public static void ConfigureContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Lấy access_token
        /// </summary>
        /// <returns></returns>
        public static string GetToken()
        {
            if (IsAuthenticated)
            {
                var accessToken = _context.GetTokenAsync("access_token")
               .GetAwaiter()
               .GetResult();

                if (string.IsNullOrWhiteSpace(accessToken))
                {
                    throw new CoreException("access_token empty");
                }
                return accessToken;
            }
            else
            {
                throw new CoreException("unauthorized");
            }
        }

        /// <summary>
        /// Lấy tên user đăng nhập
        /// </summary>
        /// <returns></returns>
        public static string GetAccountName()
        {
            if (CoreUtility.IsAuthenticated)
            {
                try
                {
                    string? accountName = _context.User?.FindFirstValue(CoreConsts.CoreConsts.ClaimAccountName);
                    if (string.IsNullOrWhiteSpace(accountName))
                    {
                        throw new CoreException("account_name empty");
                    }
                    return accountName;
                }
                catch
                {
                    throw new CoreException("account_name empty");
                }
            }
            else
            {
                throw new CoreException("unauthorized");
            }
        }

        /// <summary>
        /// Lấy Id Token
        /// </summary>
        /// <returns></returns>
        public static string GetJid()
        {
            if (CoreUtility.IsAuthenticated)
            {
                try
                {
                    string? accountName = _context.User?.FindFirstValue(CoreConsts.CoreConsts.ClaimIdToken);
                    if (string.IsNullOrWhiteSpace(accountName))
                    {
                        throw new CoreException("account_jid_empty");
                    }
                    return accountName;
                }
                catch
                {
                    throw new CoreException("account_jid_empty");
                }
            }
            else
            {
                throw new CoreException("unauthorized");
            }
        }



        /// <summary>
        /// Lấy Id user đăng nhập
        /// </summary>
        /// <returns></returns>
        public static int GetAccountId()
        {
            if (CoreUtility.IsAuthenticated)
            {
                try
                {
                    return Convert.ToInt32(_context.User?.FindFirstValue(CoreConsts.CoreConsts.ClaimAccountId));
                }
                catch (Exception ex)
                {
                    throw new CoreException(ex.ToString());
                }
            }
            else
            {
                throw new CoreException("unauthorized");
            }
        }

        /// <summary>
        /// Lấy thông salechannelId
        /// </summary>
        /// <returns></returns>
        public static int GetSaleChannelId()
        {
            if (CoreUtility.IsAuthenticated)
            {
                try
                {
                    return Convert.ToInt16(_context.User?.FindFirstValue(CoreConsts.CoreConsts.ClaimSaleChannelId));
                }
                catch (Exception ex)
                {
                    throw new CoreException(ex.ToString());
                }
            }
            else
            {
                throw new CoreException("unauthorized");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetHeaderByKey(string key)
        {
            try
            {
                if (_context == null)
                {
                    return string.Empty;
                }

                if (_context.Request.Headers.TryGetValue(key, out var value))
                {
                    return value;
                }
                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Kiểm tra client đã đăng nhập
        /// </summary>
        public static bool IsAuthenticated
        {
            get
            {
                var isAuthenticated = _context?.User?.Identities?.FirstOrDefault()?.IsAuthenticated;
                if (!isAuthenticated.HasValue)
                {
                    return false;
                }

                return isAuthenticated.Value;
            }
        }

        /// <summary>
        /// Ghi log
        /// </summary>
        /// <param name="content"></param>
        public static void SetCustomLog(string content)
        {
            string currentLog = string.Empty;
            if (_context != null)
            {
                try
                {
                    if (_context.Items["PUBD_CustomLog"] != null)
                    {
                        currentLog = _context.Items["PUBD_CustomLog"].ToString();
                        _context.Items.Remove("PUBD_CustomLog");
                        currentLog += content + "\r\n";
                    }
                    else
                    {
                        currentLog += content + "\r\n";
                    }
                    _context.Items.Add("PUBD_CustomLog", currentLog);
                }
                catch
                {
                    //_context.Items.Remove("PUBD_CustomLog");
                }
            }
        }

        /// <summary>
        /// Get content log
        /// </summary>
        /// <returns></returns>
        public static string GetCustomLog()
        {
            if (_context != null)
            {
                try
                {
                    if (_context.Items["PUBD_CustomLog"] != null)
                    {
                        return _context.Items["PUBD_CustomLog"].ToString();
                    }
                    return string.Empty;
                }
                catch
                {
                    return string.Empty;
                }
            }
            return string.Empty;
        }
    }
}
