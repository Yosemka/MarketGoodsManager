using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketGoodsManager
{
    [Serializable]
    class SectionGoods
    {
        private string name;
        TwoLinkedList goodsList;
        const string noName = "no name";
        public SectionGoods(string newName)
        {
            name = newName;
            goodsList = new TwoLinkedList();
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
        //public void AppendGood(string goodName, double goodPrice)
        //{
        //    if(GetGoodByName(goodName) != null)
        //    {
        //        Console.WriteLine($"Товар с именем \"{goodName}\" уже существует. Невозможно создать вторую позицию с таким же именем.");
        //    }
        //    else
        //    {
        //        if (goodPrice < 0)
        //            goodPrice = 0;

        //        goodsList.Add(new Goods(goodName, goodPrice));
        //    }            
        //}
        private void AppendGood()
        {
            if (GetGoodByName(noName) != null)
            {
                Console.WriteLine("Товар с именем \"no name\" уже существует. Невозможно создать вторую позицию с таким же именем.");
            }
            else
            {
                Console.WriteLine($"Товар добавлен на позицию {goodsList.Add(new Goods(noName, 0))}");
            }
        }
        public void AddGoodAtPosition(int position)
        {
            if(position < 1)
            {
                Console.WriteLine("Некорректное место вставки. Число должно быть натуральным, то есть целым и положительным.");
            }
            else
            {
                if (position > goodsList.Count)
                    AppendGood();
                else
                {

                }
            }

            if(GetGoodByName(noName) != null)
            {
                Console.WriteLine("Товар с именем \"no name\" уже существует. Невозможно создать вторую позицию с таким же именем.");
            }
            else
            {
                goodsList.Add(new Goods(noName, 0));
            }            
        }

        public void DeleteGood(string name) 
        {
            Goods deleteGood = GetGoodByName(name);
            if (deleteGood != null)
            {
                if (goodsList.Remove(deleteGood.Name))
                    Console.WriteLine($"Товар \"{name}\" успешно удален.");
                else
                    Console.WriteLine($"Неуспешное удаление товара \"{name}\".");
            }
            else
            {
                Console.WriteLine($"Товар \"{name}\" не найден. Удаление невозможно.");
            }                
        }

        public void ChangePrice(string name, double newPrice) { }

        public void ChangeName(string name, string newName) { }

        public Goods GetGoodByName(string name)
        {
            return goodsList.GetElementByName(name);
        }

        public double GetTotalSectionPrice()
        {
            Goods[] allGoods = goodsList.GetAllElements();
            double totalPrice = 0;
            if(allGoods != null)
                foreach(Goods goods in allGoods)
                    totalPrice += goods.Price;
            
            return totalPrice;            
        }

        public void ShowAllGoods()
        {
            Goods[] allGoods = goodsList.GetAllElements();

            if (allGoods != null)
                for(int i = 0; i< allGoods.Length; i++)
                    Console.WriteLine($"{i + 1}) {allGoods[i].Name}\t{allGoods[i].Price}\n");

        }

        public void Serialize()
        {

        }

        public void Deserialize()
        {

        }
    }
}
