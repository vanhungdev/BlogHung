using BlogHung.Infrastructure.Configuration;
using Dapper;
using FtelWebAdmin.Infrastructure.Database;
using System.Data;
using System.Data.SqlClient;
using static BlogHung.Infrastructure.DatabaseCore.CoreControlsDataprovider;
using static Dapper.SqlMapper;


namespace BlogHung.Infrastructure.Database
{
    public class SqlServer : IQuery
    {
        private static AppSettings _coreAppSettings => AppSettingServices.Get;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbKey"></param>
        /// <returns></returns>
        private string GetConnectionStrings(string dbKey)
        {
            var obj = _coreAppSettings.ConnectionStringSettings;
            return obj.GetType().GetProperty(dbKey) == null ? null : obj.GetType().GetProperty(dbKey).GetValue(obj, null).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int Execute(string connectionString, string sql, object param = null, int commandTimeout = 20)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    return conn.Execute(sql, param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataprovider"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(string dataprovider, object param = null, int commandTimeout = 20)
        {
            ControlsDataproviderAttribute attribute = ControlsDataprovider.GetAttribute(dataprovider);
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionStrings(attribute.Database)))
                {
                    conn.Open();
                    var result = conn.Query<T>(attribute.Name, param, commandType: attribute.Type, commandTimeout: commandTimeout);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataprovider"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> QueryAsync<T>(string dataprovider, object param = null, int commandTimeout = 20)
        {
            ControlsDataproviderAttribute attribute = ControlsDataprovider.GetAttribute(dataprovider);
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionStrings(attribute.Database)))
                {
                    conn.Open();
                    var result = await conn.QueryAsync<T>(attribute.Name, param, commandType: attribute.Type, commandTimeout: commandTimeout);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public (IEnumerable<T1>, IEnumerable<T2>) QueryMulitple<T1, T2>(string dataprovider, object param = null, int commandTimeout = 20)
        {
            ControlsDataproviderAttribute attribute = ControlsDataprovider.GetAttribute(dataprovider);
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionStrings(attribute.Database)))
                {
                    conn.Open();
                    var result = conn.QueryMultiple(attribute.Name, param, commandType: attribute.Type, commandTimeout: commandTimeout);
                    var resultT1 = result.Read<T1>();
                    var resultT2 = result.Read<T2>();

                    return (resultT1.ToList(), resultT2.ToList());
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public (IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>) QueryMulitple<T1, T2, T3>(string dataprovider, object param = null, int commandTimeout = 20)
        {
            ControlsDataproviderAttribute attribute = ControlsDataprovider.GetAttribute(dataprovider);
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionStrings(attribute.Database)))
                {
                    conn.Open();
                    var result = conn.QueryMultiple(attribute.Name, param, commandType: attribute.Type, commandTimeout: commandTimeout);
                    var resultT1 = result.Read<T1>();
                    var resultT2 = result.Read<T2>();
                    var resultT3 = result.Read<T3>();

                    return (resultT1.ToList(), resultT2.ToList(), resultT3.ToList());
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public (IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>) QueryMulitple<T1, T2, T3, T4>(string dataprovider, object param = null, int commandTimeout = 20)
        {
            ControlsDataproviderAttribute attribute = ControlsDataprovider.GetAttribute(dataprovider);
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionStrings(attribute.Database)))
                {
                    conn.Open();
                    var result = conn.QueryMultiple(attribute.Name, param, commandType: attribute.Type, commandTimeout: commandTimeout);
                    var resultT1 = result.Read<T1>();
                    var resultT2 = result.Read<T2>();
                    var resultT3 = result.Read<T3>();
                    var resultT4 = result.Read<T4>();

                    return (resultT1.ToList(), resultT2.ToList(), resultT3.ToList(), resultT4.ToList());
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="dataprovider"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public async Task<(IEnumerable<T1>, IEnumerable<T2>)> QueryMulitpleAsync<T1, T2>(string dataprovider, object param = null, int commandTimeout = 20)
        {
            ControlsDataproviderAttribute attribute = ControlsDataprovider.GetAttribute(dataprovider);
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionStrings(attribute.Database)))
                {
                    conn.Open();
                    var result = await conn.QueryMultipleAsync(attribute.Name, param, commandType: attribute.Type, commandTimeout: commandTimeout);
                    var resultT1 = result.Read<T1>();
                    var resultT2 = result.Read<T2>();

                    return (resultT1.ToList(), resultT2.ToList());
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="dataprovider"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public async Task<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>)> QueryMulitpleAsync<T1, T2, T3>(string dataprovider, object param = null, int commandTimeout = 20)
        {
            ControlsDataproviderAttribute attribute = ControlsDataprovider.GetAttribute(dataprovider);
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionStrings(attribute.Database)))
                {
                    conn.Open();
                    var result = await conn.QueryMultipleAsync(attribute.Name, param, commandType: attribute.Type, commandTimeout: commandTimeout);
                    var resultT1 = result.Read<T1>();
                    var resultT2 = result.Read<T2>();
                    var resultT3 = result.Read<T3>();

                    return (resultT1.ToList(), resultT2.ToList(), resultT3.ToList());
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(string connectionString, string sql, object param = null, int commandTimeout = 20)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var result = conn.Query<T>(sql, param, commandTimeout: commandTimeout, commandType: CommandType.Text);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public (IEnumerable<T1>, IEnumerable<T2>) QueryMulitple<T1, T2>(string connectionString, string sql, object param = null, int commandTimeout = 20)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var result = conn.QueryMultiple(sql, param, commandType: CommandType.Text, commandTimeout: commandTimeout);
                    var resultT1 = result.Read<T1>();
                    var resultT2 = result.Read<T2>();

                    return (resultT1.ToList(), resultT2.ToList());
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public async Task<(IEnumerable<T1>, IEnumerable<T2>)> QueryMulitpleAsync<T1, T2>(string connectionString, string sql, object param = null, int commandTimeout = 20)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var result = await conn.QueryMultipleAsync(sql, param, commandType: CommandType.Text, commandTimeout: commandTimeout);
                    var resultT1 = result.Read<T1>();
                    var resultT2 = result.Read<T2>();

                    return (resultT1.ToList(), resultT2.ToList());
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public (IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>) QueryMulitple<T1, T2, T3>(string connectionString, string sql, object param = null, int commandTimeout = 20)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var result = conn.QueryMultiple(sql, param, commandType: CommandType.Text, commandTimeout: commandTimeout);
                    var resultT1 = result.Read<T1>();
                    var resultT2 = result.Read<T2>();
                    var resultT3 = result.Read<T3>();

                    return (resultT1.ToList(), resultT2.ToList(), resultT3.ToList());
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public async Task<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>)> QueryMulitpleAsync<T1, T2, T3>(string connectionString, string sql, object param = null, int commandTimeout = 20)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var result = await conn.QueryMultipleAsync(sql, param, commandType: CommandType.Text, commandTimeout: commandTimeout);
                    var resultT1 = result.Read<T1>();
                    var resultT2 = result.Read<T2>();
                    var resultT3 = result.Read<T3>();

                    return (resultT1.ToList(), resultT2.ToList(), resultT3.ToList());
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public (IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>) QueryMulitple<T1, T2, T3, T4>(string connectionString, string sql, object param = null, int commandTimeout = 20)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var result = conn.QueryMultiple(sql, param, commandType: CommandType.Text, commandTimeout: commandTimeout);
                    var resultT1 = result.Read<T1>();
                    var resultT2 = result.Read<T2>();
                    var resultT3 = result.Read<T3>();
                    var resultT4 = result.Read<T4>();

                    return (resultT1.ToList(), resultT2.ToList(), resultT3.ToList(), resultT4.ToList());
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> QueryAsync<T>(string connectionString, string sql, object param = null, int commandTimeout = 20)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var result = await conn.QueryAsync<T>(sql, param, commandType: CommandType.Text, commandTimeout: commandTimeout);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public async Task<int> ExecuteAsync(string connectionString, string sql, object param = null, int commandTimeout = 20)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var result = await conn.ExecuteAsync(sql, param, commandType: CommandType.Text, commandTimeout: commandTimeout);
                    return result;
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public T ExecuteScalar<T>(string connectionString, string sql, object param = null, int commandTimeout = 20)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var result = conn.ExecuteScalar<T>(sql, param, commandType: CommandType.Text, commandTimeout: commandTimeout);
                    return result;
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public async Task<T> ExecuteScalarAsync<T>(string connectionString, string sql, object param = null, int commandTimeout = 20)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var result = await conn.ExecuteScalarAsync<T>(sql, param, commandType: CommandType.Text, commandTimeout: commandTimeout);
                    return result;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
