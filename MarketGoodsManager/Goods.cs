using System;
using System.Collections.Generic;
using System.Text;

namespace MarketGoodsManager
{
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

        public void SetName(string val)
        {
            name = val;
        }
        public void SetPrice(float val)
        {
            price = val;
        }

        public string GetName()
        {
            return name;
        }
        public double GetPrice()
        {
            return price;
        }
    }
}
