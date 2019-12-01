using master.worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    class WorkerBitcoinMaxValor : Worker<BitcoinValueData, double>
    {
        internal WorkerBitcoinMaxValor(BitcoinValueData[] vector, int índiceDesde, int índiceHasta) :
            base(vector, índiceDesde, índiceHasta) { }

        /// <summary>
        /// En la porción del vector que le toca, calcula el máximo valor del Bitcoin.
        /// </summary>
        protected override void Calcular()
        {

            // Opción 1
            // Con más de 1 worker, esta opción es más lenta con más de 1 worker que con 1, ya que está optimizado para hacerlo junto
            //this.resultado = this.vector.Max(bitcoin => bitcoin.Value); 
            
            // Opción 2
            this.resultado = 0;
            for (int i = this.índiceDesde; i <= this.índiceHasta; i++)
                this.resultado = Math.Max(this.resultado, this.vector[i].Value);
        }
    }
}