using System;

namespace MarketGoodsManager
{
    //struct GoodsList
    //{

    //}
    class Program
    {
        
        static void Main(string[] args)
        {


            Supermarket sp = new Supermarket(56);
            sp.AddNewSection("Бытовая химия");
            sp.Name = "Пятерочка";
            sp.AddNewSection("Бытовая химия");
            sp.AddNewSection("Бытовая");
            

            SectionGoods section_1 = sp.GetSection("Бытовая химия");
            
            if (section_1 != null)
            {
                section_1.GetTotalSectionPrice();
                section_1.AppendGood();
            }
            else
                section_1.AppendGood();

            section_1.ShowAllGoods();

            Console.WriteLine("Hello World!");
        }
    }
}
