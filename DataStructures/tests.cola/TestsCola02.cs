using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TPP.Practicas.Utils;

namespace TPP.Practicas.Cola
{
    /// <summary>
    /// Prueba todas las operaciones de la clase Cola, con varios hilos.
    /// </summary>
    [TestClass]
    public class TestsCola02
    {
        private ColaConcurrente<int> cola;

        [TestCleanup]
        public void CleanTests()
        {
            cola = null;
        }

        /// <summary>
        /// Crea varios Threads, y de forma paralela, cada Thread añade el mismo elemento
        /// repetidas veces en la cola. Si la cola es Thread Safe, no debería dar problemas
        /// al llamar el método Añadir desde distintos hilos. Como se añade el mismo elemento,
        /// es facil comprobar cual es el resultado esperado: La cola deberá tener numThreads * numVeces elementos,
        /// y todos los elementos de la cola deberán ser el mismo, el especificado en la variable elemento.
        /// </summary>
        [TestMethod]
        public void TestAñadirVariosThreads()
        {
            cola = new ColaConcurrente<int>();

            int numThreads = 10;
            int elemento = 5;
            int numVeces = 1000;

            Thread[] threads = new Thread[numThreads];
            for (int i = 0; i < numThreads; i++)
                threads[i] = new Thread(AñadirMismoElementoXVecesEnCola);

            foreach (var thread in threads)
                thread.Start(new Tuple<int, int>(elemento, numVeces));
            foreach (var thread in threads)
                thread.Join();

            Assert.IsFalse(cola.EstáVacía(),
                "El método Añadir() llamado con varios Threads deja una cola vacía.");
            Assert.AreEqual(numThreads * numVeces, cola.NumeroElementos,
                "El método Añadir() llamado con varios Threads no añade el número de elementos esperado.");
            for (int i = 0; i < numThreads * numVeces; i++)
                Assert.AreEqual(elemento, cola.Extraer(),
                    "El método Añadir() llamado con varios Threads no añade los elementos esperados.");
        }

        private void AñadirMismoElementoXVecesEnCola(object objectTupla)
        {
            var tupla = (Tuple<int, int>) objectTupla;
            int elemento = tupla.Item1;
            int numVeces = tupla.Item2;

            for (int i = 0; i < numVeces; i++)
                cola.Añadir(elemento);
        }

        /// <summary>
        /// Crea varios Threads, y de forma paralela, cada Thread extrae un elemento de la cola, hasta extraerlos todos.
        /// Todos los elementos de la cola son el mismo elemento. Si la cola es Thread Safe, no debería dar problemas
        /// al llamar el método Extraer desde distintos hilos. El resultado esperado es el siguiente:
        /// Al llamar a Extraer() la cola deberá devolver siempre el mismo elemento (el especificado en la variable elemento)
        /// y al final, la cola deberá estar vacía.
        /// </summary>
        [TestMethod]
        public void TestExtraerVariosThreads()
        {
            cola = new ColaConcurrente<int>();

            int numThreads = 10;
            int elemento = 5;
            int totalElementos = 10000;

            // El total de elementos debe ser divisible entre el número de hilos
            Debug.Assert(totalElementos % numThreads == 0);

            // Se añade el elemento las veces indicadas
            for (int i = 0; i < totalElementos; i++)
                cola.Añadir(elemento);

            Thread[] threads = new Thread[numThreads];
            for (int i = 0; i < numThreads; i++)
                threads[i] = new Thread(ExtraerElementoXVecesEnCola);

            var numVecesExtraerElemento = totalElementos / numThreads;
            foreach (var thread in threads)
                thread.Start(new Tuple<int, int>(elemento, numVecesExtraerElemento));
            foreach (var thread in threads)
                thread.Join();

            // Al final, la cola deberá estar vacía
            Assert.IsTrue(cola.EstáVacía(),
                "El método Extraer() llamado con varios Threads no deja la cola vacía, aunque debería.");
            Assert.AreEqual(0, cola.NumeroElementos,
                "El método Extraer() llamado con varios Threads no deja la cola vacía, aunque debería.");
        }

        private void ExtraerElementoXVecesEnCola(object objectTupla)
        {
            var tupla = (Tuple<int, int>) objectTupla;
            int elemento = tupla.Item1;
            int numVeces = tupla.Item2;

            for (int i = 0; i < numVeces; i++)
            {
                // Se comprueba también que el elemento devuelto por PrimerElemento() coincide con 'elemento'  
                Assert.AreEqual(elemento, cola.PrimerElemento(),
                    "El elemento obtenido con PrimerElemento() no coincide con el esperado.");
                // Cada vez que se extrae un elemento, se comprueba que coincide con 'elemento'
                var extraido = cola.Extraer();
                Assert.AreEqual(elemento, extraido,
                    "El elemento extraido no coincide con el esperado.");
            }
        }
    }
}