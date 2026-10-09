//En este ejercicio modificamos el ejercicio 2:
/*
El reporte debe aparecer ordenado por el primer aprellido
el final del reporte debe mostrar los totales de estudiantes en cada literal
y los estudiantes reprobados
*/
/*
    Vamos a modificar el ejercicio 3. Los datos se guardarán en un txt y crear un menú con las siguientes opciones:
    Agregar un estudiante
    Eliminar un estudiante
    Y reportes
    Y salir.
    (Ahora los estudiantes se guardarán en un curso)
*/

/*Pequeñas pruebas antes de la modificación
//se toma un nombre
List<string> nombres = new List<string>()
{
    "Adhara Black",
    "Tiffany Lindorf",
    "Lionissa Cerrano"
};
nombres.Sort((a, b) =>
{
    string apellidoA = a.Split(' ')[1]; //tomamos uno de los apellidos
    string apellidoB = b.Split(' ')[1]; //tomamos otro de los apellidos

    return apellidoA.CompareTo(apellidoB); //lo comparamos entre ambos
});

foreach (string nombre in nombres)
{
    Console.WriteLine(nombre);
}
*/

using System.Linq;

bool runPrograma = true;
Dictionary<string, Estudiante> estudiantes = new Dictionary<string, Estudiante>();
string archivo = "estudiantes.txt";

Console.Write("Inserte el nombre del curso: ");
string curso = Console.ReadLine();

if (File.Exists(archivo))
{
    string[] registros = File.ReadAllLines(archivo);

    foreach (string registro in registros)
    {
        string[] datos = registro.Split('|');

        if (datos.Length == 7 && datos[0] == curso)
        {
            string alumno = datos[1];
            List<double> notas = new List<double>();

            for (int i = 2; i < 6; i++)
            {
                notas.Add(double.Parse(datos[i]));
            }

            double promedio = notas.Average();
            char literal;

            if (promedio >= 90 && promedio <= 100)
            {
                literal = 'A';
            }
            else if (promedio >= 80 && promedio < 90)
            {
                literal = 'B';
            }
            else if (promedio >= 70 && promedio < 80)
            {
                literal = 'C';
            }
            else
            {
                literal = 'D';
            }

            estudiantes[alumno] = new Estudiante
            {
                Notas = notas,
                Promedio = promedio,
                Literal = literal
            };
        }
    }
}

while (runPrograma)
{
    Console.WriteLine($"""
    
    ====== Colegio Dios es Bueno ======
    Curso: {curso}
    
    1. Agregar un estudiante
    2. Eliminar un estudiante
    3. Reportes
    4. Salir
    """);

    Console.Write("Seleccione una opción: ");
    int respuesta = int.Parse(Console.ReadLine());

    switch (respuesta)
    {
        case 1:
            Console.Write("Inserte nombre del alumno/a: ");
            string alumno = Console.ReadLine();

            if (estudiantes.ContainsKey(alumno))
            {
                Console.WriteLine("Este estudiante ya está registrado.");
                break;
            }

            List<double> notas = new List<double>();

            for (int i = 0; i < 4; i++)
            {
                Console.Write($"Inserte la nota {i + 1}: ");
                double nota = double.Parse(Console.ReadLine());

                notas.Add(nota);
            }

            double promedio = notas.Average();
            char literal;

            if (promedio >= 90 && promedio <= 100)
            {
                literal = 'A';
            }
            else if (promedio >= 80 && promedio < 90)
            {
                literal = 'B';
            }
            else if (promedio >= 70 && promedio < 80)
            {
                literal = 'C';
            }
            else
            {
                literal = 'D';
            }

            estudiantes.Add(alumno, new Estudiante
            {
                Notas = notas,
                Promedio = promedio,
                Literal = literal
            });

            List<string> registros = new List<string>();

            foreach (var estudiante in estudiantes)
            {
                registros.Add(
                    $"{curso}|{estudiante.Key}|" +
                    $"{estudiante.Value.Notas[0]}|" +
                    $"{estudiante.Value.Notas[1]}|" +
                    $"{estudiante.Value.Notas[2]}|" +
                    $"{estudiante.Value.Notas[3]}|{estudiante.Value.Literal}"
                );
            }

            File.WriteAllLines(archivo, registros);
            Console.WriteLine("Estudiante agregado correctamente.");
            break;

        case 2:
            Console.Write("Inserte el nombre del estudiante a eliminar: ");
            string nombreEliminar = Console.ReadLine();

            if (estudiantes.Remove(nombreEliminar))
            {
                List<string> registrosActualizados = new List<string>();

                foreach (var estudiante in estudiantes)
                {
                    registrosActualizados.Add(
                        $"{curso}|{estudiante.Key}|" +
                        $"{estudiante.Value.Notas[0]}|" +
                        $"{estudiante.Value.Notas[1]}|" +
                        $"{estudiante.Value.Notas[2]}|" +
                        $"{estudiante.Value.Notas[3]}|{estudiante.Value.Literal}"
                    );
                }

                File.WriteAllLines(archivo, registrosActualizados);
                Console.WriteLine("Estudiante eliminado correctamente.");
            }
            else
            {
                Console.WriteLine("No se encontró el estudiante.");
            }
            break;

        case 3:
            int excelente = 0;
            int bien = 0;
            int pasable = 0;
            int reprobado = 0;

            Console.WriteLine($"\n====== Reporte del curso {curso} ======");

            var estudiantesOrdenados = estudiantes.OrderBy(estudiante =>
                estudiante.Key.Split(' ').Last()
            );

            Console.WriteLine(
                $"{"Estudiante",-20}" +
                $"{"P1",-10}" +
                $"{"P2",-10}" +
                $"{"P3",-10}" +
                $"{"P4",-10}" +
                $"{"Promedio",-12}" +
                $"{"Literal",-10}"
            );

            foreach (var estudiante in estudiantesOrdenados)
            {
                if (estudiante.Value.Literal == 'A')
                {
                    excelente++;
                }
                else if (estudiante.Value.Literal == 'B')
                {
                    bien++;
                }
                else if (estudiante.Value.Literal == 'C')
                {
                    pasable++;
                }
                else
                {
                    reprobado++;
                }

                Console.WriteLine(
                    $"{estudiante.Key,-20}" +
                    $"{estudiante.Value.Notas[0],-10}" +
                    $"{estudiante.Value.Notas[1],-10}" +
                    $"{estudiante.Value.Notas[2],-10}" +
                    $"{estudiante.Value.Notas[3],-10}" +
                    $"{estudiante.Value.Promedio,-12:F2}" +
                    $"{estudiante.Value.Literal}"
                );
            }

            Console.WriteLine(
                $"\nCantidad de estudiantes de cada literal:\n" +
                $"Estudiantes en A: {excelente}\n" +
                $"Estudiantes en B: {bien}\n" +
                $"Estudiantes en C: {pasable}\n" +
                $"Estudiantes reprobados: {reprobado}"
            );
            break;

        case 4:
            runPrograma = false;
            break;

        default:
            Console.WriteLine("Seleccione una opción entre 1 y 4.");
            break;
    }
}

class Estudiante
{
    public List<double> Notas { get; set; }
    public double Promedio { get; set; }
    public char Literal { get; set; }
}