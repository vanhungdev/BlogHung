using System;
using System.Data;
using static BlogHung.Infrastructure.Configuration.AppSettingDetail;

namespace BlogHung.Infrastructure.DatabaseCore
{
    public static class CoreControlsDataprovider
    {
        private static ConnectionStringSettings connectionStrings;

        static CoreControlsDataprovider()
        {
            connectionStrings = new ConnectionStringSettings();
        }

        [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
        public class ControlsDataproviderAttribute : Attribute
        {
            public ControlsDataproviderAttribute(string db, string name, CommandType type)
            {
                this.Database = db;
                this.Name = name;
                this.Type = type;
            }
            public string Database { get; private set; }
            public string Name { get; private set; }
            public CommandType Type { get; private set; }
        }
    }
}
