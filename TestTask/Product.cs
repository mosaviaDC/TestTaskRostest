using System;
using System.Collections.Generic;
using System.Text;

namespace takenProductsTask
{
     class Product
    {
        public string ProductName { get; private set; }
        public int ProductWeight { get; private set; }
        public string ProductPackageType { get; private set; }

        public Product(string ProductName, int ProductWeight, string ProductPackageType)
        {
            this.ProductPackageType = ProductPackageType;
            this.ProductWeight = ProductWeight; 
            this.ProductName = ProductName;
        }
    }
}
