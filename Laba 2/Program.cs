using System;

public class Triangle
{
    // Вершини трикутника
    private Point[] vertices;

    // Конструктор для створення трикутника за координатами вершин
    public Triangle(Point[] vertices)
    {
        if (vertices.Length != 3)
            throw new ArgumentException("Triangle must have exactly 3 vertices.");

        this.vertices = vertices;
    }

    // Метод для обчислення площі трикутника
    public double Area()
    {
        double a = vertices[0].DistanceTo(vertices[1]);
        double b = vertices[1].DistanceTo(vertices[2]);
        double c = vertices[2].DistanceTo(vertices[0]);
        double s = (a + b + c) / 2; // Півпериметр
        return Math.Sqrt(s * (s - a) * (s - b) * (s - c));
    }

    // Метод для обчислення периметра трикутника
    public double Perimeter()
    {
        double a = vertices[0].DistanceTo(vertices[1]);
        double b = vertices[1].DistanceTo(vertices[2]);
        double c = vertices[2].DistanceTo(vertices[0]);
        return a + b + c;
    }

    // Метод для визначення типу трикутника
    public string Type()
    {
        double a = vertices[0].DistanceTo(vertices[1]);
        double b = vertices[1].DistanceTo(vertices[2]);
        double c = vertices[2].DistanceTo(vertices[0]);
        if (a == b && b == c)
            return "Рівносторонній";
        else if (a == b || b == c || c == a)
            return "Рівнобедрений";
        else
        {
            double[] sides = { a, b, c };
            Array.Sort(sides);
            if (sides[2] * sides[2] < sides[0] * sides[0] + sides[1] * sides[1])
                return "Гострокутний";
            else if (sides[2] * sides[2] == sides[0] * sides[0] + sides[1] * sides[1])
                return "Прямокутний";
            else
                return "Тупокутний";
        }
    }
}

// Клас для представлення точки на площині
public class Point
{
    public double X { get; }
    public double Y { get; }

    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }

    // Метод для обчислення відстані між двома точками
    public double DistanceTo(Point other)
    {
        double dx = X - other.X;
        double dy = Y - other.Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Введення координат вершин трикутника з консолі
        Console.WriteLine("Введіть координати вершин трикутника:");
        Point[] vertices = new Point[3];
        for (int i = 0; i < 3; i++)
        {
            Console.Write($"X координата вершини {i + 1}: ");
            double x = Convert.ToDouble(Console.ReadLine());
            Console.Write($"Y координата вершини {i + 1}: ");
            double y = Convert.ToDouble(Console.ReadLine());
            vertices[i] = new Point(x, y);
        }

        // Створення об'єкта трикутника за введеними координатами
        Triangle triangle = new Triangle(vertices);

        // Виведення властивостей трикутника
        Console.WriteLine("Площа трикутника: " + triangle.Area());
        Console.WriteLine("Периметр трикутника: " + triangle.Perimeter());
        Console.WriteLine("Тип трикутника: " + triangle.Type());
    }
}
