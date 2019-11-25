using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace diccionario
{
    [TestClass]
    public class TestsDiccionario01
    {
        private IDictionary<int, string> dicc;

        [TestCleanup]
        public void CleanTests()
        {
            dicc = null;
        }

        /// <summary>
        /// 1. Añadir elementos con clave y valor dados
        /// </summary>
        [TestMethod]
        public void TestAdd()
        {
            dicc = new Dictionary<int, string>();

            // Comprueba que antes de añadir elementos el diccionario está vacío
            Assert.AreEqual(0, dicc.Count,
                "Un diccionario vacío no tiene 0 pares (clave, valor).");

            // Comprueba que se añade un elemento correctamente
            dicc.Add(1, "first");
            Assert.AreEqual(1, dicc.Count,
                "Add() no incrementa en 1 el número de elementos.");
            Assert.AreEqual("first", dicc[1],
                "Add() no añade el elemento especificado.");

            // Comprueba que se añade un elemento usando []
            dicc[2] = "second";
            Assert.AreEqual(2, dicc.Count,
                "Añadir un elemento usando [] no incrementa en 1 el número de elementos.");
            Assert.AreEqual("second", dicc[2],
                "Añadir un elemento usando [] no añade el elemento especificado.");
        }

        /// <summary>
        /// 2. Conocer el número de pares de la colección
        /// </summary>
        [TestMethod]
        public void TestCount()
        {
            dicc = new Dictionary<int, string>();

            // Comprueba que un diccionario vacío tiene 0 elementos
            Assert.AreEqual(0, dicc.Count,
                "Count no indica que un diccionario vacío tiene 0 elementos.");

            // Comprueba que un diccionario con 5 pares indica que tiene 5 elementos
            dicc = new Dictionary<int, string>
            {
                {1, "first"},
                {2, "second"},
                {3, "third"},
                {4, "fourth"},
                {5, "fifth"}
            };

            Assert.AreEqual(5, dicc.Count,
                "Count no indica que un diccionario con 5 elementos tiene 5 elementos.");
        }

        /// <summary>
        /// 3. Obtener y reescribir el valor para una clave dada
        /// </summary>
        [TestMethod]
        public void TestIndexing()
        {
            dicc = new Dictionary<int, string> {{1, "first"}, {2, "second"}};

            // Probamos a obtener y modificar un par
            Assert.AreEqual("first", dicc[1],
                "El valor devuelto para una clave existente no es correcto.");
            dicc[1] = "modifiedFirst";
            Assert.AreEqual("modifiedFirst", dicc[1],
                "Modificar el valor de un par con una clave existente no funciona correctamente.");

            // Probamos a obtener y modificar otro par
            Assert.AreEqual("second", dicc[2],
                "El valor devuelto para una clave existente no es correcto.");
            dicc[2] = "modifiedSecond";
            Assert.AreEqual("modifiedSecond", dicc[2],
                "Modificar el valor de un par con una clave existente no funciona correctamente.");
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void TestIndexingThrowsException()
        {
            dicc = new Dictionary<int, string>();
            string val = dicc[0];
        }

        /// <summary>
        /// 4. Saber si una clave está o no en la colección
        /// </summary>
        [TestMethod]
        public void TestContainsKey()
        {
            dicc = new Dictionary<int, string> {{1, "first"}};

            // Comprobamos que la clave 1 está en la colección
            Assert.IsTrue(dicc.ContainsKey(1),
                "ContainsKey() indica que el diccionario no contiene una clave que está en el diccionario.");

            // Comprobamos que la clave 2 no está en la colección
            Assert.IsFalse(dicc.ContainsKey(2),
                "ContainsKey() indica que el diccionario contiene una clave que no está en el diccionario.");
        }

        /// <summary>
        /// 5. Borrar, cuando exista, el elemento de la colección pasando su clave
        /// </summary>
        [TestMethod]
        public void TestRemove()
        {
            dicc = new Dictionary<int, string> {{1, "first"}, {2, "second"}};

            // Borramos el par con la clave 1, que está en la colección
            Assert.IsTrue(dicc.Remove(1),
                "Remove() no devuelve true con una clave que está en el diccionario.");
            Assert.AreEqual(1, dicc.Count,
                "Remove() no decrementa en 1 el número de elementos tras eliminar un elemento del diccionario.");
            Assert.IsFalse(dicc.ContainsKey(1),
                "Remove() no elimina correctamente una clave que está en el diccionario.");

            // Comprobamos que retorna retorna false y no modifica el diccionario al intentar borrar una clave que no está
            Assert.IsFalse(dicc.Remove(3),
                "Remove() devuelve true con una clave que no está en el diccionario.");
            Assert.AreEqual(1, dicc.Count,
                "Remove() decrementa en 1 el número de elementos tras intentar " +
                "eliminar un elemento que no está en el diccionario.");
        }

        /// <summary>
        /// 6. Iteración mediante un foreach
        /// </summary>
        [TestMethod]
        public void TestIteratorForEach()
        {
            dicc = new Dictionary<int, string> {{1, "first"}, {2, "second"}, {3, "third"}};

            int key = 1, forLoopExecutions = 0;
            string[] values = new string[] {"first", "second", "third"};
            foreach (var current in dicc)
            {
                // Comprobamos que las claves coinciden
                Assert.AreEqual(key, current.Key,
                    "La clave obtenida con el iterador usando un foreach no coincide con la clave esperada.");
                // Comprobamos que los valores coinciden
                Assert.AreEqual(values[key - 1], current.Value,
                    "El valor obtenido con el iterador usando un foreach no coincide con el valor esperado.");

                key++;
                forLoopExecutions++;
            }

            // Nos aseguramos de que se ha ejecutado el bucle las veces necesarias
            Assert.AreEqual(3, forLoopExecutions,
                "El iterador ejecutado con el foreach no ha recorrido todos los elementos del diccionario.");

            // Comprobamos que al finalizar el foreach el metodo Reset() del iterador lo resetea correctamente
            forLoopExecutions = 0;
            foreach (var current in dicc)
            {
                // Comprobamos que las claves coinciden
                Assert.AreEqual(1, current.Key,
                    "El metodo Reset() del iterador del diccionario no funciona correctamente.");
                forLoopExecutions++;
                break;
            }

            // Nos aseguramos de que se ha ejecutado el bucle las veces necesarias
            Assert.AreEqual(1, forLoopExecutions,
                "El metodo Reset() del iterador del diccionario no funciona correctamente. " +
                "No permite iterar de nuevo por el diccionario.");
        }
    }
}