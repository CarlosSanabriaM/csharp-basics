using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lab10
{
    [TestClass]
    public class TestsBitcoinMaxValor
    {
        /// <summary>
        /// Comprueba que el resultado del máximo valor sea 7240,
        /// independientemente del número de hilos.
        /// </summary>
        [TestMethod]
        public void TestBitcoinNumValoresPorEncima()
        {
            var data = Utils.GetBitcoinData();

            double expectedResult = 7240.0;
            var arrayNumHilos = new int[] {1, 2, 4, 8, 16};

            foreach (var numHilos in arrayNumHilos)
            {
                Assert.AreEqual(expectedResult,
                    CalcularBitcoinMaxValorConXHilos(data, numHilos),
                    "MasterBitcoinMaxValor con {0} hilos no da el resultado esperado", numHilos);
            }
        }

        /// <summary>
        /// Calcular el máximo valor del Bitcoin, usando x hilos, mediante un Master-worker.
        /// </summary>
        /// <param name="data">Array de elementos de tipo BitcoinValueData.</param>
        /// <param name="numHilos">Número de hilos = número de workers</param>
        /// <returns>Máximo valor del Bitcoin.</returns>
        private static double CalcularBitcoinMaxValorConXHilos(BitcoinValueData[] data, int numHilos)
        {
            // Se crea el Master
            var master = new MasterBitcoinMaxValor(data, numHilos);

            // Se lanza la ejecución
            double resultado = master.Calcular();

            // Se retorna el resultado
            return resultado;
        }
    }
}