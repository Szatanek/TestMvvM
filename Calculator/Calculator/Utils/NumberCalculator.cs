using Calculator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Utils
{
    public static class NumberCalculator
    {
        /// <summary>
        /// Wykonywanie obliczen.
        /// </summary>
        /// <param name="firstValue">Pierwsza wartosc.</param>
        /// <param name="secondValue">Druga wartosc.</param>
        /// <param name="operation">Typ operacji arytmetycznej.</param>
        /// <returns>Wynik operacji arytmetycznej.</returns>
        public static int GetResult(int firstValue, int secondValue, OperationType operation)
        {
            switch (operation)
            {
                case OperationType.Add:
                    return firstValue + secondValue;
                case OperationType.Subtract:
                    return firstValue - secondValue;
                case OperationType.Multiply:
                    return firstValue * secondValue;
                case OperationType.Divide:
                    return firstValue / secondValue;
                default:
                    return 0;
            }
        }
    }
}
