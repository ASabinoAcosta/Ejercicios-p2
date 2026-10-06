/*
Realice un programa que solicite de dos valores al usuario y luego imprima por pantalla los
resultados de las operaciones:

suma, resta, multiplicación, división y raíz cuadrada de cada valor
*/

double sumar(double numero1, double numero2)
{
    return numero1 + numero2;
}

double restar(double numero1, double numero2)
{
    return numero1 - numero2;
}

double multiplicar(double numero1, double numero2)
{
    return numero1 * numero2;
}

double dividir(double numero1, double numero2)
{
    return numero1 / numero2;
}

double raizCuadrada(double numero)
{
    return Math.Sqrt(numero);
}

Console.WriteLine("Escriba un número: ");
double numero1 = double.Parse(Console.ReadLine());
Console.WriteLine("Escriba un segundo número: ");
double numero2 = double.Parse(Console.ReadLine());

Console.WriteLine($"""
El resultado de las operaciones realizadas con sus números es:
Suma: {numero1} + {numero2} = {sumar(numero1, numero2):F2}
Resta: {numero1} - {numero2} = {restar(numero1, numero2):F2}
Multiplicación: {numero1} * {numero2} = {multiplicar(numero1, numero2):F2}
División:{numero1} / {numero2} = {dividir(numero1, numero2):F2}
Raíz cuadrada: √{numero1} = {raizCuadrada(numero1):F4}, √{numero2} = {raizCuadrada(numero2):F2}
""");
