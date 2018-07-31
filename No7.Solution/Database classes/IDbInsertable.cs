using System.Collections.Generic;
using System.Data.SqlClient;

namespace No7.Solution
{
    internal interface IDbInsertable<T>
    {
        void Insert(SqlConnection connection, SqlTransaction transaction, List<T> items);
    }
}

// Интерфейс, описывающий все сущности, которые могут добавлять записи определенного типа в БД