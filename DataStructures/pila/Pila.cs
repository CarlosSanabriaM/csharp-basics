using System;
using System.Diagnostics;
using lista;

namespace pila
{
    public class Pila<T>
    {
        private uint numeroMaxElementos;
        private Lista<T> lista;

        public bool EstaVacía
        {
            get { return lista.NumeroElementos == 0; }
        }

        public bool EstaLlena
        {
            get { return lista.NumeroElementos == numeroMaxElementos; }
        }

        public Pila(uint numeroMaxElementos)
        {
            this.numeroMaxElementos = numeroMaxElementos;
            this.lista = new Lista<T>();
        }

        /// <summary>
        /// Añade el elemento a la cima de la pila.
        /// Si la pila está llena, lanza una excepción.
        /// </summary>
        /// <param name="elem"></param>
        public void Push(T elem)
        {
            // Invariante
            CheckInvariante();
            
            int numElementos = lista.NumeroElementos;
            
            // Precondiciones
            if(EstaLlena)
                throw new InvalidOperationException("La pila está llena.");
            
            // Funcionalidad del método
            lista.Añadir(elem);
            
            // Postcondiciones
            Debug.Assert(lista.NumeroElementos == numElementos + 1, 
                "Añadir un elemento a la pila no incrementa el número de elementos.");
            
            // Invariante
            CheckInvariante();
        }
        
        /// <summary>
        /// Saca el elemento en la cima de la pila, es decir, lo retorna y lo borra de la pila.
        /// </summary>
        /// <returns>Elemento en la cima de la pila.</returns>
        public T Pop()
        {
            // Invariante
            CheckInvariante();
            
            int numElementos = lista.NumeroElementos;
            
            // Precondiciones
            if(EstaVacía)
                throw new InvalidOperationException("La pila está vacía.");
            
            // Funcionalidad del método
            T toReturn = lista.GetElemento(lista.NumeroElementos - 1);
            lista.Borrar();
            
            // Postcondiciones
            Debug.Assert(lista.NumeroElementos == numElementos - 1, 
                "Sacar un elemento de la pila no decrementa el número de elementos.");
            
            // Invariante
            CheckInvariante();

            return toReturn;
        }

        /// <summary>
        /// Comprueba la invariante de la clase.
        /// </summary>
        private void CheckInvariante()
        {
            // No debe poder tener más elementos que el maximo permitido
            Debug.Assert(lista.NumeroElementos <= numeroMaxElementos, 
                "La pila tiene más elementos que el número máximo permitido.");

            // No puede estar vacia y llena a la vez
            Debug.Assert(!(EstaVacía && EstaLlena), 
                "La pila está vacía y llena a la vez.");

            // Si está vacía, entonces el número de elementos de la lista debe ser 0
            if(EstaVacía)
                Debug.Assert(lista.NumeroElementos == 0, 
                    "La lista está vacía, pero el número de elementos de la lista no es 0.");
            
            // Si NO está vacía, entonces el número de elementos de la lista debe ser mayor de 0
            if(!EstaVacía)
                Debug.Assert(lista.NumeroElementos > 0, 
                    "La lista no está vacía, pero el número de elementos de la lista no es mayor de 0.");
            
            // Si está llena, entonces el número de elementos de la lista debe ser igual al número máximo de elementos de la pila
            if(EstaLlena)
                Debug.Assert(lista.NumeroElementos == numeroMaxElementos, 
                    "La lista está llena, pero el número de elementos de la lista " +
                    "no es igual al número máximo de elementos de la pila.");
            
            // Si NO está llena, entonces el número de elementos de la lista debe ser menor que el número máximo de elementos de la pila
            if(!EstaLlena)
                Debug.Assert(lista.NumeroElementos < numeroMaxElementos, 
                    "La lista no está llena, pero el número de elementos de la lista " +
                    "no es menor que el número máximo de elementos de la pila.");
        }

        public override string ToString()
        {
            return "Stack(" + lista + ")";
        }
    }
}