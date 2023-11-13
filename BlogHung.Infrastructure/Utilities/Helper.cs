using BlogHung.Infrastructure.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace BlogHung.Infrastructure.Utilities
{
    public static partial class Helper
    {
        public static AppSettings Settings => AppSettingServices.Get;
        public static IServiceProvider ServiceProvider { get; set; }
        private static Random _randomErorCode = new Random(GetSeedRandom());

        /// <summary>
        /// Tạo mã code lỗi
        /// b: business
        /// </summary>
        /// <returns></returns>
        public static string GenerateErrorId(string prefix = "b")
        {
            var randomValue = _randomErorCode.Next(12345610, 98976919);
            return randomValue.ToString().Insert(0, $"0x{prefix}{DateTime.Now.Day}");
        }


        /// <summary>
        /// Format ngày tháng từ định dạng chung về 1 format khác
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string FormatDateTime(DateTime dateTime, string format)
        {
            return dateTime.ToString(format);
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

        /// <summary>
        /// Mã hóa SHA256
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ConvertToSHA512(string input)
        {
            using (SHA512 sha512Hash = SHA512.Create())
            {
                //From String to byte array
                byte[] sourceBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha512Hash.ComputeHash(sourceBytes);
                return BitConverter.ToString(hashBytes).Replace("-", String.Empty).ToLower();
            }
        }

        /// <summary>
        /// Dịch ngược cipher text ra plain text theo key input
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public static (bool, string) DecryptString(this string cipherText, string key)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);
            string plainText = "";
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            try
                            {
                                plainText = streamReader.ReadToEnd();
                            }
                            catch (Exception ex)
                            {
                                return (false, ex.ToString());
                            }
                        }
                    }
                }
            }
            return (true, plainText);
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
    }
}
