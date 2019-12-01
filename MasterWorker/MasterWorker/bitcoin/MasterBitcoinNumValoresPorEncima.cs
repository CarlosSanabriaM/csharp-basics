using master.worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    /// <summary>
    /// Permite calcular el número de veces que el valor del Bitcoin está por encima (es >) de un valor dado.
    /// </summary>
    public class MasterBitcoinNumValoresPorEncima : Master<BitcoinValueData, uint, uint>
    {
        private double valorFrontera;

        public MasterBitcoinNumValoresPorEncima(BitcoinValueData[] vector, int numeroHilos, double valorFrontera) : 
            base(vector, numeroHilos)
        {
            this.valorFrontera = valorFrontera;
        }

        protected override Worker<BitcoinValueData, uint> CrearWorker(int índiceDesde, int índiceHasta)
        {
            return new WorkerBitcoinNumValoresPorEncima(this.vector, índiceDesde, índiceHasta, this.valorFrontera);
        }

        /// <summary>
        /// Junta los resultados de los workers, que consiste en sumar los resultados de cada uno,
        /// para así devolver el número de veces que el valor del Bitcoin es superior al valorFrontera.
        /// </summary>
        protected override uint JuntarResultadosWorkers(Worker<BitcoinValueData, uint>[] workers)
        {
            uint resultado = 0;
            foreach (var worker in workers)
                resultado += worker.Resultado;
            return resultado;
        }
    }
}
