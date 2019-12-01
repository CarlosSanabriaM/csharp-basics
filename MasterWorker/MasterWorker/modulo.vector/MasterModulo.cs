using master.worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modulo.vector
{
    public class MasterModulo : Master<short, long, double>
    {

        public MasterModulo(short[] vector, int numeroHilos) : base(vector, numeroHilos) { }

        protected override Worker<short, long> CrearWorker(int índiceDesde, int índiceHasta)
        {
            return new WorkerModulo(this.vector, índiceDesde, índiceHasta);
        }

        protected override double JuntarResultadosWorkers(Worker<short, long>[] workers)
        {
            long resultado = 0;
            foreach (var worker in workers)
                resultado += worker.Resultado;
            return Math.Sqrt(resultado);
        }
    }
}
