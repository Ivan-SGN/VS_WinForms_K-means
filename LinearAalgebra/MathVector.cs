using LinearAalgebra;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LinearAalgebra
{
    /// <summary> Класс, реализующий интерфейс IMathVector.</summary
    public class MathVector : IMathVector
    {
        private double[] _data;
        private int _dimensions;

        /// <summary>
        /// Конструктор, принимающий длинну вектора.
        /// </summary>
        /// <param name="lenght"> Длинна вектора.</param>
        /// <exception cref="NegativeDimensionException"> Если длинна окражется <= 0.</exception>
        public MathVector(int lenght)
        {
            if (lenght < 0)
                throw new NegativeDimensionException();
            _dimensions = lenght;
            _data = new double[_dimensions];
            for (int i = 0; i < _dimensions; i++)
            {
                _data[i] = 0;
            }
        }

        /// <summary>
        /// Конструктор, инициализрующий вектор через массив.
        /// </summary>
        /// <param name="arr">Массив,из которого беруться числа для инициализации и длинна</param>
        public MathVector(double[] arr)
        {
            _dimensions = arr.Length;
            _data = new double[_dimensions];
            for (int i = 0; i < _dimensions; i++)
            {
                _data[i] = arr[i];
            }
        }

        ///<inheritdoc cref="IMathVector."/>
        /// <summary>
        /// Получить длинну вектора.
        /// </summary>
        /// <returns> Длинна вектора.</returns>
        private double GetLenght()
        {
            double res = 0;
            for (int i = 0; i < _dimensions; i++)
            {
                res += Math.Pow(_data[i], 2);
            }
            return Math.Abs(Math.Sqrt(res));
        }

        /// <inheritdoc cref="IMathVector.Dimensions"/>
        public int Dimensions => _dimensions;

        /// <summary>
        /// Индексатор для доступа к элементам вектора. Нумерация с нуля.
        /// </summary>
        /// <param name="i">Номер элемента вектора.</param>
        /// <returns>i-й элемент ветктора</returns>
        /// <exception cref="OutOfRangeException"> Если был выход за пределы вектора.</exception>
        public double this[int i]
        {
            get
            {
                if (i >= this.Dimensions)
                    throw new OutOfRangeException();
                else
                    return _data[i];
            }
            set
            {
                if (i >= this.Dimensions)
                    throw new OutOfRangeException();
                else
                    _data[i] = value;
            }
        }

        /// <inheritdoc cref="IMathVector.Length"/>
        public double Length => GetLenght();

        /// <inheritdoc cref="IMathVector.CalcDistance(IMathVector)"/>
        /// <inheritdoc cref="IMathVector.CalcDistance(IMathVector)"/>
        /// <inheritdoc cref="OutOfRangeException"/>
        /// <param name="vector">Расстояние котрого нужно высчитать</param>
        /// <returns>Евклидово расстояние для.</returns>
        /// <exception cref="OutOfRangeException"></exception>
        public double CalcDistance(IMathVector vector)
        {
            if (Dimensions != vector.Dimensions)
            {
                throw new OutOfRangeException();
            }
            double res = 0;
            for (int i = 0; i < Dimensions; i++)
            {
                res += Math.Pow(this[i] - vector[i], 2);
            }
            return Math.Sqrt(res);
        }

        /// <summary>
        /// Получить итератор.
        /// </summary>
        /// <returns>Итератор для координат вектора</returns>
        public IEnumerator GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        /// <inheritdoc cref="IMathVector.Multiply(IMathVector)"/>
        /// <inheritdoc cref="OutOfRangeException"/>
        /// <param name="vector">Вектор, на который умножается.</param>
        /// <returns>Резльтат умножения векторов.</returns>
        /// <exception cref="OutOfRangeException"></exception>
        public IMathVector Multiply(IMathVector vector)
        {
            if (vector.Dimensions != this.Dimensions)
                throw new OutOfRangeException();
            else
            {
                IMathVector resMlp = new MathVector(vector.Dimensions);
                for (int i = 0; i < Dimensions; i++)
                {
                    resMlp[i] = this[i] * vector[i];
                }
                return resMlp;
            }
        }

        /// <inheritdoc cref="IMathVector.MultiplyNumber(double)"/>
        /// <param name="number">Число, на которое умножатся вектор.</param>
        /// <returns>Умноженный на число вектор.</returns>
        public IMathVector MultiplyNumber(double number)
        {
            IMathVector resMlpNum = new MathVector(Dimensions);
            for (int i = 0; i < Dimensions; i++)
            {
                resMlpNum[i] = this[i] * number;
            }
            return resMlpNum;
        }

        /// <inheritdoc cref="IMathVector.ScalarMultiply(IMathVector)"/>
        /// <inheritdoc cref="OutOfRangeException"/>
        /// <param name="vector"> Вектор, которым будет скалярное умножение. </param>
        /// <returns>Результат скалярного умножения.</returns>
        /// <exception cref="OutOfRangeException"></exception>
        public double ScalarMultiply(IMathVector vector)
        {
            if (vector.Dimensions != this.Dimensions)
                throw new OutOfRangeException();
            else
            {
                double resSclrMlp = 0;
                for (int i = 0; i < Dimensions; i++)
                {
                    resSclrMlp += Math.Abs(this[i]) * Math.Abs(vector[i]);
                }
                return resSclrMlp;
            }
        }

        /// <inheritdoc cref="IMathVector.Sum(IMathVector))"/>
        /// <inheritdoc cref="OutOfRangeException"/>
        /// <param name="vector">Вектор,с которым будет суммирование.</param>
        /// <returns>Суммированный вектор.</returns>
        /// <exception cref="OutOfRangeException"></exception>
        public IMathVector Sum(IMathVector vector)
        {
            IMathVector resSum = new MathVector(vector.Dimensions);
            if (vector.Dimensions != this.Dimensions)
                throw new OutOfRangeException();
            else
            {
                for (int i = 0; i < Dimensions; i++)
                {
                    resSum[i] = this[i] + vector[i];
                }
                return resSum;
            }
        }

        /// <inheritdoc cref="IMathVector.SumNumber(double)"/>
        /// <param name="number">Число, с которым будет суммироваться вектор.</param>
        /// <returns>Суммированный вектор.</returns>
        public IMathVector SumNumber(double number)
        {
            IMathVector resSumNum = new MathVector(Dimensions);
            for (int i = 0; i < Dimensions; i++)
            {
                resSumNum[i] = this[i] + number;
            }
            return resSumNum;
        }

        /// <inheritdoc cref="IMathVector.SubNumber(double))"/>
        /// <param name="number">Число, которое будет вычитаться</param>
        /// <returns>Вычтенный вектор.</returns>
        public IMathVector SubNumber(double number)
        {
            IMathVector resSubNum = new MathVector(Dimensions);
            for (int i = 0; i < Dimensions; i++)
            {
                resSubNum[i] = this[i] - number;
            }
            return resSubNum;
        }

        /// <inheritdoc cref="IMathVector.DvsNumber(double))"/>
        /// <inheritdoc cref="MathExceptoin"/>
        /// <param name="number">Число, на которое будет деление.</param>
        /// <returns>Поделенный вектор</returns>
        /// <exception cref="MathExceptoin"></exception>
        public IMathVector DvsNumber(double number)
        {
            if (number == 0)
                throw new MathExceptoin();
            else
            {
                IMathVector resDvsNum = new MathVector(Dimensions);
                for (int i = 0; i < Dimensions; i++)
                {
                    resDvsNum[i] = this[i] / number;
                }
                return resDvsNum;
            }
        }

        /// <inheritdoc cref="IMathVector.Sub(IMathVector))"/>
        /// <inheritdoc cref="OutOfRangeException"/>
        /// <param name="vector">Вектор, который будет вычитаться</param>
        /// <returns>Вычтенный вектор</returns>
        /// <exception cref="OutOfRangeException"></exception>
        public IMathVector Sub(IMathVector vector)
        {
            IMathVector resSub = new MathVector(vector.Dimensions);
            if (vector.Dimensions != this.Dimensions)
                throw new OutOfRangeException();
            else
            {
                for (int i = 0; i < Dimensions; i++)
                {
                    resSub[i] = this[i] - vector[i];
                }
                return resSub;
            }
        }

        /// <inheritdoc cref="IMathVector.Dvs(IMathVector))"/>
        /// <inheritdoc cref="OutOfRangeException"/>
        /// <param name="vector">Вектор, на который будет деление</param>
        /// <returns>Поделенный вектор.</returns>
        /// <exception cref="OutOfRangeException"></exception>
        public IMathVector Dvs(IMathVector vector)
        {
            IMathVector resDvs = new MathVector(vector.Dimensions);
            if (vector.Dimensions != this.Dimensions)
                throw new OutOfRangeException();
            else
            {
                for (int i = 0; i < Dimensions; i++)
                {
                    if (vector[i] == 0)
                        throw new MathExceptoin();
                    else
                        resDvs[i] = this[i] / vector[i];
                }
                return resDvs;
            }
        }

        /// <inheritdoc cref="IMathVector.ScalarMultiply(IMathVector))"/>
        /// <inheritdoc cref="OutOfRangeException"/>
        /// <param name="vector">Вектор, на который будет скалярное умножение.</param>
        /// <returns>Умноженный вектор.</returns>
        /// <exception cref="OutOfRangeException"></exception>
        public IMathVector ScalarMultiplyVector(IMathVector vector)
        {
            IMathVector resSclMlp = new MathVector(vector.Dimensions);
            if (vector.Dimensions != this.Dimensions)
                throw new OutOfRangeException();
            else
            {
                for (int i = 0; i < Dimensions; i++)
                {
                    resSclMlp[i] = Math.Abs(this[i]) * Math.Abs(vector[i]);
                }
                return resSclMlp;
            }
        }

        public static IMathVector operator +(MathVector vectorFst, MathVector vectorSnd)
        {
            return vectorFst.Sum(vectorSnd);
        }

        public static IMathVector operator +(MathVector vectorFst, double num)
        {
            return vectorFst.SumNumber(num);
        }

        public static IMathVector operator -(MathVector vectorFst, MathVector vectorSnd)
        {
            return vectorFst.Sub(vectorSnd);
        }

        public static IMathVector operator -(MathVector vectorFst, double num)
        {
            return vectorFst.SubNumber(num);
        }

        public static IMathVector operator *(MathVector vectorFst, MathVector vectorSnd)
        {
            return vectorFst.Multiply(vectorSnd);
        }

        public static IMathVector operator *(MathVector vectorFst, double num)
        {
            return vectorFst.MultiplyNumber(num);
        }

        public static IMathVector operator /(MathVector vectorFst, MathVector vectorSnd)
        {
            return vectorFst.Dvs(vectorSnd);
        }

        public static IMathVector operator /(MathVector vectorFst, double num)
        {
            return vectorFst.DvsNumber(num);
        }

        public static IMathVector operator %(MathVector vectorFst, MathVector vectorSnd)
        {
            return vectorFst.ScalarMultiplyVector(vectorSnd);
        }

        /// <summary>
        /// Перегрузка конвертации вектора в строку.
        /// </summary>
        /// <returns> Контактинированная строка из элементов вектора.</returns>
        public override string ToString()
        {
            string res = "";
            foreach (var e in _data)
            {
                res += " " + Convert.ToString(e) + ";";
            }
            return res;
        }

    }
}
