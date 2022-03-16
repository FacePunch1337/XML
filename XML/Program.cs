using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

namespace XML
{

    
    class Program
    {
        [Serializable]
        public class Set<T> where T : class
        {
            private static List<T> list = new List<T>();

            public bool Add<T2>() where T2 : T, new()
            {
                var tn = typeof(T).Name;

                if (!list.Any(x => x is T2))
                {
                    
                    list.Add(new T2());
                    return true;
                }
                else
                {
                    throw new Exception($"The list cannot contain elements of the same type");
                    return false;
                }
            }
            public T2 Get<T2>() where T2 : class
            {

                foreach (T item in list)
                {
                    if (item is T2) return (T2)Convert.ChangeType(item, typeof(T2));
                }
                return null;

            }
            public void Print()
            {
                foreach (T item in list)
                {
                    Console.WriteLine(item);
                }
            }
        }
        public class Product
        {
            [XmlAttribute]
            public String Name { get; set; }
            [XmlAttribute]
            public double Price { get; set; }
            [XmlAttribute]
            public double Discount { get; set; }

           
            public Product()
            {
                
            }

            


            public override string ToString()
            {
                return $"Name: {Name}\nPrice: {Price}$\nDiscount: {Discount}%\n";
            }
        }

         class Acer : Product
        {
          
            public Acer()
            {
               
                
                base.Name = "Acer";
                base.Price = 300;
                base.Discount = 10;
            }
        }

         class DELL : Product
        {
            public DELL()
            {
                base.Name = "DELL";
                base.Price = 400;
                base.Discount = 5;
            }
        }

         class MSI : Product
        {
            public MSI()
            {
                base.Name = "MSI";
                base.Price = 600;
                base.Discount = 5;
            }
        }



        static void XmlDemo(Set<Product> data)
        {
            XmlSerializer xml = new XmlSerializer(typeof(Set<Product>));

            using (var xmlFile = new StreamWriter("ser.xml"))
            {
                xml.Serialize(xmlFile, data);
            }

            using (var xmlFile = new StreamReader("ser.xml"))
            {
                var res = (Set<Product>)xml.Deserialize(xmlFile);
                Console.WriteLine(res);
            }
        }


        static void Main(string[] args)
        {


            Set<Product> set_prod = new Set<Product>();
            set_prod.Add<Acer>();
            set_prod.Add<DELL>();
            set_prod.Add<MSI>();
            set_prod.Print();
            
            XmlDemo(set_prod);
            

        }
    }
}
