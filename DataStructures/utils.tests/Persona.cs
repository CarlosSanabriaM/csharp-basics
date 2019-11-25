using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPP.Practicas.Utils
{
    public class Persona
    {
        public String Nombre { get; }

        public String Apellido1 { get; }

        public string Nif { get; }

        public override String ToString()
        {
            return String.Format("{0} {1} con NIF {2}", Nombre, Apellido1, Nif);
        }

        public Persona(String nombre, String apellido1, string nif)
        {
            this.Nombre = nombre;
            this.Apellido1 = apellido1;
            this.Nif = nif;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj == this)
                return true;

            Persona otro = obj as Persona;
            if (otro == null)
                return false;

            return otro.Nombre == Nombre
                   && otro.Apellido1 == Apellido1
                   && otro.Nif == Nif;
        }

        /// <summary>
        /// We override Equals(Persona) following the advice of Microsoft:
        /// 'It is also recommended that in addition to implementing Equals (object),
        /// any class also implement Equals (type) for their own type, to enhance performance.'
        /// </summary>
        /// <param name="otro"></param>
        /// <returns></returns>
        public bool Equals(Persona otro)
        {
            if (otro == null)
                return false;

            return otro.Nombre == Nombre
                   && otro.Apellido1 == Apellido1
                   && otro.Nif == Nif;
        }

        public override int GetHashCode()
        {
            // If your overridden Equals method returns true when two objects are tested for equality,
            // your overridden GetHashCode method must return the same value for the two objects.
            return (int) Nombre[0]
                   + (int) Apellido1[0]
                   + (int) Nif[0];
        }
    }
}