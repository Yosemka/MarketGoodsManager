using System;
using System.Collections.Generic;
using System.Text;

namespace MarketGoodsManager
{
    [Serializable]
    class Goods
    {
        private string name;        //по умолчанию модификатор доступа internal
        private double price;
                
        public Goods(string newName, double newPrice)
        {
            name = newName;
            price = newPrice;
        }

        public Goods()
        {
            name = "без названия";
            price = 0.0;
        }

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

        public void Serialize()
        {

        }

        public void Deserialize()
        {

        }
    }
}
