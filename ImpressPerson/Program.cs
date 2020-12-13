using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;


namespace ImpressPerson
{
    class Program

    {
        static  string _textFile;
        static string _browserFile;
        static List<String> _commands = new List<String>();
        static IWebDriver _driver;
        static void Main(string[] args)
        {

            GetAndReadTextFile();

            if(_commands.Any())
            {
                foreach (string cmd in _commands)
                {


                    Thread tReturn = new Thread(() => { CommandProcessor.runCommand(cmd); });
                    tReturn.Priority = ThreadPriority.AboveNormal;
                    tReturn.Start();


                }
            }
            else
            {
                Console.WriteLine("The commands are empty, please check your commands.txt file");
            }
            

            StartBrowser();

            

            Console.ReadLine();


        }
        private static void GetAndReadTextFile()
        {
            //Actual physical desktop - not the virtual folder
            _textFile = Path.Combine(Directory.GetCurrentDirectory(),"Command", "commands.txt");
            if(File.Exists(_textFile))
            {
                using (StreamReader reader = new StreamReader(new FileStream(_textFile, FileMode.Open)))
                {
                    string line;
                    int counter = 0;
                    while ((line = reader.ReadLine()) != null)
                    {
                        _commands.Add(line);
                    }

                    Console.WriteLine($"_browser has {counter} lines");
                }

            }
            else
            {
                Console.WriteLine("Please create a commands.txt file on the desktop with your commands");
            }
            
        }

        private static void StartBrowser()
        {
            _browserFile = Path.Combine(Directory.GetCurrentDirectory(),"Command", "browser.txt");
            if(File.Exists(_browserFile))
                {
                using (StreamReader reader = new StreamReader(new FileStream(_browserFile, FileMode.Open)))
                {
                    string line;
                    int counter = 0;
                    while ((line = reader.ReadLine()) != null)
                    {
                        _driver = new ChromeDriver();
                        _driver.Navigate().GoToUrl(line);
                        counter++;
                    }

                    reader.Close();
                    Console.WriteLine($"_browser has {counter} lines");
            }

                var readingKey = Console.ReadLine();
                if (readingKey == ".")
                {
                    _driver.Close();
                }
                
            }
            else
            {
                Console.WriteLine("Please create a browser.txt file on the desktop with your browser url");
            }
           

        }

    }

    public static class CommandProcessor
    {
        static string d = null;

        public static void runCommand(string command)
        {
            Thread.Sleep(10000);
            string starttime;
            string endtime;
            //* Create your Process
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "/T:03 /c" + command + "";
            process.StartInfo.UseShellExecute = true;
            starttime = "Started at " + DateTime.Now + "\n";
            //* Start process and handlers
            process.Start();
            process.WaitForExit();
            endtime = "Completed at " + DateTime.Now + "\n";

            d += "========================================================================";
            d += starttime + endtime;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(d);
            Console.ReadLine();

        }

    }
}
