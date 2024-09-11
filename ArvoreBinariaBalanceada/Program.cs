// See https://aka.ms/new-console-template for more information


using System.Collections.Generic.RedBlack;
using System.Diagnostics;

using var stream = File.OpenRead("populacao-municipios.csv");

List<City> array = [];

Stopwatch sw = new();
sw.Start();
using (var reader = new StreamReader(stream))
{
    string? line;
    reader.ReadLine(); // skip header
    while ((line = reader.ReadLine()) != null)
    {
        var parts = line.Split(',');
        array.Add(new City(parts[0], long.Parse(parts[1].Replace(".", ""))));
    }
}

Console.WriteLine($"Inserted {array.Count} cities in {sw.ElapsedMilliseconds}ms");

Console.WriteLine("Starting search");

sw.Restart();
var city = array.FirstOrDefault(c => c.Name == "Brasília");
Console.WriteLine($"Found {city} in {sw.ElapsedMilliseconds}ms");

using var stream2 = File.OpenRead("populacao-municipios.csv");

RedBlackTree<long, string> tree = new();

using (var reader = new StreamReader(stream2))
{
    string? line;
    reader.ReadLine(); // skip header
    while ((line = reader.ReadLine()) != null)
    {
        var parts = line.Split(',');
        tree.TryAdd(long.Parse(parts[1].Replace(".", "")), parts[0]);
    }
}

Console.WriteLine($"Inserted {tree.Count} cities in {sw.ElapsedMilliseconds}ms");

Console.WriteLine("Starting search");

sw.Restart();
var city2 = tree.GetData(2982818);

Console.WriteLine($"Found {city2} in {sw.ElapsedMilliseconds}ms");

Console.WriteLine($"end");

record City(string Name, long Population);
