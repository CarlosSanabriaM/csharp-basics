using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace lista
{
    /// <summary>
    /// Prueba que se pueda iterar correctamente por la lista.
    /// </summary>
    [TestClass]
    public class TestsLista03
    {
        private Lista<int> lista;

        [TestCleanup]
        public void CleanTests()
        {
            lista = null;
        }

        [TestMethod]
        public void TestIteradorAMano()
        {
            lista = new Lista<int>(1, 2, 3);

            IEnumerator<int> enumerator = lista.GetEnumerator();
            int expected = 1;
            while (enumerator.MoveNext())
            {
                int current = enumerator.Current;
                Assert.AreEqual(expected++, current,
                    "El elemento obtenido con el iterador manualmente no coincide con el esperado.");
            }

            // Nos aseguramos de que se ha ejecutado el bucle las veces necesarias
            Assert.AreEqual(4, expected,
                "El iterador ejecutado manualmente no ha recorrido todos los elementos de la lista.");
        }

        [TestMethod]
        public void TestIteradorForEach()
        {
            lista = new Lista<int>(1, 2, 3);

            int expected = 1;
            foreach (var current in lista)
            {
                Assert.AreEqual(expected++, current,
                    "El elemento obtenido con el iterador usdando un foreach no coincide con el esperado.");
            }

            // Nos aseguramos de que se ha ejecutado el bucle las veces necesarias
            Assert.AreEqual(4, expected,
                "El iterador ejecutado con el foreach no ha recorrido todos los elementos de la lista.");
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestCurrentAManoThrowsException()
        {
            lista = new Lista<int>(1, 2, 3);

            // Iteramos hasta el final de la lista
            IEnumerator<int> enumerator = lista.GetEnumerator();
            while (enumerator.MoveNext()) { }

            // Comprobamos que lanza una excepción, ya que currentNode es null
            // y Current llama a currentNode.Valor
            int current = enumerator.Current;
        }

        [TestMethod]
        public void TestResetAMano()
        {
            lista = new Lista<int>(1, 2, 3);

            // Iteramos hasta el final de la lista
            IEnumerator<int> enumerator = lista.GetEnumerator();
            while (enumerator.MoveNext()) { }

            // Hacemos un reset y comprobamos que al iterar de nuevo por toda la lista los elementos son los correctos
            enumerator.Reset();
            int expected = 1;
            while (enumerator.MoveNext())
            {
                int current = enumerator.Current;
                Assert.AreEqual(expected++, current,
                    "El elemento obtenido con el iterador manualmente no coincide con el esperado, " +
                    "después de hacer un Reset() del iterador.");
            }

            // Nos aseguramos de que se ha ejecutado el bucle las veces necesarias
            Assert.AreEqual(4, expected,
                "El iterador ejecutado manualmente no ha recorrido todos los elementos de la lista, " +
                "después de hacer un Reset() del iterador.");
        }

        [TestMethod]
        public void TestResetForEach()
        {
            lista = new Lista<int>(1, 2, 3);

            // Iteramos hasta el final de la lista
            foreach (var current in lista) { }
            // El foreach ya llama al método Reset() al final

            // Comprobamos que al iterar de nuevo por toda la lista los elementos son los correctos
            int expected = 1;
            foreach (var current in lista)
            {
                Assert.AreEqual(expected++, current,
                    "El elemento obtenido con el iterador usando un foreach no coincide con el esperado, " +
                    "después de hacer un Reset() del iterador.");
            }
            
            // Nos aseguramos de que se ha ejecutado el bucle las veces necesarias
            Assert.AreEqual(4, expected,
                "El iterador usando un foreach no ha recorrido todos los elementos de la lista, " +
                "después de hacer un Reset() del iterador.");
        }
    }
}