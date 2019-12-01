using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lab10
{
    [TestClass]
    public class TestsBitcoinNumValoresPorEncima
    {
        /// <summary>
        /// Comprueba que el resultado del número de valores por encima de 7000 sea 2826,
        /// independientemente del número de hilos.
        /// </summary>
        [TestMethod]
        public void TestBitcoinNumValoresPorEncima()
        {
            var data = Utils.GetBitcoinData();
            double valorFrontera = 7000.0;

            uint expectedResult = 2826;
            var arrayNumHilos = new int[] {1, 2, 4, 8, 16};

            foreach (var numHilos in arrayNumHilos)
            {
                Assert.AreEqual(expectedResult,
                    CalcularBitcoinNumValoresPorEncimaConXHilos(data, numHilos, valorFrontera),
                    "MasterBitcoinNumValoresPorEncima con {0} hilos no da el resultado esperado", numHilos);
            }
        }

        /// <summary>
        /// Calcular el número de veces que el valor del Bitcoin está por encima (es >) de un valor dado,
        /// usando x hilos, mediante un Master-worker.
        /// </summary>
        /// <param name="data">Array de elementos de tipo BitcoinValueData.</param>
        /// <param name="numHilos">Número de hilos = número de workers</param>
        /// <param name="valorFrontera">Valor a partir del cuál se quiere saber cuantas veces el valor del Bitcoin es mayor o igual.</param>
        /// <returns>Número de veces que el valor del Bitcoin está por encima (es >) del valor dado.</returns>
        private static uint CalcularBitcoinNumValoresPorEncimaConXHilos(BitcoinValueData[] data, int numHilos,
                                                                        double valorFrontera)
        {
            // Se crea el Master
            var master = new MasterBitcoinNumValoresPorEncima(data, numHilos, valorFrontera);

            // Se lanza la ejecución
            uint resultado = master.Calcular();

            // Se retorna el resultado
            return resultado;
        }
    }
}