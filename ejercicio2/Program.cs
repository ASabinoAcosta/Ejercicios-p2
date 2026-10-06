/*
El colegio "Dios es bueno" necesita obtener la lista de las calificaciones de los estudiantes.
Para esto deberá escribir un programa en C# que obtenga los nombres de los estudiantes y las calificaciones.

Debe solicitar los datos de forma continua hasta que el usuario determine que ya no desea ingresar más datos.r

*/

//Dado que se toman muchas complicaciones al hacer que los valores tengan sus relaciones
//Es mejor hacer una clase para los dos datos

using System.Linq;

bool runPrograma = true;
Dictionary<string, Estudiante> estudiantes = new Dictionary<string, Estudiante>();
char literal;

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

foreach (var estudiante in estudiantes)
{
    Console.WriteLine(
        $"{estudiante.Key,-20}" +
        $"{estudiante.Value.Notas[0],-10}" +  //Con estos -10 y -12 nos aseguramos la estética de los espacios
        $"{estudiante.Value.Notas[1],-10}" +
        $"{estudiante.Value.Notas[2],-10}" +
        $"{estudiante.Value.Notas[3],-10}" +
        $"{estudiante.Value.Promedio,-12:F2}" +
        $"{estudiante.Value.Literal}"
    );
}

class Estudiante
{
    public List<double> Notas { get; set; }
    public double Promedio { get; set; }
    public char Literal { get; set; }
}
