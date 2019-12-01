using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace memorizacion
{
    /// <summary>
    /// Clase que demuestra la optimización de memorización
    /// </summary>
    static class FibonacciMemorizacion
    {
        /// <summary>
        /// Valores de la invocación memorizados
        /// </summary>
        private static IDictionary<long, long> valores = new Dictionary<long, long>();

        /// <summary>
        /// Función Fibonacci recursiva memorizada y eager
        /// </summary>
        internal static long FibonacciEager(long n)
        {
            if (valores.Keys.Contains(n))
                // * Si ya se calculó, devolvemos el valor cacheado
                return valores[n];
            // * En caso contrario, lo guardamos antes de devolverlo
            long valor = n <= 2 ? 1 : FibonacciEager(n - 2) + FibonacciEager(n - 1);
            valores.Add(n, valor);
            return valor;
        }
        
        /// <summary>
        /// Función Fibonacci recursiva memorizada y lazy
        /// </summary>
        internal static IEnumerable<long> FibonacciLazy(long n)
        {
            if (valores.Keys.Contains(n))
                // * Si ya se calculó, devolvemos el valor cacheado
                yield return valores[n];
            else
            {
                // * En caso contrario, lo guardamos antes de devolverlo
                long valor = n <= 2 ? 1 : FibonacciEager(n - 2) + FibonacciEager(n - 1);
                valores.Add(n, valor);
                yield return valor;
            }
        }

        internal static void ResetCache()
        {
            valores = new Dictionary<long, long>();
        }
    }
}