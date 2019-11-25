using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace pila
{
    /// <summary>
    /// Prueba todas las operaciones de la clase Pila.
    /// </summary>
    [TestClass]
    public class TestsPila01
    {
        private Pila<int> pila;

        [TestCleanup]
        public void CleanTests()
        {
            pila = null;
        }

        [TestMethod]
        public void TestPilaPush()
        {
            pila = new Pila<int>(3);
            pila.Push(1);
            pila.Push(2);
            pila.Push(3);

            Assert.AreEqual("Stack([1, 2, 3])", pila.ToString(),
                "La operación Push() no añade los elementos correctamente.");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestPilaPushThrowsException()
        {
            pila = new Pila<int>(3);
            pila.Push(1);
            pila.Push(2);
            pila.Push(3);
            pila.Push(4); // Exception
        }

        [TestMethod]
        public void TestPilaPop()
        {
            pila = new Pila<int>(3);
            pila.Push(1);
            pila.Push(2);
            pila.Push(3);

            Assert.AreEqual(3, pila.Pop(),
                "La operación Pop() no retorna el elemento en la cima de la pila.");
            Assert.AreEqual("Stack([1, 2])", pila.ToString(),
                "La operación Pop() no elimina el elemento en la cima de la pila.");

            Assert.AreEqual(2, pila.Pop(),
                "La operación Pop() no retorna el elemento en la cima de la pila.");
            Assert.AreEqual("Stack([1])", pila.ToString(),
                "La operación Pop() no elimina el elemento en la cima de la pila.");

            Assert.AreEqual(1, pila.Pop(),
                "La operación Pop() no retorna el elemento en la cima de la pila.");
            Assert.AreEqual("Stack([])", pila.ToString(),
                "La operación Pop() no elimina el elemento en la cima de la pila.");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestPilaPopThrowsException()
        {
            pila = new Pila<int>(3);
            pila.Pop();
        }

    }
}
