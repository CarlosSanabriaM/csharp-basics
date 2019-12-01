
namespace master.worker
{
    /// <summary>
    /// Clase base abstracta para crear Workers.
    /// </summary>
    /// <typeparam name="TElemVector">Tipo de los elementos del vector</typeparam>
    /// <typeparam name="TResultado">Tipo del resultado devuelvo por el Worker</typeparam>
    public abstract class Worker<TElemVector, TResultado> {

        protected TElemVector[] vector;

        protected int índiceDesde, índiceHasta;

        protected TResultado resultado;

        public TResultado Resultado {
            get { return this.resultado; }
        }

        protected internal Worker(TElemVector[] vector, int índiceDesde, int índiceHasta) {
            this.vector = vector;
            this.índiceDesde = índiceDesde;
            this.índiceHasta = índiceHasta;
        }

        protected internal abstract void Calcular();

    }

}
