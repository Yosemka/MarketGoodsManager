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
            
            Goods serGood = new Goods("chocolate", 42.3);
            Console.WriteLine(serGood.ToString());
            Stream goodStream = File.Open("Good.dat", FileMode.Create);
            BinaryFormatter binary = new BinaryFormatter();
            binary.Serialize(goodStream, serGood);
            goodStream.Close();
            serGood = null;
            
            goodStream = File.Open("Good.dat", FileMode.Open);
            binary = new BinaryFormatter();
            serGood = (Goods)binary.Deserialize(goodStream);
            goodStream.Close();
            Console.WriteLine("Результат десериализации: " + serGood.ToString());
            
            XmlSerializer ser = new XmlSerializer(typeof(Goods));

            string path = @"C:\Users\Mollusc\source\repos\MarketGoodsManager\MarketGoodsManager\bin\Debug\netcoreapp2.1\good.xml";
            using (FileStream st = File.Create(path))
            {
                File.SetAttributes(path, FileAttributes.Normal);
            }

            serGood.Name = "Water";
            serGood.Price = 56;

            using (TextWriter tw = new StreamWriter(path))
            {
                ser.Serialize(tw, serGood);
            }
            serGood = null;
            XmlSerializer deser = new XmlSerializer(typeof(Goods));
            TextReader reader = new StreamReader(path);
            object obj = deser.Deserialize(reader);
            serGood = (Goods)obj;
            reader.Close();
            Console.WriteLine("Результат десериализации: " + serGood.ToString());

            Supermarket sp = new Supermarket(56);

            Stream stream = File.Open("SupermarketData.dat", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stream, sp);
            stream.Close();
            
            stream = File.Open("SupermarketData.dat", FileMode.Open);
           
            bf = new BinaryFormatter();
            Supermarket deserialize = new Supermarket();
            deserialize = (Supermarket)bf.Deserialize(stream);
            stream.Close();
            Console.WriteLine(deserialize.ToString());

           


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
            else
                section_1.AddGoodAtPosition(4);

            section_1.ShowAllGoods();

            Console.WriteLine("Hello World!");
        }
    }
}
