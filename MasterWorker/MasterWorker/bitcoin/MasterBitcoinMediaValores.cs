using master.worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    // TODO
    /// <summary>
    /// Permite calcular la media de los valores del Bitcoin.
    /// </summary>
    public class MasterBitcoinMediaValores : Master<BitcoinValueData, double, double>
    {
        public MasterBitcoinMediaValores(BitcoinValueData[] vector, int numeroHilos) :
            base(vector, numeroHilos) { }

        protected override Worker<BitcoinValueData, double> CrearWorker(int índiceDesde, int índiceHasta)
        {
            return new WorkerBitcoinMediaValores(this.vector, índiceDesde, índiceHasta);
        }

        /// <summary>
        /// Junta los resultados de los workers, que consiste en sumar los resultados de cada uno,
        /// y dividir entre el tamaño del vector, para así devolver la media.
        /// </summary>
        protected override double JuntarResultadosWorkers(Worker<BitcoinValueData, double>[] workers)
        {
            double resultado = 0;
            foreach (var worker in workers)
                resultado += worker.Resultado;
            return resultado / this.vector.Length;
        }
    }
}