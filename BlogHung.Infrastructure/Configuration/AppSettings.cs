
using static BlogHung.Infrastructure.Configuration.AppSettingDetail;

namespace BlogHung.Infrastructure.Configuration
{
    public class AppSettings
    {
        public IdentityServer IdentityServer { get; set; }
        public ConnectionStringSettings ConnectionStringSettings { get; set; }
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
    }
}
