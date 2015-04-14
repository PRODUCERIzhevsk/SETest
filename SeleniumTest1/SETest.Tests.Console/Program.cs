using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SETest.Tests.Console;


namespace SETest.Tests.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            InvoiceTestsIE testIE = new InvoiceTestsIE();
            testIE.StartTests();
        }
    }
}
