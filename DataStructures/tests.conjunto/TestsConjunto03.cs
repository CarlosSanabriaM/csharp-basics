using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace conjunto
{
    
    /// <summary>
    /// Prueba que la sobrecarga de los operadores de la clase Conjunto funcione correctamente.
    /// </summary>
    [TestClass]
    public class TestsConjunto03
    {

        [TestMethod]
        public void TestConjuntoOperadorMas01()
        {
            Conjunto<String> conjuntoStrings = new Conjunto<String>("w", "o", "r", "l");
            Conjunto<String> conjuntoStrings2 = conjuntoStrings + "d";
            
            Assert.AreEqual(5, conjuntoStrings2.NumeroElementos,
                "El operador + del conjunto no incrementa el número de elementos en 1 al añadir un elemento no repetido.");
            Assert.AreEqual("{w, o, r, l, d}", conjuntoStrings2.ToString(), 
                "El operador + del conjunto no añade el nuevo elemento (no repetido) al final del conjunto.");
            // Comprobamos que conjuntoStrings no se ve modificado
            Assert.AreEqual(4, conjuntoStrings.NumeroElementos,
                "El operador + del conjunto modifica el conjunto sobre el que se aplica.");
            Assert.AreEqual("{w, o, r, l}", conjuntoStrings.ToString(), 
                "El operador + del conjunto modifica el conjunto sobre el que se aplica.");
            
            conjuntoStrings2 += "d";
            Assert.AreEqual(5, conjuntoStrings2.NumeroElementos,
                "El operador + del conjunto incrementa el número de elementos en 1 al añadir un elemento repetido.");
            Assert.AreEqual("{w, o, r, l, d}", conjuntoStrings2.ToString(), 
                "El operador + del conjunto añade un elemento repetido al final del conjunto.");
        }
        
        [TestMethod]
        public void TestConjuntoOperadorMas02()
        {
            Conjunto<String> conjuntoStrings = new Conjunto<String>("w", "o", "r", "l");
            Conjunto<String> conjuntoStrings2 = conjuntoStrings + new String[] {"d", "!", "!"};
            
            Assert.AreEqual(6, conjuntoStrings2.NumeroElementos,
                "El operador + del conjunto usado con un array no incrementa el número de elementos " +
                "del conjunto en el número de elementos del array que no se repiten en el conjunto.");
            Assert.AreEqual("{w, o, r, l, d, !}", conjuntoStrings2.ToString(), 
                "El operador + del conjunto usado con un array no añade los elementos correctamente al conjunto.");
            // Comprobamos que conjuntoStrings no se ve modificado
            Assert.AreEqual(4, conjuntoStrings.NumeroElementos,
                "El operador + del conjunto modifica el conjunto sobre el que se aplica.");
            Assert.AreEqual("{w, o, r, l}", conjuntoStrings.ToString(), 
                "El operador + del conjunto modifica el conjunto sobre el que se aplica.");
        }
        
        [TestMethod]
        public void TestConjuntoOperadorMenos01()
        {
            Conjunto<String> conjuntoStrings = new Conjunto<String>("w", "o", "r", "l");
            Conjunto<String> conjuntoStrings2 = conjuntoStrings - "l";
            
            Assert.AreEqual(3, conjuntoStrings2.NumeroElementos,
                "El operador - del conjunto con un elemento que está en el conjunto " +
                "no decrementa el número de elementos en 1.");
            Assert.AreEqual("{w, o, r}", conjuntoStrings2.ToString(), 
                "El operador - del conjunto con un elemento que está en el conjunto " +
                "no elimina dicho elemento.");
            // Comprobamos que conjuntoStrings no se ve modificado
            Assert.AreEqual(4, conjuntoStrings.NumeroElementos,
                "El operador - del conjunto modifica el conjunto sobre el que se aplica.");
            Assert.AreEqual("{w, o, r, l}", conjuntoStrings.ToString(), 
                "El operador - del conjunto modifica el conjunto sobre el que se aplica.");
            
            conjuntoStrings2 -= "l";
            Assert.AreEqual(3, conjuntoStrings2.NumeroElementos,
                "El operador - del conjunto con un elemento que no está en el conjunto " +
                "decrementa el número de elementos en 1.");
            Assert.AreEqual("{w, o, r}", conjuntoStrings2.ToString(), 
                "El operador - del conjunto con un elemento que no está en el conjunto modifica el conjunto.");
        }
        
        [TestMethod]
        public void TestConjuntoOperadorMenos02()
        {
            Conjunto<String> conjuntoStrings = new Conjunto<String>("w", "o", "r", "l");
            Conjunto<String> conjuntoStrings2 = conjuntoStrings - new String[] {"r", "l", "l"};
            
            Assert.AreEqual(2, conjuntoStrings2.NumeroElementos,
                "El operador - del conjunto usado con un array no decrementa el número de elementos " +
                "del conjunto en el número de elementos del array que no se repiten en el conjunto.");
            Assert.AreEqual("{w, o}", conjuntoStrings2.ToString(), 
                "El operador - del conjunto usado con un array no borra los elementos correctamente del conjunto.");
            // Comprobamos que conjuntoStrings no se ve modificado
            Assert.AreEqual(4, conjuntoStrings.NumeroElementos,
                "El operador - del conjunto modifica el conjunto sobre el que se aplica.");
            Assert.AreEqual("{w, o, r, l}", conjuntoStrings.ToString(), 
                "El operador - del conjunto modifica el conjunto sobre el que se aplica.");
        }

        [TestMethod]
        public void TestConjuntoOperadorCorchetes()
        {
            Conjunto<int> conjunto = new Conjunto<int>(1, 2, 3);

            // Probamos a retornar elementos en varias posiciones del conjunto
            Assert.AreEqual(1, conjunto[0],
                "La operación de obtener un elemento no lo retorna correctamente");
            Assert.AreEqual(3, conjunto.NumeroElementos,
                "Obtener un elemento de un conjunto con 3 elementos modifica el número de elementos.");

            Assert.AreEqual(2, conjunto[1],
                "La operación de obtener un elemento no lo retorna correctamente");
            Assert.AreEqual(3, conjunto.NumeroElementos,
                "Obtener un elemento de un conjunto con 3 elementos modifica el número de elementos.");

            Assert.AreEqual(3, conjunto[2],
                "La operación de obtener un elemento no lo retorna correctamente");
            Assert.AreEqual(3, conjunto.NumeroElementos,
                "Obtener un elemento de un conjunto con 3 elementos modifica el número de elementos.");
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestConjuntoGetThrowsException()
        {
            Conjunto<int> conjunto = new Conjunto<int>();
            int val = conjunto[0];
        }
        
        [TestMethod]
        public void TestConjuntoOperadorUnion()
        {
            Conjunto<int> conjunto1 = new Conjunto<int>(1, 2, 3);
            Conjunto<int> conjunto2 = new Conjunto<int>(2, 3, 4);
            Conjunto<int> union = conjunto1 | conjunto2;
            
            Assert.AreEqual(4, union.NumeroElementos,
                "El operador | del conjunto con otro conjunto no tiene el número de elementos " +
                "equivalente a los elementos únicos de cada conjunto.");
            Assert.AreEqual("{1, 2, 3, 4}", union.ToString(), 
                "El operador | del conjunto con otro conjunto no tiene los elementos únicos de cada conjunto.");
            
            // Comprobamos que los conjuntos iniciales no se ven modificados
            Assert.AreEqual(3, conjunto1.NumeroElementos,
                "El operador | modifica el primer conjunto.");
            Assert.AreEqual("{1, 2, 3}", conjunto1.ToString(), 
                "El operador | modifica el primer conjunto.");
            
            Assert.AreEqual(3, conjunto2.NumeroElementos,
                "El operador | modifica el segundo conjunto.");
            Assert.AreEqual("{2, 3, 4}", conjunto2.ToString(), 
                "El operador | modifica el segundo conjunto.");
        }
        
        [TestMethod]
        public void TestConjuntoOperadorInterseccion()
        {
            Conjunto<int> conjunto1 = new Conjunto<int>(1, 2, 3);
            Conjunto<int> conjunto2 = new Conjunto<int>(2, 3, 4);
            Conjunto<int> interseccion = conjunto1 & conjunto2;
            
            Assert.AreEqual(2, interseccion.NumeroElementos,
                "El operador & del conjunto con otro conjunto no tiene el número de elementos " +
                "equivalente a los elementos que aparecen en ambos conjuntos.");
            Assert.AreEqual("{2, 3}", interseccion.ToString(), 
                "El operador & del conjunto con otro conjunto no tiene los elementos que aparecen en ambos conjuntos.");
            
            // Comprobamos que los conjuntos iniciales no se ven modificados
            Assert.AreEqual(3, conjunto1.NumeroElementos,
                "El operador & modifica el primer conjunto.");
            Assert.AreEqual("{1, 2, 3}", conjunto1.ToString(), 
                "El operador & modifica el primer conjunto.");
            
            Assert.AreEqual(3, conjunto2.NumeroElementos,
                "El operador & modifica el segundo conjunto.");
            Assert.AreEqual("{2, 3, 4}", conjunto2.ToString(), 
                "El operador & modifica el segundo conjunto.");
        }
        
        [TestMethod]
        public void TestConjuntoOperadorDiferencia()
        {
            Conjunto<int> conjunto1 = new Conjunto<int>(1, 2, 3, 4, 5);
            Conjunto<int> conjunto2 = new Conjunto<int>(2, 3, 4, 6);
            Conjunto<int> interseccion = conjunto1 - conjunto2;
            
            Assert.AreEqual(2, interseccion.NumeroElementos,
                "El operador - del conjunto con otro conjunto no tiene el número de elementos " +
                "equivalente a los elementos del primer conjunto que no aparecen en el segundo.");
            Assert.AreEqual("{1, 5}", interseccion.ToString(), 
                "El operador - del conjunto con otro conjunto no tiene los elementos " +
                "del primer conjunto que no aparecen en el segundo.");
            
            // Comprobamos que los conjuntos iniciales no se ven modificados
            Assert.AreEqual(5, conjunto1.NumeroElementos,
                "El operador - modifica el primer conjunto.");
            Assert.AreEqual("{1, 2, 3, 4, 5}", conjunto1.ToString(), 
                "El operador - modifica el primer conjunto.");
            
            Assert.AreEqual(4, conjunto2.NumeroElementos,
                "El operador - modifica el segundo conjunto.");
            Assert.AreEqual("{2, 3, 4, 6}", conjunto2.ToString(), 
                "El operador - modifica el segundo conjunto.");
        }
        
        [TestMethod]
        public void TestConjuntoOperadorExistencia()
        {
            Conjunto<int> conjunto = new Conjunto<int>(1, 2);
            
            Assert.IsTrue(conjunto ^ 1,
                "El operador ^ no indica que un conjunto contiene un número, cuando sí lo contiene.");
            Assert.IsTrue(conjunto ^ 2,
                "El operador ^ no indica que un conjunto contiene un número, cuando sí lo contiene.");
            Assert.IsFalse(conjunto ^ 3,
                "El operador ^ indica que un conjunto contiene un número, cuando NO lo contiene.");
        }
        
    }
}
