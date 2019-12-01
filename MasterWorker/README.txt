Se incluye:
-MasterWorker => Carpeta que contiene la solución con el código C#
-3 ficheros CSV con los resultados de medir el context switching en 3 master-workers distintos que usan los datos de Bitcoin:
	-resultadosBitcoinNumValoresPorEncima.csv
	-resultadosBitcoinMediaValores.csv
	-resultadosBitcoinMaxValor
-Fichero excel Resultados.xlsx, que contiene las gráficas para los 3 csv anteriores

La solución C# contiene los siguientes proyectos:
-master.worker => Contiene 2 clases bases abstractas (Master y Worker), que usan patrones de diseño y genericidad para facilitar la creación de nuevos master-worker, que se crearán como subclases de esas clases base en otros proyectos.
-modulo.vector => Contiene un Master y un Worker concretos (que implementan las clases base abstractas) para calcular, como en clase de laboratorio, el módulo de un vector.
-bitcoin => Contiene 3 implementaciones de los Master-workers, y una clase ContextSwitching que permite, usando genericidad y programación funcional, medir el context switching de cualquier master-worker. También contiene un Main en el que se permite ejecutar diversos master-workers o calcular el context switching de uno de ellos (lo especifica el usuario por consola).
-tests.bitcoin => Contiene los tests para los 3 master-workers de Bitcoin.