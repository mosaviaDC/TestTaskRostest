using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace takenProductsTask
{
    internal class Factory
    {
        public Product FactoryProduct { get;  private set; }
        public string FactoryName { get; private set; }
        public float Efficiency { get; private set; }

        public delegate void ProduceHandler(object sender, NotifyEventArgs e);

        public event ProduceHandler OnFactoryProduceProducts;
        
        public Factory(string FactoryName, float koef,Product product)
        {
            this.Efficiency = koef;
            this.FactoryName = FactoryName;
            this.FactoryProduct = product;
        }

        public void Produce(Storage storage)
        {
            Thread.Sleep(100); //имитация работы
            lock (storage)
            {
                for (int i = 0; i < (int)Efficiency; i++)
                   storage.StorageProducts.Add(FactoryProduct);
            }
            OnFactoryProduceProducts?.Invoke(this, new NotifyEventArgs(Efficiency, $"{ FactoryName } произвела {(int) Efficiency} продукции"));
        }
    }
}
