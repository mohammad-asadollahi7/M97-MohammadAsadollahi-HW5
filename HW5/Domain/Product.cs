

using IronBarCode;

namespace HW5.Domain
{
    public class Product
    {
        public int Id { get; set; }

        public BarcodeResult BarCode { get; set; }
        
        public string Name { get; set; }



    }
}
