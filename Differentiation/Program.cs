using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Linq;
using System;

namespace Differentiation
{
    class Program
    {
        private static readonly string[] All_Peoples = new string[]
        {
            "ALL_NAMES_HERE"
        };

        private static XmlSerializer formatter = new XmlSerializer(typeof(string[]));

        static void Main(string[] args)
        {
            List<string> Has = Load();
            if (Has == null)
            {
                Save(All_Peoples);
                Has = Load();
            }
            if (Has != null && Has.Count == 0)
            {
                Save(All_Peoples);
                Has = Load();
            }

            Random rand = new Random();

            for (int i = 0; i < 2; i++)
            {
                if (Has.Count == 0)
                {
                    break;
                }
                var human1 = Has[rand.Next(0, Has.Count)];
                Has.Remove(human1);
                Console.WriteLine($"#{i + 1}: {human1}");
            }
            Save(Has.ToArray());
            Console.ReadLine();
        }

        static List<string> Load()
        {
            List<string> Persons;

            using (FileStream fs = new FileStream("people.xml", FileMode.OpenOrCreate))
            {
                if (fs.Length == 0) return null;
                Persons = ((string[])formatter.Deserialize(fs)).ToList();
            }

            return Persons;
        }

        static void Save(string[] varr)
        {
            using (FileStream fs = new FileStream("people.xml", FileMode.Create))
            {
                formatter.Serialize(fs, varr);
            }
        }
    }
}
