using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace takenProductsTask
{
    internal class Storage
    {
        public int Capacity { get; private set; }
        public List<Product> StorageProducts { get; set; }
        /// <summary>
        /// Фабрики, работающие со складом
        /// </summary>
        private Factory[] StorageFactories;
        /// <summary>
        /// Грузовики, работающие со складом
        /// </summary>
        private Truck[] StorageTrucks;

        private readonly CancellationToken cancellationToken;
        public Storage(int M,Factory[] factories, Truck[] trucks, CancellationTokenSource cancellationToken) 
        {
            int factoriesEfficiencies = 0;
            foreach (var p in factories)
            {
                factoriesEfficiencies += (int) p.Efficiency;
            }
            this.Capacity = M * factoriesEfficiencies;
            this.StorageFactories = factories;
            this.StorageTrucks = trucks;
            foreach (var factory in factories)
            {
                factory.OnFactoryProduceProducts += Storage_OnFactoryProduceProducts;
            }
            foreach(var truck in trucks)
            {
                truck.OnTruckGetProductsFromStorage += Truck_OnTruckGetProductsFromStorage;
            }
            this.cancellationToken = cancellationToken.Token;
            this.StorageProducts = new List<Product>(Capacity);
            Console.WriteLine($"Вместимость склада {Capacity} у.е.");
        
        }

        private void Truck_OnTruckGetProductsFromStorage(object sender, NotifyEventArgs e)
        {
            Console.WriteLine($"{e.Message}");

        }

        private void Storage_OnFactoryProduceProducts(object sender, NotifyEventArgs e)
        {
            Console.WriteLine($"{e.Message}");

            if (StorageProducts.Count >= Capacity * 0.95)
            {
                foreach (var truck in StorageTrucks)
                {
                    new Task(() => { truck.PickUpProducts(this);}).Start();
                }
            }
           
        }

        public void Work()
        {
                     foreach (var factories in StorageFactories)
                        new Task(() =>
                        {
                            while (!cancellationToken.IsCancellationRequested)
                                factories.Produce(this);
                        },cancellationToken)
                        .Start();
        }
    }
}
