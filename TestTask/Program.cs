using System;
using System.Threading;

namespace takenProductsTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource StorageWorkCancellationTokenSource = new CancellationTokenSource();
            int n = 150;
            int M = 300;
            Factory factoryA = new Factory("Фабрика A",n, new Product("Продукция A", 5, "Упоковка типа А"));
            Factory factoryB = new Factory("Фабрика B", n*1.1f, new Product("Продукция B", 15, "Упоковка типа B"));
            Factory factoryC = new Factory("Фабрика C", n*1.2f, new Product("Продукция C", 2, "Упоковка типа С"));
            Factory factoryD = new Factory("Фабрика D", n * 1.3f, new Product("Продукция D", 5, "Упоковка типа D"));
            Storage storage = new Storage( M, new Factory[]
            {
                factoryA,
                factoryB,
                factoryC,
                factoryD
            }, new Truck[] { 
                new Truck(50, "Первый грузовик"),
                new Truck(100, "Второй грузовик"),
                new Truck(150, "Третий грузовик"),
            }, StorageWorkCancellationTokenSource);
            storage.Work();
            var key = Console.ReadKey();


        }
    }
}
