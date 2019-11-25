using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace conjunto
{
    
    /// <summary>
    /// Prueba todas las operaciones de la clase Conjunto.
    /// </summary>
    [TestClass]
    public class TestsConjunto01
    {

        private Conjunto<int> conjunto;

        [TestCleanup]
        public void CleanTests()
        {
            conjunto = null;
        }
        
        [TestMethod]
        public void TestConjuntoConstructorConjuntoVacio()
        {
            conjunto = new Conjunto<int>();
            
            Assert.AreEqual(0, conjunto.NumeroElementos,
                "El constructor de conjunto vacío no crea una conjunto con 0 elementos.");
            Assert.AreEqual("{}", conjunto.ToString(),
                "El constructor de conjunto vacío no crea una conjunto vacío.");
        }

        [TestMethod]
        public void TestConjuntoConstructorUnSoloElemento()
        {
            conjunto = new Conjunto<int>(1);
            
            Assert.AreEqual(1, conjunto.NumeroElementos,
                "El constructor de conjunto con un elemento no crea un conjunto con 1 elemento.");
            Assert.AreEqual("{1}", conjunto.ToString(),
                "El constructor de conjunto con un 1 como parámetro no crea un conjunto con un 1.");
        }

        [TestMethod]
        public void TestConjuntoConstructorVariosEnteros()
        {
            conjunto = new Conjunto<int>(1, 2, 1, 3);
            
            Assert.AreEqual(3, conjunto.NumeroElementos,
                "El constructor de conjunto con 4 elementos como parámetro (uno de ellos repetido) no crea una conjunto con 3 elementos.");
            Assert.AreEqual("{1, 2, 3}", conjunto.ToString(),
                "El constructor de conjunto con varios elementos como parámetro no crea un conjunto con los números 1, 2 y 3.");
        }

        [TestMethod]
        public void TestConjuntoConstructorArray()
        {
            conjunto = new Conjunto<int>(new int[] { 1, 2, 1, 3 });
            
            Assert.AreEqual(3, conjunto.NumeroElementos,
                "El constructor de conjunto con 4 elementos como parámetro (uno de ellos repetido) no crea una conjunto con 3 elementos.");
            Assert.AreEqual("{1, 2, 3}", conjunto.ToString(),
                "El constructor de conjunto con varios elementos como parámetro no crea un conjunto con los números 1, 2 y 3.");
        }
        
        [TestMethod]
        public void TestConjuntoConstructorCopia()
        {
            Conjunto<int> conjuntoOtro = new Conjunto<int>(1, 2, 3); 
            conjunto = new Conjunto<int>(conjuntoOtro);
            
            Assert.AreEqual(conjuntoOtro.NumeroElementos, conjunto.NumeroElementos,
                "El conjunto creado con el constructor de copia tiene el mismo número de elementos que el conjunto original.");
            Assert.AreEqual(conjuntoOtro.ToString(), conjunto.ToString(),
                "El conjunto creado con el constructor de copia tiene los mismos elementos que el conjunto original.");
            
            // Comprobamos que los dos conjuntos son totalmente independientes (modificar uno no modifica el otro)
            conjuntoOtro.AddLast(4);
            conjunto.RemoveFirst();
            
            Assert.AreEqual(4, conjuntoOtro.NumeroElementos,
                "El conjunto creado con el constructor de copia no es independiente del conjunto original.");
            Assert.AreEqual("{1, 2, 3, 4}", conjuntoOtro.ToString(),
                "El conjunto creado con el constructor de copia no es independiente del conjunto original.");
            Assert.AreEqual(2, conjunto.NumeroElementos,
                "El conjunto creado con el constructor de copia no es independiente del conjunto original.");
            Assert.AreEqual("{2, 3}", conjunto.ToString(),
                "El conjunto creado con el constructor de copia no es independiente del conjunto original.");
        }

        [TestMethod]
        public void TestConjuntoAddLast()
        {
            conjunto = new Conjunto<int>();

            conjunto.AddLast(1);
            Assert.AreEqual(1, conjunto.NumeroElementos, 
                "Añadir un elemento al final a un conjunto vacío no incrementa el número de elementos a 1.");
            Assert.AreEqual("{1}", conjunto.ToString(), 
                "La operación de añadir al final un 1 no lo añade correctamente");
            
            conjunto.AddLast(2);
            Assert.AreEqual(2, conjunto.NumeroElementos, 
                "Añadir un elemento (no repetido) al final a un conjunto con 1 elemento no incrementa el número de elementos a 2.");
            Assert.AreEqual("{1, 2}", conjunto.ToString(), 
                "Añadir un elemento (no repetido) al final a un conjunto con 1 elemento no lo añade correctamente.");    
            
            conjunto.AddLast(2);
            Assert.AreEqual(2, conjunto.NumeroElementos, 
                "Añadir un elemento (repetido) al final a un conjunto incrementa el número de elementos.");
            Assert.AreEqual("{1, 2}", conjunto.ToString(), 
                "Añadir un elemento (repetido) al final a un conjunto lo añade.");
        }
        
        [TestMethod]
        public void TestConjuntoAddFirst()
        {
            conjunto = new Conjunto<int>(1, 2);

            conjunto.AddFirst(0);
            Assert.AreEqual(3, conjunto.NumeroElementos, 
                "Añadir un elemento (no repetido) al principio a un conjunto con 2 elementos no incrementa el número de elementos a 3.");
            Assert.AreEqual("{0, 1, 2}", conjunto.ToString(), 
                "La operación de añadir al principio un elemento no repetido no lo añade correctamente.");
            
            conjunto.AddFirst(2);
            Assert.AreEqual(3, conjunto.NumeroElementos, 
                "Añadir un elemento (repetido) al principio a un conjunto incrementa el número de elementos.");
            Assert.AreEqual("{0, 1, 2}", conjunto.ToString(), 
                "La operación de añadir al principio un elemento repetido lo añade.");
        }

        [TestMethod]
        public void TestConjuntoAdd()
        {
            conjunto = new Conjunto<int>(0, 1, 2);
            
            conjunto.Add(1, 4);
            Assert.AreEqual(4, conjunto.NumeroElementos,
                "Añadir un elemento (no repetido) en medio a un conjunto con 3 elementos no incrementa el número de elementos a 4.");
            Assert.AreEqual("{0, 4, 1, 2}", conjunto.ToString(),
                "La operación de añadir un 4 (no repetido) en la posición 1 no lo añade correctamente");
            
            conjunto.Add(1, 1);
            Assert.AreEqual(4, conjunto.NumeroElementos,
                "Añadir un elemento (repetido) en medio a un conjunto con 4 elementos incrementa el número de elementos a 5.");
            Assert.AreEqual("{0, 4, 1, 2}", conjunto.ToString(),
                "La operación de añadir un 1 (repetido) en la posición 1 lo añade.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestConjuntoAddThrowsException01()
        {
            conjunto = new Conjunto<int>();            
            conjunto.Add(0, 2);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestConjuntoAddThrowsException02()
        {
            conjunto = new Conjunto<int>(1);            
            conjunto.Add(-1, 2);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestConjuntoAddThrowsException03()
        {
            conjunto = new Conjunto<int>(1);            
            conjunto.Add(1, 2);
        }

        [TestMethod]
        public void TestConjuntoRemoveAt()
        {
            conjunto = new Conjunto<int>(1, 2, 3, 4);

            conjunto.RemoveAt(1);
            Assert.AreEqual(3, conjunto.NumeroElementos,
                "Borrar un elemento del medio de un conjunto de 4 elementos no decrementa el número de elementos a 3.");
            Assert.AreEqual("{1, 3, 4}", conjunto.ToString(),
                "La operación de borrar el elemento en la posición 1 no lo borra correctamente");
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestConjuntoRemoveAtThrowsException01()
        {
            conjunto = new Conjunto<int>();            
            conjunto.RemoveAt(0);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestConjuntoRemoveAtThrowsException02()
        {
            conjunto = new Conjunto<int>(1);            
            conjunto.RemoveAt(-1);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestConjuntoRemoveAtThrowsException03()
        {
            conjunto = new Conjunto<int>(1);            
            conjunto.RemoveAt(1);
        }
      
        [TestMethod]
        public void TestConjuntoRemoveFirst()
        {
            conjunto = new Conjunto<int>(1, 3, 4);
            
            conjunto.RemoveFirst();
            Assert.AreEqual(2, conjunto.NumeroElementos,
                "Borrar un elemento al principio de un conjunto de 3 elementos no decrementa el número de elementos a 2.");
            Assert.AreEqual("{3, 4}", conjunto.ToString(),
                "La operación de borrar el elemento al principio no lo borra correctamente");
        }
        
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestConjuntoRemoveFirstThrowsException()
        {
            conjunto = new Conjunto<int>();            
            conjunto.RemoveFirst();
        }

        [TestMethod]
        public void TestConjuntoRemoveLast()
        {
            conjunto = new Conjunto<int>(3, 4);
            
            conjunto.RemoveLast();
            Assert.AreEqual(1, conjunto.NumeroElementos,
                "Borrar un elemento al final de un conjunto de 2 elementos no decrementa el número de elementos a 1.");
            Assert.AreEqual("{3}", conjunto.ToString(),
                "La operación de borrar el elemento al final no lo borra correctamente");
        }
        
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestConjuntoRemoveLastThrowsException()
        {
            conjunto = new Conjunto<int>();            
            conjunto.RemoveLast();
        }
        
        [TestMethod]
        public void TestConjuntoRemoveValue()
        {
            conjunto = new Conjunto<int>(1, 3, 4);

            bool wasRemoved = conjunto.RemoveValue(3);
            Assert.AreEqual(2, conjunto.NumeroElementos,
                "Borrar usando RemoveValue() un elemento que está en el conjunto no decrementa en 1 el número de elementos.");
            Assert.AreEqual(true, wasRemoved,
                "Borrar usando RemoveValue() un elemento que está en el conjunto no retorna true.");
            Assert.AreEqual("{1, 4}", conjunto.ToString(),
                "Borrar usando RemoveValue() un elemento que está en el conjunto no lo borra correctamente.");
            
            wasRemoved = conjunto.RemoveValue(3);
            Assert.AreEqual(2, conjunto.NumeroElementos,
                "Borrar usando RemoveValue() un elemento que NO está en el conjunto modifica el número de elementos.");
            Assert.AreEqual(false, wasRemoved,
                "Borrar usando RemoveValue() un elemento que NO está en el conjunto no retorna false.");
            Assert.AreEqual("{1, 4}", conjunto.ToString(),
                "Borrar usando RemoveValue() un elemento que NO está en el conjunto modifica el conjunto.");
        }

        [TestMethod]
        public void TestConjuntoGet()
        {
            conjunto = new Conjunto<int>(1, 2, 3);

            // Probamos a retornar elementos en varias posiciones de el conjunto
            Assert.AreEqual(1, conjunto.Get(0),
                "La operación de obtener un elemento no lo retorna correctamente");
            Assert.AreEqual(3, conjunto.NumeroElementos,
                "Obtener un elemento de un conjunto con 3 elementos modifica el número de elementos.");

            Assert.AreEqual(2, conjunto.Get(1),
                "La operación de obtener un elemento no lo retorna correctamente");
            Assert.AreEqual(3, conjunto.NumeroElementos,
                "Obtener un elemento de un conjunto con 3 elementos modifica el número de elementos.");

            Assert.AreEqual(3, conjunto.Get(2),
                "La operación de obtener un elemento no lo retorna correctamente");
            Assert.AreEqual(3, conjunto.NumeroElementos,
                "Obtener un elemento de un conjunto con 3 elementos modifica el número de elementos.");
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestConjuntoGetThrowsException01()
        {
            conjunto = new Conjunto<int>();
            conjunto.Get(0);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestConjuntoGetThrowsException02()
        {
            conjunto = new Conjunto<int>(1);            
            conjunto.Get(-1);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestConjuntoGetThrowsException03()
        {
            conjunto = new Conjunto<int>(1);            
            conjunto.Get(1);
        }
        
        [TestMethod]
        public void TestConjuntoContains()
        {
            conjunto = new Conjunto<int>();
            Assert.AreEqual(false, conjunto.Contains(1),
                "El método Contains() indica que un conjunto vacío contiene el número 1.");
            
            conjunto = new Conjunto<int>(1,2,3);
            Assert.AreEqual(true, conjunto.Contains(1),
                "El método Contains() no indica que un conjunto contiene un número, cuando sí lo contiene.");
            Assert.AreEqual(true, conjunto.Contains(2),
                "El método Contains() no indica que un conjunto contiene un número, cuando sí lo contiene.");
            Assert.AreEqual(true, conjunto.Contains(3),
                "El método Contains() no indica que un conjunto contiene un número, cuando sí lo contiene.");
            Assert.AreEqual(false, conjunto.Contains(4),
                "El método Contains() indica que un conjunto contiene un número, cuando no lo contiene.");
        }
        
    }
}
