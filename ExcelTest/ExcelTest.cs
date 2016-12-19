using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelConnect;

namespace ExcelTest
{
    class ExcelTest
    {
        static void Main(string[] args)
        {
            new WorkSheet("foobar", "c:\\users\\acarhart.northamerica\\foobar.csv", new String[] { "A","B","C" }, new String[] { "B" });
        }
    }
}
