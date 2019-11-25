using System;
using lista;

namespace conjunto
{
    /// <summary>
    /// Clase que representa un conjunto ordenado (no hay elementos repetidos, pero los elementos están ordenados).
    /// Este fichero implementa las operaciones principales del Conjunto.
    /// </summary>
    /// <typeparam name="T">Tipos de los elementos del conjunto.</typeparam>
    public partial class Conjunto<T>
    {
        private readonly Lista<T> _lista;

        /// <summary>
        ///     Crea un conjunto vacío.
        /// </summary>
        public Conjunto()
        {
            _lista = new Lista<T>();
        }

        /// <summary>
        ///     Crea un conjunto con un único elemento.
        /// </summary>
        /// <param name="valor">Elemento a añadir en el conjunto.</param>
        public Conjunto(T valor)
        {
            _lista = new Lista<T>(valor);
        }

        /// <summary>
        ///     Crea un conjunto con los elementos pasados como parámetro.
        ///     El número de argumentos es variable.
        ///     También puede pasarse como parámetro un array.
        /// </summary>
        /// <param name="valores"></param>
        public Conjunto(params T[] valores)
        {
            _lista = new Lista<T>(EliminaRepetidos(valores));
        }

        /// <summary>
        ///     Constructor de copia.
        /// </summary>
        /// <param name="conjunto">Conjunto a copiar.</param>
        public Conjunto(Conjunto<T> conjunto)
        {
            _lista = new Lista<T>(conjunto._lista);
        }

        public int NumeroElementos => _lista.NumeroElementos;

        /// <summary>
        ///     Añade un nuevo elemento al final del conjunto.
        ///     Coste temporal: O(1)
        /// </summary>
        /// <param name="value">Valor del elemento a añadir</param>
        /// <returns>Si el elemento se pudo añadir (no existe en el conjunto) retorna true, y false en caso contrario</returns>
        public bool AddLast(T value)
        {
            if (_lista.Contains(value))
                return false;

            _lista.AddLast(value);
            return true;
        }

        /// <summary>
        ///     Añade un nuevo elemento al inicio del conjunto.
        ///     Coste temporal: O(1)
        /// </summary>
        /// <param name="value">Valor del elemento a añadir</param>
        /// <returns>Si el elemento se pudo añadir (no existe en el conjunto) retorna true, y false en caso contrario</returns>
        public bool AddFirst(T value)
        {
            if (_lista.Contains(value))
                return false;

            _lista.AddFirst(value);
            return true;
        }

        /// <summary>
        ///     Añade un nuevo elemento en la posición indicada.
        ///     Coste temporal: O(n)
        /// </summary>
        /// <param name="pos">Posición donde añadir el elemento.</param>
        /// <param name="value">Valor del elemento a añadir.</param>
        /// <returns>Si el elemento se pudo añadir (no existe en el conjunto) retorna true, y false en caso contrario</returns>
        public bool Add(int pos, T value)
        {
            if (_lista.Contains(value))
                return false;

            _lista.Add(pos, value);
            return true;
        }

        /// <summary>
        ///     Elimina el primer elemento del conjunto.
        ///     Coste temporal: O(1).
        /// </summary>
        public void RemoveFirst()
        {
            _lista.RemoveFirst();
        }

        /// <summary>
        ///     Elimina el último elemento del conjunto.
        ///     Coste temporal: O(n).
        /// </summary>
        public void RemoveLast()
        {
            _lista.RemoveLast();
        }

        /// <summary>
        ///     Elimina el elemento en la posición indicada.
        ///     Coste temporal: O(n)
        /// </summary>
        /// <param name="pos">Índice del elemento a eliminar</param>
        public void RemoveAt(int pos)
        {
            _lista.RemoveAt(pos);
        }

        /// <summary>
        ///     Elimina el elemento indicado del conjunto, si está presente.
        /// </summary>
        /// <param name="value">Elemento a eliminar del conjunto.</param>
        /// <returns>True si se encontró y eliminó el elemento, false en caso contrario.</returns>
        public bool RemoveValue(T value)
        {
            return _lista.RemoveValue(value);
        }

        /// <summary>
        ///     Retorna el elemento en la posición indicada.
        ///     Coste temporal: O(n)
        /// </summary>
        /// <returns>Elemento en la posición indicada.</returns>
        /// <param name="pos">Índice del elemento.</param>
        public T Get(int pos)
        {
            return _lista.Get(pos);
        }

        /// <summary>
        ///     Indica si el elemento pasado como parámetro está contenido en el conjunto.
        /// </summary>
        /// <param name="elem">Elemento a comprobar si está en el conjunto.</param>
        /// <returns>Cierto si el elemento está en el conjunto, y falso en caso contrario.</returns>
        public bool Contains(T elem)
        {
            return _lista.Contains(elem);
        }

        /// <summary>
        ///     Devuelve un String con los elementos del conjunyo al estilo Python.
        /// </summary>
        /// <returns>String con los elementos del conjunto.</returns>
        public override string ToString()
        {
            string toReturn = _lista.ToString();

            // Eliminamos los corchetes "[...]" del String que devuelve la lista
            toReturn = toReturn.Substring(1, toReturn.Length - 2);

            return "{" + toReturn + "}";
        }

        private static T[] EliminaRepetidos(T[] valores)
        {
            var valoresSinRepetidos = new T[valores.Length];
            var numValoresSinRepetidos = 0;

            foreach (var valor in valores)
            {
                var valorRepetido = false;
                for (var i = 0; i < numValoresSinRepetidos; i++)
                    if (valoresSinRepetidos[i].Equals(valor))
                    {
                        valorRepetido = true;
                        break;
                    }

                if (!valorRepetido)
                {
                    valoresSinRepetidos[numValoresSinRepetidos] = valor;
                    numValoresSinRepetidos++;
                }
            }

            Array.Resize(ref valoresSinRepetidos, numValoresSinRepetidos);
            return valoresSinRepetidos;
        }
    }
}