using master.worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modulo.vector
{
    class WorkerModulo : Worker<short, long>
    {
        
        internal WorkerModulo(short[] vector, int índiceDesde, int índiceHasta): base(vector, índiceDesde, índiceHasta) { }

        protected override void Calcular()
        {
            this.resultado = 0;
            for (int i = this.índiceDesde; i <= this.índiceHasta; i++)
                this.resultado += this.vector[i] * this.vector[i];
        }
    }
}
