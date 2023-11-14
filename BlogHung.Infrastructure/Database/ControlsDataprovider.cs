using System;
using System.Data;
using System.Reflection;
using static BlogHung.Infrastructure.Configuration.AppSettingDetail;
using static BlogHung.Infrastructure.DatabaseCore.CoreControlsDataprovider;

namespace FtelWebAdmin.Infrastructure.Database
{
    public static class ControlsDataprovider
    {
        /// <summary>
        /// 
        /// </summary>
        private static ConnectionStringSettings connectionStrings;

        public static class StoreAndFunctionInfo
        {

        }

        /// <summary>
        /// get value attribute
        /// </summary>
        /// <param name="propertyName">property cần lấy</param>
        /// <returns></returns>
        public static ControlsDataproviderAttribute GetAttribute(string propertyName)
        {
            PropertyInfo propertyInfo = typeof(StoreAndFunctionInfo).GetProperty(propertyName.ToString());
            ControlsDataproviderAttribute attribute = (ControlsDataproviderAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(ControlsDataproviderAttribute));
            if (attribute != null)
            {
                return attribute;
            }
            return null;
        }
    }
}
