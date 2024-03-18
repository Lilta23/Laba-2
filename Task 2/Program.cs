using Newtonsoft.Json;
using System;
using System.IO;

public class Triangle
{
    private Point[] vertices;

    public Triangle(Point[] vertices)
    {
        if (vertices.Length != 3)
            throw new ArgumentException("Triangle must have exactly 3 vertices.");
        this.vertices = vertices;
    }

    public bool AreEqual(Triangle other)
    {
        for (int i = 0; i < 3; i++)
        {
            if (!vertices[i].AreEqual(other.vertices[i]))
                return false;
        }
        return true;
    }

    public double Area()
    {
        double a = vertices[0].DistanceTo(vertices[1]);
        double b = vertices[1].DistanceTo(vertices[2]);
        double c = vertices[2].DistanceTo(vertices[0]);
        double s = (a + b + c) / 2; // Півпериметр
        return Math.Sqrt(s * (s - a) * (s - b) * (s - c));
    }

    public double Perimeter()
    {
        double a = vertices[0].DistanceTo(vertices[1]);
        double b = vertices[1].DistanceTo(vertices[2]);
        double c = vertices[2].DistanceTo(vertices[0]);
        return a + b + c;
    }

    public double Height(double a)
    {
        return 2 * Area() / a;
    }

    public double Median(double a)
    {
        int b = 1, c = 1, s = 1;
        return 0.5 * Math.Sqrt(2 * b * b + 2 * c * c - a * a);
    }

    public double Bisector(double a)
    {
        int b = 1, c = 1, s = 1;
        return 2 * b * c * Math.Cos(0.5 * Math.Acos((b * b + c * c - a * a) / (2 * b * c)));
    }

    public double Inradius()
    {
        return 2 * Area() / Perimeter();
    }

    public double Circumradius()
    {
        double a = vertices[0].DistanceTo(vertices[1]);
        double b = vertices[1].DistanceTo(vertices[2]);
        double c = vertices[2].DistanceTo(vertices[0]);
        return (a * b * c) / (4 * Area());
    }

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

    public void Rotate(double angle, int vertexIndex)
    {
        Point vertex = vertices[vertexIndex % 3];
        for (int i = 0; i < 3; i++)
        {
            double newX = (vertices[i].X - vertex.X) * Math.Cos(angle) - (vertices[i].Y - vertex.Y) * Math.Sin(angle) + vertex.X;
            double newY = (vertices[i].X - vertex.X) * Math.Sin(angle) + (vertices[i].Y - vertex.Y) * Math.Cos(angle) + vertex.Y;
            vertices[i] = new Point(newX, newY);
        }
    }
}

public class Point
{
    public double X { get; }
    public double Y { get; }

    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }

    public double DistanceTo(Point other)
    {
        double dx = X - other.X;
        double dy = Y - other.Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }

    public bool AreEqual(Point other)
    {
        return X == other.X && Y == other.Y;
    }
}



class Program
{
    static void Main(string[] args)
    {
        // Створення об'єкту класу Triangle
        var vertices = new[] { new[] { 0, 0 }, new[] { 3, 0 }, new[] { 0, 4 } };
        var triangle = new { Vertices = vertices };

        // Серіалізація об'єкту у JSON рядок
        string json = JsonConvert.SerializeObject(triangle);

        // Збереження JSON у файл
        string filePath = @"C:\Users\usertop\Desktop\test.json";
        File.WriteAllText(filePath, json);
        Console.WriteLine("Triangle object serialized and saved to file: " + filePath);

        // Читання JSON з файлу
        string jsonFromFile = File.ReadAllText(filePath);

        // Десеріалізація JSON у об'єкт
        var deserializedTriangle = JsonConvert.DeserializeObject<dynamic>(jsonFromFile);

        // Виведення типу трикутника
        Console.WriteLine("Deserialized triangle type: " + GetTriangleType(deserializedTriangle.Vertices));
    }

    // Метод для визначення типу трикутника
    static string GetTriangleType(int[][] vertices)
    {
        double a = Math.Sqrt(Math.Pow(vertices[1][0] - vertices[0][0], 2) + Math.Pow(vertices[1][1] - vertices[0][1], 2));
        double b = Math.Sqrt(Math.Pow(vertices[2][0] - vertices[1][0], 2) + Math.Pow(vertices[2][1] - vertices[1][1], 2));
        double c = Math.Sqrt(Math.Pow(vertices[0][0] - vertices[2][0], 2) + Math.Pow(vertices[0][1] - vertices[2][1], 2));

        if (a == b && b == c)
            return "Рівносторонній";
        else if (a == b || b == c || c == a)
            return "Рівнобедрений";
        else if (Math.Pow(c, 2) == Math.Pow(a, 2) + Math.Pow(b, 2) ||
                 Math.Pow(a, 2) == Math.Pow(b, 2) + Math.Pow(c, 2) ||
                 Math.Pow(b, 2) == Math.Pow(c, 2) + Math.Pow(a, 2))
            return "Прямокутний";
        else if (Math.Pow(c, 2) < Math.Pow(a, 2) + Math.Pow(b, 2) &&
                 Math.Pow(a, 2) < Math.Pow(b, 2) + Math.Pow(c, 2) &&
                 Math.Pow(b, 2) < Math.Pow(c, 2) + Math.Pow(a, 2))
            return "Гострокутний";
        else
            return "Тупокутний";
    }
}
