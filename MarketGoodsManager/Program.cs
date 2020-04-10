using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;

namespace MarketGoodsManager
{
    //struct GoodsList
    //{

    //}
    class Program
    {
        
        static void Main(string[] args)
        {
            
            //Goods serGood = new Goods("chocolate", 42.3);
            //Console.WriteLine(serGood.ToString());
            //Stream goodStream = File.Open("Good.dat", FileMode.Create);
            //BinaryFormatter binary = new BinaryFormatter();
            //binary.Serialize(goodStream, serGood);
            //goodStream.Close();
            //serGood = null;
            
            //goodStream = File.Open("Good.dat", FileMode.Open);
            //binary = new BinaryFormatter();
            //serGood = (Goods)binary.Deserialize(goodStream);
            //goodStream.Close();
            //Console.WriteLine("Результат десериализации: " + serGood.ToString());
            


            Supermarket sp = new Supermarket(56);

            sp.AddNewSection("Бытовая химия");
            sp.Name = "Пятерочка";
            sp.AddNewSection("Бытовая химия");
            sp.AddNewSection("Бытовая");
            

            SectionGoods section_1 = sp.GetSection("Бытовая химия");

            if (section_1 != null)
            {
                section_1.GetTotalSectionPrice();
                section_1.AddGoodAtPosition(12);
            }
            
            section_1.GetElementByPosition(section_1.AddGoodAtPosition(4)).Name = "Thanks";
            section_1.AddGoodAtPosition(4);

            section_1.GetElementByPosition(section_1.AddGoodAtPosition(4)).Name = "Хамелеон";
            section_1.GetElementByPosition(section_1.AddGoodAtPosition(5)).Name = "Усургут";
            //section_1.ShowAllGoods(); 
            section_1.DeleteGood("Thanks");
            //section_1.ShowAllGoods();
            section_1.GetElementByPosition(section_1.AddGoodAtPosition(1)).Name = "WOW";
            //section_1.ShowAllGoods();

            Goods g = section_1.GetElementByPosition(1);
            g.Price = 89;
            g.Serialize();
            g.Name = "";
            g.Price = 0;
            g.Deserialize();

            Console.WriteLine(g.ToString());

            section_1.Serialize();
            section_1.Deserialize();
            section_1.ShowAllGoods();

            sp.Serialize();
            sp.Deserialize();
            
            Console.WriteLine("Hello World!");
        }
    }
}
