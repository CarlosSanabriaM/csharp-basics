using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TPP.Practicas.Utils;

namespace TPP.Practicas.Cola
{
    /// <summary>
    /// Prueba todas las operaciones de la clase Cola, con un solo hilo.
    /// </summary>
    [TestClass]
    public class TestsCola01
    {
        private ColaConcurrente<int> cola;

        [TestCleanup]
        public void CleanTests()
        {
            cola = null;
        }

        [TestMethod]
        public void TestConstructorColaVacia()
        {
            cola = new ColaConcurrente<int>();

            Assert.AreEqual(0, cola.NumeroElementos,
                "El constructor de cola vacía no crea una cola con 0 elementos.");
            Assert.IsTrue(cola.EstáVacía(),
                "El constructor de cola vacía no crea una cola vacía.");
            Assert.AreEqual("<-[]<-", cola.ToString(),
                "El toString() de una cola vacía no indica que está vacía.");
        }

        [TestMethod]
        public void TestColaConstructorUnSoloElemento()
        {
            cola = new ColaConcurrente<int>(1);

            Assert.AreEqual(1, cola.NumeroElementos,
                "El constructor de cola con un elemento no crea una cola con 1 elemento.");
            Assert.AreEqual("<-[1]<-", cola.ToString(),
                "El constructor de cola con un 1 como parámetro no crea una cola con un 1.");
        }

        [TestMethod]
        public void TestColaConstructorVariosEnteros()
        {
            cola = new ColaConcurrente<int>(1, 2, 3);

            Assert.AreEqual(3, cola.NumeroElementos,
                "El constructor de cola con varios elementos como parámetro no crea una cola con 3 elementos.");
            Assert.AreEqual("<-[1, 2, 3]<-", cola.ToString(),
                "El constructor de cola con varios elementos como parámetro no crea una cola con los números 1, 2 y 3.");
        }

        [TestMethod]
        public void TestColaConstructorArray()
        {
            cola = new ColaConcurrente<int>(new int[] {1, 2, 3});

            Assert.AreEqual(3, cola.NumeroElementos,
                "El constructor de cola con varios elementos como parámetro no crea una cola con 3 elementos.");
            Assert.AreEqual("<-[1, 2, 3]<-", cola.ToString(),
                "El constructor de cola con varios elementos como parámetro no crea una cola con los números 1, 2 y 3.");

            // Probamos con un array vacío
            cola = new ColaConcurrente<int>(new int[] { });

            Assert.AreEqual(0, cola.NumeroElementos,
                "El constructor de cola con un array vacío como parámetro no crea una cola con 0 elementos.");
            Assert.AreEqual("<-[]<-", cola.ToString(),
                "El constructor de cola con un array vacío como parámetro no crea una cola vacía.");
        }

        [TestMethod]
        public void TestColaConstructorCopia()
        {
            ColaConcurrente<int> colaOtro = new ColaConcurrente<int>(1, 2, 3);
            cola = new ColaConcurrente<int>(colaOtro);

            Assert.AreEqual(colaOtro.NumeroElementos, cola.NumeroElementos,
                "La cola creada con el constructor de copia no tiene el mismo número de elementos que la cola original.");
            Assert.AreEqual(colaOtro.ToString(), cola.ToString(),
                "La cola creada con el constructor de copia no tiene los mismos elementos que la cola original.");

            // Comprobamos que las dos colas son totalmente independientes (modificar una no modifica la otra)
            colaOtro.Añadir(4);
            cola.Extraer();

            Assert.AreEqual(4, colaOtro.NumeroElementos,
                "La cola creada con el constructor de copia no es independiente de la cola original.");
            Assert.AreEqual("<-[1, 2, 3, 4]<-", colaOtro.ToString(),
                "La cola creada con el constructor de copia no es independiente de la cola original.");
            Assert.AreEqual(2, cola.NumeroElementos,
                "La cola creada con el constructor de copia no es independiente de la cola original.");
            Assert.AreEqual("<-[2, 3]<-", cola.ToString(),
                "La cola creada con el constructor de copia no es independiente de la cola original.");
        }

        [TestMethod]
        public void TestColaNumeroElementos()
        {
            cola = new ColaConcurrente<int>();
            Assert.AreEqual(0, cola.NumeroElementos);

            cola = new ColaConcurrente<int>(1);
            Assert.AreEqual(1, cola.NumeroElementos);

            cola = new ColaConcurrente<int>(1, 2, 3);
            Assert.AreEqual(3, cola.NumeroElementos);
        }

        [TestMethod]
        public void TestColaPrimerElemento()
        {
            cola = new ColaConcurrente<int>(5);
            Assert.AreEqual(5, cola.PrimerElemento());

            cola = new ColaConcurrente<int>(5, 6, 7);
            Assert.AreEqual(5, cola.PrimerElemento());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestColaPrimerElementoThrowsException()
        {
            cola = new ColaConcurrente<int>();
            cola.PrimerElemento();
        }

        [TestMethod]
        public void TestColaEstáVacía()
        {
            cola = new ColaConcurrente<int>();
            Assert.IsTrue(cola.EstáVacía());

            cola = new ColaConcurrente<int>(1);
            Assert.IsFalse(cola.EstáVacía());

            cola = new ColaConcurrente<int>(1, 2, 3);
            Assert.IsFalse(cola.EstáVacía());
        }

        [TestMethod]
        public void TestColaAñadir()
        {
            cola = new ColaConcurrente<int>();

            cola.Añadir(5);
            Assert.AreEqual(1, cola.NumeroElementos,
                "Añadir un elemento en una cola vacía no incrementa el número de elementos a 1.");
            Assert.AreEqual("<-[5]<-", cola.ToString(),
                "Añadir() no añade el elemento correctamente");
            Assert.AreEqual(5, cola.PrimerElemento());

            cola.Añadir(6);
            Assert.AreEqual(2, cola.NumeroElementos,
                "Añadir un elemento a una cola con 1 elemento no incrementa el número de elementos a 2.");
            Assert.AreEqual("<-[5, 6]<-", cola.ToString(),
                "Añadir() no añade el elemento correctamente");
            Assert.AreEqual(5, cola.PrimerElemento());
        }

        [TestMethod]
        public void TestColaExtraer()
        {
            cola = new ColaConcurrente<int>(5, 6);

            var extraido = cola.Extraer();
            Assert.AreEqual(5, extraido,
                "El elemento extraido con Extraer() no es el esperado.");
            Assert.AreEqual(1, cola.NumeroElementos,
                "Extraer() no decrementa el número de elementos en 1.");
            Assert.AreEqual("<-[6]<-", cola.ToString(),
                "Extraer() no borra el primer elemento de la cola.");

            // Probamos a Extraer() cuando la cola solo tiene un elemento
            extraido = cola.Extraer();
            Assert.AreEqual(6, extraido,
                "El elemento extraido con Extraer() no es el esperado.");
            Assert.AreEqual(0, cola.NumeroElementos,
                "Extraer() no decrementa el número de elementos en 1.");
            Assert.IsTrue(cola.EstáVacía(),
                "Extraer() no deja la cola vacía.");
            Assert.AreEqual("<-[]<-", cola.ToString(),
                "Extraer() no borra el primer elemento de la cola.");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestColaExtraerThrowsException()
        {
            cola = new ColaConcurrente<int>();
            cola.Extraer();
        }

        [TestMethod]
        public void TestColaEquals()
        {
            ColaConcurrente<Persona> cola1 = new ColaConcurrente<Persona>(Factoria.CrearPersonas());
            ColaConcurrente<Persona> cola2 = new ColaConcurrente<Persona>(Factoria.CrearPersonas());
            ColaConcurrente<Persona> cola3 = cola1;
            ColaConcurrente<Persona> cola4 = new ColaConcurrente<Persona>(cola2);
            cola4.Extraer();

            // Comprobamos que indica que 2 variables que apuntan a la misma referencia cola son iguales.
            Assert.IsTrue(cola1.Equals(cola3), "El método Equals() no indica que dos colas iguales lo sean.");

            // Comprobamos que indica que 2 colas iguales lo son.
            Assert.IsTrue(cola1.Equals(cola2), "El método Equals() no indica que dos colas iguales lo sean.");

            // Comprobamos que indica que colas distintas no son iguales
            Assert.IsFalse(cola1.Equals(null),
                "El método Equals() indica que dos colas son iguales cuando se le pasa null.");
            Assert.IsFalse(cola1.Equals(new ColaConcurrente<int>()),
                "El método Equals() indica que dos colas son iguales cuando se le pasa una cola de otro tipo.");
            Assert.IsFalse(cola1.Equals(cola4),
                "El método Equals() indica que dos colas son iguales cuando las colas tienen distinto tamaño.");
            cola2.Extraer();
            cola2.Añadir(new Persona("X", "X", "X"));
            Assert.IsFalse(cola1.Equals(cola2),
                "El método Equals() indica que dos colas son iguales cuando tienen el mismo tamaño pero elementos distintos.");

            cola1.Extraer();
            cola1.Añadir(new Persona("X", "X", "X"));
            Assert.IsTrue(cola1.Equals(cola2),
                "El método Equals() indica que dos colas son distintas cuando tienen los mismos elementos.");
        }
    }
}