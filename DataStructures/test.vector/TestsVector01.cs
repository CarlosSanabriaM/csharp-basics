using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace vector
{
    [TestClass]
    public class TestsVector01
    {
        private IList<int> lista;

        [TestCleanup]
        public void CleanTests()
        {
            lista = null;
        }

        /// <summary>
        /// 1. Añadir elementos
        /// </summary>
        [TestMethod]
        public void TestAdd()
        {
            lista = new List<int>();

            // Comprueba que antes de añadir elementos la lista esté vacía
            Assert.AreEqual(0, lista.Count,
                "Una lista vacía no tiene 0 elementos.");

            // Comprueba que se añade un elemento correctamente
            lista.Add(10);
            Assert.AreEqual(1, lista.Count,
                "Add() no incrementa en 1 el número de elementos.");
            Assert.AreEqual(10, lista[0],
                "Add() no añade el elemento especificado.");

            // Comprueba que se añade un segundo elemento correctamente
            lista.Add(11);
            Assert.AreEqual(2, lista.Count,
                "Add() no incrementa en 1 el número de elementos.");
            Assert.AreEqual(10, lista[0],
                "Add() modifica los elementos previos de la lista.");
            Assert.AreEqual(11, lista[1],
                "Add() no añade correctamente el elemento especificado.");
        }

        /// <summary>
        /// 2. Conocer el número de elementos
        /// </summary>
        [TestMethod]
        public void TestCount()
        {
            lista = new List<int>();

            // Comprueba que una lista vacía tiene 0 elementos
            Assert.AreEqual(0, lista.Count,
                "Count no indica que una lista vacía tiene 0 elementos.");

            // Comprueba que una lista con 5 elementos indica que tiene 5 elementos
            lista = new List<int>(new int[] {1, 2, 3, 4, 5});
            Assert.AreEqual(5, lista.Count,
                "Count no indica que una lista con 5 elementos tiene 5 elementos.");
        }

        /// <summary>
        /// 3. Obtener y reescribir el elemento de la posición n-ésima
        /// </summary>
        [TestMethod]
        public void TestIndexing()
        {
            lista = new List<int>(new int[] {1, 2, 3});

            // Probamos a obtener y modificar la posición 0
            Assert.AreEqual(1, lista[0],
                "El valor devuelto de la posición 0 no es correcto.");
            lista[0] = 100;
            Assert.AreEqual(100, lista[0],
                "Modificar la posición 0 con [0] no funciona correctamente.");

            // Probamos a obtener y modificar la posición central
            Assert.AreEqual(2, lista[1],
                "El valor devuelto de la posición 1 no es correcto.");
            lista[1] = 200;
            Assert.AreEqual(200, lista[1],
                "Modificar la posición 1 con [1] no funciona correctamente.");

            // Probamos a obtener y modificar la última posición
            Assert.AreEqual(3, lista[lista.Count - 1],
                "El valor devuelto de la última posición no es correcto.");
            lista[lista.Count - 1] = 300;
            Assert.AreEqual(300, lista[lista.Count - 1],
                "Modificar la última con [lista.Count-1] no funciona correctamente.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestIndexingThrowsException()
        {
            lista = new List<int>();
            int val = lista[0];
        }

        /// <summary>
        /// 4. Saber si un elemento está o no en el vector
        /// </summary>
        [TestMethod]
        public void TestContains()
        {
            lista = new List<int>(new int[] {1, 2, 3});

            // Comprobamos si está el elemento en la posición 0
            Assert.IsTrue(lista.Contains(1),
                "Contains() indica que la lista no contiene el primer elemento de la lista.");

            // Comprobamos si está el elemento en la posición 1
            Assert.IsTrue(lista.Contains(2),
                "Contains() indica que la lista no contiene el segundo elemento de la lista.");

            // Comprobamos si está el elemento en la última posición
            Assert.IsTrue(lista.Contains(3),
                "Contains() indica que la lista no contiene el último elemento de la lista.");

            // Comprobamos que un elemento que no está en la lista devuelva false
            Assert.IsFalse(lista.Contains(4),
                "Contains() indica que la lista contiene un elemento que no está en la lista.");
        }

        /// <summary>
        /// 5. Conocer el primer índice de un elemento dado, en el caso que exista
        /// </summary>
        [TestMethod]
        public void TestIndexOf()
        {
            // La lista tiene el primer y segundo elementos repetidos, pero el último no
            lista = new List<int>(new int[] {1, 2, 1, 2, 3});

            // Comprobamos que funciona con el elemento en la posición 0 (que también está en la posición 2)
            Assert.AreEqual(0, lista.IndexOf(1),
                "IndexOf() no funciona correctamente con un elemento que está en la posición 0 y en la 2.");

            // Comprobamos que funciona con el elemento en la posición 1 (que también está en la posición 3)
            Assert.AreEqual(1, lista.IndexOf(2),
                "IndexOf() no funciona correctamente con un elemento que está en la posición 1 y en la 3.");

            // Comprobamos que funciona con el elemento en la última posición (que no está en ninguna otra posición)
            Assert.AreEqual(lista.Count - 1, lista.IndexOf(3),
                "IndexOf() no funciona correctamente con un elemento que está en la última posición y en ninguna otra.");

            // Comprobamos que retorna -1 con un elemento que no está en la lista
            Assert.AreEqual(-1, lista.IndexOf(4),
                "IndexOf() no retorna -1 con un elemento que no está en la lista.");
        }

        /// <summary>
        /// 6. Borrar la primera aparición de un elemento dado
        /// </summary>
        [TestMethod]
        public void TestRemove()
        {
            // La lista tiene el primer y segundo elementos repetidos, pero el último no
            lista = new List<int>(new int[] {1, 2, 1, 2, 3});

            // Comprobamos que funciona con el elemento en la posición 0 (que también está en la posición 2)
            Assert.IsTrue(lista.Remove(1),
                "Remove() no devuelve true con un elemento que está en la posición 0 y en la 2.");
            Assert.AreEqual(4, lista.Count,
                "Remove() no decrementa en 1 el número de elementos tras eliminar" +
                "un elemento que está en la posición 0 y en la 2.");
            Assert.IsTrue(lista[0] == 2 && lista[1] == 1 && lista[2] == 2 && lista[3] == 3,
                "Remove() no elimina correctamente un elemento que está en la posición 0 y en la 2.");

            // Comprobamos que funciona con el elemento en la posición 1 (que también está en la posición 3)
            lista = new List<int>(new int[] {1, 2, 1, 2, 3});
            Assert.IsTrue(lista.Remove(2),
                "Remove() no devuelve true con un elemento que está en la posición 1 y en la 3.");
            Assert.AreEqual(4, lista.Count,
                "Remove() no decrementa en 1 el número de elementos tras eliminar" +
                "un elemento que está en la posición 1 y en la 3.");
            Assert.IsTrue(lista[0] == 1 && lista[1] == 1 && lista[2] == 2 && lista[3] == 3,
                "Remove() no elimina correctamente un elemento que está en la posición 1 y en la 3.");

            // Comprobamos que funciona con el elemento en la última posición (que no está en ninguna otra posición)
            lista = new List<int>(new int[] {1, 2, 1, 2, 3});
            Assert.IsTrue(lista.Remove(3),
                "Remove() no devuelve true con un elemento que está en la última posición y en ninguna otra.");
            Assert.AreEqual(4, lista.Count,
                "Remove() no decrementa en 1 el número de elementos tras eliminar" +
                "un elemento que está en la última posición y en ninguna otra.");
            Assert.IsTrue(lista[0] == 1 && lista[1] == 2 && lista[2] == 1 && lista[3] == 2,
                "Remove() no elimina correctamente un elemento que está en la última posición y en ninguna otra.");

            // Comprobamos que retorna retorna false y no modifica la lista con un elemento que no está en la lista
            lista = new List<int>(new int[] {1, 2, 1, 2, 3});
            Assert.IsFalse(lista.Remove(4),
                "Remove() no devuelve false con un elemento que no está en la lista.");
            Assert.AreEqual(5, lista.Count,
                "Remove() decrementa en 1 el número de elementos tras tratar de eliminar " +
                "un elemento que no está en la lista.");
        }

        /// <summary>
        /// 7. Iteración mediante un foreach
        /// </summary>
        [TestMethod]
        public void TestIteratorForEach()
        {
            lista = new List<int>(new int[] {1, 2, 3});

            int expected = 1, forLoopExecutions = 0;
            foreach (var current in lista)
            {
                Assert.AreEqual(expected++, current,
                    "El elemento obtenido con el iterador usando un foreach no coincide con el esperado, " +
                    "después de hacer un Reset() del iterador.");
                forLoopExecutions++;
            }

            // Nos aseguramos de que se ha ejecutado el bucle las veces necesarias
            Assert.AreEqual(3, forLoopExecutions,
                "El iterador ejecutado con el foreach no ha recorrido todos los elementos de la lista.");

            // Comprobamos que al finalizar el foreach el metodo Reset() del iterador lo resetea correctamente
            forLoopExecutions = 0;
            foreach (var current in lista)
            {
                Assert.AreEqual(1, current,
                    "El metodo Reset() del iterador de la lista no funciona correctamente.");
                forLoopExecutions++;
                break;
            }

            // Nos aseguramos de que se ha ejecutado el bucle las veces necesarias
            Assert.AreEqual(1, forLoopExecutions,
                "El metodo Reset() del iterador de la lista no funciona correctamente. " +
                "No permite iterar de nuevo por la lista.");
        }
    }
}