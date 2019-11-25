using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using utils;

namespace conjunto
{
    
    /// <summary>
    /// Prueba que la genericidad de la clase Conjunto funcione correctamente.
    /// </summary>
    [TestClass]
    public class TestsConjunto02
    {

        [TestMethod]
        public void TestConjuntoStrings()
        {
            Conjunto<String> conjuntoStrings = new Conjunto<String>("h", "e", "l", "l", "o");
            
            Assert.AreEqual(4, conjuntoStrings.NumeroElementos,
                "El constructor del conjunto funciona mal con Strings");
            Assert.AreEqual("{h, e, l, o}", conjuntoStrings.ToString(), 
                "El constructor del conjunto funciona mal con Strings.");
            
            conjuntoStrings.AddLast("!");
            Assert.AreEqual(5, conjuntoStrings.NumeroElementos,
                "El método AddLast() del conjunto funciona mal con Strings");
            Assert.AreEqual("{h, e, l, o, !}", conjuntoStrings.ToString(),
                "El método AddLast() del conjunto funciona mal con Strings.");
            
            conjuntoStrings.RemoveFirst();
            Assert.AreEqual(4, conjuntoStrings.NumeroElementos,
                "El método RemoveFirst() del conjunto funciona mal con Strings");
            Assert.AreEqual("{e, l, o, !}", conjuntoStrings.ToString(),
                "El método RemoveFirst() del conjunto funciona mal con Strings.");
            
            Assert.AreEqual("e", conjuntoStrings.Get(0),
                "El método Get() del conjunto funciona mal con Strings");

            Assert.AreEqual(true, conjuntoStrings.Contains("l"),
                "El método Contains() del conjunto funciona mal con Strings");
            Assert.AreEqual(false, conjuntoStrings.Contains("k"),
                "El método Contains() del conjunto funciona mal con Strings");
        }

        [TestMethod]
        public void TestConjuntoPersonas()
        {
            Conjunto<Persona> conjuntoStrings = new Conjunto<Persona>(
                new Persona("Carlos", "Sanabria", "Miranda", "12345678A"));
            
            Assert.AreEqual(1, conjuntoStrings.NumeroElementos,
                "El constructor del conjunto funciona mal con Personas");
            Assert.AreEqual("{Carlos Sanabria Miranda con NIF 12345678A}", conjuntoStrings.ToString(), 
                "El constructor del conjunto funciona mal con Personas.");

            conjuntoStrings.AddLast(new Persona("Pedro", "Pérez", "Allende", "12345678B"));
            Assert.AreEqual(2, conjuntoStrings.NumeroElementos,
                "El método AddLast() del conjunto funciona mal con Personas");
            Assert.AreEqual("{Carlos Sanabria Miranda con NIF 12345678A, " +
                            "Pedro Pérez Allende con NIF 12345678B}", conjuntoStrings.ToString(),
                "El método AddLast() del conjunto funciona mal con Personas.");
            
            conjuntoStrings.RemoveFirst();
            Assert.AreEqual(1, conjuntoStrings.NumeroElementos,
                "El método RemoveFirst() del conjunto funciona mal con Personas");
            Assert.AreEqual("{Pedro Pérez Allende con NIF 12345678B}", conjuntoStrings.ToString(),
                "El método RemoveFirst() del conjunto funciona mal con Personas.");
            
            Assert.AreEqual("Pedro Pérez Allende con NIF 12345678B", conjuntoStrings.Get(0).ToString(),
                "El método Get() del conjunto funciona mal con Personas");

            Assert.AreEqual(true, conjuntoStrings.Contains(
                    new Persona("Pedro", "Pérez", "Allende", "12345678B")),
                "El método Contains() del conjunto funciona mal con Personas");
            Assert.AreEqual(false, conjuntoStrings.Contains(
                    new Persona("Luis", "Pérez", "Allende", "12345678B")),
                "El método Contains() del conjunto funciona mal con Personas");
        }

        [TestMethod]
        public void TestConjuntoDoubles()
        {
            Conjunto<double> conjuntoStrings = new Conjunto<double>(1.1, 2.2, 3.3);
            
            Assert.AreEqual(3, conjuntoStrings.NumeroElementos,
                "El constructor del conjunto funciona mal con doubles.");
            Assert.AreEqual("{1,1, 2,2, 3,3}", conjuntoStrings.ToString(), 
                "El constructor del conjunto funciona mal con doubles.");

            conjuntoStrings.AddLast(4.4);
            Assert.AreEqual(4, conjuntoStrings.NumeroElementos,
                "El método AddLast() del conjunto funciona mal con doubles.");
            Assert.AreEqual("{1,1, 2,2, 3,3, 4,4}", conjuntoStrings.ToString(),
                "El método AddLast() del conjunto funciona mal con doubles.");
            
            conjuntoStrings.RemoveFirst();
            Assert.AreEqual(3, conjuntoStrings.NumeroElementos,
                "El método RemoveFirst() del conjunto funciona mal con doubles.");
            Assert.AreEqual("{2,2, 3,3, 4,4}", conjuntoStrings.ToString(),
                "El método RemoveFirst() del conjunto funciona mal con doubles.");
            
            Assert.AreEqual(2.2, conjuntoStrings.Get(0),
                "El método Get() del conjunto funciona mal con doubles.");

            Assert.AreEqual(true, conjuntoStrings.Contains(3.3),
                "El método Contains() del conjunto funciona mal con doubles.");
            Assert.AreEqual(false, conjuntoStrings.Contains(5.5),
                "El método Contains() del conjunto funciona mal con doubles.");
        }

    }
}
