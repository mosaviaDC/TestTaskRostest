using System;
using System.Collections.Generic;
using System.Text;

namespace takenProductsTask
{
    class NotifyEventArgs
    {
        public float ProductCount { get; set; }

        public string Message { get; set; }

        public NotifyEventArgs(float ProductCount,string Message) 
        {
            this.ProductCount = ProductCount;
            this.Message = Message;
        }

    }
}
