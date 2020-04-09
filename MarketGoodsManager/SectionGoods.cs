using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketGoodsManager
{
    [Serializable]
    class SectionGoods
    {
        //Адрес первого элемента списка 
        private Goods head;

        //Информационные поля
        private string name;        //название секции          
        private int count = 0;      //кол-во товаров

        //Конструкторы
        public SectionGoods(string newName = "no name")
        {
            name = newName;
        }
        public SectionGoods() { }
        
        //Методы доступа к информационным полям
        public int Count { get { return count; } }
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

        public void AddGoodAtPosition(int position)
        {
            if(position < 1)
            {
                Console.WriteLine("Некорректное место вставки. Число должно быть натуральным, то есть целым и положительным.");
            }
            else
            {
                if (GetGoodByName("no name") != null)
                {
                    Console.WriteLine("Товар с именем \"no name\" уже существует. Невозможно создать вторую позицию с таким же именем.");
                }
                else
                {
                    AddToList("no name", 0, position);
                }
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
            Goods[] allGoods = GetAllElements();
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

                        break;
                    }
                    else
                        current = current.Next;
                }
            }
            return ++count;
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

        public Goods[] GetAllElements()
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

        public void ShowAllGoods()
        {
            Goods[] allGoods = GetAllElements();

            if (allGoods != null)
                for (int i = 0; i < allGoods.Length; i++)
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
