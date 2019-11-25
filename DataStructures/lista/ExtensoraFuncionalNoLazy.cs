using System;
using System.Collections.Generic;
using System.Linq;

namespace TPP.Practicas.Lista
{
    public static class ExtensoraFuncional
    {
        /// <summary>
        /// Retorna una nueva colección cuyos elementos son el resultado de aplicar la función
        /// pasada como parámetro a cada uno de los elementos de la colección.
        /// </summary>
        public static IEnumerable<TResult> Map<T, TResult>(this IEnumerable<T> enumerable,
                                                           Func<T, TResult> func)
        {
            Lista<TResult> toReturn = new Lista<TResult>();

            foreach (var val in enumerable)
                toReturn.AddLast(func(val));

            return toReturn;
        }

        /// <summary>
        /// A partir de una colección de elementos, nos devuelve el primero que cumpla
        /// un criterio dado o su valor por defecto en el caso de no existir ninguno.
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
        /// </summary>
        public static IEnumerable<T> Filtrar<T>(this IEnumerable<T> enumerable,
                                                Predicate<T> predicate)
        {
            Lista<T> toReturn = new Lista<T>();

            foreach (var val in enumerable)
                if (predicate(val))
                    toReturn.AddLast(val);

            return toReturn;
        }

        /// <summary>
        /// Esta función recibe una colección de elementos y una función
        /// que recibe un primer parámetro del tipo que queremos obtener
        /// y un segundo parámetro del tipo de los elementos de la colección;
        /// su tipo devuelto es el propio del que queremos obtener.
        /// </summary>
        public static TResult Reducir<T, TResult>(this IEnumerable<T> enumerable,
                                                  Func<TResult, T, TResult> func,
                                                  TResult inicializacionAcumulador = default(TResult))
        {
            TResult result = inicializacionAcumulador;
            foreach (var val in enumerable)
            {
                result = func(result, val);
            }

            return result;
        }
    }
}