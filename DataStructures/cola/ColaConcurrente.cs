using System;
using TPP.Practicas.Lista;
// ReSharper disable All

namespace TPP.Practicas.Cola
{
    public class ColaConcurrente<T>
    {
        // Como la lista es simplemente enlazada, para que las operaciones de Añadir y Extraer de la cola
        // sean en O(1), hay que añadir elementos al final de la lista y extraer elementos (eliminar y
        // obtener el valor del elemento) al principio de la lista
        private Lista<T> lista;

        /// <summary>
        /// Devuelve el número de elementos de la cola.
        /// </summary>
        public int NumeroElementos
        {
            get
            {
                lock(lista)
                    return lista.NumeroElementos;
            }
        }

        #region Constructores

        /// <summary>
        /// Crea una cola vacía.
        /// </summary>
        public ColaConcurrente()
        {
            this.lista = new Lista<T>();
        }

        /// <summary>
        /// Crea una cola con un único elemento.
        /// </summary>
        /// <param name="valor">Valor del elemento a añadir en la cola</param>
        public ColaConcurrente(T valor)
        {
            this.lista = new Lista<T>(valor);
        }

        /// <summary>
        /// Crea una cola con los elementos pasados como parámetro.
        /// El número de argumentos es variable.
        /// También puede pasarse como parámetro un array.
        /// Los valores se añaden en el orden indicado, de izquierda a derecha,
        /// por lo que los primeros valores serán los primeros en salir de la cola.
        /// </summary>
        /// <param name="valores">Valores a añadir en la cola</param>
        public ColaConcurrente(params T[] valores)
        {
            // Como se añaden elementos al final de la lista, podemos llamar directamente
            // al constructor de la lista que recibe un array de valores
            this.lista = new Lista<T>(valores);
        }

        /// <summary>
        /// Constructor de copia.
        /// </summary>
        /// <param name="cola">Cola a copiar</param>
        public ColaConcurrente(ColaConcurrente<T> cola) : this()
        {
            this.lista = new Lista<T>(cola.lista);
        }

        #endregion

        /// <returns>Devuelve true si la cola está vacía, y false en caso contrario.</returns>
        public bool EstáVacía()
        {
            lock(lista)
                return this.NumeroElementos == 0;
        }

        /// <summary>
        /// Inserta un elemento en la cola.
        /// </summary>
        /// <param name="elem">Elemento a insertar</param>
        public void Añadir(T elem)
        {
            // Los nuevos elementos se añaden al final de la lista
            lock(lista)
                this.lista.AddLast(elem);
        }

        /// <summary>
        /// Devuelve el primer elemento de la cola, sin eliminarlo.
        /// </summary>
        /// <returns>El primer elemento de la cola.</returns>
        /// <exception cref="InvalidOperationException">Se lanza si la cola está vacía</exception>
        public T PrimerElemento()
        {
            lock (lista)
            {
                if (this.EstáVacía())
                    throw new InvalidOperationException("No se puede obtener el primer elemento de una cola vacía");
                return lista.Get(0);
            }
        }

        /// <summary>
        /// Extrae un elemento en la cola. Devuelve y elimina el primer elemento de la cola.
        /// </summary>
        /// <returns>El primer elemento de la cola.</returns>
        /// <exception cref="InvalidOperationException">Se lanza si la cola está vacía</exception>
        public T Extraer()
        {
            lock (lista)
            {
                if (this.EstáVacía())
                    throw new InvalidOperationException("No se puede extraer el primer elemento de una cola vacía");
                // Los elementos se extraen del principio de la lista
                var toReturn = this.lista.Get(0);
                this.lista.RemoveFirst();
                return toReturn;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj == this)
                return true;

            ColaConcurrente<T> otra = obj as ColaConcurrente<T>;
            if (otra == null)
                return false;

            lock(lista)
                return this.lista.Equals(otra.lista);
        }
        
        public override string ToString()
        {
            lock(lista)
                return "<-" + lista.ToString() + "<-";
        }
    }
}