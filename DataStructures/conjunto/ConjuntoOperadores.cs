namespace conjunto
{
    /// <summary>
    /// Clase que representa un conjunto ordenado (no hay elementos repetidos, pero los elementos están ordenados).
    /// Este fichero implementa la sobrecarga de operadores para el Conjunto.
    /// </summary>
    /// <typeparam name="T">Tipos de los elementos del conjunto.</typeparam>
    public partial class Conjunto<T>
    {
        /// <summary>
        /// Devuelve un conjunto al que se le añade un elemento al final.
        /// No modifica el conjunto de partida.
        /// </summary>
        /// <param name="conjunto"></param>
        /// <param name="valor">Elemento a añadir al final.</param>
        /// <returns></returns>
        public static Conjunto<T> operator +(Conjunto<T> conjunto, T valor)
        {
            Conjunto<T> toReturn = new Conjunto<T>(conjunto); // creamos una copia del conjunto original 
            toReturn.AddLast(valor);
            return toReturn;
        }
        
        /// <summary>
        /// Devuelve un conjunto al que se le añaden varios elementos al final.
        /// No modifica el conjunto de partida.
        /// </summary>
        /// <param name="conjunto"></param>
        /// <param name="valores">Elementos a añadir al final.</param>
        /// <returns></returns>
        public static Conjunto<T> operator +(Conjunto<T> conjunto, T[] valores)
        {
            Conjunto<T> toReturn = new Conjunto<T>(conjunto); // creamos una copia del conjunto original
            
            // Eliminamos los repetidos antes de añadirlos al conjunto
            // El AddLast no dejaría añadir elementos repetidos en el array "valores",
            // pero es más eficiente eliminarlos aquí que al hacer el AddLawst y recorrer el conjunto
            valores = EliminaRepetidos(valores);
            
            foreach (var valor in valores)
            {
                toReturn.AddLast(valor);
            }           
            return toReturn;
        }

        /// <summary>
        /// Devuelve un conjunto al que se le borra un elemento.
        /// No modifica el conjunto de partida.
        /// </summary>
        /// <param name="conjunto"></param>
        /// <param name="valor">Valor a borrar.</param>
        /// <returns></returns>
        public static Conjunto<T> operator -(Conjunto<T> conjunto, T valor)
        {
            Conjunto<T> toReturn = new Conjunto<T>(conjunto); // creamos una copia del conjunto original
            toReturn.RemoveValue(valor);
            return toReturn;
        }
        
        /// <summary>
        /// Devuelve un conjunto al que se le borran varios elementos.
        /// No modifica el conjunto de partida.
        /// </summary>
        /// <param name="conjunto"></param>
        /// <param name="valores">Valores a borrar.</param>
        /// <returns></returns>
        public static Conjunto<T> operator -(Conjunto<T> conjunto, T[] valores)
        {
            Conjunto<T> toReturn = new Conjunto<T>(conjunto); // creamos una copia del conjunto original
            
            // Eliminamos los repetidos antes de eliminarlos del conjunto
            valores = EliminaRepetidos(valores);
            
            foreach (var valor in valores)
            {
                toReturn.RemoveValue(valor);
            }           
            return toReturn;
        }

        public T this[int index]
        {
            get { return this.Get(index); }
            set
            {
                if (_lista.Contains(value))
                    return;
                
                _lista.Set(index, value);
            }
        }

        /// <summary>
        /// Devuelve un conjunto que es la unión de los dos conjuntos.
        /// No modifica ninguno de los dos conjuntos.
        /// </summary>
        /// <param name="conjunto1"></param>
        /// <param name="conjunto2"></param>
        /// <returns>Conjunto unión de los otros dos conjuntos.</returns>
        public static Conjunto<T> operator |(Conjunto<T> conjunto1, Conjunto<T> conjunto2)
        {
            Conjunto<T> toReturn = new Conjunto<T>(conjunto1);
            
            // TODO: Cambiar esta implementación cuando la Lista implemente IEnumerable y se pueda hacer un foreach sobre ella
            // La complejidad es muy alta, dado que Get va desde el principio del conjunto hasta el elemento i-ésimo, y esto se hace n veces.
            // Esto se podrá arreglar cuando se pueda iterar por los conjuntos.
            for (int i = 0; i < conjunto2.NumeroElementos; i++)
                toReturn.AddLast(conjunto2.Get(i));

            return toReturn;
        }
        
        /// <summary>
        /// Devuelve un conjunto que es la intersección de los dos conjuntos.
        /// No modifica ninguno de los dos conjuntos.
        /// Los elementos quedan ordenados en función de su aparición en el primer conjunto.
        /// </summary>
        /// <param name="conjunto1"></param>
        /// <param name="conjunto2"></param>
        /// <returns>Conjunto intersección de los otros dos conjuntos.</returns>
        public static Conjunto<T> operator &(Conjunto<T> conjunto1, Conjunto<T> conjunto2)
        {
            Conjunto<T> toReturn = new Conjunto<T>();
            
            // TODO: Cambiar esta implementación cuando la Lista implemente IEnumerable y se pueda hacer un foreach sobre ella
            // La complejidad es muy alta, dado que Get va desde el principio del conjunto hasta el elemento i-ésimo, y esto se hace n veces.
            // Esto se podrá arreglar cuando se pueda iterar por los conjuntos.
            // Además, Contains también es O(n)
            for (int i = 0; i < conjunto1.NumeroElementos; i++)
            {
                T value = conjunto1.Get(i);
                if (conjunto2.Contains(value))
                    toReturn.AddLast(value);
            }
                
            return toReturn;
        }
        
        /// <summary>
        /// Devuelve un conjunto que equivale a eliminar del primer conjunto todos
        /// los elementos presentes también en el segundo conjunto.
        /// No modifica el conjunto de partida.
        /// </summary>
        /// <param name="conjunto1"></param>
        /// <param name="conjunto2"></param>
        /// <returns></returns>
        public static Conjunto<T> operator -(Conjunto<T> conjunto1, Conjunto<T> conjunto2)
        {
            Conjunto<T> toReturn = new Conjunto<T>(conjunto1); // creamos una copia del conjunto original
            
            for (int i = 0; i < conjunto2.NumeroElementos; i++)
            {
                toReturn.RemoveValue(conjunto2.Get(i));
            }
            
            return toReturn;
        }
        
        /// <summary>
        /// Indica si el elemento pertenece al conjunto.
        /// </summary>
        /// <param name="conjunto"></param>
        /// <param name="valor">Elemento que se quiere saber si pertenece al conjunto.</param>
        /// <returns></returns>
        public static bool operator ^(Conjunto<T> conjunto, T valor)
        {
            return conjunto.Contains(valor);
        }
        
    }
}