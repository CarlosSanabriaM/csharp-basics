using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace lista
{
    public class Lista<T> : IEnumerable<T>
    {
        /// <summary>
        /// Es siempre el primer nodo, aunque la lista esté vacía.
        /// Evita introducir casos excepcionales, como hacer un Add en una lista vacía.
        /// </summary>
        internal Nodo<T> centinela;

        /// <summary>
        /// Referencia que apunta al último nodo de la lista.
        /// </summary>
        internal Nodo<T> ultimo;

        public int NumeroElementos { get; private set; }

        /// <summary>
        /// Crea una lista vacía.
        /// </summary>
        public Lista()
        {
            centinela = ultimo = new Nodo<T>(default(T));
            NumeroElementos = 0;
        }

        /// <summary>
        /// Crea una lista con un único elemento.
        /// </summary>
        /// <param name="valor">Valor del elemento a añadir en la lista</param>
        public Lista(T valor)
        {
            ultimo = new Nodo<T>(valor);
            centinela = new Nodo<T>(default(T), ultimo);
            NumeroElementos = 1;
        }

        /// <summary>
        /// Crea una lista con los elementos pasados como parámetro.
        /// El número de argumentos es variable.
        /// También puede pasarse como parámetro un array.
        /// </summary>
        /// <param name="valores">Valores a añadir en la lista</param>
        public Lista(params T[] valores)
        {
            // Si el array está vacío creamos una lista vacía
            if (valores.Length == 0)
            {
                centinela = ultimo = new Nodo<T>(default(T));
                NumeroElementos = 0;
                return;
            }

            ultimo = new Nodo<T>(valores[valores.Length - 1]);

            // Recorremos los valores en sentido inverso y vamos creando los nodos
            Nodo<T> sgte = ultimo;
            for (int i = valores.Length - 2; i >= 0; i--)
            {
                sgte = new Nodo<T>(valores[i], sgte);
            }

            centinela = new Nodo<T>(default(T), sgte);

            NumeroElementos = valores.Length;
        }

        /// <summary>
        /// Constructor de copia.
        /// </summary>
        /// <param name="lista">Lista a copiar</param>
        public Lista(Lista<T> lista) : this()
        {
            Nodo<T> actualThis = this.centinela;
            foreach (var valOtro in lista)
            {
                actualThis.Siguiente = new Nodo<T>(valOtro);
                actualThis = actualThis.Siguiente;
            }

            this.ultimo = actualThis;
            this.NumeroElementos = lista.NumeroElementos;
        }

        /// <summary>
        /// Comprueba las postcondiciones de los distintos métodos Add.
        /// </summary>
        /// <param name="numElementos">Número de elementos al principio del método Add.</param>
        private void AddPostconditions(int numElementos)
        {
            Debug.Assert(NumeroElementos == numElementos + 1,
                "Añadir un elemento en una lista no incrementa el número de elementos.");
        }

        /// <summary>
        /// Añade un nuevo elemento al final de la lista.
        /// Coste temporal: O(1)
        /// </summary>
        /// <param name="value">Valor del elemento a añadir</param>
        public void AddLast(T value)
        {
            // Invariante
            CheckInvariante();

            int numElementos = NumeroElementos;

            Nodo<T> nuevoNodo = new Nodo<T>(value);
            ultimo.Siguiente = nuevoNodo;
            ultimo = nuevoNodo;

            NumeroElementos++;

            // Postcondiciones
            AddPostconditions(numElementos);

            // Invariante
            CheckInvariante();
        }

        /// <summary>
        /// Añade un nuevo elemento al inicio de la lista.
        /// Coste temporal: O(1)
        /// </summary>
        /// <param name="value">Valor del elemento a añadir</param>
        public void AddFirst(T value)
        {
            // Invariante
            CheckInvariante();

            int numElementos = NumeroElementos;

            Nodo<T> nuevoNodo = new Nodo<T>(value, centinela.Siguiente);
            centinela.Siguiente = nuevoNodo;

            // Si la lista estaba vacía, ahora 'ultimo' no apunta al centinela,
            // sino que apunta al nuevo nodo añadido.
            if (NumeroElementos == 0)
                ultimo = nuevoNodo;

            NumeroElementos++;

            // Postcondiciones
            AddPostconditions(numElementos);

            // Invariante
            CheckInvariante();
        }

        /// <summary>
        /// Añade un nuevo elemento en la posición indicada.
        /// Coste temporal: O(n)
        /// </summary>
        /// <param name="pos">Posición donde añadir el elemento.</param>
        /// <param name="value">Valor del elemento a añadir.</param>
        /// <exception cref="ArgumentOutOfRangeException">Se lanza si pos está fuera de rango.</exception>
        public void Add(int pos, T value)
        {
            // Invariante
            CheckInvariante();

            int numElementos = NumeroElementos;

            // Precondiciones
            if (pos < 0 || pos > NumeroElementos - 1)
                throw new ArgumentOutOfRangeException();

            Nodo<T> actual = centinela;
            Nodo<T> anterior = null;
            for (int i = 0; i <= pos; i++)
            {
                anterior = actual;
                actual = actual.Siguiente;
            }

            Nodo<T> nuevoNodo = new Nodo<T>(value, actual);
            anterior.Siguiente = nuevoNodo;

            NumeroElementos++;

            // Postcondiciones
            AddPostconditions(numElementos);

            // Invariante
            CheckInvariante();
        }

        /// <summary>
        /// Comprueba las postcondiciones de los distintos métodos Remove.
        /// </summary>
        /// <param name="numElementos">Número de elementos al principio del método Remove.</param>
        private void RemovePostconditions(int numElementos)
        {
            Debug.Assert(NumeroElementos == numElementos - 1,
                "Eliminar un elemento en una lista no decrementa el número de elementos.");
        }

        /// <summary>
        /// Elimina el primer elemento de la lista.
        /// Coste temporal: O(1).
        /// </summary>
        /// <exception cref="InvalidOperationException">Se lanza si el número de elementos de la lista es 0.</exception>
        public void RemoveFirst()
        {
            // Invariante
            CheckInvariante();

            int numElementos = NumeroElementos;

            // Precondiciones
            if (NumeroElementos == 0)
                throw new InvalidOperationException("No se puede eliminar el primer elemento de una lista vacía");

            centinela.Siguiente = centinela.Siguiente.Siguiente;
            NumeroElementos--;

            // Si hay 0 elementos después de borrar el primero, ultimo pasa a apuntar al centinela
            if (NumeroElementos == 0)
                ultimo = centinela;

            // Postcondiciones
            RemovePostconditions(numElementos);

            // Invariante
            CheckInvariante();
        }

        /// <summary>
        /// Elimina el último elemento de la lista.
        /// Coste temporal: O(n). 
        /// </summary>
        /// <exception cref="InvalidOperationException">Se lanza si el número de elementos de la lista es 0.</exception>
        public void RemoveLast()
        {
            // Invariante
            CheckInvariante();

            int numElementos = NumeroElementos;

            // Precondiciones
            if (NumeroElementos == 0)
                throw new InvalidOperationException("No se puede eliminar el último elemento de una lista vacía.");

            // El ultimo nodo no tiene una referencia al anterior
            // Tenemos que ir desde el centinela hasta el penultimo
            Nodo<T> actual = centinela;
            while (actual.Siguiente != ultimo)
            {
                actual = actual.Siguiente;
            }

            // Actual apunta al penultimo.
            actual.Siguiente = null; // el penultimo ya no tiene nodo siguiente
            ultimo = actual; // el penultimo pasa a ser el nuevo ultimo
            NumeroElementos--;

            // Postcondiciones
            RemovePostconditions(numElementos);

            // Invariante
            CheckInvariante();
        }

        /// <summary>
        /// Elimina el elemento en la posición indicada.
        /// Coste temporal: O(n)
        /// </summary>
        /// <param name="pos">Índice del elemento a eliminar</param>
        /// <exception cref="ArgumentOutOfRangeException">Se lanza si pos está fuera de rango.</exception>
        public void RemoveAt(int pos)
        {
            // Invariante
            CheckInvariante();

            int numElementos = NumeroElementos;

            // Precondiciones
            if (pos < 0 || pos > NumeroElementos - 1)
                throw new ArgumentOutOfRangeException();

            Nodo<T> actual = centinela;
            Nodo<T> anterior = null;
            for (int i = 0; i <= pos; i++)
            {
                anterior = actual;
                actual = actual.Siguiente;
            }

            anterior.Siguiente = actual.Siguiente;
            NumeroElementos--;

            // Postcondiciones
            RemovePostconditions(numElementos);

            // Invariante
            CheckInvariante();
        }

        /// <summary>
        /// Elimina la primera ocurrencia del elemento indicado, si está presente en la lista.
        /// </summary>
        /// <param name="value">Elemento a eliminar de la lista.</param>
        /// <returns>True si se encontró y eliminó el elemento, false en caso contrario.</returns>
        public bool RemoveValue(T value)
        {
            // Invariante
            CheckInvariante();

            int numElementos = NumeroElementos;

            Nodo<T> actual = centinela;
            Nodo<T> anterior = null;
            while (actual.Siguiente != null)
            {
                anterior = actual;
                actual = actual.Siguiente;

                if(actual.Valor == null && value != null)
                    continue;

                if ((actual.Valor == null && value == null) || actual.Valor.Equals(value))
                {
                    anterior.Siguiente = actual.Siguiente;
                    NumeroElementos--;

                    // Postcondiciones
                    RemovePostconditions(numElementos);

                    // Invariante
                    CheckInvariante();

                    return true;
                }
            }

            // Postcondiciones
            Debug.Assert(NumeroElementos == numElementos,
                "RemoveAt() con un valor que no está en la lista decrementa el número de elementos.");

            // Invariante
            CheckInvariante();

            return false;
        }

        /// <summary>
        /// Retorna el elemento en la posición indicada.
        /// Coste temporal: O(n)
        /// </summary>
        /// <returns>Elemento en la posición indicada.</returns>
        /// <param name="pos">Índice del elemento.</param>
        /// <exception cref="ArgumentOutOfRangeException">Se lanza si pos está fuera de rango.</exception>
        public T Get(int pos)
        {
            // Invariante
            CheckInvariante();

            // Precondiciones
            if (pos < 0 || pos > NumeroElementos - 1)
                throw new ArgumentOutOfRangeException();

            // Si la pos es la del último, se retorna directamente, 
            // ya que tenemos referencia a él, evitando así recorrer toda la lista
            if (pos == NumeroElementos - 1)
            {
                // Invariante
                CheckInvariante();

                return ultimo.Valor;
            }

            // Si no, tenemos que recorrer la lista hasta la posición indicada
            Nodo<T> actual = centinela;
            for (int i = 0; i <= pos; i++)
            {
                actual = actual.Siguiente;
            }

            // Invariante
            CheckInvariante();

            return actual.Valor;
        }

        /// <summary>
        /// Sustituye el elemento de la posición indicada por el elemento pasado como segundo parámetro.
        /// Coste temporal: O(n)
        /// </summary>
        /// <param name="pos">Posición donde del elemento a sustituir.</param>
        /// <param name="value">Valor del elemento que va a reemplazar al elemento de la posición indicada.</param>
        /// <exception cref="ArgumentOutOfRangeException">Se lanza si pos está fuera de rango.</exception>
        public void Set(int pos, T value)
        {
            // Invariante
            CheckInvariante();

            // Precondiciones
            if (pos < 0 || pos > NumeroElementos - 1)
                throw new ArgumentOutOfRangeException();

            Nodo<T> actual = centinela;
            for (int i = 0; i <= pos; i++)
                actual = actual.Siguiente;

            actual.Valor = value;

            // Postcondiciones
            if (value == null)
                Debug.Assert(Get(pos) == null,
                    "Hacer un Set no sustituye el valor correctamente.");
            else
                Debug.Assert(Get(pos).Equals(value),
                    "Hacer un Set no sustituye el valor correctamente.");

            // Invariante
            CheckInvariante();
        }

        /// <summary>
        /// Indica si el elemento pasado como parámetro está contenido en la lista.
        /// </summary>
        /// <param name="elem">Elemento a comprobar si está en la lista.</param>
        /// <returns>Cierto si el elemento está en la lista, y falso en caso contrario.</returns>
        public bool Contains(T elem)
        {
            // Invariante
            CheckInvariante();

            foreach (var val in this)
            {
                if(val == null && elem != null)
                    continue;

                if ((val == null && elem == null) || val.Equals(elem))
                {
                    // Invariante
                    CheckInvariante();

                    return true;
                }
            }

            // Invariante
            CheckInvariante();

            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj == this)
                return true;

            Lista<T> otra = obj as Lista<T>;
            if (otra == null)
                return false;

            if (otra.NumeroElementos != NumeroElementos)
                return false;

            var thisEnumerator = GetEnumerator();
            var otraEnumerator = otra.GetEnumerator();
            while (thisEnumerator.MoveNext() && otraEnumerator.MoveNext())
            {
                if (thisEnumerator.Current == null && otraEnumerator.Current == null)
                    continue;
                if (thisEnumerator.Current == null && otraEnumerator.Current != null)
                    return false;
                if (!thisEnumerator.Current.Equals(otraEnumerator.Current))
                    return false;
            }

            return true;
        }

        // TODO: Implement this method
//        public override int GetHashCode()
//        {
//            int value = 0;
//            
//            var thisEnumerator = GetEnumerator();
//            while (thisEnumerator.MoveNext())
//            {
//                if (thisEnumerator.Current != null)
//                    value += thisEnumerator.Current.GetHashCode();
//            }
//
//            return value;
//        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerador<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Devuelve un String con los elementos de la lista al estilo Python.
        /// </summary>
        /// <returns>String con los elementos de la lista.</returns>
        public override string ToString()
        {
            String toReturn = "[";

            foreach (var val in this)
                toReturn += val + ", ";

            if (toReturn.Length > 2)
                toReturn = toReturn.Substring(0, toReturn.Length - 2);
            toReturn += "]";

            return toReturn;
        }

        /// <summary>
        /// Comprueba la invariante de la clase.
        /// </summary>
        private void CheckInvariante()
        {
            // El número de elementos de la lista debe ser siempre mayor o igual que 0
            Debug.Assert(NumeroElementos >= 0,
                "El número de elementos de la lista es negativo.");

            // El nodo centinela siempre debe tener el valor por defecto del tipo T
            Debug.Assert((centinela.Valor == null && default(T) == null)
                         || (centinela.Valor.Equals(default(T))),
                "El nodo centinela tiene un valor.");

            // El nodo siguiente al último siempre debe ser null
            Debug.Assert(ultimo.Siguiente == null,
                "El último nodo tiene una referencia a un nodo siguiente.");

            // Si está vacía, entonces centinela y último deben apuntar al mismo nodo
            if (NumeroElementos == 0)
                Debug.Assert(centinela == ultimo,
                    "La lista está vacía y el centinela y el último no apuntan al mismo nodo.");

            // Si no está vacía, entonces centinela y último NO deben apuntar al mismo nodo
            if (NumeroElementos != 0)
                Debug.Assert(centinela != ultimo,
                    "La lista no está vacía y el centinela y el último apuntan al mismo nodo.");
        }


        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Wrappers~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        /// <summary>
        /// Añade un nuevo elemento al final de la lista.
        /// Coste temporal: O(1)
        /// </summary>
        /// <param name="value">Valor del elemento a añadir</param>
        public void Añadir(T value)
        {
            this.AddLast(value);
        }

        /// <summary>
        /// Elimina el último elemento de la lista.
        /// Coste temporal: O(n). 
        /// </summary>
        /// <exception cref="Exception">Se lanza si el número de elementos de la lista es 0.</exception>
        public void Borrar()
        {
            this.RemoveLast();
        }

        /// <summary>
        /// Retorna el elemento en la posición indicada.
        /// Coste temporal: O(n)
        /// </summary>
        /// <returns>Elemento en la posición indicada.</returns>
        /// <param name="pos">Índice del elemento.</param>
        /// <exception cref="ArgumentOutOfRangeException">Se lanza si pos está fuera de rango.</exception>
        public T GetElemento(int pos)
        {
            return this.Get(pos);
        }

        /// <summary>
        /// Indica si el elemento pasado como parámetro está contenido en la lista.
        /// </summary>
        /// <param name="elem">Elemento a comprobar si está en la lista.</param>
        /// <returns>Cierto si el elemento está en la lista, y falso en caso contrario.</returns>
        public bool Contiene(T elem)
        {
            return this.Contains(elem);
        }

        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    }
}