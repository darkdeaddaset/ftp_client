using System;
using System.Linq;
using System.Threading;

namespace ftp_client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.Write("Example IP host: ftp://xxx.xxx.xx.xx\n");
            Console.Write("Enter IP host: ");
            string ip = Console.ReadLine();
            Console.WriteLine();

            Console.Write("Enter login of user: ");
            string user = Console.ReadLine();
            Console.WriteLine();

            Console.Write("Enter password: ");
            string pass = Console.ReadLine();
            Console.WriteLine();

            ftp_client ftp_Client = new ftp_client(ip, user, pass);

            Console.WriteLine("1) Upload");
            Console.WriteLine("2) Download");
            Console.WriteLine("3) Rename");
            Console.WriteLine("4) Delete");
            Console.WriteLine("5) Create Directory");
            Console.WriteLine("6) Details list of directory");

            if (Console.ReadKey(true).Key == ConsoleKey.D1 || Console.ReadKey(true).Key == ConsoleKey.NumPad1)
            {
                
                Console.Write("Enter name of file: ");
                string file = Console.ReadLine();
                Console.WriteLine();
                Console.Write("Enter local file");
                string upload_file = Console.ReadLine();

                Thread d1 = new Thread( ()=> ftp_Client.Upload(file, upload_file));
                d1.Start();

                Console.WriteLine("Operationd upload completed!");
            }

            if (Console.ReadKey(true).Key == ConsoleKey.D2 || Console.ReadKey(true).Key == ConsoleKey.NumPad2)
            {
                Console.Write("Enter name of file: ");
                string file = Console.ReadLine();
                Console.WriteLine();
                Console.Write("Enter local file: ");
                string download_file = Console.ReadLine();

                Thread d1 = new Thread(() => ftp_Client.Download(file, download_file));
                d1.Start();

                Console.WriteLine("Operationd download completed!");
            }

            if (Console.ReadKey(true).Key == ConsoleKey.D3 || Console.ReadKey(true).Key == ConsoleKey.NumPad3)
            {
                Console.Write("Enter name of file: ");
                string file = Console.ReadLine();
                Console.WriteLine();
                Console.Write("Enter local file: ");
                string rename_file = Console.ReadLine();

                Thread d1 = new Thread(() => ftp_Client.Rename(file, rename_file));
                d1.Start();

                Console.WriteLine("Operationd rename completed!");
            }


            if (Console.ReadKey(true).Key == ConsoleKey.D4 || Console.ReadKey(true).Key == ConsoleKey.NumPad4)
            {
                Console.Write("Enter name of file: ");
                string file = Console.ReadLine();
                Console.WriteLine();
                ftp_Client.Delete(file);

                Console.WriteLine("Operationd delete completed!");
            }

            if (Console.ReadKey(true).Key == ConsoleKey.D5 || Console.ReadKey(true).Key == ConsoleKey.NumPad5)
            {
                Console.Write("Enter name for new directory: ");
                string new_directory = Console.ReadLine();
                ftp_Client.createDirectory(new_directory);

                Console.WriteLine("Operationd new directory completed!");
            }

            if (Console.ReadKey(true).Key == ConsoleKey.D6 || Console.ReadKey(true).Key == ConsoleKey.NumPad6)
            {
                Console.Write("Enter name of directory: ");
                string directory = Console.ReadLine();
                string[] detailListDirectorty = ftp_Client.directoryListDetails(directory);
                for (int i = 0; i < detailListDirectorty.Count(); i++)
                    Console.WriteLine(detailListDirectorty[i]);

                Console.WriteLine("Operationd Details List of Directory completed!");
            }   
        }
    }
}
