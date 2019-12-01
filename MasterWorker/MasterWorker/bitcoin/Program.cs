using System;


namespace Lab10
{
    class Program
    {
        /// <summary>
        /// Pregunta al usuario si quiere medir el context switching o simplemente ejecutar diversos master-workers.
        /// Si el usuario indica que quiere medir el context switching, le pregunta de qué master-worker.
        /// </summary>
        static void Main()
        {
            var data = Utils.GetBitcoinData();
            double valorFrontera = 7000.0;

            Console.Write("¿Medir el  Context Switching? (s/n): ");
            string medirContextSwitching = Console.ReadLine();
            Console.WriteLine();

            if (medirContextSwitching.Equals("n"))
            {
                /* Ejecutar diversos master-workers, usando 1, 4 y 8 hilos. */
                #region BitcoinNumValoresPorEncimaConXHilos

                CalcularBitcoinNumValoresPorEncimaConXHilos(data, 1, valorFrontera);
                CalcularBitcoinNumValoresPorEncimaConXHilos(data, 4, valorFrontera);
                CalcularBitcoinNumValoresPorEncimaConXHilos(data, 8, valorFrontera);

                #endregion

                #region BitcoinMediaValoresConXHilos

                Console.WriteLine();
                CalcularBitcoinMediaValoresConXHilos(data, 1);
                CalcularBitcoinMediaValoresConXHilos(data, 4);
                CalcularBitcoinMediaValoresConXHilos(data, 8);

                #endregion

                #region BitcoinMaxValorConXHilos

                Console.WriteLine();
                CalcularBitcoinMaxValorConXHilos(data, 1);
                CalcularBitcoinMaxValorConXHilos(data, 4);
                CalcularBitcoinMaxValorConXHilos(data, 8);

                #endregion
            }
            else
            {
                /* Medir el context switching. */

                Console.Write("¿De cuál de las siguientes funcionalidades quieres leer el Context Switching:\n" +
                    "1. Número valores por encima de 7000\n" +
                    "2. Media de los valores\n" +
                    "3. Valor máximo\n");
                int numOpcion = Int32.Parse(Console.ReadLine());

                switch (numOpcion)
                {
                    case 1:
                        #region ContextSwitchingBitcoinNumValoresPorEncima

                        Console.WriteLine();
                        ContextSwitching<BitcoinValueData, uint, uint>.Medir(data,
                            (bitcoinValueDataArray, numHilos) =>
                                new MasterBitcoinNumValoresPorEncima(bitcoinValueDataArray, numHilos, valorFrontera));

                        #endregion
                        break;
                    case 2:
                        #region ContextSwitchingBitcoinMediaValores

                        Console.WriteLine();
                        ContextSwitching<BitcoinValueData, double, double>.Medir(data,
                            (bitcoinValueDataArray, numHilos) =>
                                new MasterBitcoinMediaValores(bitcoinValueDataArray, numHilos));

                        #endregion
                        break;
                    case 3:
                        #region ContextSwitchingBitcoinMaxValor

                        Console.WriteLine();
                        ContextSwitching<BitcoinValueData, double, double>.Medir(data,
                            (bitcoinValueDataArray, numHilos) =>
                                new MasterBitcoinMaxValor(bitcoinValueDataArray, numHilos));

                        #endregion
                        break;
                }

                /*
                Resultados en mi PC:
                Funcionalidad 1:
                    -Menor tiempo con 4 hilos
                    -A partir de 8 hilos el tiempo es mayor que la solución secuencial
                Funcionalidad 2:
                    -Menor tiempo con 2 hilos
                    -A partir de 9 hilos el tiempo es mayor que la solución secuencial
                Funcionalidad 3:
                    -Menor tiempo con 5 hilos
                    -A partir de 10 hilos el tiempo es mayor que la solución secuencial
                NOTA: Los resultados también dependen de la ejecución. Habría que realizar varias y tomar conclusiones.
                */

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

            // Se lanza y se calcula el tiempo que tarda
            DateTime antes = DateTime.Now;
            uint resultado = master.Calcular();
            DateTime despues = DateTime.Now;

            // Se muestra el resultado y el tiempo que ha tardado
            Console.WriteLine(
                "Número de veces que el valor del Bitcoin está por encima (es >) de {0} con {1} hilo(s): {2:N2}.",
                valorFrontera, numHilos, resultado);
            Console.WriteLine("Tiempo transcurrido: {0:N0} ticks de reloj.",
                (despues - antes).Ticks);

            // Se retorna el resultado
            return resultado;
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

            // Se lanza y se calcula el tiempo que tarda
            DateTime antes = DateTime.Now;
            double resultado = master.Calcular();
            DateTime despues = DateTime.Now;

            // Se muestra el resultado y el tiempo que ha tardado
            Console.WriteLine("Media de los valores del Bitcoin con {0} hilo(s): {1:N2}.",
                numHilos, resultado);
            Console.WriteLine("Tiempo transcurrido: {0:N0} ticks de reloj.",
                (despues - antes).Ticks);

            // Se retorna el resultado
            return resultado;
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

            // Se lanza y se calcula el tiempo que tarda
            DateTime antes = DateTime.Now;
            double resultado = master.Calcular();
            DateTime despues = DateTime.Now;

            // Se muestra el resultado y el tiempo que ha tardado
            Console.WriteLine("Máximo valor del Bitcoin con {0} hilo(s): {1:N2}.",
                numHilos, resultado);
            Console.WriteLine("Tiempo transcurrido: {0:N0} ticks de reloj.",
                (despues - antes).Ticks);

            // Se retorna el resultado
            return resultado;
        }
    }
}