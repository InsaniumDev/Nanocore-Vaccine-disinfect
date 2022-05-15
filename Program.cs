using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace nanocore_vaccine
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Nanocore Vaccine V1");
            Console.Write("Press any key to continue...");
            Console.ReadKey();
            Console.WriteLine("");
            string[] directories = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            List<string> nanodirs = new List<string>();

            foreach(string name in directories)
            {
                foreach (Match match in Regex.Matches(name, @"[\w-]{8}-[\w-]{4}-[\w-]{4}-[\w-]{4}-[\w-]{12}"))
                {
                    nanodirs.Add(match.Value);
                    Console.WriteLine($"detected nanocore directory! {match.Value}");
                }
            }

            foreach(var dir in nanodirs)
            {
                string[] files = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\" + dir, "*", SearchOption.AllDirectories);
                Console.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\" + dir);
                foreach(var file in files)
                {
                    string fileName = Path.GetFileName(file);
                    Console.WriteLine("Found Nanocore rat! " + fileName);
                    ProcessStartInfo psi = new ProcessStartInfo("cmd.exe");
                    psi.Arguments = "/c taskkill /f /im " + fileName;
                    psi.WorkingDirectory = @"C:\windows\system32\";
                    Process.Start(psi);
                    Console.WriteLine("Killed " + fileName);
                    try
                    {
                        File.Delete(file);
                    }
                    catch
                    {

                    }
                }
                try
                {
                    Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\" + dir, true);
                }
                catch(Exception ex)
                {
                    Console.WriteLine("disinfect failed try to run this as administrator or run it again!");
                }
            }
            Console.WriteLine("Your computer has been vaccinated! Press any key to exit");
            Console.ReadKey();
        }
    }
}
