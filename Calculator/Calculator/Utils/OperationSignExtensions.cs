using Calculator.ViewModels;
using System.Collections.Generic;

namespace Calculator.Utils
{
    /// <summary>
    /// Rozszerzenia do typow operacji.
    /// </summary>
    public static class OperationSignExtensions
    {
        /// <summary>
        /// Slownik przechowujacy tekstowe znaki dla operacji arytmetycznych.
        /// </summary>
        private static Dictionary<OperationType, string> _operationDictionary = new Dictionary<OperationType, string>
        {
            { OperationType.Add, "+" },
            { OperationType.Subtract, "-" },
            { OperationType.Multiply, "*" },
            { OperationType.Divide, "/" },
        };

        /// <summary>
        /// Zwraca znak tekstowy dla wybranej operacji arytmetycznej.
        /// </summary>
        /// <param name="operation">Typ operacji arytmetycznej.</param>
        /// <returns>Znak tekstowy.</returns>
        public static string GetSign(this OperationType operation)
        {
            return _operationDictionary[operation];
        }
    }
}
