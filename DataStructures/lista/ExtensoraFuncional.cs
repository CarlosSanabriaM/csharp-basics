using System;
using System.Collections.Generic;
using System.Linq;

namespace lista
{
    public static class ExtensoraFuncional
    {
        /// <summary>
        /// Retorna un IEnumerable cuyos elementos son el resultado de aplicar la función
        /// pasada como parámetro a cada uno de los elementos del IEnumerable.
        /// En Linq se llama Select.
        /// </summary>
        public static IEnumerable<TResult> Map<T, TResult>(this IEnumerable<T> enumerable,
                                                           Func<T, TResult> func)
        {
            foreach (var val in enumerable)
                yield return func(val); // lazy
        }

        /// <summary>
        /// A partir de una colección de elementos, nos devuelve el primero que cumpla
        /// un criterio dado o su valor por defecto en el caso de no existir ninguno.
        /// En Linq se llama FirstOrDefault.
        /// </summary>
        public static T Buscar<T>(this IEnumerable<T> enumerable,
                                  Predicate<T> predicate)
        {
            foreach (var val in enumerable)
                if (predicate(val))
                    return val;
            return default(T);
        }

        /// <summary>
        /// A partir de una colección de elementos, nos devuelve todos aquellos
        /// que cumplan un criterio dado (siendo éste parametrizable).
        /// En Linq se llama Where.
        /// </summary>
        public static IEnumerable<T> Filtrar<T>(this IEnumerable<T> enumerable,
                                                Predicate<T> predicate)
        {
            foreach (var val in enumerable)
                if (predicate(val))
                    yield return val; // lazy
        }

        /// <summary>
        /// Esta función recibe una colección de elementos y una función
        /// que recibe un primer parámetro del tipo que queremos obtener
        /// y un segundo parámetro del tipo de los elementos de la colección;
        /// su tipo devuelto es el propio del que queremos obtener.
        /// En Linq se llama Aggregate.
        /// </summary>
        public static TResult Reducir<T, TResult>(this IEnumerable<T> enumerable,
                                                  Func<TResult, T, TResult> func,
                                                  TResult inicializacionAcumulador = default(TResult))
        {
            TResult result = inicializacionAcumulador;
            foreach (var val in enumerable)
                result = func(result, val);

            return result;
        }

        /// <summary>
        /// Devuelve un IEnumerable que permite iterar de derecha a izquierda.
        /// En Linq se llama Reverse.
        /// </summary>
        public static IEnumerable<T> Invertir<T>(this IEnumerable<T> enumerable)
        {
            // Los IEnumerables no permiten iterar de derecha a izquierda (no es algo que solicite implementar dicha interfaz)
            // Necesitamos guardar en un buffer (un array) toda la información del IEnumerable, y luego iterar del revés. 
            var array = enumerable.ToArray();
            for (var i = array.Length - 1; i >= 0; i--)
                yield return array[i]; // lazy
        }

        /// <summary>
        /// Itera por los elementos del IEnumerable de izquierda a derecha
        /// y les aplica el Action pasado como parámetro. Dicho action no debe
        /// modificar los elementos de la lista.
        /// En Linq solo se permite para IList.
        /// </summary>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var val in enumerable)
                action(val);
        }
    }
}