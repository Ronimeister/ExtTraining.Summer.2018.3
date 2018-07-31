using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace No7.Solution
{
    internal static class DbConnection<T>
    {
        #region Public API
        public static void AddToDatabase(string databaseName, List<T> items, IDbInsertable<T> inserter)
        {
            InputValidation(databaseName, items, inserter);
            AddToDatabaseInner(databaseName, items, inserter);
        }
        #endregion

        #region Private methods
        private static void InputValidation(string databaseName, List<T> items, IDbInsertable<T> inserter)
        {
            if (string.IsNullOrEmpty(databaseName))
            {
                throw new ArgumentException($"{nameof(databaseName)} can't be equal to null or empty!");
            }

            if (items == null)
            {
                throw new ArgumentNullException($"{nameof(items)} can't be equal to null!");
            }

            if (inserter == null)
            {
                throw new ArgumentNullException($"{nameof(inserter)} can't be equal to null!");
            }
        }

        private static void AddToDatabaseInner(string databaseName, List<T> items, IDbInsertable<T> inserter)
        {            
            string connectionString = ConfigurationManager.ConnectionStrings[$"{databaseName}"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    inserter.Insert(connection, transaction, items);
                    transaction.Commit();
                }
                connection.Close();
            }

            System.Console.WriteLine("INFO: {0} trades processed", items.Count);
        }
        #endregion
    }
}

//Статический класс, позволяющий добавить в БД запись определенного типа, исходя из параметра inserter, реализующего интерфейс IDbInsertable