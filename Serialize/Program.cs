using System;
using System.IO;

namespace Serialize
{
    partial class Program
    {
        static void Main(string[] args)
        {
            ListRandom list = new ListRandom();
            var random = new Random();

            for (int i = 0; i < 10; i++)
            {
                list.AddTail(random.Next(1000).ToString());
            }
            var node = list.Head;
            Console.WriteLine($"Head is {list.Head.Data}");
            Console.WriteLine($"Tail is {list.Tail.Data}");
            Console.WriteLine();

            while (node != null)
            {
                Console.WriteLine(node.Data);
                node = node.Next;
            }

            list.SetRandomElementInNodeField();
            Console.WriteLine();
            node = list.Head;
            while (node != null)
            {
                Console.WriteLine($"{node.Data} set random link -> {node.Random.Data}");
                node = node.Next;
            }
            Console.WriteLine();
            var s = new FileStream("serialize.nodes", FileMode.OpenOrCreate);
            list.Serialize(s);
            s.Close();
            Console.WriteLine("Serialize OK -> file is \"serialize.nodes\"");

            Console.WriteLine();

            s = new FileStream("serialize.nodes", FileMode.Open);
            list.Deserialize(s);
            s.Close();
            Console.WriteLine("Deserialize OK -> file is \"serialize.nodes\"");

            Console.WriteLine();
            Console.WriteLine($"Head is {list.Head.Data}");
            Console.WriteLine($"Tail is {list.Tail.Data}");
            Console.WriteLine();
            node = list.Head;
            while (node != null)
            {
                Console.WriteLine($"{node.Data} set random link -> {node.Random.Data}");
                node = node.Next;
            }
            Console.ReadLine();
        }


    }
}
