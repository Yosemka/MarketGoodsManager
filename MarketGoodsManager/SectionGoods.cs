using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Xml.Serialization;

namespace MarketGoodsManager
{
    [Serializable]
    public class SectionGoods
    {
        [XmlIgnore]
        //Адрес первого элемента списка 
        private Goods head;
        [XmlIgnore]
        //Информационные поля
        private string name;        //название секции          
        [XmlIgnore]
        private int count = 0;      //кол-во товаров
        
        private Goods[] allGoods;
        //Конструкторы
        public SectionGoods(string newName = "no name")
        {
            name = newName;
        }
        public SectionGoods() { }
        
        //Методы доступа к информационным полям
        public int Count { get { return count; }  set { count = value; } }
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
        public Goods[] AllElements
        { 
            get
            {
                if (IsEmpty)
                {
                    Console.WriteLine("Список товаров пуст.");
                    return null;
                }
                else
                {
                    Goods[] allGoods = new Goods[count];
                    Goods current = head;
                    for (int i = 0; i < count; i++)
                    {
                        allGoods[i] = current;
                        current = current.Next;
                    }
                    return allGoods;
                }
            }
            set
            {
                allGoods = value;
            }
        }
        public int AddGoodAtPosition(int position)
        {
            if(position < 1)
            {
                Console.WriteLine("Некорректное место вставки. Число должно быть натуральным, то есть целым и положительным.");
                return -1;
            }
            else
            {
                //if (GetGoodByName("no name") != null)
                //{
                //    Console.WriteLine("Товар с именем \"no name\" уже существует. Невозможно создать вторую позицию с таким же именем.");
                //    return -1;
                //}
                //else
                //{
                    return AddToList("no name", 0, position);
                //}
            }

                      
        }

        public void DeleteGood(string name) 
        {
            Goods deleteGood = GetGoodByName(name);
            if (deleteGood != null)
            {
                if (Remove(deleteGood.Name))
                    Console.WriteLine($"Товар \"{name}\" успешно удален.");
                else
                    Console.WriteLine($"Неуспешное удаление товара \"{name}\".");
            }
            else
            {
                Console.WriteLine($"Товар \"{name}\" не найден. Удаление невозможно.");
            }                
        }
        public Goods GetGoodByName(string name)
        {
            return GetElementByName(name);
        }

        public double GetTotalSectionPrice()
        {
            Goods[] allGoods = this.AllElements;
            double totalPrice = 0;
            if(allGoods != null)
                foreach(Goods goods in allGoods)
                    totalPrice += goods.Price;
            
            return totalPrice;            
        }

        public int AddToList(string nm, double pr, int position)
        {
            Goods node = new Goods(nm, pr);
            Goods current = head;

            if (head == null)
            {
                head = node;
                head.Next = node;
                head.Previous = node;
                position = 1;
            }
            else
            {
                for (int i = 1; i <= position; i++)
                {
                    if (i == position || position > count)
                    {
                        node.Next = current;
                        node.Previous = current.Previous;
                        current.Previous.Next = node;
                        current.Previous = node;
                        
                        if (position == 1)
                            head = node;
                        else
                            if(position > count)
                                position = count + 1;

                        break;
                    }
                    else
                        current = current.Next;
                }
            }
            count++;
            return position;
            //return count;
        }

        // удаление элемента
        public bool Remove(string name)
        {
            Goods current = head;
            Goods removedItem = GetElementByName(name);

            if (removedItem == null)
            {
                return false;
            }
            else
            {
                // если удаляется единственный элемент списка
                if (count == 1)
                    head = null;
                else
                {
                    // если удаляется первый элемент
                    if (removedItem == head)
                    {
                        head = head.Next;
                    }
                    removedItem.Previous.Next = removedItem.Next;
                    removedItem.Next.Previous = removedItem.Previous;
                }
                count--;
                return true;
            }
        }

        public bool IsEmpty { get { return count == 0; } }

        public void Clear()
        {
            head = null;
            count = 0;
        }

        public Goods GetElementByName(string name)
        {
            Goods current = head;

            if (IsEmpty)
            {
                Console.WriteLine("Список товаров пуст.");
                return null;
            }
            else
            {
                do
                {
                    if (current.Name == name)
                        return current;

                    current = current.Next;
                }
                while (current != head || current != null);

                Console.WriteLine($"Товар с именем \"{name}\" не найден.");
                return null;
            }
        }

        public Goods GetElementByPosition(int position)
        {
            Goods current = head;
            if (IsEmpty)
            {
                Console.WriteLine("Список товаров пуст.");
                return null;
            }
            else
            {
                if(position < 1)
                {
                    Console.WriteLine("Неверно задана позиция.");
                    return null;
                }
                else
                {
                    int counter = 1;
                    do
                    {
                        if (counter == position)
                            return current;
                        counter++;
                        current = current.Next;
                    }
                    while ((current != head || current != null) && counter <= count);

                    Console.WriteLine($"Товар с именем \"{name}\" не найден.");
                    return null;
                }
            }
        }

        

        public void ShowAllGoods()
        {
            Goods[] allGoods = AllElements;

            if (allGoods != null)
                for (int i = 0; i < allGoods.Length; i++)
                    Console.WriteLine($"{i + 1}) {allGoods[i].Name}\t{allGoods[i].Price}\n");

        }

        public void Serialize()
        {
            XmlSerializer ser = new XmlSerializer(typeof(SectionGoods));
            string path = @"C:\Users\Mollusc\source\repos\MarketGoodsManager\MarketGoodsManager\bin\Debug\netcoreapp2.1\good.xml";

            //Goods[] goods = this.AllElements;
            File.WriteAllText(path, string.Empty);
            using (FileStream tw = new FileStream(path, FileMode.OpenOrCreate))
            {
                ser.Serialize(tw, this);
            }
        }

        public void Deserialize()
        {
            string path = @"C:\Users\Mollusc\source\repos\MarketGoodsManager\MarketGoodsManager\bin\Debug\netcoreapp2.1\good.xml";
            XmlSerializer ser = new XmlSerializer(typeof(SectionGoods));

            using (FileStream st = new FileStream(path, FileMode.Open))
            {
                //this.Name = "";
                //this.Count = 0;
                //this.AllElements;
                SectionGoods goods = (SectionGoods)ser.Deserialize(st);
                this.Count = goods.Count;
                this.Name = goods.Name;
                
                //if (goods.AllElements.Length > 0)
                //{
                //    for (int i = 0; i < goods.AllElements.Length; i++)
                //    {
                //        AddGoodAtPosition(i + 1);
                //        GetElementByPosition(i + 1).Name = goods.AllElements[i].Name;
                //        GetElementByPosition(i + 1).Price = goods.AllElements[i].Price;
                //    }
                //}
                //GetElementByPosition(2).Name = "WOW 2";
            }
        }
    }
}
