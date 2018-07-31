namespace No7.Solution
{
    public interface IDbRecordValidation
    {
        bool IsValid(string[] fields, int lineCount);
    }
}

// Интерфейс, описывающий все типы, созданные для валидации поступивших в них данных
    //Решил использовать эту стратегию для более удобной и расширяемой валидации каких-либо типов