using System;
using System.Globalization;
using System.Text;
using Message;
using static System.Net.Mime.MediaTypeNames;

string[] arguments = Environment.GetCommandLineArgs();
double a, b, c;
if (arguments.Length == 1)
{
    Console.WriteLine(Messages.UnknownError);
    return;
}
if (arguments.Length != 4)
{
    Console.WriteLine(Messages.NoTriangleError);

    return;
}
if (!double.TryParse(arguments[1], out a))
{
    if (!double.TryParse(arguments[1].Replace('.', ','), out a))
    {
        Console.WriteLine(Messages.UnknownError);
        return;
    }
}

if (!double.TryParse(arguments[2], out b))
{
    if (!double.TryParse(arguments[2].Replace('.', ','), out b))
    {
        Console.WriteLine(Messages.UnknownError);
        return;
    }
}

if (!double.TryParse(arguments[3], out c))
{
    if (!double.TryParse(arguments[3].Replace('.', ','), out c))
    {
        Console.WriteLine(Messages.UnknownError);
        return;
    }
}
if (a == double.PositiveInfinity || b == Double.PositiveInfinity || c == Double.PositiveInfinity || a == Double.NegativeInfinity || b == Double.NegativeInfinity || c == Double.NegativeInfinity)
{
    Console.WriteLine(Messages.UnknownError);
    return;
}
double[] argums = new double[3] { a, b, c };
Array.Sort(argums);
a = argums[0];
b = argums[1];
c = argums[2];
if ((a + b) <= c || (b + c) <= a || (a + c) <= b || (a <= 0) || (b <= 0) || (c <= 0))
{
    Console.WriteLine(Messages.NoTriangleError);
    return;
}
if (a < b & b < c)
{
    Console.WriteLine(Messages.Simple);
    return;
}
if ((a.Equals(b) & !a.Equals(c)) || (b.Equals(c) & !b.Equals(a)) || (a.Equals(c) & !a.Equals(b)))
{
    Console.WriteLine(Messages.Isosceles);
    return;
}
if (a.Equals(b) & a.Equals(c))
{
    Console.WriteLine(Messages.Equilateral);
    return;
}
Console.WriteLine(Messages.UnknownError);
return;