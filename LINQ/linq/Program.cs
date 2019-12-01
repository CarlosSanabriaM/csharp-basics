using System;
using System.Collections.Generic;
using System.Linq;

namespace linq
{
    public class Program
    {
        private Model modelo = new Model();

        static void Main(string[] args)
        {
            Program consultas = new Program();
            //Consultas
            consultas.Query1();
            consultas.Query2();
            consultas.Query3();
            consultas.Query4();
            consultas.Query5();
            consultas.Query6();
            consultas.Query7();
            consultas.Query8();
            consultas.Query9();
            consultas.Query10();

            Console.ReadLine();
        }


        // Nombre de los empleados mayores de 60 años
        private void Query1()
        {
            Console.WriteLine("\nQuery1\n");
            var nombreEmpleadosMayores60 = modelo.Employees
                                                 .Where(empleado => empleado.Age > 60)
                                                 .Select(empleado => empleado.Name);
            Show(nombreEmpleadosMayores60);

            // Show(modelo.Employees.Where(empleado => empleado.Age > 60).Select(empleado => empleado.Name));
        }

        // Nombre e email de los empleados que trabajan en Asturias
        private void Query2()
        {
            Console.WriteLine("\nQuery2\n");
            var nombreEmailEmpleadosAsturias = modelo.Employees
                                                     .Where(empleado => empleado.Province.ToLower().Equals("asturias"))
                                                     .Select(empleado =>
                                                         new {Nombre = empleado.Name, Correo = empleado.Email});
            Show(nombreEmailEmpleadosAsturias);
        }

        // Nombre de los departamentos con más de un empleado mayor de 18 años.
        // El departamento también debe tener alguna oficina con número que comienza con "2.1".
        private void Query3()
        {
            Console.WriteLine("\nQuery3\n");
            // Mejor poner varios wheres que poner un where con una condición que sea un AND.
            // Es más facil de paralelizar, como las pipes de SO.
            var res = modelo.Departments
                            .Where(d => d.Employees.Count(e => e.Age > 18) > 1)
                            .Where(d => d.Employees.Any(e => e.Office.Number.StartsWith("2.1")))
                            .Select(d => d.Name);
            Show(res);
        }

        // Mostrar las llamadas telefónicas realizadas por cada empleado.
        // Cada línea (de consola) debe mostrar el nombre del empleado y la duración de la llamada en segundos.
        private void Query4()
        {
            Console.WriteLine("\nQuery4\n");

            #region Forma1

            Console.WriteLine("Forma 1");
            // Esta forma es compleja y tiene mal rendimiento
            var res1 = modelo.PhoneCalls
                             .Where(call => modelo.Employees.Any(e => e.TelephoneNumber.Equals(call.SourceNumber)))
                             .Select(call => new
                             {
                                 Nombre = modelo.Employees.First(e => e.TelephoneNumber.Equals(call.SourceNumber)).Name,
                                 Duracion = call.Seconds
                             });
            Show(res1);

            #endregion

            #region Forma2

            Console.WriteLine("Forma 2");
            var res2 = modelo.PhoneCalls
                             .Join(modelo.Employees,
                                 call => call.SourceNumber,
                                 emp => emp.TelephoneNumber,
                                 (call, emp) => new {Nombre = emp.Name, Duracion = call.Seconds});
            Show(res2);

            #endregion

            #region Forma3

            Console.WriteLine("Forma 3");
            var res3 = from call in modelo.PhoneCalls
                       from emp in modelo.Employees
                       where call.SourceNumber.Equals(emp.TelephoneNumber)
                       select new {Nombre = emp.Name, Duracion = call.Seconds};
            Show(res3);

            #endregion
        }

        // Mostrar, agrupados por provincia, el nombre de los empleados
        // Provincias en orden alfabético
        // Empleados en orden alfabético
        private void Query5()
        {
            Console.WriteLine("\nQuery5\n");
            var res = modelo.Employees
                            .GroupBy(
                                emp => emp.Province,
                                emp => emp.Name,
                                (prov, names) => new
                                {
                                    Provincia = prov,
                                    Nombres = names.OrderBy(name => name)
                                }
                            )
                            .OrderBy(group => group.Provincia);

            foreach (var group in res)
            foreach (var nombre in group.Nombres)
                Console.WriteLine("Provincia: {0}, Nombre: {1}", group.Provincia, nombre);
        }

        // Mostrar, ordenados por edad, los nombres de los empleados pertenecientes al
        // departamento de “Computer Science” que tienen un despacho en la “Faculty of Science”
        // y que han hecho llamadas con duración superior a 1 minuto.
        private void Query6()
        {
            Console.WriteLine("\nQuery6\n");
            var res = modelo.Employees
                            .Where(emp => emp.Department.Name.ToLower().Equals("computer science"))
                            .Where(emp => emp.Office.Building.ToLower().Equals("faculty of science"))
                            .Where(emp => modelo.PhoneCalls.Any(call =>
                                call.SourceNumber.Equals(emp.TelephoneNumber) &&
                                call.Seconds > 60))
                            .OrderBy(emp => emp.Age)
                            .Select(emp => emp.Name);
            Show(res);
        }

        // Mostrar la suma en segundos de las llamadas hechas por
        // los empleados del departamento de “Computer Science”
        private void Query7()
        {
            Console.WriteLine("\nQuery7\n");
            var res = modelo.Employees
                            .Where(emp => emp.Department.Name.ToLower().Equals("computer science"))
                            .Join(
                                modelo.PhoneCalls,
                                emp => emp.TelephoneNumber,
                                call => call.SourceNumber,
                                (emp, call) => call
                            )
                            .Aggregate(
                                0,
                                (secs, call) => secs + call.Seconds
                            );
            Console.WriteLine("Resultado: {0}", res);
        }

        // Mostrar las llamadas de teléfono hechas por cada departamento,
        // ordenadas por el nombre del departamento. Cada línea debe tener
        // la siguiente estructura: “Departamento=<Nombre>, Duración=<Segundos>”
        private void Query8()
        {
            Console.WriteLine("\nQuery8\n");
            var res = modelo.Employees
                            .Join(
                                modelo.PhoneCalls,
                                emp => emp.TelephoneNumber,
                                call => call.SourceNumber,
                                (emp, call) => new {Departamento = emp.Department.Name, Duracion = call.Seconds}
                            )
                            .OrderBy(obj => obj.Departamento);
            Show(res);
        }

        // Mostrar los departamentos con el empleado más joven, además del
        // nombre dicho empleado más joven y su edad. Tened en cuenta que
        // puede existir más de un empleado más joven.
        private void Query9()
        {
            Console.WriteLine("\nQuery9\n");
            var res = modelo.Employees
                            .Where(emp => emp.Age.Equals(modelo.Employees.Min(e => e.Age)))
                            .Select(emp => new
                            {
                                Departmento = emp.Department.Name,
                                Empleado = emp.Name,
                                Edad = emp.Age
                            });
            Show(res);
        }

        // Mostrar el departamento que tenga la mayor duración de llamadas
        // telefónicas (en segundos), sumando la duración de las llamadas de
        // todos los empleados que pertenecen al mismo. Mostrar también el nombre
        // de dicho departamento (puede suponerse que solo hay un departamento que cumplirá esta condición.
        private void Query10()
        {
            Console.WriteLine("\nQuery10\n");

            #region Forma 1

            Console.WriteLine("Forma 1");
            var res1 = modelo.Employees
                             .GroupJoin(
                                 modelo.PhoneCalls,
                                 emp => emp.TelephoneNumber,
                                 call => call.SourceNumber,
                                 (emp, calls) => new
                                 {
                                     emp,
                                     callsDuration = calls.Sum(call => call.Seconds)
                                 }
                             )
                             .GroupBy(
                                 obj => obj.emp.Department,
                                 obj => obj.callsDuration,
                                 (dep, callsDuration) => new
                                 {
                                     Departamento = dep,
                                     NombreDepartamento = dep.Name,
                                     DuracionLlamadas = callsDuration.Sum()
                                 })
                             .OrderByDescending(obj => obj.DuracionLlamadas)
                             .First();
            Console.WriteLine(res1);

            #endregion
            
            #region Forma 2

            // Separa el GroupJoin en un Join y luego un GroupBy. Es mejor la forma anterior.
            Console.WriteLine("Forma 2");
            var res2 = modelo.Employees
                             .Join(
                                 modelo.PhoneCalls,
                                 emp => emp.TelephoneNumber,
                                 call => call.SourceNumber,
                                 (emp, call) => new {emp.Department, call.Seconds}
                             )
                             .GroupBy(
                                 obj => obj.Department,
                                 obj => obj.Seconds
                                 )
                             .Select(grp => new
                             {
                                 Departamento = grp.Key,
                                 NombreDepartamento = grp.Key.Name,
                                 DuracionLLamadas = grp.Sum()
                             })
                             .OrderByDescending(obj => obj.DuracionLLamadas)
                             .First();
            Console.WriteLine(res2);

            #endregion
            
            #region Forma 3

            Console.WriteLine("Forma 3");
            var res3 = modelo.Employees
                             .GroupJoin(
                                 modelo.PhoneCalls,
                                 emp => emp.TelephoneNumber,
                                 call => call.SourceNumber,
                                 (emp, calls) => new
                                 {
                                     emp,
                                     callsDuration = calls.Sum(call => call.Seconds)
                                 }
                             )
                             .GroupBy(
                                 obj => obj.emp.Department,
                                 obj => obj.callsDuration,
                                 (dep, callsDuration) => new
                                 {
                                     Departamento = dep,
                                     NombreDepartamento = dep.Name,
                                     DuracionLlamadas = callsDuration.Sum()
                                 })
                             .Aggregate(
                                 (previousObj, currentObj) =>
                                 {
                                     if (currentObj.DuracionLlamadas > previousObj.DuracionLlamadas)
                                         return currentObj;
                                     return previousObj;
                                 }
                             );
            Console.WriteLine(res3);

            #endregion

            #region Forma 4

            // Separa el GroupJoin en un Join y luego un GroupBy. Es mejor la forma anterior.
            Console.WriteLine("Forma 4");
            var res4 = modelo.Employees
                             .Join(
                                 modelo.PhoneCalls,
                                 emp => emp.TelephoneNumber,
                                 call => call.SourceNumber,
                                 (emp, call) => new {emp, call}
                             )
                             .GroupBy(
                                 obj => obj.emp,
                                 obj => obj.call.Seconds,
                                 (emp, callsSeconds) => new
                                 {
                                     emp,
                                     callsDuration = callsSeconds.Sum()
                                 })
                             .GroupBy(
                                 obj => obj.emp.Department,
                                 obj => obj.callsDuration,
                                 (dep, callsDuration) => new
                                 {
                                     Departamento = dep,
                                     NombreDepartamento = dep.Name,
                                     DuracionLlamadas = callsDuration.Sum()
                                 })
                             .Aggregate(
                                 (previousObj, currentObj) =>
                                 {
                                     if (currentObj.DuracionLlamadas > previousObj.DuracionLlamadas)
                                         return currentObj;
                                     return previousObj;
                                 }
                             );
            Console.WriteLine(res4);

            #endregion
        }

        private static void Show<T>(IEnumerable<T> colección)
        {
            foreach (var item in colección)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("Elementos en la colección: {0}.", colección.Count());
        }
    }
}