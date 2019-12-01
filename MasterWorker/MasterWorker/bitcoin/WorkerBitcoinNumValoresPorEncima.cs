using master.worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{

    class WorkerBitcoinNumValoresPorEncima : Worker<BitcoinValueData, uint>
    {
        private readonly double valorFrontera;

        internal WorkerBitcoinNumValoresPorEncima(BitcoinValueData[] vector, int índiceDesde, int índiceHasta, double valorFrontera): 
            base(vector, índiceDesde, índiceHasta)
        {
            this.valorFrontera = valorFrontera;
        }

        /// <summary>
        /// En la porción del vector que le toca, calcula el número de veces que
        /// los valores del Bitcoin son superiores al valorFrontera.
        /// </summary>
        protected override void Calcular()
        {
            this.resultado = 0;
            for (int i = this.índiceDesde; i <= this.índiceHasta; i++)
                if(this.vector[i].Value > valorFrontera)
                    this.resultado++;
        }
    }
}
