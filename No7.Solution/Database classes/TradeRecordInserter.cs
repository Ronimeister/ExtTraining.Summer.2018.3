using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace No7.Solution
{
    internal class TradeRecordInserter : IDbInsertable<TradeRecord>
    {
        public void Insert(SqlConnection connection, SqlTransaction transaction, List<TradeRecord> items)
        {
            InputValidation(connection, transaction, items);
            InsertInner(connection, transaction, items);
        }

        private void InputValidation(SqlConnection connection, SqlTransaction transaction, List<TradeRecord> items)
        {
            if (connection == null)
            {
                throw new ArgumentNullException($"{nameof(connection)} can't be equal to null!");
            }

            if (transaction == null)
            {
                throw new ArgumentNullException($"{nameof(transaction)} can't be equal to null!");
            }

            if (items == null)
            {
                throw new ArgumentNullException($"{nameof(items)} can't be equal to null!");
            }
        }

        private void InsertInner(SqlConnection connection, SqlTransaction transaction, List<TradeRecord> items)
        {
            foreach (var item in items)
            {
                SqlCommand command = connection.CreateCommand();
                command.Transaction = transaction;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "dbo.Insert_Trade";
                command.Parameters.AddWithValue("@sourceCurrency", item.SourceCurrency);
                command.Parameters.AddWithValue("@destinationCurrency", item.DestinationCurrency);
                command.Parameters.AddWithValue("@lots", item.Lots);
                command.Parameters.AddWithValue("@price", item.Price);

                command.ExecuteNonQuery();
            }
        }
    }
}

//Тип, реализующий интерфейс IDInsertable и позволяющий добавлять в БД записи типа TradeRecord