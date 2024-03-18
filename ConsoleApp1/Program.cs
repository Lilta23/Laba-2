
using System;
using System.IO;
using Newtonsoft.Json;

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
