using System;
namespace lista
{
    /// <summary>
    /// Almacena un valor y una referencia al nodo siguiente en la lista.
    /// </summary>
    internal class Nodo<T>
    {
        public T Valor { get; set; }
        public Nodo<T> Siguiente { get; set; }

        /// <summary>
        /// Crea un nuevo nodo con el valor y el nodo siguiente indicados.
        /// </summary>
        /// <param name="valor">Valor del nodo</param>
        /// <param name="siguiente">Nodo siguiente</param>
        public Nodo(T valor, Nodo<T> siguiente = null)
        {
            this.Valor = valor;
            this.Siguiente = siguiente;
        }
        
    }
}
