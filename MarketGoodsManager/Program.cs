using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;
using System.Linq;

namespace MarketGoodsManager
{
    class Program
    {
        static string supermarketXmlPath = @"C:\Users\Mollusc\source\repos\MarketGoodsManager\MarketGoodsManager\bin\Debug\netcoreapp2.1\xml";
        static string supermarketXmlName;
        static string sectionXmlName;
        static void Main(string[] args)
        {
            
            Supermarket sptry = GetFiles();
            Console.WriteLine(sptry.ToString());



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


            Console.WriteLine("");

            //Console.WriteLine("Супермаркет \"{}\"", );
            //cout << "Добавить ";

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
            Console.WriteLine(section_1.ToString()); 
            section_1.DeleteGood("Thanks");
            //section_1.ShowAllGoods();
            section_1.GetElementByPosition(section_1.AddGoodAtPosition(1)).Name = "WOW";
            Console.WriteLine(section_1.ToString());

            section_1.Serialize();
            section_1.Deserialize();
            section_1.ToString();

            sp.Serialize();
            sp.Deserialize();
            Console.WriteLine(sp.ToString());

            Console.WriteLine("Hello World!");
        } 
        static Supermarket GetFiles()
        {
            if(Directory.Exists(supermarketXmlPath))
            {
                //DirectoryInfo dirInfo= new DirectoryInfo(supermarketXmlPath);
                string[] filesFullPath = Directory.GetFiles(supermarketXmlPath);
                
                Supermarket supermarket = new Supermarket();
                if(filesFullPath.Length > 0)
                {
                    string fileName;
                    foreach (string st in filesFullPath)
                    {
                        fileName = Path.GetFileName(st);

                        if (fileName.Contains("supermarket", StringComparison.OrdinalIgnoreCase))
                        {
                            supermarket.Deserialize(supermarketXmlPath + "\\" + fileName);
                            return supermarket;
                        }                      
                    
                    }
                    return null;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                Directory.CreateDirectory(supermarketXmlPath);
                return null;
            }
            
        }
    }

  
}
