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

        //TODO: Making code less quick and dirty.

        public static void Main(string[] args)
        {
            //boolean variable to check if the while loop should be exited.
            bool confirm = false;

            //A string list to store the IDs for the streams
            List<string> streamID = new List<string> { };


            Console.WriteLine("Where to find URL ID: https://ustream.univie.ac.at/media/core.html?format=hls&id=XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX");
            while(!confirm)
            {
                Console.WriteLine("Insert URL ID: ");

                streamID.Add(Console.ReadLine());

                //Switch statements to check if user wants to add more files or if they are done.
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

            //Start download
            Download(streamID);


        }

        //A static function which will start downloading the streams via FFmpeg
        public static void Download(List<string> streamIDs)
        {

            //Tells user how many streams will be downloaded.
            Console.WriteLine("{0} Streams will be downloaded.", streamIDs.Count);

            
            //For loop which will run FFmpeg as often as needed.
            for (int i = 0; i < streamIDs.Count; i++)
            {
                //create a new process for ffmpeg with the arguments to download the streams.
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "ffmpeg.exe",
                        Arguments = String.Format("-i https://stream-cd.univie.ac.at/opencast_default/smil:engage-player_{0}_presentation.smil/playlist.m3u8 -c copy -bsf:a aac_adtstoasc {0}.mp4", streamIDs[i])
                    }
                };

                //Start the process 
                process.Start();
                //Wait until the process is done
                process.WaitForExit();
                //Dispose the process
                process.Dispose();
                //Repeat

            }

            //Done.
            Console.WriteLine("Done!");
            Console.ReadLine();

            
        }

    }

    
}
