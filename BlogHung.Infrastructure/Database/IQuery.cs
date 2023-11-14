using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogHung.Infrastructure.Database
{
    public interface IQuery
    {
        //
        // Parameters:
        //   connectionString:
        //
        //   sql:
        //
        //   param:
        //
        //   commandTimeout:
        int Execute(string connectionString, string sql, object param = null, int commandTimeout = 20);
        //
        // Parameters:
        //   connectionString:
        //
        //   sql:
        //
        //   param:
        //
        //   commandTimeout:
        Task<int> ExecuteAsync(string connectionString, string sql, object param = null, int commandTimeout = 20);
        //
        // Parameters:
        //   connectionString:
        //
        //   sql:
        //
        //   param:
        //
        //   commandTimeout:
        T ExecuteScalar<T>(string connectionString, string sql, object param = null, int commandTimeout = 20);
        //
        // Parameters:
        //   connectionString:
        //
        //   sql:
        //
        //   param:
        //
        //   commandTimeout:
        Task<T> ExecuteScalarAsync<T>(string connectionString, string sql, object param = null, int commandTimeout = 20);
        //
        // Parameters:
        //   connectionString:
        //
        //   sql:
        //
        //   param:
        //
        //   commandTimeout:
        //
        // Type parameters:
        //   T:
        IEnumerable<T> Query<T>(string connectionString, string sql, object param = null, int commandTimeout = 20);
        //
        // Parameters:
        //   dataprovider:
        //
        //   param:
        //
        //   commandTimeout:
        //
        // Type parameters:
        //   T:
        IEnumerable<T> Query<T>(string dataprovider, object param = null, int commandTimeout = 20);
        //
        // Parameters:
        //   connectionString:
        //
        //   sql:
        //
        //   param:
        //
        //   commandTimeout:
        //
        // Type parameters:
        //   T:
        Task<IEnumerable<T>> QueryAsync<T>(string connectionString, string sql, object param = null, int commandTimeout = 20);
        //
        // Parameters:
        //   dataprovider:
        //
        //   param:
        //
        //   commandTimeout:
        //
        // Type parameters:
        //   T:
        Task<IEnumerable<T>> QueryAsync<T>(string dataprovider, object param = null, int commandTimeout = 20);
        //
        // Parameters:
        //   dataprovider:
        //
        //   param:
        //
        //   commandTimeout:
        //
        // Type parameters:
        //   T1:
        //
        //   T2:
        //
        //   T3:
        //
        //   T4:
        (IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>) QueryMulitple<T1, T2, T3, T4>(string dataprovider, object param = null, int commandTimeout = 20);
        //
        // Parameters:
        //   dataprovider:
        //
        //   param:
        //
        //   commandTimeout:
        //
        // Type parameters:
        //   T1:
        //
        //   T2:
        //
        //   T3:
        (IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>) QueryMulitple<T1, T2, T3>(string dataprovider, object param = null, int commandTimeout = 20);
        //
        // Parameters:
        //   connectionString:
        //
        //   sql:
        //
        //   param:
        //
        //   commandTimeout:
        //
        // Type parameters:
        //   T1:
        //
        //   T2:
        //
        //   T3:
        (IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>) QueryMulitple<T1, T2, T3>(string connectionString, string sql, object param = null, int commandTimeout = 20);
        //
        // Parameters:
        //   connectionString:
        //
        //   sql:
        //
        //   param:
        //
        //   commandTimeout:
        //
        // Type parameters:
        //   T1:
        //
        //   T2:
        //
        //   T3:
        //
        //   T4:
        (IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>) QueryMulitple<T1, T2, T3, T4>(string connectionString, string sql, object param = null, int commandTimeout = 20);
        //
        // Parameters:
        //   dataprovider:
        //
        //   param:
        //
        //   commandTimeout:
        //
        // Type parameters:
        //   T1:
        //
        //   T2:
        (IEnumerable<T1>, IEnumerable<T2>) QueryMulitple<T1, T2>(string dataprovider, object param = null, int commandTimeout = 20);
        //
        // Parameters:
        //   connectionString:
        //
        //   sql:
        //
        //   param:
        //
        //   commandTimeout:
        //
        // Type parameters:
        //   T1:
        //
        //   T2:
        (IEnumerable<T1>, IEnumerable<T2>) QueryMulitple<T1, T2>(string connectionString, string sql, object param = null, int commandTimeout = 20);
        //
        // Parameters:
        //   dataprovider:
        //
        //   param:
        //
        //   commandTimeout:
        //
        // Type parameters:
        //   T1:
        //
        //   T2:
        Task<(IEnumerable<T1>, IEnumerable<T2>)> QueryMulitpleAsync<T1, T2>(string dataprovider, object param = null, int commandTimeout = 20);
        //
        // Parameters:
        //   connectionString:
        //
        //   sql:
        //
        //   param:
        //
        //   commandTimeout:
        //
        // Type parameters:
        //   T1:
        //
        //   T2:
        Task<(IEnumerable<T1>, IEnumerable<T2>)> QueryMulitpleAsync<T1, T2>(string connectionString, string sql, object param = null, int commandTimeout = 20);
        //
        // Parameters:
        //   connectionString:
        //
        //   sql:
        //
        //   param:
        //
        //   commandTimeout:
        //
        // Type parameters:
        //   T1:
        //
        //   T2:
        //
        //   T3:
        Task<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>)> QueryMulitpleAsync<T1, T2, T3>(string connectionString, string sql, object param = null, int commandTimeout = 20);
        //
        // Parameters:
        //   dataprovider:
        //
        //   param:
        //
        //   commandTimeout:
        //
        // Type parameters:
        //   T1:
        //
        //   T2:
        //
        //   T3:
        Task<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>)> QueryMulitpleAsync<T1, T2, T3>(string dataprovider, object param = null, int commandTimeout = 20);
    }
}
