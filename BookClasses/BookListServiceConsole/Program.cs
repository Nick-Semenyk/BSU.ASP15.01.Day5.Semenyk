using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookListServiceConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Stream stream = new FileStream("books.b", FileMode.OpenOrCreate);
            
        }
    }
}
