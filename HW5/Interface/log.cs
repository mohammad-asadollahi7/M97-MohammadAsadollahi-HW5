using HW5.Domain;
using HW5.Interface.Dto;
using System.Diagnostics;

namespace HW5.Interface
{
    public class Log
    {
        string? txtFilePath;
        public Log()
        {
            string? projectPath = Directory.GetParent
                                  (AppDomain.CurrentDomain.BaseDirectory)?
                                 .Parent?.Parent?.Parent?.FullName;
            txtFilePath = Path.Combine(projectPath, $"DataBase/log.txt");
        }

        public void Logger(Stock productInStock, Stock existProduct)
        {
            using (TextWriter tw = File.AppendText(txtFilePath))
            {
                tw.WriteLine("Time: {0}", DateTime.Now);
                tw.WriteLine($"{productInStock.ProductQuantity} " +
                             $"items of {productInStock.Name}" +
                             $" product were bought for " +
                             $"${productInStock.ProductPrice}");
                tw.WriteLine("Current stock: {0}\tCurrent price: {1}",
                              existProduct.ProductQuantity,
                              existProduct.ProductPrice);
                tw.WriteLine("---------------------------------");
            }
        }

        public void Logger(Stock productInStock, int cnt)
        {
            using (TextWriter tw = File.AppendText(txtFilePath))
            {
                tw.WriteLine("Time: {0}", DateTime.Now);
                tw.WriteLine($"{cnt} " +
                             $"items of {productInStock.Name}" +
                             $" product were sold.");
                tw.WriteLine("Current stock: {0}",
                             productInStock.ProductQuantity);
                tw.WriteLine("---------------------------------");
            }
        }
    }
}
