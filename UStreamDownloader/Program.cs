using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

namespace UStreamDownloader
{
    class Program
    {
        public static void Main(string[] args)
        {
            bool confirm = false;

            List<string> streamID = new List<string> { };


            Console.WriteLine("Where to find URL ID: https://ustream.univie.ac.at/media/core.html?format=hls&id=XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX");
            while(!confirm)
            {
                Console.WriteLine("Insert URL ID: ");

                streamID.Add(Console.ReadLine());

                Console.WriteLine("Are you Done? (Yes/No)");
                switch (Console.ReadLine().ToLower())
                {
                    case "yes":
                        confirm = true;
                        break;
                    case "y":
                        confirm = true;
                        break;
                    case "no":
                        continue;
                    case "n":
                        continue;
                    default:
                        continue;
                }


                
            }

            Download(streamID);


        }

        public static void Download(List<string> streamIDs)
        {

            Console.WriteLine("{0} Streams will be downloaded.", streamIDs.Count);

            

            for (int i = 0; i < streamIDs.Count-1; i++)
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "ffmpeg.exe",
                        Arguments = String.Format("-i https://stream-cd.univie.ac.at/opencast_default/smil:engage-player_{0}_presentation.smil/playlist.m3u8 -c copy -bsf:a aac_adtstoasc {0}.mp4", streamIDs[i])
                    }
                };

                process.Start();
                process.WaitForExit();
                process.Dispose();

            }

            Console.WriteLine("Done!");
            Console.ReadLine();

            
        }

    }

    
}
