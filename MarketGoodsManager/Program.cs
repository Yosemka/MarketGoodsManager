using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;
using System.Linq;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;

namespace MarketGoodsManager
{
    struct Menu
    {
        public int level;
        public int index;
        public Menu(int lvl, int idx)
        {
            level = lvl;
            index = idx;
        }
    };
    enum MenuLevel : byte
    {
        Begin,
        Create,
        SupermarketEdit,
        SectionCollection,
        SectionEdit,
        GoodsMenu
    }
    class Program
    {
        static string supermarketXmlPath = @"C:\Users\Mollusc\source\repos\MarketGoodsManager\MarketGoodsManager\bin\Debug\netcoreapp2.1\xml";
        static string supermarketXmlName;
        static string sectionXmlName;
        
        //Уровень меню 1
        static Menu openSupermarketFile = new Menu(1, 0);
        static Menu createSupermarketFile = new Menu(1, 1);
        //Уровень меню 2
        static Menu showAllSections = new Menu(2, 0);
        static Menu addNewSection = new Menu(2, 1);
        static Menu deleteSection = new Menu(2, 2);
        static Menu serializeSupermarket = new Menu(2, 3);
        static Menu deserializeSupermarket = new Menu(2, 4);
        static Menu changeSupermarketName = new Menu(2, 5);
        //Уровень меню 4
        static Menu showAllGoods = new Menu(4, 0);
        static Menu addNewGoods = new Menu(4, 1);
        static Menu serializeSection = new Menu(4, 2);
        static Menu deserializeSection = new Menu(4, 3);
        static Menu changeSectionName = new Menu(4, 4);
        //Уровень меню 6
        static Menu changeGoodName = new Menu(6, 0);
        static Menu changeGoodPrice = new Menu(6, 1);
        static Menu deleteGood = new Menu(6, 2);
        static Menu serializeGood = new Menu(6, 3);
        static Menu deserializeGood = new Menu(6, 4);

        static Menu[] exitProgram = new Menu[5];
        static Menu currentMenu = new Menu(0, 0);
        static int currentSection;
        static string[][] menu = new string[7][];
        
        static Supermarket mainMarket;
        
        //  ИНИЦИАЛИЗАЦИЯ ПЕРЕМЕННЫХ  //
        static void MenuInit()
        {
            menu[0] = new string[] { "Начать работу", "Выход" };
            menu[1] = new string[] { "Открыть существующий файл с данными супермаркета", "Создать новый супермаркет", "Выйти" };
            menu[2] = new string[] { "Показать все отделы", "Добавить отдел", "Удалить последний отдел", "Сохранить данные о супермаркете в файл", "Выгрузить данные о супермаркете из файла", "Изменить название супермаркета", "Выйти" };
            menu[3] = new string[] { }; //здесь будет список отделов
            menu[4] = new string[] { "Показать все товары", "Добавить товар", "Сохранить данные об отделе в файл", "Выгрузить данные об отделе из файла", "Изменить название секции", "Выйти" };
            menu[5] = new string[] { }; //здесь будет список товаров
            menu[6] = new string[] { "Изменить название", "Изменить цену", "Удалить товар", "Сохранить данные о товаре в файл", "Выгрузить данные о товаре из файла", "Выйти" };

            exitProgram[0].level = 0;   exitProgram[0].index = 1;
            exitProgram[1].level = 1;   exitProgram[1].index = 2;
            exitProgram[2].level = 2;   exitProgram[2].index = 6; 
            //exitProgram[3].level = 3;   exitProgram[3].index = 5;            
            exitProgram[4].level = 4;   exitProgram[4].index = 5;            
            exitProgram[6].level = 6;   exitProgram[6].index = 5;            
        }
        
        //  СОЗДАНИЕ НОВОГО СУПЕРМАРКЕТА  //
        static void CreateSupermarketMenu()
        {
            Console.WriteLine("Введите название супермаркета и количество отделов\nНазвание: ");
            string name = Console.ReadLine();
            int amount;
            Console.WriteLine("Количество отделов");
            while (!int.TryParse(Console.ReadLine(), out amount))
            {                
                Console.MoveBufferArea(0, Console.CursorTop - 2, Console.BufferWidth, 1, Console.BufferWidth, Console.CursorTop - 2, ' ', Console.ForegroundColor, Console.BackgroundColor);
                Console.MoveBufferArea(0, Console.CursorTop - 1, Console.BufferWidth, 1, Console.BufferWidth, Console.CursorTop - 1, ' ', Console.ForegroundColor, Console.BackgroundColor);
                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 2);
                Console.WriteLine("Неверное значение. Введите количество отделов.");
            }
            mainMarket = new Supermarket(amount);
            mainMarket.Name = name;
        }
        //  ДОБАВИТЬ СЕКЦИЮ  //
        static void AddSectionMenu()
        {
            if (!mainMarket.IsQueueFull())
            {
                Console.WriteLine("\nВведите название новой секции: ");
                mainMarket.AddNewSection(Console.ReadLine());
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Список заполнен. Нет свободного места для добавления секции.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        //  ДЕСЕРИАЛИЗАЦИЯ СУПЕРМАРКЕТА  //
        static void DeserializeSupermarketMenu()
        {
            Console.WriteLine("Введите полный путь к файлу: ");
            mainMarket = new Supermarket();
            if (Console.ReadKey(intercept: true).Key != ConsoleKey.Escape)
            {
                mainMarket.Deserialize(Console.ReadLine());
                
            }
            else
            {
                Console.MoveBufferArea(0, Console.CursorTop - 2, Console.BufferWidth, 1, Console.BufferWidth, Console.CursorTop - 2, ' ', Console.ForegroundColor, Console.BackgroundColor);
                Console.MoveBufferArea(0, Console.CursorTop - 1, Console.BufferWidth, 1, Console.BufferWidth, Console.CursorTop - 1, ' ', Console.ForegroundColor, Console.BackgroundColor);
                currentMenu.level--;
                currentMenu.index = 0;
            }
        }
        //  СЕРИАЛИЗАЦИЯ СУПЕРМАРКЕТА  //
        static void SerializeSupermarketMenu()
        {
            Console.WriteLine("Информация о супермаркете сохранена в файле " + mainMarket.Serialize());
        }
        //  ДОБАВИТЬ ТОВАР  //
        static void AddGoodMenu()
        {
            SectionGoods section = mainMarket.AllExistSections[currentSection];
            Console.WriteLine("\nВведите номер позиции товара: ");
            int position, realPosition;
            double price;
            while (!int.TryParse(Console.ReadLine(), out position))
            {
                ClearConsoleLines(2); 
                Console.WriteLine("Неверное значение. Введите порядковый номер позиции, в которую нужно вставить товар.");
            }
            realPosition = section.AddGoodAtPosition(position);
            Console.WriteLine("Введите название товара: ");
            section.GetElementByPosition(realPosition).Name = Console.ReadLine();
            Console.WriteLine("Введите цену товара: ");
            while (!double.TryParse(Console.ReadLine(), out price) && price >= 0)
            {
                ClearConsoleLines(2);
                Console.WriteLine("Неверное значение. Введите положительное число.");
            }
            section.GetElementByPosition(realPosition).Price = price;
            ClearConsoleLines(6);
            //Console.ForegroundColor = ConsoleColor.Red;
            //Console.WriteLine("Список заполнен. Нет свободного места для добавления секции.");
            //Console.ForegroundColor = ConsoleColor.White;

        }
        static void ClearConsoleLines(int linesAmount)
        {
            for(int i = 1; i <= linesAmount; i++)
                Console.MoveBufferArea(0, Console.CursorTop - i, Console.BufferWidth, 1, Console.BufferWidth, Console.CursorTop - i, ' ', Console.ForegroundColor, Console.BackgroundColor);

            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - linesAmount);
        }
        //  МЕНЮ УРОВНЯ 1  //
        static bool NeedRefreshCreateMenu()
        {
            //Открыть файл с информацией
            if (currentMenu.Equals(openSupermarketFile))
            {
                DeserializeSupermarketMenu();
                return true;
            }
            else //Создать новый супермаркет
            if (currentMenu.Equals(createSupermarketFile))
            {
                CreateSupermarketMenu();
                return true;
            }
            else return false;
        }

        //  МЕНЮ УРОВНЯ 2  //
        static bool NeedRefreshSupermarketEditMenu()
        {
            if (currentMenu.Equals(showAllSections))
            {
                return true;
            }
            else if (currentMenu.Equals(addNewSection))
            {
                AddSectionMenu();
                return false;
            }
            else if (currentMenu.Equals(deleteSection))
            {
                mainMarket.DeleteSectionAndGoods();
                return true;
            }
            else if (currentMenu.Equals(serializeSupermarket))
            {
                SerializeSupermarketMenu();
                return true;
            }
            else if (currentMenu.Equals(deserializeSupermarket))
            {
                DeserializeSupermarketMenu();
                return true;
            }
            else return false;
        }
        static bool NeedRefreshSectionEditMenu()
        {
            if (currentMenu.Equals(showAllGoods))
            {
                Console.WriteLine(mainMarket.AllExistSections[currentSection].ToString());
                return false;
            }
            else if (currentMenu.Equals(addNewGoods))
            {
                AddGoodMenu();
                return false;
            }
            else if (currentMenu.Equals(deleteGoods))
            {
                mainMarket.DeleteSectionAndGoods();
                return false;
            }
            else if (currentMenu.Equals(serializeSection))
            {
                SerializeSupermarketMenu();
                return false;
            }
            else if (currentMenu.Equals(deserializeSection))
            {
                DeserializeSupermarketMenu();
                return false;
            }
            else return false;
        }
        static bool IsNeedChangeMenuLevel()
        {        //Закрыть программу
            if(exitProgram.Contains<Menu>(currentMenu))  //Выбран ли вариант выхода из программы
            {
                Environment.Exit(0);        
            }
            else 
                switch(currentMenu.level)
                {
                    case (int)MenuLevel.Create: return NeedRefreshCreateMenu();
                    case (int)MenuLevel.SupermarketEdit: return NeedRefreshSupermarketEditMenu();
                    case (int)MenuLevel.SectionCollection: break;
                    case (int)MenuLevel.SectionEdit: return NeedRefreshSectionEditMenu();
                    case (int)MenuLevel.GoodsMenu: break;
                }
            return true;
        }      
        static void ShowMenu(int level, int index)
        {
            Console.Clear();
            if(currentMenu.level == (int)MenuLevel.SupermarketEdit)
            {
                Console.WriteLine($"Супермаркет {mainMarket.Name}. Количество существующих отделов {mainMarket.SectionAmount}." +
                    $"Максимально возможное число секций {mainMarket.QueueSize}\n");
            }
            else if(currentMenu.level == (int)MenuLevel.SectionCollection)
            {
                menu[3] = new string[mainMarket.SectionAmount];
                for(int i = 0; i < mainMarket.SectionAmount; i++)
                {
                    menu[3][i] = mainMarket.AllExistSections[i].Name + "\t\t" + mainMarket.AllExistSections[i].Count;
                }
                Console.WriteLine("Список существующих секций.\nНазвание секции  |  Кол-во товаров в секции.\n" +
                    "Выберите нужную секцию для просмотра и редактирвоания.");
            }
            else if(currentMenu.level == (int)MenuLevel.SectionEdit)
            {
                Console.WriteLine($"Секция: {mainMarket.AllExistSections[currentSection].Name}. " +
                    $"Количество товаров: {mainMarket.AllExistSections[currentSection].Count}\n");
            }
            for (int i = 0; i < menu[level].Length; i++)
            {                
                if (index == i)
                    Console.WriteLine($">> {menu[level][i]}");
                else
                    Console.WriteLine($"   {menu[level][i]}");
            }
        }
        static void ReadUserKey()
        {
            switch(Console.ReadKey(intercept: true).Key)
            {
                case ConsoleKey.Enter:
                    {
                        if(IsNeedChangeMenuLevel())
                        {
                            if (currentMenu.level == (int)MenuLevel.SectionCollection)
                                currentSection = currentMenu.index;
                            
                            if (currentMenu.level < menu.Length - 1)//увеличение уровня (глубины) меню
                                currentMenu.level++;
                            else
                                currentMenu.level = 0;
                            currentMenu.index = 0;       //обновление индекс пункта меню, если это не индекс секции
                            ShowMenu(currentMenu.level, currentMenu.index);
                        }
                    }
                    break;
                case ConsoleKey.Escape:
                    {
                        if (currentMenu.level == 0)
                            currentMenu.level = menu.Length - 1;
                        else
                            currentMenu.level--;
                        currentMenu.index = 0;
                        ShowMenu(currentMenu.level, currentMenu.index);
                    }
                    break;
                case ConsoleKey.DownArrow:
                    {
                        if (currentMenu.index < menu[currentMenu.level].Length - 1)
                            currentMenu.index++;
                        else
                            currentMenu.index = 0;
                        ShowMenu(currentMenu.level, currentMenu.index);
                    }
                    break;
                case ConsoleKey.UpArrow:
                    {
                        if (currentMenu.index == 0)
                            currentMenu.index = menu[currentMenu.level].Length - 1;
                        else
                            currentMenu.index--;
                        ShowMenu(currentMenu.level, currentMenu.index);
                    }
                    break;      
                    
            }
        }
        static void Main(string[] args)
        {
            MenuInit();
            Supermarket sp = new Supermarket();

            sp.AddNewSection("бытовая химия");
            sp.Name = "пятерочка";
            sp.AddNewSection("бытовая химия");
            sp.AddNewSection("бытовая");
            sp.Serialize();

            ShowMenu(0, 0);
            while (true)
            {
                ReadUserKey();
            }


            //Supermarket sptry = GetFiles();
            //Console.WriteLine(sptry.ToString());



            //Goods serGood = new Goods("chocolate", 42.3);
            //Console.WriteLine(serGood.ToString());
            //Stream goodStream = File.Open("Good.dat", FileMode.Create);
            //BinaryFormatter binary = new BinaryFormatter();
            //binary.Serialize(goodStream, serGood);
            //goodStream.Close();
            //serGood = null;

            //goodStream = File.Open("Good.dat", FileMode.Open);
            //binary = new BinaryFormatter();
            //serGood = (Goods)binary.Deserialize(goodStream);
            //goodStream.Close();
            //Console.WriteLine("Результат десериализации: " + serGood.ToString());


            //Console.WriteLine("");

            ////Console.WriteLine("Супермаркет \"{}\"", );
            ////cout << "Добавить ";

            //Supermarket sp = new Supermarket(6);

            //sp.AddNewSection("бытовая химия");
            //sp.Name = "пятерочка";
            //sp.AddNewSection("бытовая химия");
            //sp.AddNewSection("бытовая");

            
            //sp.Deserialize(sp.Serialize());
            //Console.WriteLine(sp.ToString());
            //SectionGoods section_1 = sp.GetSection("Бытовая химия");

            //if (section_1 != null)
            //{
            //    section_1.GetTotalSectionPrice();
            //    section_1.AddGoodAtPosition(12);
            //}

            //section_1.GetElementByPosition(section_1.AddGoodAtPosition(4)).Name = "Thanks";
            //section_1.AddGoodAtPosition(4);

            //section_1.GetElementByPosition(section_1.AddGoodAtPosition(4)).Name = "Хамелеон";
            //section_1.GetElementByPosition(section_1.AddGoodAtPosition(5)).Name = "Усургут";
            //Console.WriteLine(section_1.ToString()); 
            //section_1.DeleteGood("Thanks");
            ////section_1.ShowAllGoods();
            //section_1.GetElementByPosition(section_1.AddGoodAtPosition(1)).Name = "WOW";
            //Console.WriteLine(section_1.ToString());

            //section_1.Serialize();
            //section_1.Deserialize();
            //section_1.ToString();

            //sp.Serialize();
            //sp.Deserialize();
            //Console.WriteLine(sp.ToString());

            Console.WriteLine("Hello World!");
        } 
        static Supermarket GetFiles()
        {
            if(Directory.Exists(supermarketXmlPath))
            {
                //DirectoryInfo dirInfo= new DirectoryInfo(supermarketXmlPath);
                string[] filesFullPath = Directory.GetFiles(supermarketXmlPath);
                
                Supermarket supermarket = new Supermarket();
                if(filesFullPath.Length > 0)
                {
                    string fileName;
                    foreach (string st in filesFullPath)
                    {
                        fileName = Path.GetFileName(st);

                        if (fileName.Contains("supermarket", StringComparison.OrdinalIgnoreCase))
                        {
                            supermarket.Deserialize(supermarketXmlPath + "\\" + fileName);
                            return supermarket;
                        }                      
                    
                    }
                    return null;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                Directory.CreateDirectory(supermarketXmlPath);
                return null;
            }
            
        }

        
    }

  
}
