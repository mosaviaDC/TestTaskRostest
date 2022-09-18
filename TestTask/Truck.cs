using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace takenProductsTask
{
    internal class Truck
    {

        public int TruckCapacity { get; private set; }

        public delegate void Unload(object sender, NotifyEventArgs e);

        public event Unload OnTruckGetProductsFromStorage;
        public string TruckName { get;  private set; }
        public Truck(int TruckCapacity,string TruckName)
        {
            this.TruckName = TruckName;
            this.TruckCapacity = TruckCapacity;
        }


        public void PickUpProducts(Storage storage)
        {
            
            Thread.Sleep(2000); // Имитация работы
            lock (storage)
            {
                if (TruckCapacity < storage.StorageProducts.Count) 
                {
                    var takenProducts = storage.StorageProducts.Take(TruckCapacity).ToList();
                   
                        var ProductsTypeA = takenProducts?.Where(p => p.ProductName.Contains("A")).ToList();
                        var ProductsTypeB = takenProducts?.Where(p => p.ProductName.Contains("B")).ToList();
                        var ProductsTypeC = takenProducts?.Where(p => p.ProductName.Contains("C")).ToList();
                        var ProductsTypeD = takenProducts?.Where(p => p.ProductName.Contains("D")).ToList();
                        storage.StorageProducts.RemoveRange(0, TruckCapacity);
                        OnTruckGetProductsFromStorage?.Invoke(this, new NotifyEventArgs(TruckCapacity, $"{TruckName} забрал со склада {TruckCapacity} продукции из них:\n" +
                            $"Продукции A - {ProductsTypeA.Count};\n" +
                            $"Продукции B - {ProductsTypeB.Count};\n" +
                            $"Продукции С - {ProductsTypeC.Count}.\n" +
                            $"Продукции D - {ProductsTypeD.Count}.\n"
                            ));
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
    }
}
