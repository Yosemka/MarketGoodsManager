using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace MarketGoodsManager
{
    [Serializable]
    public class Supermarket
    {
        private string name;
        static private int sectionAmount;
        private const string defaultPath = @"C:\Users\Mollusc\source\repos\MarketGoodsManager\MarketGoodsManager\bin\Debug\netcoreapp2.1\";

        private static int size;
        private SectionGoods[] sectionsQueue;

        private int front = -1;
        private int rear = -1;
        private const int QUEUE_EMPTY_CONST = -2;
        private const int QUEUE_FULL_CONST = -1;

        public Supermarket()
        {
            name = "no name";
            sectionAmount = 0;
            if (size <= 0)
                size = 10;
            sectionsQueue = new SectionGoods[size];
        }

        public Supermarket(int sz)
        {
            name = "no name";
            sectionAmount = 0;
            if (sz > 0)
                size = sz;
            else
                size = 1;
            sectionsQueue = new SectionGoods[size];
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
        public int SectionAmount
        {
            get
            {
                return sectionAmount;
            }
            set
            {
                sectionAmount = value;
            }
        }
        public int QueueSize
        {
            get { return size; }
            set { size = value; }
        }
        [XmlArrayItem("SectionName")]
        public SectionGoods[] AllExistSections
        {
            get
            {
                SectionGoods[] sections = new SectionGoods[sectionAmount];
                if (!IsQueueEmpty())
                {
                    for (int i = front; i <= rear; i++)
                        sections[i - front] = sectionsQueue[i];
                    return sections;
                }
                else return null;
            }
            set
            {
                sectionAmount = 0;
                for (int i = 0; i < value.Length; i++)
                {
                    AddInQueue(value[i]);
                }
            }
        }
        public void AddNewSection(string sectionName)
        {
            if (FindSection(sectionName) != QUEUE_EMPTY_CONST)
            {
                Console.WriteLine($"Создание секции с повторным названием \"{sectionName}\" невозможно. Введите другое имя.");
            }
            else
            {
                int newSectionConst = AddInQueue(new SectionGoods(sectionName));
                if (newSectionConst != QUEUE_FULL_CONST)
                {
                    Console.WriteLine($"Отдел \"{sectionsQueue[newSectionConst].Name}\" создан успешно.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Список заполнен. Нет свободного места для добавления секции.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        public void DeleteSectionAndGoods()
        {
            string deletedSectionName = DeleteFromQueue();

            if (deletedSectionName == null)
                Console.WriteLine("В списке нет отделов. Удаление несуществующих элементов невозможно.");
            else
                Console.WriteLine($"Отдел \"{deletedSectionName/*sectionsQueue[deletedSectionName + 1].Name*/}\" и список товаров отдела успешно удалены.");
        }

        public SectionGoods GetSection(string sectionName)
        {
            int tmp = FindSection(sectionName);
            if (tmp == QUEUE_EMPTY_CONST)
            {
                Console.WriteLine($"Секция \"{sectionName}\" не найдена.");
                return null;
            }
            else
            {
                return sectionsQueue[tmp];
            }
        }

        //Функции для работы со списком
        private int AddInQueue(SectionGoods section)
        {
            if (IsQueueFull())
            {
                return -1;
            }
            else
            {
                if (IsQueueEmpty())
                    front = rear = 0;
                else
                    rear++;

                sectionsQueue[rear] = section;
                sectionAmount++;
                return rear;
            }
        }
        private string DeleteFromQueue()
        {
            if (IsQueueEmpty())
            {
                return null;//QUEUE_EMPTY_CONST;
            }
            else
            {
                String tmpSectionName = sectionsQueue[front].Name;
                sectionsQueue[front] = null;
                if (IsFrontOnTheFirstRignElement())
                {
                    front = -1;
                    rear = -1;
                }
                else if (IsFrontOnTheLastRignElement())
                {
                    front = 0;
                }
                else
                {
                    front++;
                }
                sectionAmount--;
                return tmpSectionName;//front;
            }
        }

        public void Clear()
        {
            for(int i = 0; i < sectionsQueue.Length; i++)
            {
                sectionsQueue[i] = null;
            }
            front = rear = -1;
            SectionAmount = 0;
        }
        //Вспомогательные/проверочные функции
        private int FindSection(string nameToFind)
        {
            //foreach(SectionGoods sec in sectionsQueue)
            for (int i = 0; i <= rear; i++)
            {
                if (sectionsQueue[i].Name == nameToFind)
                    return i;
            }
            return QUEUE_EMPTY_CONST;
        }
        private bool IsQueueEmpty()
        {
            if (front == -1 && rear == -1)
                return true;
            else
                return false;
        }
        public bool IsQueueFull()
        {
            if ((front == 0 && rear == size - 1) || rear == front - 1)
                return true;
            else
                return false;
        }
        private bool IsFrontOnTheLastRignElement()
        {
            if (front == size - 1)
                return true;
            else
                return false;
        }
        private bool IsFrontOnTheFirstRignElement()
        {
            if (front == rear)
                return true;
            else
                return false;
        }
        public override string ToString()
        {
            string str = string.Empty;
            if (!IsQueueEmpty())
            {
                foreach (SectionGoods section in AllExistSections)
                    str += section.ToString();
            }
            else
                str += "Нет существующих секций.";
            return str;
        }
        public string Serialize()
        {
            string path = defaultPath + this.Name + ".xml";
            //XmlSerializer ser = new XmlSerializer(typeof(SectionGoods[]));
            XmlSerializer ser = new XmlSerializer(typeof(Supermarket));
            File.WriteAllText(path, string.Empty);

            using(FileStream st = new FileStream(path, FileMode.OpenOrCreate))
            {
                ser.Serialize(st, this);
            }
            return path;
        }
        public void Deserialize(string path = defaultPath + "supermarket.xml")
        {
            //XmlSerializer ser = new XmlSerializer(typeof(SectionGoods[]));
            XmlSerializer ser = new XmlSerializer(typeof(Supermarket));
            
            using(FileStream st = new FileStream(path, FileMode.OpenOrCreate))
            {
                //SectionGoods[] super = (SectionGoods[])ser.Deserialize(st);
                Supermarket sp = (Supermarket)ser.Deserialize(st);
                
                this.Name = sp.Name;
                this.SectionAmount = sp.SectionAmount;
                this.front = sp.front;
                this.rear = sp.rear;
                Array.Resize<SectionGoods>(ref this.sectionsQueue, sp.QueueSize);
                for(int i = 0; i < sp.sectionsQueue.Length; i++)
                {
                    this.sectionsQueue[i] = sp.sectionsQueue[i];
                }
                //if (super.Length > 0)
                //{
                //    
                //    this.Name = "NAME";
                //   
                //}
                //if(SectionAmount > 0)
                //{
                //    front = front + SectionAmount;
                //    rear = front + SectionAmount;
                //}

            }
        }

        //public Supermarket(SerializationInfo info, StreamingContext context)
        //{
        //    name = (string)info.GetValue("Name", typeof(string));
        //    sectionAmount = (int)info.GetValue("GoodsAmount", typeof(int));
        //    //sectionsQueue[] = (SectionGoods)info.GetValue("GoodsList", typeof(SectionGoods));
        //}

        //public void GetObjectData(SerializationInfo info, StreamingContext context)
        //{
        //    info.AddValue("Name", name);
        //    info.AddValue("GoodsAmount", sectionAmount);
        //    //info.AddValue("GoodsList", sectionsQueue);
        //}
    }
}

