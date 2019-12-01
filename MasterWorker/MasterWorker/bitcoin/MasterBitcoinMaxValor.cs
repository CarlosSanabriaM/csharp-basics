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
    /// Permite calcular el máximo valor del Bitcoin.
    /// </summary>
    public class MasterBitcoinMaxValor : Master<BitcoinValueData, double, double>
    {
        public MasterBitcoinMaxValor(BitcoinValueData[] vector, int numeroHilos) :
            base(vector, numeroHilos) { }

        protected override Worker<BitcoinValueData, double> CrearWorker(int índiceDesde, int índiceHasta)
        {
            return new WorkerBitcoinMaxValor(this.vector, índiceDesde, índiceHasta);
        }

        /// <summary>
        /// Junta los resultados de los workers, que consiste en calcular el máximo de cada uno.
        /// </summary>
        protected override double JuntarResultadosWorkers(Worker<BitcoinValueData, double>[] workers)
        {
            //Opción 1
            // return workers.Max(w => w.Resultado);
            
            // Opción 2
            double resultado = 0;
            foreach (var worker in workers)
                resultado = Math.Max(resultado, worker.Resultado);
            
            return resultado;
        }
    }
}