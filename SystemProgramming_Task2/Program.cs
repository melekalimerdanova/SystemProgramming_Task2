using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace SystemProgram_Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string vlcPath = @"C:\Program Files\VideoLAN\VLC\vlc.exe";

            string musicFile = "song.mpeg";
            string videoFile = "video.mp4";

            while (true)
            {
                Console.WriteLine("1. Mahnini oxut");
                Console.WriteLine("2. Videonu oxut");
                Console.WriteLine("3. Hamisini dayandir");
                Console.WriteLine("4. Hem mahnin hem videonu ac");
                Console.WriteLine("5. Programdan cix");
                Console.Write("Seciminizi daxil edin: ");

                string? input = Console.ReadLine();

                if (input == "1")
                {
                    if (!File.Exists(musicFile))
                    {
                        Console.WriteLine("Mahni fayli qovluqda tapilmadi!");
                        continue;
                    }

                    Thread musicThread = new Thread(() =>
                    {
                        Process.Start(vlcPath, $"\"{musicFile}\"");
                    });
                    musicThread.IsBackground = true;
                    musicThread.Start();
                }
                else if (input == "2")
                {
                    if (!File.Exists(videoFile))
                    {
                        Console.WriteLine("Video fayli qovluqda tapilmadi!");
                        continue;
                    }

                    Thread videoThread = new Thread(() =>
                    {
                        Process.Start(vlcPath, $"\"{videoFile}\"");
                    });
                    videoThread.IsBackground = true;
                    videoThread.Start();
                }
                else if (input == "3")
                {
                    // Isleyen VLC proseslerini baglayir
                    Process[] activeProcesses = Process.GetProcessesByName("vlc");
                    foreach (var process in activeProcesses)
                    {
                        try
                        {
                            process.Kill();
                        }
                        catch { }
                    }
                    Console.WriteLine("Butun pleyerler baglandi.");
                }
                else if (input == "4")
                {
                    if (!File.Exists(musicFile) || !File.Exists(videoFile))
                    {
                        Console.WriteLine("Fayllardan her hansi biri catismir!");
                        continue;
                    }

                    Thread musicThread = new Thread(() =>
                    {
                        Process.Start(vlcPath, $"\"{musicFile}\"");
                    });

                    Thread videoThread = new Thread(() =>
                    {
                        Thread.Sleep(200);
                        Process.Start(vlcPath, $"\"{videoFile}\"");
                    });

                    musicThread.IsBackground = true;
                    videoThread.IsBackground = true;

                    musicThread.Start();
                    videoThread.Start();

                    Console.WriteLine("Her iki fayl eyni anda basladildi.");
                }
                else if (input == "5")
                {
                    Console.WriteLine("Proqram bitdi.");
                    break;
                }
                else
                {
                    Console.WriteLine("Duzgun secim edin!");
                }
            }
        }
    }
}