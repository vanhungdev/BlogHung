using BlogHung.Infrastructure.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace BlogHung.Infrastructure.Utilities
{
    public static partial class CoreUtility
    {
        public static AppSettings CoreSettings => AppSettingServices.Get;
        private static IHttpClientFactory _httpClientFactory;
        private static Random _coreRandomErorCode = new Random(GetSeedRandom());

        public static void ConfigureHttpClientFactory(IHttpClientFactory httpClient)
        {
            _httpClientFactory = httpClient;
        }

        public static IHttpClientFactory HttpClientFactory()
        {
            return _httpClientFactory;
        }

        public static bool IsDevelopment()
        {
            return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").ToLower().Equals("development");
        }

        public static bool IsStaging()
        {
            return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").ToLower().Equals("staging");
        }

        public static bool IsProduction()
        {
            return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").ToLower().Equals("production");
        }

        public static bool IsLocal()
        {
            try
            {
                return _context.Request.Host.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        public static string CreateHMAC256(string input, string secretKey)
        {
            byte[] inputs = Encoding.UTF8.GetBytes(input);
            byte[] keys = Encoding.UTF8.GetBytes(secretKey);
            HMACSHA256 hmac256 = new HMACSHA256(keys);
            byte[] hashByte = hmac256.ComputeHash(inputs);

            StringBuilder sb = new StringBuilder();
            foreach (byte x in hashByte)
            {
                sb.Append(string.Format("{0:x2}", x));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        public static byte[] CreateHMAC256Byte(string input, string secretKey)
        {
            byte[] inputs = Encoding.UTF8.GetBytes(input);
            byte[] keys = Encoding.UTF8.GetBytes(secretKey);
            HMACSHA256 hmac256 = new HMACSHA256(keys);
            byte[] hashByte = hmac256.ComputeHash(inputs);
            return hashByte;
        }

        /// <summary>
        /// Tạo mã code lỗi
        /// c: mã lỗi chung của Core
        /// </summary>
        /// <returns></returns>
        internal static string GetErrorCode(string prefix = "c")
        {
            var randomValue = _coreRandomErorCode.Next(12345610, 98976910);
            return randomValue.ToString().Insert(0, $"0x{prefix}{DateTime.Now.Day}");
        }

        /// <summary>
        /// Seed random error code
        /// </summary>
        /// <returns></returns>
        private static int GetSeedRandom()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var intBytes = new byte[4];
                rng.GetBytes(intBytes);
                return BitConverter.ToInt32(intBytes, 0);
            }
        }

        public static String ToMD5(this String s)
        {
            var bytes = Encoding.UTF8.GetBytes(s);
            var hash = MD5.Create().ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
        public static String ToSlug(this String s)
        {
            String[][] symbols = {
                                 new String[] { "[áàảãạăắằẳẵặâấầẩẫậ]", "a" },
                                 new String[] { "[đ]", "d" },
                                 new String[] { "[éèẻẽẹêếềểễệ]", "e" },
                                 new String[] { "[íìỉĩị]", "i" },
                                 new String[] { "[óòỏõọôốồổỗộơớờởỡợ]", "o" },
                                 new String[] { "[úùủũụưứừửữự]", "u" },
                                 new String[] { "[ýỳỷỹỵ]", "y" },
                                 new String[] { "[\\s'\";,]", "-" }
                             };
            s = s.ToLower();
            foreach (var ss in symbols)
            {
                s = Regex.Replace(s, ss[0], ss[1]);
            }
            return s;
        }

        public static String ToStringNospace(this String s)
        {
            String[][] symbols = {
                                 new String[] { "[áàảãạăắằẳẵặâấầẩẫậ]", "a" },
                                 new String[] { "[đ]", "d" },
                                 new String[] { "[éèẻẽẹêếềểễệ]", "e" },
                                 new String[] { "[íìỉĩị]", "i" },
                                 new String[] { "[óòỏõọôốồổỗộơớờởỡợ]", "o" },
                                 new String[] { "[úùủũụưứừửữự]", "u" },
                                 new String[] { "[ýỳỷỹỵ]", "y" },
                                 new String[] { "[\\s'\";,]", "" }
                             };
            s = s.ToLower();
            foreach (var ss in symbols)
            {
                s = Regex.Replace(s, ss[0], ss[1]);
            }
            return s;
        }
        public static String GetFileExtension(this String s)
        {
            string[] Name_extension = s.Split('.');
            int countarray = Name_extension.Count();
            s = Name_extension[countarray - 1];
            return s;
        }


    }
}
