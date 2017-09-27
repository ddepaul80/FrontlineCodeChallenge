using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontlineCodeChallenge_DePaul
{
    class Program
    {
        static void Main(string[] args)
        {
            StringConverter sc = new StringConverter();
            sc.ConvertOriginal_FirstIdea();
            sc.ConvertOriginal_SecondIdea();
            sc.ConvertBonus();
            Console.WriteLine("Press enter to exit...");
            Console.ReadLine();
        }
    }
}
