using System.Collections;
using System.Collections.Generic;

namespace lista
{
    internal class Enumerador<T> : IEnumerator<T>
    {
        private readonly Lista<T> _lista;
        private Nodo<T> _currentNode;

        public T Current
        {
            get { return _currentNode.Valor; }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public Enumerador(Lista<T> lista)
        {
            this._lista = lista;
            _currentNode = lista.centinela;
        }

        /// <summary>
        /// Avanza al siguiente nodo de la lista
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            _currentNode = _currentNode.Siguiente;
            return _currentNode != null;
        }

        /// <summary>
        /// Reinicia el iterador.
        /// Hace que apunte al nodo centinela de la lista.
        /// </summary>
        public void Reset()
        {
            _currentNode = _lista.centinela;
        }

        public void Dispose(){ }
        
    }
}