using System;
using System.Collections.Generic;
using System.IO;

namespace Serialize
{
    partial class Program
    {
        class ListRandom
        {
            public ListNode Head;
            public ListNode Tail;
            public int Count;
            public void Serialize(Stream s)
            {
                var list = new List<ListNode>();
                var node = Head;
                while (node != null)
                {
                    list.Add(node);
                    node = node.Next;
                }
                using (StreamWriter sw = new StreamWriter(s))
                {
                    string nullString = "null";
                    foreach (var item in list)
                    {
                        sw.Write($"{item.Data ?? nullString}:");
                        sw.Write($"{item.Previous?.Data ?? nullString}:");
                        sw.Write($"{item.Random?.Data ?? nullString};");
                    }
                }
            }
            public void Deserialize(Stream s)
            {
                var randomDataList = new List<string>();
                var previousDataList = "";
                Head = null;
                Count = 0;
                string[] file = new string[0];
                try
                {
                    using (StreamReader sr = new StreamReader(s))
                    {
                        var readString = sr.ReadToEnd();
                            file = readString.Split(';', StringSplitOptions.RemoveEmptyEntries);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Press Enter to exit.");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                foreach (var item in file)
                {
                    var nodeArray = item.Split(':');
                    var newNode = new ListNode();
                    newNode.Data = nodeArray[0];
                    var findNodePrevious = Find(previousDataList);
                    newNode.Previous = findNodePrevious;
                    previousDataList = nodeArray[1];
                    randomDataList.Add(nodeArray[2]);
                    AddTailNode(newNode);
                }
                var node = Head;
                while (node != null)
                {
                    node.Random = Find(randomDataList[0]);
                    randomDataList.RemoveAt(0);
                    node = node.Next;
                }
            }
            public ListNode Find(string key)
            {
                if (Count == 0)
                {
                    return null;
                }
                ListNode current = Head;
                while (current.Data != key)
                {
                    current = current.Next;
                    if (current == null)
                    {
                        return null;
                    }
                }

                return current;
            }
            public void AddHead(string item)
            {
                ListNode newNode = new ListNode();
                newNode.Data = item;
                if (Count != 0)
                {
                    newNode.Next = Head;
                    Head.Previous = newNode;
                }
                Head = newNode;
                Count++;
            }
            public void AddTail(string item)
            {
                var newNode = new ListNode();
                newNode.Data = item;
                if (Count == 0)
                {
                    Head = newNode;
                }
                else
                {
                    ListNode tail = FindTail();
                    tail.Next = newNode;
                    newNode.Previous = tail;
                    Tail = newNode;
                }
                Count++;
            }
            public void AddTailNode(ListNode node)
            {
                if (Count == 0)
                {
                    Head = node;
                }
                else
                {
                    var tail = FindTail();
                    tail.Next = node;
                    node.Previous = tail;
                    Tail = node;
                }

                Count++;
            }
            public void SetRandomElementInNodeField()
            {
                var random = new Random();
                var node = Head;
                var randomNode = Head;
                while (node != null)
                {
                    var randomNumber = random.Next(0, Count);
                    for (int i = 0; i < randomNumber; i++)
                    {
                        if (randomNode.Next != null)
                        {
                            randomNode = randomNode.Next;
                        }
                    }
                    node.Random = randomNode;
                    randomNode = Head;
                    node = node.Next;
                }
            }
            private ListNode FindTail()
            {
                if (Count == 0)
                {
                    return null;
                }
                ListNode current = Head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                return current;
            }

        }

    }
}
