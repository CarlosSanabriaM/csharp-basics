using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lab10
{
    [TestClass]
    public class TestsBitcoinMediaValores
    {
        /// <summary>
        /// Comprueba que el resultado de la media de valores sea  5936.73,
        /// independientemente del número de hilos.
        /// </summary>
        [TestMethod]
        public void TestBitcoinNumValoresPorEncima()
        {
            var data = Utils.GetBitcoinData();

            double expectedResult = 5936.73;
            var arrayNumHilos = new int[] {1, 2, 4, 8, 16};

            foreach (var numHilos in arrayNumHilos)
            {
                Assert.AreEqual(expectedResult,
                    Math.Round(CalcularBitcoinMediaValoresConXHilos(data, numHilos), 2), // Redondeamos a 2 dígitos
                    "MasterBitcoinMediaValores con {0} hilos no da el resultado esperado", numHilos);
            }
        }

        /// <summary>
        /// Calcular la media de los valores del Bitcoin, usando x hilos, mediante un Master-worker.
        /// </summary>
        /// <param name="data">Array de elementos de tipo BitcoinValueData.</param>
        /// <param name="numHilos">Número de hilos = número de workers</param>
        /// <returns>Media de los valores del Bitcoin.</returns>
        private static double CalcularBitcoinMediaValoresConXHilos(BitcoinValueData[] data, int numHilos)
        {
            // Se crea el Master
            var master = new MasterBitcoinMediaValores(data, numHilos);

            // Se lanza la ejecución
            double resultado = master.Calcular();

            // Se retorna el resultado
            return resultado;
        }
    }
}