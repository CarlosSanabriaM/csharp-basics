using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPP.Practicas.Utils
{
    public class Angulo
    {
        public double Radianes { get; private set; }

        public int Cuadrante
        {
            get
            {
                if (Seno() >= 0 && Coseno() >= 0)
                    return 1;
                if (Seno() >= 0 && Coseno() <= 0.0001)
                    return 2;
                if (Seno() <= 0.0001 && Coseno() <= 0.0001)
                    return 3;
                else 
                    return 4;
            }
        }

        public float Grados
        {
            get { return (float)(this.Radianes / Math.PI * 180); }
        }

        public Angulo(double radianes)
        {
            this.Radianes = radianes;
        }

        public Angulo(float grados)
        {
            this.Radianes = grados / 180.0 * Math.PI;
        }

        public double Seno()
        {
            return Math.Sin(this.Radianes);
        }

        public double Coseno()
        {
            return Math.Cos(this.Radianes);
        }

        public double Tangente()
        {
            return Seno() / Coseno();
        }


        public override string ToString()
        {
            return Grados + " grados";
        }

        public override bool Equals(object obj)
        {
            Angulo otro = obj as Angulo;
            if (otro == null)
                return false;

            return this.Radianes.Equals(otro.Radianes);
        }
    }
	
}
