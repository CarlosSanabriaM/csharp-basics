using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TPP.Laboratory.Concurrency.Lab11;

namespace entrega12
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            # region TPL

            Console.WriteLine("\nTPL\n___");
            Stopwatch chronoTPL = new Stopwatch();
            chronoTPL.Start();
            var wordsCountTPL = CountWordsInParallelWithTPL(print: false);
            chronoTPL.Stop();
            Console.WriteLine("Elapsed time: {0:N} milliseconds.", chronoTPL.ElapsedMilliseconds);

            #endregion

            # region PLINQ

            Console.WriteLine("\nPLINQ\n___");
            Stopwatch chronoPLINQ = new Stopwatch();
            chronoPLINQ.Start();
            var wordsCountPLINQ = CountWordsInParallelWithPLINQ(print: false);
            chronoPLINQ.Stop();
            Console.WriteLine("Elapsed time: {0:N} milliseconds.", chronoPLINQ.ElapsedMilliseconds);

            #endregion

            # region Sequential

            Console.WriteLine("\nSequential\n___");
            Stopwatch chronoSequential = new Stopwatch();
            chronoSequential.Restart();
            var wordsCountSequential = CountWordsInSequential(print: false);
            chronoSequential.Stop();
            Console.WriteLine("Elapsed time: {0:N} milliseconds.", chronoSequential.ElapsedMilliseconds);

            #endregion

            // Nos aseguramos de que las versiones en paralelo y la secuencial dan el mismo resultado
            Debug.Assert(wordsCountTPL.Equals(wordsCountSequential));
            Debug.Assert(wordsCountPLINQ.Equals(wordsCountSequential));

            Console.WriteLine();
            ShowTimeBenefit("TPL", "Sequential",
                chronoTPL.ElapsedMilliseconds, chronoSequential.ElapsedMilliseconds);
            ShowTimeBenefit("PLINQ", "Sequential",
                chronoPLINQ.ElapsedMilliseconds, chronoSequential.ElapsedMilliseconds);

            /*En muchos casos, una consulta se puede ejecutar en paralelo, pero la sobrecarga que implica configurar 
            la consulta en paralelo supera el beneficio obtenido en el rendimiento. Si una consulta no realiza mucho 
            cálculo o si el origen de datos es pequeño, una consulta PLINQ podría ser más lenta que una consulta LINQ secuencial.*/
        }

        private static IDictionary<string, int> CountWordsInSequential(bool print = true)
        {
            // 1. Cargar el fichero
            String text = Processing.ReadTextFile(@"../../../clarin.txt");

            // 2. Dividir en palabras
            var textWords = Processing.DivideIntoWords(text);

            // 2. Crear un diccionaro y almacenar en él la cuenta de cada palabra del texto 
            IDictionary<string, int> wordsCountDict = new Dictionary<string, int>();
            foreach (var word in textWords)
            {
                if (wordsCountDict.ContainsKey(word))
                    wordsCountDict[word]++;
                else
                    wordsCountDict.Add(word, 1);
            }

            // 4. Imprimir el diccionario
            if (print)
                ShowDict(wordsCountDict);

            // 5. Devolver el diccionario
            return wordsCountDict;
        }

        public static IDictionary<string, int> CountWordsInParallelWithTPL(bool print = true)
        {
            // 1. Cargar las lineas del fichero
            IEnumerable<String> textLines = Processing.ReadTextFileLines(@"../../../clarin.txt");

            // 2. Por cada línea, en paralelo, creamos un diccionario en el que se cuente
            // el número de apariciones de cada palabra en esa línea
            List<IDictionary<string, int>> dictionaries = new List<IDictionary<string, int>>();
            Parallel.ForEach(textLines, line =>
            {
                // 2.1. Dividir en palabras
                var lineWords = Processing.DivideIntoWords(line);

                // 2.1. Crear el diccionario y meter en él la cuenta de cada palabra
                var dictionary = new Dictionary<string, int>();
                foreach (var word in lineWords)
                {
                    if (dictionary.ContainsKey(word))
                        dictionary[word]++;
                    else
                        dictionary.Add(word, 1);
                }

                lock (dictionaries)
                    dictionaries.Add(dictionary);
            });

            // 3. Juntar los diccionarios en un solo
            var wordsCountDict = dictionaries.AsParallel().Aggregate(
                new Dictionary<string, int>(),
                (finalDict, currentDict) =>
                {
                    foreach (var key in currentDict.Keys)
                    {
                        if (finalDict.ContainsKey(key))
                            finalDict[key] += currentDict[key];
                        else
                            finalDict.Add(key, currentDict[key]);
                    }

                    return finalDict;
                }
            );

            // 4. Imprimir el diccionario
            if (print)
                ShowDict(wordsCountDict);

            // 5. Devolver el diccionario
            return wordsCountDict;
        }

        public static IDictionary<string, int> CountWordsInParallelWithPLINQ(bool print = true)
        {
            // 1. Cargar las lineas del fichero
            IEnumerable<String> textLines = Processing.ReadTextFileLines(@"../../../clarin.txt");

            var wordsCountDict = textLines.AsParallel()
                                          .Select(line => Processing.DivideIntoWords(line))
                                          .Select(lineWords =>
                                          {
                                              var dictionary = new Dictionary<string, int>();
                                              foreach (var word in lineWords)
                                              {
                                                  if (dictionary.ContainsKey(word))
                                                      dictionary[word]++;
                                                  else
                                                      dictionary.Add(word, 1);
                                              }

                                              return dictionary;
                                          })
                                          .Aggregate(
                                              new Dictionary<string, int>(),
                                              (previousDict, currentDict) =>
                                              {
                                                  foreach (var key in currentDict.Keys)
                                                  {
                                                      if (previousDict.ContainsKey(key))
                                                          previousDict[key] += currentDict[key];
                                                      else
                                                          previousDict.Add(key, currentDict[key]);
                                                  }

                                                  return previousDict;
                                              }
                                          );

            // 4. Imprimir el diccionario
            if (print)
                ShowDict(wordsCountDict);

            // 5. Devolver el diccionario
            return wordsCountDict;
        }

        public static void ShowDict<TKey, TValue>(IDictionary<TKey, TValue> dict)
        {
            foreach (var key in dict.Keys)
            {
                Console.WriteLine("{0}: {1}", key, dict[key]);
            }
        }

        public static void ShowTimeBenefit(string method1, string method2, long time1, long time2)
        {
            var benefit = (time2 / (double) time1) - 1;
            Console.WriteLine("{0} has a time benefit against {1} of {2:P} percent", method1, method2, benefit);
        }
        
    }
}