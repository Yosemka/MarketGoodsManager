using System;
using System.Collections.Generic;
using System.Text;

namespace MarketGoodsManager
{
    class SectionGoods
    {
        private string name;
        private int goodsAmount;
        private struct GoodsList
        {
            Goods head;
            Goods next;
            Goods prev;
        }       

        public SectionGoods(string newName)
        {
           name = newName;
        }

        ~SectionGoods()
        {
            Console.WriteLine($"Отдел \"{this.Name}\" был удален");
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
        public int GoodsAmount
        {
            get
            {
                return goodsAmount;
            }         
        }
        public void AddGood(string goodName, float goodPrice)
        {
            if(goodPrice < 0)
            {

            }
            else
            {
                
            }
        }

        public void DeleteGood(string name) { }

        public void ChangePrice(string name, double newPrice) { }

        public void ChangeName(string name, string newName) { }

        private bool IsNameExist(string nameToFind)
        {
            return true;
        }

        public int GetGoodsAmount() { return goodsAmount; }

        public double GetTotalSectionPrice()
        {
            return 3.0;
        }
    }
}
