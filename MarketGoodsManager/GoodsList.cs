using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http.Headers;
using System.Text;

namespace MarketGoodsManager
{
    [Serializable]
    class GoodsList
    {
       
        public class TwoLinkedList          //Вложенный класс Элемент списка
        {
            //Поля
            public Goods Data { get; set; }                 //Тип поля данных элемента списка
            public TwoLinkedList Next { get; set; }
            public TwoLinkedList Previous { get; set; }

            //Конструкторы
            public TwoLinkedList(Goods data)
            {
                Data = data;
            }

            public TwoLinkedList() { }

        }
                       
        private TwoLinkedList head;
        int count = 0;
        
        public int Add(Goods data)
        {
            TwoLinkedList node = new TwoLinkedList(data);

            if (head == null)
            {
                head = node;
                head.Next = node;
                head.Previous = node;
            }
            else
            {
                node.Previous = head.Previous;
                node.Next = head;
                head.Previous.Next = node;
                head.Previous = node;
            }
            return ++count;
            //return count;
        }

        // удаление элемента
        public bool Remove(string name)
        {
            TwoLinkedList current = head;
            TwoLinkedList removedItem = GetListItemByName(name);

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
        public int Count { get { return count; } }
        public bool IsEmpty { get { return count == 0; } }

        public void Clear()
        {
            head = null;
            count = 0;
        }

        public Goods GetElementByName(string name)
        {
            TwoLinkedList current = head;

            if (IsEmpty)
            {
                Console.WriteLine("Список товаров пуст.");
                return null;
            }
            else
            {
                do
                {
                    if (current.Data.Name == name) 
                        return current.Data;
                
                    current = current.Next;
                }
                while (current != head || current != null);
                
                Console.WriteLine($"Товар с именем \"{name}\" не найден.");
                return null;
            }
        }  
        
        public TwoLinkedList GetListItemByName(string name)
        {
            TwoLinkedList current = head;

            if (IsEmpty)
            {
                return null;
            }
            else
            {
                do
                {
                    if (current.Data.Name == name)
                        return current;

                    current = current.Next;
                }
                while (current != head || current != null);

                return null;
            }
        }

        public Goods[] GetAllElements()
        {
            if(IsEmpty)
            {
                Console.WriteLine("Список товаров пуст.");
                return null;
            }
            else
            {
                Goods[] allGoods = new Goods[count];
                TwoLinkedList current = head;
                for(int i = 0; i < count; i++)
                {
                    allGoods[i] = current.Data;
                    current = current.Next;
                }
                return allGoods;
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
