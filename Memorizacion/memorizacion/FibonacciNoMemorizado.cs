using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace memorizacion
{
    static class FibonacciNoMemorizado
    {
        /// <summary>
        /// Función Fibonacci recursiva sin memorizar
        /// </summary>
        internal static int Fibonacci(int n)
        {
            return n <= 2 ? 1 : Fibonacci(n - 2) + Fibonacci(n - 1);
        }
    }
}