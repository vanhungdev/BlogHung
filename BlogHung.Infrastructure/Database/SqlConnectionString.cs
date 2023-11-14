using BlogHung.Infrastructure.Utilities;

namespace BlogHung.Infrastructure.Database
{
    public static class SqlConnectionString
    {
        /// <summary>
        /// SalePlatformRead
        /// </summary>
        public static string DatabaseRead
        {
            get
            {
                return Helper.Settings.ConnectionStringSettings.SqlServerConnect;
            }
        }  
    }
}
