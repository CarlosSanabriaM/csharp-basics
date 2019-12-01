using master.worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    class WorkerBitcoinMediaValores : Worker<BitcoinValueData, double>
    {
        internal WorkerBitcoinMediaValores(BitcoinValueData[] vector, int índiceDesde, int índiceHasta) :
            base(vector, índiceDesde, índiceHasta) { }

        /// <summary>
        /// En la porción del vector que le toca, suma los valores del Bitcoin.
        /// </summary>
        protected override void Calcular()
        {
            this.resultado = 0;
            for (int i = this.índiceDesde; i <= this.índiceHasta; i++)
                this.resultado += this.vector[i].Value;
        }
    }
}