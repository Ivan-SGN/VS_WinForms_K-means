using System;
using System.Collections.Generic;
using System.Text;

namespace LinearAalgebra
{
    public class MyException : Exception
    {
        public MyException(string message)
        : base(message) { }
    }

    /// <summary>
    /// Задаваемая длина вектора <= 0. 
    /// </summary>
    public class NegativeDimensionException : MyException
    {
        public NegativeDimensionException(string message = "Negative dimentsion")
        : base(message) { }
    }


    /// <summary>
    /// Выход за пределы массива.
    /// </summary>
    public class OutOfRangeException : MyException
    {
        public OutOfRangeException(string message = "Out of range of vector")
        : base(message) { }
    }

    /// <summary>
    /// Математическая ошибка.
    /// </summary>
    public class MathExceptoin : MyException
    {
        public MathExceptoin(string message = "Dividing by zero")
        : base(message) { }
    }
}


