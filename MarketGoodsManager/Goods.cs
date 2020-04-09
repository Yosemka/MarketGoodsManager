using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Text;

namespace MarketGoodsManager
{
    [Serializable]
    public class Goods : ISerializable
    {
        //Информационные поля
        private string name;
        private double price;
        //указатели на следующий и предыдущий элементы
        public Goods Next { get; set; }
        public Goods Previous { get; set; }
        
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
            return string.Format("Название товара: {0}\t Цена товара: {1}\n", name, price);
        }
        public void Serialize()
        {

        }

        public Goods(SerializationInfo info, StreamingContext context)
        {
            name = (string)info.GetValue("Name", typeof(string));
            price = (double)info.GetValue("Price", typeof(double));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", name);
            info.AddValue("Price", price);
        }
    }
}
