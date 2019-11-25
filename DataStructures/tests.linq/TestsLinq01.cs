using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TPP.Practicas.Lista;
using TPP.Practicas.Utils;

namespace TPP.Practicas.Linq
{
    /// <summary>
    /// Prueba los métodos de Linq
    /// </summary>
    [TestClass]
    public class TestsLinq01
    {
        private string messageElementInCollection =
            "El método Buscar() no retorna la primera aparición del elemento que cumpla el criterio dado.";

        private string messageElementNotInCollection =
            "El método Buscar() no retorna default(T) al pasarle un criterio que no lo cumple ningún elemento de la colección.";

        #region MetodosAuxiliares

        /// <summary>
        /// Método auxiliar que comprueba si Buscar() encuentra el elemento esperado.
        /// </summary>
        private void AssertFirst<T>(Lista<T> elementos, Predicate<T> predicate, T expectedElemento,
                                    string message)
        {
            var predicateAsFunc = new Func<T, bool>(predicate);
            // Usamos FirstOrDefault para que nos devuelva null y no tire una excepción.
            var primerElemento = elementos.FirstOrDefault(predicateAsFunc);
            Assert.AreEqual(expectedElemento, primerElemento, message);
        }

        /// <summary>
        /// Método auxiliar que comprueba si Filtrar() devuelve la colección esperada.
        /// </summary>
        private void AssertWhere<T>(Lista<T> elementos, Predicate<T> predicate, int[] expectedIndicesElementos)
        {
            Lista<T> copiaElementos = new Lista<T>(elementos);
            var predicateAsFunc = new Func<T, bool>(predicate);
            var elementosFiltrados = elementos.Where(predicateAsFunc);

            // Comprobamos que el numero de elementos de la colección filtrada es el esperado
            Assert.AreEqual(expectedIndicesElementos.Length, elementosFiltrados.Count(),
                "El método Filtrar() no retorna un IEnumerable con el número de elementos esperado.");

            // Comprobamos que los elementos filtrados son las esperados
            int i = 0;
            foreach (var elementoFiltrado in elementosFiltrados)
            {
                var elementoEsperado = elementos.Get(expectedIndicesElementos[i++]);
                Assert.AreEqual(elementoEsperado, elementoFiltrado,
                    "El método Filtrar() no retorna los elementos esperados");
            }

            // Comprobamos que no se modifica la lista original
            Assert.AreEqual(copiaElementos, elementos,
                "El método Filtrar() modifica el IEnumerable original.");
        }

        /// <summary>
        /// Método auxiliar que genera un array de indices desde un número hasta otro.
        /// </summary>
        private int[] GetArrayIndices(int from, int to)
        {
            int numIndices = to - from + 1;
            int[] arrayIndices = new int[numIndices];
            for (int i = 0, j = from; i < numIndices; i++, j++)
                arrayIndices[i] = j;
            return arrayIndices;
        }

        /// <summary>
        /// Método auxiliar que comprueba si 2 diccionarios son iguales.
        /// </summary>
        private bool EqualDictionarys<TKey, TValue>(IDictionary<TKey, TValue> d1, IDictionary<TKey, TValue> d2)
        {
            return d1.Count == d2.Count && !d1.Except(d2).Any();
        }

        #endregion

        /// <summary>
        /// Pruébese obteniendo la lista de los cuadrantes de los ángulos.
        /// </summary>
        [TestMethod]
        public void TestSelectAngulos()
        {
            Lista<Angulo> angulos = new Lista<Angulo>(Factoria.CrearAngulos());
            Lista<Angulo> copiaAngulos = new Lista<Angulo>(angulos);

            var cuadrantesAngulos = angulos.Select(angulo => angulo.Cuadrante);

            // Comprobamos que los elementos son los esperados
            Assert.AreEqual(angulos.NumeroElementos, cuadrantesAngulos.Count(),
                "El método Select() modifica el numero de elementos del IEnumerable.");

            int i = 0;
            foreach (var cuadrante in cuadrantesAngulos)
            {
                int expectedCuadrante;
                if (i <= 90) expectedCuadrante = 1;
                else if (i <= 180) expectedCuadrante = 2;
                else if (i <= 270) expectedCuadrante = 3;
                else expectedCuadrante = 4;

                Assert.AreEqual(expectedCuadrante, cuadrante,
                    "El método Select() no retorna los elementos esperados");

                i++;
            }

            // Comprobamos que no se modifica la lista original
            Assert.AreEqual(copiaAngulos, angulos,
                "El método Select() modifica el IEnumerable original.");
        }

        /// <summary>
        /// Pruébese obteniendo los "apellidos, nombre" (como un único string) de cada una de las personas de la lista.
        /// </summary>
        [TestMethod]
        public void TestSelectPersonas()
        {
            Lista<Persona> personas = new Lista<Persona>(Factoria.CrearPersonas());
            Lista<Persona> copiaPersonas = new Lista<Persona>(personas);

            var apellidosNombres = personas.Select(persona => persona.Apellido1 + ", " + persona.Nombre);

            // Comprobamos que los elementos son los esperados
            Assert.AreEqual(personas.NumeroElementos, apellidosNombres.Count(),
                "El método Select() modifica el numero de elementos del IEnumerable.");

            var enumerator = personas.GetEnumerator();
            foreach (var apellidosNombre in apellidosNombres)
            {
                enumerator.MoveNext();
                var persona = enumerator.Current;
                Assert.AreEqual(persona.Apellido1 + ", " + persona.Nombre, apellidosNombre,
                    "El método Select() no retorna los elementos esperados");
            }

            // Comprobamos que no se modifica la lista original
            Assert.AreEqual(copiaPersonas, personas,
                "El método Select() modifica el IEnumerable original.");
        }

        /// <summary>
        /// Pruebe la búsqueda de personas por nombre y aquellas cuyo nif termina en una letra dada.
        /// </summary>
        [TestMethod]
        public void TestFirstPersonas()
        {
            Lista<Persona> personas = new Lista<Persona>(Factoria.CrearPersonas());

            #region BusquedaPersonasNombre

            /* Búsqueda de personas por nombre */
            // Buscamos la primera persona cuyo nombre sea María
            AssertFirst(personas, persona => persona.Nombre == "María", personas.Get(0), messageElementInCollection);
            // Buscamos la primera persona cuyo nombre sea Juan
            AssertFirst(personas, persona => persona.Nombre == "Juan", personas.Get(1), messageElementInCollection);
            // Buscamos la primera persona cuyo nombre sea Miguel
            AssertFirst(personas, persona => persona.Nombre == "Miguel", personas.Get(5), messageElementInCollection);
            // Buscamos una persona que no está en la lista y comprobamos que devuelve default(Persona) = null
            AssertFirst(personas, persona => persona.Nombre == "Ricardo", null, messageElementNotInCollection);

            #endregion

            #region BusquedaPersonasLetraDni

            /* Búsqueda de personas cuyo nif termina en una letra dada */
            // Buscamos la primera persona cuyo nif termine en la letra A
            AssertFirst(personas, persona => persona.Nif[persona.Nif.Length - 1] == 'A',
                personas.Get(0), messageElementInCollection);
            // Buscamos la primera persona cuyo nif termine en la letra F
            AssertFirst(personas, persona => persona.Nif[persona.Nif.Length - 1] == 'F',
                personas.Get(1), messageElementInCollection);
            // Buscamos la primera persona cuyo nif termine en la letra T
            AssertFirst(personas, persona => persona.Nif[persona.Nif.Length - 1] == 'T',
                personas.Get(5), messageElementInCollection);
            // Buscamos una persona que no está en la lista y comprobamos que devuelve default(Persona) = null
            AssertFirst(personas, persona => persona.Nif[persona.Nif.Length - 1] == 'X',
                null, messageElementNotInCollection);

            #endregion
        }

        /// <summary>
        /// Pruebe la búsqueda de ángulos rectos y en un cuadrante.
        /// </summary>
        [TestMethod]
        public void TestFirstAngulos()
        {
            Lista<Angulo> angulos = new Lista<Angulo>(Factoria.CrearAngulos());

            #region BusquedaAngulosRectos

            /* Búsqueda de ángulos rectos */
            // Buscamos el primer ángulo de 90 grados
            AssertFirst(angulos, angulo => Math.Abs(Math.Abs(angulo.Grados) - 90.0) <= 0.001,
                angulos.Get(90), messageElementInCollection);

            #endregion

            #region BusquedaAngulosCuadrantes

            /* Búsqueda de ángulos en un cuadrante */
            // Buscamos el primer ángulo del primer cuadrante
            AssertFirst(angulos, angulo => angulo.Cuadrante == 1,
                angulos.Get(0), messageElementInCollection);
            // Buscamos el primer ángulo del segundo cuadrante
            AssertFirst(angulos, angulo => angulo.Cuadrante == 2,
                angulos.Get(91), messageElementInCollection);
            // Buscamos el primer ángulo del tercer cuadrante
            AssertFirst(angulos, angulo => angulo.Cuadrante == 3,
                angulos.Get(181), messageElementInCollection);
            // Buscamos el primer ángulo del cuarto cuadrante
            AssertFirst(angulos, angulo => angulo.Cuadrante == 4,
                angulos.Get(271), messageElementInCollection);

            #endregion
        }

        /// <summary>
        /// Pruebe el filtrado de personas por nombre y aquellas cuyo nif termina en una letra dada.
        /// </summary>
        [TestMethod]
        public void TestWherePersonas()
        {
            Lista<Persona> personas = new Lista<Persona>(Factoria.CrearPersonas());

            #region FiltradoPersonasNombre

            /* Filtrado de personas por nombre */
            // Filtramos personas cuyo nombre sea María
            AssertWhere(personas, persona => persona.Nombre == "María", new[] {0, 7});
            // Filtramos personas cuyo nombre sea Juan
            AssertWhere(personas, persona => persona.Nombre == "Juan", new[] {1, 8});
            // Filtramos personas cuyo nombre sea Miguel
            AssertWhere(personas, persona => persona.Nombre == "Miguel", new[] {5});
            // Filtramos personas cuyo nombre sea uno que no está en la lista y comprobamos que devuelve un IEnumerable sin elementos
            AssertWhere(personas, persona => persona.Nombre == "Ricardo", new int[] { });

            #endregion

            #region FiltradoPersonasLetraDni

            /* Filtrado de personas cuyo nif termina en una letra dada */
            // Filtramos personas cuyo nif termine en la letra A
            AssertWhere(personas, persona => persona.Nif[persona.Nif.Length - 1] == 'A', new[] {0, 3});
            // Filtramos personas cuyo nif termine en la letra F
            AssertWhere(personas, persona => persona.Nif[persona.Nif.Length - 1] == 'F', new[] {1, 6});
            // Filtramos personas cuyo nif termine en la letra T
            AssertWhere(personas, persona => persona.Nif[persona.Nif.Length - 1] == 'T', new[] {5});
            // Filtramos personas cuya letra del nif sea una que no está en la lista y comprobamos que devuelve un IEnumerable sin elementos
            AssertWhere(personas, persona => persona.Nif[persona.Nif.Length - 1] == 'X', new int[] { });

            #endregion
        }

        /// <summary>
        /// Pruebe el filtrado de ángulos rectos y en un cuadrante.
        /// </summary>
        [TestMethod]
        public void TestWhereAngulos()
        {
            Lista<Angulo> angulos = new Lista<Angulo>(Factoria.CrearAngulos());

            #region FiltradoAngulosRectos

            /* Filtrado de ángulos rectos */
            // Filtramos los ángulos de 90 grados
            AssertWhere(angulos, angulo => Math.Abs(Math.Abs(angulo.Grados) - 90.0) <= 0.001, new[] {90});

            #endregion

            #region FiltradoAngulosCuadrantes

            /* Filtrado de ángulos de cada cuadrante */
            // Filtramos los ángulos del primer cuadrante
            AssertWhere(angulos, angulo => angulo.Cuadrante == 1, GetArrayIndices(0, 90));

            // Filtramos los ángulos del segundo cuadrante
            AssertWhere(angulos, angulo => angulo.Cuadrante == 2, GetArrayIndices(91, 180));

            // Filtramos los ángulos del tercer cuadrante
            AssertWhere(angulos, angulo => angulo.Cuadrante == 3, GetArrayIndices(181, 270));

            // Filtramos los ángulos del cuarto cuadrante
            AssertWhere(angulos, angulo => angulo.Cuadrante == 4, GetArrayIndices(271, 360));

            #endregion
        }

        /// <summary>
        /// Pruébese para calcular la suma de todos los grados de los
        /// ángulos de la colección y para calcular el seno máximo.
        /// </summary>
        [TestMethod]
        public void TestAggregateAngulos()
        {
            Lista<Angulo> angulos = new Lista<Angulo>(Factoria.CrearAngulos());

            #region SumaGradosAngulos

            var sumaGrados =
                angulos.Aggregate(0.0, (acumulado, angulo) => acumulado + angulo.Grados);
            var expectedSumaGrados =
                (float) GetArrayIndices(0, 360).Aggregate((acumulado, val) => acumulado + val);
            Assert.AreEqual(expectedSumaGrados, sumaGrados, "El método Aggregate() no funciona correctamente.");

            #endregion

            #region SenoMaximoAngulos

            var senoMaximo = angulos.Aggregate(0.0, (senoMax, angulo) => Math.Max(senoMax, angulo.Seno()));
            var expectedSenoMaximo = 1.0;
            Assert.AreEqual(expectedSenoMaximo, senoMaximo, "El método Aggregate() no funciona correctamente.");

            #endregion
        }

        /// <summary>
        /// Pruébese para conocer la distribución de personas por nombre
        /// (esto es, decir que hay 10 personas con nombre "María", 3 con nombre "Pedro"...)
        /// </summary>
        [TestMethod]
        public void TestAggregatePersonas()
        {
            Lista<Persona> personas = new Lista<Persona>(Factoria.CrearPersonas());

            var distribucionPersonasNombre = personas.Aggregate(new Dictionary<string, int>(),
                (dict, persona) =>
                {
                    if (dict.ContainsKey(persona.Nombre))
                        dict[persona.Nombre]++;
                    else
                        dict[persona.Nombre] = 1;
                    return dict;
                });

            var expectedDistribucion = new Dictionary<string, int>
            {
                {"María", 2},
                {"Juan", 2},
                {"Pepe", 1},
                {"Luis", 1},
                {"Carlos", 1},
                {"Miguel", 1},
                {"Cristina", 1}
            };
            Assert.IsTrue(EqualDictionarys(expectedDistribucion, distribucionPersonasNombre),
                "El método Aggregate() no funciona correctamente.");
        }
    }
}