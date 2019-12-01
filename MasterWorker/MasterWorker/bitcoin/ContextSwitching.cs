using System;
using System.IO;
using master.worker;

namespace Lab10
{
    /// <summary>
    /// Clase estática que permite medir el context switching de un master-worker.
    /// </summary>
    /// <typeparam name="TElemVector">Tipo de los elementos del vector</typeparam>
    /// <typeparam name="TResultadoWorker">Tipo del resultado devuelvo por cada Worker</typeparam>
    /// <typeparam name="TResultadoMaster">Tipo del resultado devuelvo por el Master, tras juntar los resultados de los Workers</typeparam>
    public static class ContextSwitching<TElemVector, TResultadoWorker, TResultadoMaster>
    {
        private const string SEPARADOR_CSV = ";";

        /// <summary>
        /// Función que mide el Context Switching al ejecutar varios master-workers.
        /// </summary>
        /// <param name="data">Array de datos usado por el master-worker. El tipo de los elementos del array es TElemVector</param>
        /// <param name="funcionCreaMaster">Función que recibe un array de tipo TElemVector (array de datos) y
        /// un entero (número de hilos), y devuelve un Master. Esto evita tener que crear un método Medir para cada Master-worker.</param>
        /// <param name="numeroMaximoHilos"></param>
        // TODO: Otra alternativa es hacer esto usando un FactoryMethod, pero para no llenar el proyecto de clases he optado por esta solución, que aprovecha la programación funcional
        public static void Medir(
            TElemVector[] data, 
            Func<TElemVector[], int, Master<TElemVector, TResultadoWorker, TResultadoMaster>> funcionCreaMaster,
            int numeroMaximoHilos = 50)
        {
            MostrarLinea(Console.Out, "Numero de Hilos", "Ticks", "Resultado");
            for (int numeroHilos = 1; numeroHilos <= numeroMaximoHilos; numeroHilos++)
            {
                var master = funcionCreaMaster(data, numeroHilos);
                DateTime antes = DateTime.Now;
                TResultadoMaster resultado = master.Calcular();
                DateTime despues = DateTime.Now;
                MostrarLinea(Console.Out, numeroHilos, (despues - antes).Ticks, resultado);
                GC.Collect(); // Lanzamos el recolector
                GC.WaitForFullGCComplete();
            }
        }

        static void MostrarLinea(TextWriter flujo, string tituloNumeroHilos, string tituloTicks, string tituloResultado)
        {
            flujo.WriteLine("{0}{3}{1}{3}{2}{3}", tituloNumeroHilos, tituloTicks, tituloResultado, SEPARADOR_CSV);
        }

        static void MostrarLinea(TextWriter flujo, int numeroHilos, long ticks, TResultadoMaster resultado)
        {
            flujo.WriteLine("{0}{3}{1:N0}{3}{2}{3}", numeroHilos, ticks, resultado, SEPARADOR_CSV);
        }
    }
}