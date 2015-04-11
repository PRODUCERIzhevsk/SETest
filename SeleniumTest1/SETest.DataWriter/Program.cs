using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using SETest.Data;

namespace SETest.DataWriter
{
    public class Program
    {
        static void Main(string[] args)
        {

            double a = 1.1;
            double b = 1.0;

            Console.WriteLine(a-b);
            Console.ReadKey();

            /*
            Invoice invoice = new Invoice
            {
                IdSender = 1,
                NameSender = "User1",
                IdRecipient = 2,
                NameRecipient = "User2",
                item = new Good {Name = "ItemName"},
                Count = 100,
                Price = 200,
                Summ = 300
            };

            Invoice.SaveToXML(invoice, "invoce.xml");
            */

            /*
            invoice = Invoice.LoadFromXML("invoce.xml");
            Console.WriteLine(invoice.IdRecipient);
            Console.ReadLine();
            */
        }
    }
}
