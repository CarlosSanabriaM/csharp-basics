using System;
using System.Threading;

namespace master.worker
{
    /// <summary>
    /// Clase base abstracta para crear Masters.
    /// </summary>
    /// <typeparam name="TElemVector">Tipo de los elementos del vector</typeparam>
    /// <typeparam name="TResultadoWorker">Tipo del resultado devuelvo por cada Worker</typeparam>
    /// <typeparam name="TResultadoFinal">Tipo del resultado devuelvo por el Master, tras juntar los resultados de los Workers</typeparam>
    public abstract class Master<TElemVector, TResultadoWorker, TResultadoFinal> {

        protected TElemVector[] vector;

        private int numeroHilos;

        public Master(TElemVector[] vector, int numeroHilos) {
            if (numeroHilos < 1 || numeroHilos > vector.Length)
                throw new ArgumentException("El número de hilos ha de ser menor o igual que los elementos del vector.");
            this.vector = vector;
            this.numeroHilos = numeroHilos;
        }

        /// <summary>
        /// Template Method
        /// </summary>
        public TResultadoFinal Calcular() {
            var workers = new Worker<TElemVector, TResultadoWorker>[this.numeroHilos];
            int elementosPorHilo = this.vector.Length/numeroHilos;
            for(int i=0; i < this.numeroHilos; i++)
                workers[i] = CrearWorker(
                    i*elementosPorHilo, 
                    (i<this.numeroHilos-1) ? (i+1)*elementosPorHilo-1: this.vector.Length-1 // último
                );

            Thread[] hilos = new Thread[workers.Length];
            for(int i=0;i<workers.Length;i++) {
                hilos[i] = new Thread(workers[i].Calcular); 
                hilos[i].Name = "Worker Vector Módulo " + (i+1); 
                hilos[i].Priority = ThreadPriority.BelowNormal; 
                hilos[i].Start();  
            }

            // Esperamos a que todos los hilos terminen
            foreach (var hilo in hilos)
                hilo.Join();

            // Realizamos el cálculo final juntando los resultados de cada worker
            return JuntarResultadosWorkers(workers);
        }

        /// <summary>
        /// Factory Method
        /// </summary>
        protected abstract Worker<TElemVector, TResultadoWorker> CrearWorker(int índiceDesde, int índiceHasta);

        /// <summary>
        /// Junta los resultados de los Workers, para devolver el resultado final.
        /// </summary>
        protected abstract TResultadoFinal JuntarResultadosWorkers(Worker<TElemVector, TResultadoWorker>[] workers);

    }

}
