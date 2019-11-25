using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace memorizacion
{
    class Program
    {
        static void Main(string[] args)
        {
            long terminoFibonacci = 300000;
            long resultado;

            var crono = new Stopwatch();
            crono.Start();
            resultado = FibonacciMemorizacion.FibonacciEager(terminoFibonacci);
            crono.Stop();
            long ticksEagerMemorizadoPrimeraLlamada = crono.ElapsedTicks;
            Console.WriteLine("Versión Eager Memorizada, primera llamada: {0:N} ticks. Resultado: {1}.",
                ticksEagerMemorizadoPrimeraLlamada, resultado);

            crono.Restart();
            resultado = FibonacciMemorizacion.FibonacciEager(terminoFibonacci);
            crono.Stop();
            long ticksEagerMemorizadoSegundaLlamada = crono.ElapsedTicks;
            Console.WriteLine("Versión Eager Memorizada, segunda llamada: {0:N} ticks. Resultado: {1}.",
                ticksEagerMemorizadoSegundaLlamada, resultado);


            FibonacciMemorizacion.ResetCache();
            
            crono.Restart();
            var fibonacciLazyResult = FibonacciMemorizacion.FibonacciLazy(terminoFibonacci);
            crono.Stop();
            long ticksLazyMemorizadoPrimeraLlamada = crono.ElapsedTicks;
            Console.WriteLine("Versión Lazy Memorizada, creación del IEnumerable: {0:N} ticks.",
                ticksLazyMemorizadoPrimeraLlamada);
            
            crono.Restart();
            var enumerator = fibonacciLazyResult.GetEnumerator();
            enumerator.MoveNext();
            resultado = enumerator.Current;
            crono.Stop();
            long ticksLazyMemorizadoPrimeraIteracion = crono.ElapsedTicks;
            Console.WriteLine("Versión Lazy Memorizada, primera ejecución, se itera una vez y se obtiene el resultado: {0:N} ticks. Resultado: {1}.",
                ticksLazyMemorizadoPrimeraIteracion, resultado);
            
            crono.Restart();
            enumerator = fibonacciLazyResult.GetEnumerator();
            enumerator.MoveNext();
            resultado = enumerator.Current;
            crono.Stop();
            long ticksLazyMemorizadoSegundaIteracion = crono.ElapsedTicks;
            Console.WriteLine("Versión Lazy Memorizada, segunda ejecución, se itera una vez y se obtiene el resultado: {0:N} ticks. Resultado: {1}.",
                ticksLazyMemorizadoSegundaIteracion, resultado);            

        }
    }
}