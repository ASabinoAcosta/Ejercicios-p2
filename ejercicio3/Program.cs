//En este ejercicio modificamos el ejercicio 2:
/*
El reporte debe aparecer ordenado por el primer aprellido
el final del reporte debe mostrar los totales de estudiantes en cada literal
y los estudiantes reprobados
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
char literal;
int excelente= 0;
int bien= 0;
int pasable=0;
int reprobado=0;

while (runPrograma)
{
    Console.Write("Inserte nombre del alumno/a: ");
    string alumno = Console.ReadLine();

    List<double> notas = new List<double>();

    for (int i = 0; i < 4; i++)
    {
        Console.Write($"Inserte la nota {i + 1}: ");
        double nota = double.Parse(Console.ReadLine());

        notas.Add(nota);
    }

    //Nuevo truco para ahorrarse el escribir la fórmula
    double promedio = notas.Average();

    if (promedio >=90 && promedio<= 100)
    {
        literal = 'A';
    }
    else if (promedio >= 80 && promedio < 90)
    {
        literal = 'B';
    }
    else if (promedio>=70 && promedio < 80)
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

    Console.WriteLine("Desea agregar más alumnos? Sí = 1, No = 0");
    int respuesta = int.Parse(Console.ReadLine());
    switch (respuesta)
    {
        case 1:
            break;
        case 0:
            runPrograma = false;
            break;
        default:
            Console.WriteLine(@"Solo puede elegir una respuesta entre 0 y 1.
                                Se considerará que decidió seguir.");
            break;
    }
    

}

Console.WriteLine("====== Colegio Dios es Bueno ======");

var estudiantesOrdenados = estudiantes.OrderBy(estudiante =>
    estudiante.Key.Split(' ')[1]
); //Mejor manera de ordenarlos que la planeada (evita errores)

Console.WriteLine(
        $"{"Estudiante",-20}"+
        $"{"P1",-10}"+
        $"{"P2",-10}"+
        $"{"P3",-10}"+
        $"{"P4",-10}"+
        $"{"Promedio",-10}"+
        $"{"Literal", -10}"
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

Console.WriteLine($"""
Cantidad de estudiantes de cada literal:
Estudiantes en A: {excelente}
Estudiantes en B: {bien}
Estudiantes en C: {pasable}
Estudiantes reprobados: {reprobado}
""");

class Estudiante
{
    public List<double> Notas { get; set; }
    public double Promedio { get; set; }
    public char Literal { get; set; }
}