
using static BlogHung.Infrastructure.Configuration.AppSettingDetail;

namespace BlogHung.Infrastructure.Configuration
{
    public class AppSettings
    {
        public IdentityServer IdentityServer { get; set; }
        public ConnectionStringSettings ConnectionStringSettings { get; set; }
        public RedisSettings RedisSettings { get; set; }
        public MongoSettings MongoSettings { get; set; }

    }

    public class AppSettingDetail
    {
        public class IdentityServer
        {
            public string SecretKey { get; set; }
            public string Issuer { get; set; }
            public string Audience { get; set; }
            public int LifeTime { get; set; }
        }
        public class ConnectionStringSettings
        {
            public string SqlServerConnect { get; set; }
        }
        public class MongoSettings
        {
            /// <summary>
            /// Kết nối dùng để ghi
            /// </summary>
            public string ServerWrite { set; get; }

            /// <summary>
            /// Kết nối dùng để đọc
            /// </summary>
            public string ServerRead { set; get; }
        }
        public class RedisSettings
        {
            /// <summary>
            /// Kết nối dùng để ghi
            /// </summary>
            public string ServerWrite { get; set; }

            /// <summary>
            /// Kết nối dùng để đọc
            /// </summary>
            public string ServerRead { get; set; }

            /// <summary>
            /// DB
            /// </summary>
            public int DatabaseNumber { get; set; }
        }
    }
}
