using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TPP.Practicas.Utils;

namespace lista
{
    /// <summary>
    /// Prueba todas las operaciones de la clase Lista.
    /// </summary>
    [TestClass]
    public class TestsLista01
    {
        private Lista<int> lista;

        [TestCleanup]
        public void CleanTests()
        {
            lista = null;
        }

        [TestMethod]
        public void TestConstructorListaVacia()
        {
            lista = new Lista<int>();

            Assert.AreEqual(0, lista.NumeroElementos,
                "El constructor de lista vacía no crea una lista con 0 elementos.");
            Assert.AreEqual("[]", lista.ToString(),
                "El constructor de lista vacía no crea una lista vacía.");
        }

        [TestMethod]
        public void TestListaConstructorUnSoloElemento()
        {
            lista = new Lista<int>(1);

            Assert.AreEqual(1, lista.NumeroElementos,
                "El constructor de lista con un elemento no crea una lista con 1 elemento.");
            Assert.AreEqual("[1]", lista.ToString(),
                "El constructor de lista con un 1 como parámetro no crea una lista con un 1.");
        }

        [TestMethod]
        public void TestListaConstructorVariosEnteros()
        {
            lista = new Lista<int>(1, 2, 3);

            Assert.AreEqual(3, lista.NumeroElementos,
                "El constructor de lista con varios elementos como parámetro no crea una lista con 3 elementos.");
            Assert.AreEqual("[1, 2, 3]", lista.ToString(),
                "El constructor de lista con varios elementos como parámetro no crea una lista con los números 1, 2 y 3.");
        }

        [TestMethod]
        public void TestListaConstructorArray()
        {
            lista = new Lista<int>(new int[] {1, 2, 3});

            Assert.AreEqual(3, lista.NumeroElementos,
                "El constructor de lista con varios elementos como parámetro no crea una lista con 3 elementos.");
            Assert.AreEqual("[1, 2, 3]", lista.ToString(),
                "El constructor de lista con varios elementos como parámetro no crea una lista con los números 1, 2 y 3.");

            // Probamos con un array vacío
            lista = new Lista<int>(new int[] { });

            Assert.AreEqual(0, lista.NumeroElementos,
                "El constructor de lista con un array vacío como parámetro no crea una lista con 0 elementos.");
            Assert.AreEqual("[]", lista.ToString(),
                "El constructor de lista con un array vacío como parámetro no crea una lista vacía.");
        }

        [TestMethod]
        public void TestListaConstructorCopia()
        {
            Lista<int> listaOtro = new Lista<int>(1, 2, 3);
            lista = new Lista<int>(listaOtro);

            Assert.AreEqual(listaOtro.NumeroElementos, lista.NumeroElementos,
                "La lista creada con el constructor de copia tiene el mismo número de elementos que la lista original.");
            Assert.AreEqual(listaOtro.ToString(), lista.ToString(),
                "La lista creada con el constructor de copia tiene los mismos elementos que la lista original.");

            // Comprobamos que las dos listas son totalmente independientes (modificar una no modifica la otra)
            listaOtro.AddLast(4);
            lista.RemoveFirst();

            Assert.AreEqual(4, listaOtro.NumeroElementos,
                "La lista creada con el constructor de copia no es independiente de la lista original.");
            Assert.AreEqual("[1, 2, 3, 4]", listaOtro.ToString(),
                "La lista creada con el constructor de copia no es independiente de la lista original.");
            Assert.AreEqual(2, lista.NumeroElementos,
                "La lista creada con el constructor de copia no es independiente de la lista original.");
            Assert.AreEqual("[2, 3]", lista.ToString(),
                "La lista creada con el constructor de copia no es independiente de la lista original.");
        }

        [TestMethod]
        public void TestListaAddLast()
        {
            lista = new Lista<int>();

            lista.AddLast(1);
            Assert.AreEqual(1, lista.NumeroElementos,
                "Añadir un elemento al final a una lista vacía no incrementa el número de elementos a 1.");
            Assert.AreEqual("[1]", lista.ToString(),
                "La operación de añadir al final un 1 no lo añade correctamente");

            lista.AddLast(2);
            Assert.AreEqual(2, lista.NumeroElementos,
                "Añadir un elemento al final a una lista con 1 elemento no incrementa el número de elementos a 2.");
            Assert.AreEqual("[1, 2]", lista.ToString(),
                "La operación de añadir al final un 2 no lo añade correctamente");
        }

        [TestMethod]
        public void TestListaAddFirst()
        {
            lista = new Lista<int>(1, 2);

            lista.AddFirst(0);
            Assert.AreEqual(3, lista.NumeroElementos,
                "Añadir un elemento al principio a una lista con 2 elementos no incrementa el número de elementos a 3.");
            Assert.AreEqual("[0, 1, 2]", lista.ToString(),
                "La operación de añadir al principio un 0 no lo añade correctamente");

            // Probamos a añadir al principio de una lista vacía
            lista = new Lista<int>();

            lista.AddFirst(0);
            Assert.AreEqual(1, lista.NumeroElementos,
                "Añadir un elemento al principio a una lista vacía no incrementa el número de elementos a 1.");
            Assert.AreEqual("[0]", lista.ToString(),
                "Añadir un elemento al principio a una lista vacía no añade dicho elemento a la lista.");
        }

        [TestMethod]
        public void TestListaAdd()
        {
            lista = new Lista<int>(0, 1, 2);

            lista.Add(1, 4);
            Assert.AreEqual(4, lista.NumeroElementos,
                "Añadir un elemento en medio a una lista con 3 elementos no incrementa el número de elementos a 4.");
            Assert.AreEqual("[0, 4, 1, 2]", lista.ToString(),
                "La operación de añadir un 4 en la posición 1 no lo añade correctamente");

            // Probamos a añadir en la posición 0 (desplaza el resto de elementos a la dcha)
            lista.Add(0, -1);
            Assert.AreEqual(5, lista.NumeroElementos,
                "Añadir un elemento usando Add() al principio de una lista no incrementa el número de elementos en 1.");
            Assert.AreEqual("[-1, 0, 4, 1, 2]", lista.ToString(),
                "Añadir un elemento usando Add() al principio de una lista no añade dicho elemento al principio.");

            // Probamos a añadir en la última posición (desplaza el resto de elementos a la dcha,
            // por lo que el elemento añadido no es el último, sino el penúltimo).
            lista.Add(lista.NumeroElementos - 1, 9);
            Assert.AreEqual(6, lista.NumeroElementos,
                "Añadir un elemento usando Add() en la última posición de una lista no incrementa el número de elementos en 1.");
            Assert.AreEqual("[-1, 0, 4, 1, 9, 2]", lista.ToString(),
                "Añadir un elemento usando Add() en la última posición de una lista no añade dicho elemento como el penúltimo.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestListaAddThrowsException()
        {
            lista = new Lista<int>();
            // Como no hay elementos en la lista, la posición 0 realmente no existe todavía
            lista.Add(0, 2);
        }

        [TestMethod]
        public void TestListaRemoveAt()
        {
            lista = new Lista<int>(1, 2, 3, 4);

            lista.RemoveAt(1);
            Assert.AreEqual(3, lista.NumeroElementos,
                "Borrar un elemento del medio de una lista de 4 elementos no decrementa el número de elementos a 3.");
            Assert.AreEqual("[1, 3, 4]", lista.ToString(),
                "La operación de borrar el elemento en la posición 1 no lo borra correctamente");

            // Probamos a eliminar la posición 0
            lista.RemoveAt(0);
            Assert.AreEqual(2, lista.NumeroElementos,
                "Borrar usando RemoveAt() el elemento en la posición 0 de una lista no decrementa el número de elementos en 1.");
            Assert.AreEqual("[3, 4]", lista.ToString(),
                "Borrar usando RemoveAt() el elemento en la posición 0 de una lista no elimina el primer elemento.");

            // Probamos a eliminar la última posición
            lista.RemoveAt(lista.NumeroElementos - 1);
            Assert.AreEqual(1, lista.NumeroElementos,
                "Borrar usando RemoveAt() el elemento en la última posición de una lista no decrementa el número de elementos en 1.");
            Assert.AreEqual("[3]", lista.ToString(),
                "Borrar usando RemoveAt() el elemento en la última posición de una lista no elimina el último elemento.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestListaRemoveAtThrowsException01()
        {
            lista = new Lista<int>();
            lista.RemoveAt(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestListaRemoveAtThrowsException02()
        {
            lista = new Lista<int>(1);
            lista.RemoveAt(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestListaRemoveAtThrowsException03()
        {
            lista = new Lista<int>(1);
            lista.RemoveAt(1);
        }

        [TestMethod]
        public void TestListaRemoveFirst()
        {
            lista = new Lista<int>(1, 2);

            lista.RemoveFirst();
            Assert.AreEqual(1, lista.NumeroElementos,
                "Borrar un elemento al principio de una lista no decrementa el número de elementos en 1.");
            Assert.AreEqual("[2]", lista.ToString(),
                "La operación de borrar el elemento al principio no lo borra correctamente");

            // Probamos a borrar el primer elemento cuando la lista solo tiene un elemento
            lista.RemoveFirst();
            Assert.AreEqual(0, lista.NumeroElementos,
                "Borrar un elemento al principio de una lista con un único elemento no decrementa el número de elementos a 0.");
            Assert.AreEqual("[]", lista.ToString(),
                "Borrar un elemento al principio de una lista con un único elemento no deja la lista vacía.");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestListaRemoveFirstThrowsException()
        {
            lista = new Lista<int>();
            lista.RemoveFirst();
        }

        [TestMethod]
        public void TestListaRemoveLast()
        {
            lista = new Lista<int>(3, 4);

            lista.RemoveLast();
            Assert.AreEqual(1, lista.NumeroElementos,
                "Borrar un elemento al final de una lista no decrementa el número de elementos en 1.");
            Assert.AreEqual("[3]", lista.ToString(),
                "La operación de borrar el elemento al final no lo borra correctamente");

            // Probamos a borrar el último elemento cuando la lista solo tiene un elemento
            lista.RemoveLast();
            Assert.AreEqual(0, lista.NumeroElementos,
                "Borrar un elemento al final de una lista con un único elemento no decrementa el número de elementos a 0.");
            Assert.AreEqual("[]", lista.ToString(),
                "Borrar un elemento al final de una lista con un único elemento no deja la lista vacía.");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestListaRemoveLastThrowsException()
        {
            lista = new Lista<int>();
            lista.RemoveLast();
        }

        [TestMethod]
        public void TestListaRemoveValue()
        {
            lista = new Lista<int>(1, 3, 4);

            bool wasRemoved = lista.RemoveValue(3);
            Assert.AreEqual(2, lista.NumeroElementos,
                "Borrar usando RemoveValue() un elemento que está en la lista no decrementa en 1 el número de elementos.");
            Assert.AreEqual("[1, 4]", lista.ToString(),
                "Borrar usando RemoveValue() un elemento que está en la lista no lo borra correctamente.");
            Assert.IsTrue(wasRemoved,
                "Borrar usando RemoveValue() un elemento que está en la lista no retorna true.");

            wasRemoved = lista.RemoveValue(3);
            Assert.AreEqual(2, lista.NumeroElementos,
                "Borrar usando RemoveValue() un elemento que NO está en la lista modifica el número de elementos.");
            Assert.AreEqual("[1, 4]", lista.ToString(),
                "Borrar usando RemoveValue() un elemento que NO está en la lista modifica la lista.");
            Assert.IsFalse(wasRemoved,
                "Borrar usando RemoveValue() un elemento que NO está en la lista no retorna false.");

            // Intentamos borrar el último elemento, para probar si funciona bien en los extremos
            wasRemoved = lista.RemoveValue(4);
            Assert.AreEqual(1, lista.NumeroElementos,
                "Borrar usando RemoveValue() el último elemento no decrementa el número de elementos en 1.");
            Assert.AreEqual("[1]", lista.ToString(),
                "Borrar usando RemoveValue() el último elemento no elimina dicho elemento de la lista.");
            Assert.IsTrue(wasRemoved,
                "Borrar usando RemoveValue() el último elemento no retorna true.");
            
            // Probamos con una lista que tiene nulls para ver que no da problemas
            Persona p = new Persona("C", "S", "1");
            Lista<Persona> listaConNulls = new Lista<Persona>(null, p, null);
            wasRemoved = listaConNulls.RemoveValue(p);
            
            Assert.AreEqual(2, listaConNulls.NumeroElementos,
                "Borrar usando RemoveValue() un elemento que está en la lista no decrementa en 1 el número de elementos.");
            Assert.AreEqual("[, ]", listaConNulls.ToString(),
                "Borrar usando RemoveValue() un elemento que está en la lista no lo borra correctamente.");
            Assert.IsTrue(wasRemoved,
                "Borrar usando RemoveValue() un elemento que está en la lista no retorna true.");
            
            wasRemoved = listaConNulls.RemoveValue(null);
            Assert.AreEqual(1, listaConNulls.NumeroElementos,
                "Borrar usando RemoveValue() un elemento que está en la lista no decrementa en 1 el número de elementos.");
            Assert.AreEqual("[]", listaConNulls.ToString(),
                "Borrar usando RemoveValue() un elemento que está en la lista no lo borra correctamente.");
            Assert.IsTrue(wasRemoved,
                "Borrar usando RemoveValue() un elemento que está en la lista no retorna true.");
        }

        [TestMethod]
        public void TestListaGet()
        {
            lista = new Lista<int>(1, 2, 3);

            // Probamos a retornar elementos en varias posiciones de la lista
            Assert.AreEqual(1, lista.Get(0),
                "La operación de obtener un elemento no lo retorna correctamente");
            Assert.AreEqual(3, lista.NumeroElementos,
                "Obtener un elemento de una lista con 3 elementos modifica el número de elementos.");

            Assert.AreEqual(2, lista.Get(1),
                "La operación de obtener un elemento no lo retorna correctamente");
            Assert.AreEqual(3, lista.NumeroElementos,
                "Obtener un elemento de una lista con 3 elementos modifica el número de elementos.");

            Assert.AreEqual(3, lista.Get(2),
                "La operación de obtener un elemento no lo retorna correctamente");
            Assert.AreEqual(3, lista.NumeroElementos,
                "Obtener un elemento de una lista con 3 elementos modifica el número de elementos.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestListaGetThrowsException01()
        {
            lista = new Lista<int>();
            lista.Get(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestListaGetThrowsException02()
        {
            lista = new Lista<int>(1);
            lista.Get(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestListaGetThrowsException03()
        {
            lista = new Lista<int>(1);
            lista.Get(1);
        }

        [TestMethod]
        public void TestListaSet()
        {
            lista = new Lista<int>(0, 1, 2);

            lista.Set(0, 9);
            Assert.AreEqual(3, lista.NumeroElementos,
                "Hacer un Set() incrementa el número de elementos.");
            Assert.AreEqual("[9, 1, 2]", lista.ToString(),
                "Hacer un Set() no modifica correctamente la lista.");

            lista.Set(1, 8);
            Assert.AreEqual(3, lista.NumeroElementos,
                "Hacer un Set() incrementa el número de elementos.");
            Assert.AreEqual("[9, 8, 2]", lista.ToString(),
                "Hacer un Set() no modifica correctamente la lista.");

            lista.Set(2, 7);
            Assert.AreEqual(3, lista.NumeroElementos,
                "Hacer un Set() incrementa el número de elementos.");
            Assert.AreEqual("[9, 8, 7]", lista.ToString(),
                "Hacer un Set() no modifica correctamente la lista.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestListaSetThrowsException01()
        {
            lista = new Lista<int>();
            lista.Set(0, 9);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestListaSetThrowsException02()
        {
            lista = new Lista<int>(1);
            lista.Set(-1, 9);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestListaSetThrowsException03()
        {
            lista = new Lista<int>(1);
            lista.Set(1, 9);
        }

        [TestMethod]
        public void TestListaContains()
        {
            lista = new Lista<int>();
            Assert.IsFalse(lista.Contains(1),
                "El método Contains() indica que una lista vacía contiene el número 1.");

            lista = new Lista<int>(1, 2, 3);
            Assert.IsTrue(lista.Contains(1),
                "El método Contains() no indica que una lista contiene un número, cuando sí lo contiene.");
            Assert.IsTrue(lista.Contains(2),
                "El método Contains() no indica que una lista contiene un número, cuando sí lo contiene.");
            Assert.IsTrue(lista.Contains(3),
                "El método Contains() no indica que una lista contiene un número, cuando sí lo contiene.");
            Assert.IsFalse(lista.Contains(4),
                "El método Contains() indica que una lista contiene un número, cuando no lo contiene.");
            
            // Probamos con una lista que tiene nulls para ver que no da problemas
            Persona p = new Persona("C", "S", "1");
            Lista<Persona> listaConNulls = new Lista<Persona>(null, p, null);
            Assert.IsTrue(listaConNulls.Contains(p),
                "El método Contains() no indica que una lista contiene una persona, cuando sí la contiene.");
            Assert.IsTrue(listaConNulls.Contains(null),
                "El método Contains() no indica que una lista contiene null, cuando sí lo contiene.");
        }

        [TestMethod]
        public void TestListaEquals()
        {
            Lista<Persona> lista1 = new Lista<Persona>(Factoria.CrearPersonas());
            Lista<Persona> lista2 = new Lista<Persona>(Factoria.CrearPersonas());
            Lista<Persona> lista3 = lista1;
            Lista<Persona> lista4 = new Lista<Persona>(lista2);
            lista4.RemoveLast();

            // Comprobamos que indica que 2 variables que apuntan a la misma referencia lista son iguales.
            Assert.IsTrue(lista1.Equals(lista3), "El método Equals() no indica que dos listas iguales lo sean.");

            // Comprobamos que indica que 2 listas iguales lo son.
            Assert.IsTrue(lista1.Equals(lista2), "El método Equals() no indica que dos listas iguales lo sean.");

            // Comprobamos que indica que listas distintas no son iguales
            Assert.IsFalse(lista1.Equals(null),
                "El método Equals() indica que dos listas son iguales cuando se le pasa null.");
            Assert.IsFalse(lista1.Equals(new Lista<int>()),
                "El método Equals() indica que dos listas son iguales cuando se le pasa una lista de otro tipo.");
            Assert.IsFalse(lista1.Equals(lista4),
                "El método Equals() indica que dos listas son iguales cuando las listas tienen distinto tamaño.");
            lista2.Set(0, null);
            Assert.IsFalse(lista1.Equals(lista2),
                "El método Equals() indica que dos listas son iguales cuando tienen el mismo tamaño pero elementos distintos.");

            lista1.Set(0, null);
            Assert.IsTrue(lista1.Equals(lista2),
                "El método Equals() indica que dos listas son distintas cuando tienen los mismos elementos.");
        }
    }
}