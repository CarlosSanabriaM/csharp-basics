using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TPP.Practicas.Utils;

namespace lista
{
    
    /// <summary>
    /// Prueba que la genericidad de la clase Lista funcione correctamente.
    /// </summary>
    [TestClass]
    public class TestsLista02
    {

        [TestMethod]
        public void TestListaStrings()
        {
            Lista<String> listaStrings = new Lista<String>("h", "e", "l", "l", "o");
            
            Assert.AreEqual(5, listaStrings.NumeroElementos,
                "El constructor de la lista funciona mal con Strings");
            Assert.AreEqual("[h, e, l, l, o]", listaStrings.ToString(), 
                "El constructor de la lista funciona mal con Strings.");
            
            listaStrings.AddLast("!");
            Assert.AreEqual(6, listaStrings.NumeroElementos,
                "El método AddLast() de la lista funciona mal con Strings");
            Assert.AreEqual("[h, e, l, l, o, !]", listaStrings.ToString(),
                "El método AddLast() de la lista funciona mal con Strings.");
            
            listaStrings.RemoveFirst();
            Assert.AreEqual(5, listaStrings.NumeroElementos,
                "El método RemoveFirst() de la lista funciona mal con Strings");
            Assert.AreEqual("[e, l, l, o, !]", listaStrings.ToString(),
                "El método RemoveFirst() de la lista funciona mal con Strings.");
            
            Assert.AreEqual("e", listaStrings.Get(0),
                "El método Get() de la lista funciona mal con Strings");

            Assert.AreEqual(true, listaStrings.Contains("l"),
                "El método Contains() de la lista funciona mal con Strings");
            Assert.AreEqual(false, listaStrings.Contains("k"),
                "El método Contains() de la lista funciona mal con Strings");
        }

        [TestMethod]
        public void TestListaPersonas()
        {
            Lista<Persona> listaStrings = new Lista<Persona>(
                new Persona("Carlos", "Sanabria", "12345678A"));
            
            Assert.AreEqual(1, listaStrings.NumeroElementos,
                "El constructor de la lista funciona mal con Personas");
            Assert.AreEqual("[Carlos Sanabria con NIF 12345678A]", listaStrings.ToString(), 
                "El constructor de la lista funciona mal con Personas.");

            listaStrings.AddLast(new Persona("Pedro", "Pérez", "12345678B"));
            Assert.AreEqual(2, listaStrings.NumeroElementos,
                "El método AddLast() de la lista funciona mal con Personas");
            Assert.AreEqual("[Carlos Sanabria con NIF 12345678A, " +
                            "Pedro Pérez con NIF 12345678B]", listaStrings.ToString(),
                "El método AddLast() de la lista funciona mal con Personas.");
            
            listaStrings.RemoveFirst();
            Assert.AreEqual(1, listaStrings.NumeroElementos,
                "El método RemoveFirst() de la lista funciona mal con Personas");
            Assert.AreEqual("[Pedro Pérez con NIF 12345678B]", listaStrings.ToString(),
                "El método RemoveFirst() de la lista funciona mal con Personas.");
            
            Assert.AreEqual("Pedro Pérez con NIF 12345678B", listaStrings.Get(0).ToString(),
                "El método Get() de la lista funciona mal con Personas");

            Assert.AreEqual(true, listaStrings.Contains(
                    new Persona("Pedro", "Pérez", "12345678B")),
                "El método Contains() de la lista funciona mal con Personas");
            Assert.AreEqual(false, listaStrings.Contains(
                    new Persona("Luis", "Pérez", "12345678B")),
                "El método Contains() de la lista funciona mal con Personas");
        }

        [TestMethod]
        public void TestListaDoubles()
        {
            Lista<double> listaStrings = new Lista<double>(1.1, 2.2, 3.3);
            
            Assert.AreEqual(3, listaStrings.NumeroElementos,
                "El constructor de la lista funciona mal con doubles.");
            Assert.AreEqual("[1,1, 2,2, 3,3]", listaStrings.ToString(), 
                "El constructor de la lista funciona mal con doubles.");

            listaStrings.AddLast(4.4);
            Assert.AreEqual(4, listaStrings.NumeroElementos,
                "El método AddLast() de la lista funciona mal con doubles.");
            Assert.AreEqual("[1,1, 2,2, 3,3, 4,4]", listaStrings.ToString(),
                "El método AddLast() de la lista funciona mal con doubles.");
            
            listaStrings.RemoveFirst();
            Assert.AreEqual(3, listaStrings.NumeroElementos,
                "El método RemoveFirst() de la lista funciona mal con doubles.");
            Assert.AreEqual("[2,2, 3,3, 4,4]", listaStrings.ToString(),
                "El método RemoveFirst() de la lista funciona mal con doubles.");
            
            Assert.AreEqual(2.2, listaStrings.Get(0),
                "El método Get() de la lista funciona mal con doubles.");

            Assert.AreEqual(true, listaStrings.Contains(3.3),
                "El método Contains() de la lista funciona mal con doubles.");
            Assert.AreEqual(false, listaStrings.Contains(5.5),
                "El método Contains() de la lista funciona mal con doubles.");
        }

    }
}
