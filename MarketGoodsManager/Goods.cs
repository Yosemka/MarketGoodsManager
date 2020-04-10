using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace MarketGoodsManager
{
    [Serializable]
    public class Goods //: ISerializable
    {
        //Информационные поля
        private string name;
        private double price;
        //указатели на следующий и предыдущий элементы
        [XmlIgnore]
        public Goods Next { get; set; }
        [XmlIgnore] 
        public Goods Previous { get; set; }
        [XmlIgnore]
        static string path = @"C:\Users\Mollusc\source\repos\MarketGoodsManager\MarketGoodsManager\bin\Debug\netcoreapp2.1\good.xml";
        //Конструкторы
        public Goods(string newName = "no name", double newPrice = 0)
        {
            name = newName;
            price = newPrice;
        }

        public Goods() { }

        //Методы доступа к информационным полям
        
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}\t {1}\n", name, price);
        }
        public void Serialize()
        {
            XmlSerializer ser = new XmlSerializer(typeof(Goods));

            File.WriteAllText(path, string.Empty);
            using (FileStream st = new FileStream(path, FileMode.OpenOrCreate))
            {
                
                ser.Serialize(st, this);
            }
            //XmlSerializer deser = new XmlSerializer(typeof(Goods));
            //TextReader reader = new StreamReader(path);
            //object obj = deser.Deserialize(reader);
            //serGood = (Goods)obj;
            //reader.Close();
            //Console.WriteLine("Результат десериализации: " + serGood.ToString());
        }

        public void Deserialize()
        {
            XmlSerializer ser = new XmlSerializer(typeof(Goods));
            using (FileStream st = new FileStream(path, FileMode.Open))
            {
                Goods des = (Goods)ser.Deserialize(st);
                this.Name = des.Name;
                this.Price = des.Price;
            }
        }

        //public Goods(SerializationInfo info, StreamingContext context)
        //{
        //    name = (string)info.GetValue("Name", typeof(string));
        //    price = (double)info.GetValue("Price", typeof(double));
        //}

        //public void GetObjectData(SerializationInfo info, StreamingContext context)
        //{
        //    info.AddValue("Name", name);
        //    info.AddValue("Price", price);
        //}
    }
}
