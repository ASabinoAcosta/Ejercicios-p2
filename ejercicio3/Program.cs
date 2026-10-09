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

        if (datos.Length == 8 && datos[1] == curso)
        {
            string matricula = datos[0];
            string alumno = datos[2];
            List<double> notas = new List<double>();

            for (int i = 3; i < 7; i++)
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

            estudiantes[matricula] = new Estudiante
            {
                Matricula = matricula,
                Nombre = alumno,
                Notas = notas,
                Promedio = promedio,
                Literal = literal
            };
        }
    }
}

void GuardarEstudiantes()
{
    List<string> registros = new List<string>();

    if (File.Exists(archivo))
    {
        foreach (string registro in File.ReadAllLines(archivo))
        {
            string[] datos = registro.Split('|');

            if (datos.Length != 8 || datos[1] != curso)
            {
                registros.Add(registro);
            }
        }
    }

    foreach (var estudiante in estudiantes)
    {
        registros.Add(
            $"{estudiante.Value.Matricula}|{curso}|{estudiante.Value.Nombre}|" +
            $"{estudiante.Value.Notas[0]}|" +
            $"{estudiante.Value.Notas[1]}|" +
            $"{estudiante.Value.Notas[2]}|" +
            $"{estudiante.Value.Notas[3]}|" +
            $"{estudiante.Value.Literal}"
        );
    }

    File.WriteAllLines(archivo, registros);
}

string GenerarMatricula()
{
    int numero = 1;

    while (estudiantes.ContainsKey($"CDB-{numero:D4}"))
    {
        numero++;
    }

    return $"CDB-{numero:D4}";
}

while (runPrograma)
{
    Console.WriteLine($@"
    ====== Colegio Dios es Bueno ======
    Curso: {curso}

    1. Agregar un estudiante
    2. Eliminar un estudiante
    3. Reportes
    4. Salir
    ");

    Console.Write("Seleccione una opción: ");
    int respuesta = int.Parse(Console.ReadLine());

    switch (respuesta)
    {
        case 1:
            string matricula = GenerarMatricula();

            Console.WriteLine($"Matrícula asignada: {matricula}");
            Console.Write("Inserte nombre del alumno/a: ");
            string alumno = Console.ReadLine();

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

            estudiantes.Add(matricula, new Estudiante
            {
                Matricula = matricula,
                Nombre = alumno,
                Notas = notas,
                Promedio = promedio,
                Literal = literal
            });

            GuardarEstudiantes();
            Console.WriteLine("Estudiante agregado correctamente.");
            break;

        case 2:
            Console.Write("Inserte la matrícula del estudiante a eliminar: ");
            string matriculaEliminar = Console.ReadLine();

            if (estudiantes.Remove(matriculaEliminar))
            {
                GuardarEstudiantes();
                Console.WriteLine("Estudiante eliminado correctamente.");
            }
            else
            {
                Console.WriteLine("No se encontró un estudiante con esa matrícula.");
            }
            break;

        case 3:
            int excelente = 0;
            int bien = 0;
            int pasable = 0;
            int reprobado = 0;

            Console.WriteLine($"\n====== Reporte del curso {curso} ======");

            var estudiantesOrdenados = estudiantes.OrderBy(estudiante =>
                estudiante.Value.Nombre.Split(' ').Last()
            );

            Console.WriteLine(
                $"{"Matrícula",-15}" +
                $"{"Estudiante",-25}" +
                $"{"P1",-8}" +
                $"{"P2",-8}" +
                $"{"P3",-8}" +
                $"{"P4",-8}" +
                $"{"Promedio",-12}" +
                $"{"Literal",-8}"
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
                    $"{estudiante.Value.Matricula,-15}" +
                    $"{estudiante.Value.Nombre,-25}" +
                    $"{estudiante.Value.Notas[0],-8}" +
                    $"{estudiante.Value.Notas[1],-8}" +
                    $"{estudiante.Value.Notas[2],-8}" +
                    $"{estudiante.Value.Notas[3],-8}" +
                    $"{estudiante.Value.Promedio,-12:F2}" +
                    $"{estudiante.Value.Literal,-8}"
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
    public string Matricula { get; set; }
    public string Nombre { get; set; }
    public List<double> Notas { get; set; }
    public double Promedio { get; set; }
    public char Literal { get; set; }
}