using System;
using System.IO;

namespace No7.Solution
{
    public abstract class DbHandler
    {
        #region Public API
        public void Handle(Stream stream, string databaseName)
        {
            InputValidation(stream, databaseName);
            HandleInner(stream, databaseName);
        }
        #endregion

        #region Private methods
        protected abstract void HandleInner(Stream stream, string databaseName);

        private  void InputValidation(Stream stream, string databaseName)
        {
            if (stream == null)
            {
                throw new ArgumentNullException($"{nameof(stream)} can't be equal to null!");
            }

            if (string.IsNullOrEmpty(databaseName))
            {
                throw new ArgumentException($"{nameof(databaseName)} can't be equal to null or empty!");
            }
        }
        #endregion
    }
}

//Решил реализовать шаблонный метод для хэндлеров, т.к. подумал, что в БД мб добавлена запись любого типа.
    //Данный класс - основа для всей иерархии(состоящей из двух классов :) )