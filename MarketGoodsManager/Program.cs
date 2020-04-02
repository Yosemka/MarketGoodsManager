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
            sp.DeleteSectionAndGoods();
            sp.DeleteSectionAndGoods();
            sp.DeleteSectionAndGoods();

            SectionGoods section_1 = sp.GetSection("Бытовая химия");
            section_1.AddGood();

            Console.WriteLine("Hello World!");
        }
    }
}
