using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace UStreamDownloader
{
    class Program
    {

        //TODO: Making code less quick and dirty.

        private static string ffmpegOsPlatform { get; set; }
        public static void Main(string[] args)
        {
            //Check for operating system once
             if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                            ffmpegOsPlatform = "ffmpeg.exe";
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                            ffmpegOsPlatform = "ffmpeg";
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                            //throw a not implemented exception because i do not have a computer running macOS to test this.
                            //TODO: Add macOS support
                            throw new NotImplementedException("OS X is not supported (yet)");
            else 
                            throw new NotImplementedException("your operating system is not supported");
            


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
        static void Download(List<string> streamIDs)
        {

            //Tells user how many streams will be downloaded.
            Console.WriteLine("{0} Streams will be downloaded.", streamIDs.Count);

            
            //For loop which will run FFmpeg as often as needed.
            for (int i = 0; i < streamIDs.Count; i++)
            {
                
                ;
                
                //create a new process for ffmpeg with the arguments to download the streams.
                //also check for operating system
               
                    
                        
                      
                
                    //Putting in using to make sure that the process is being loaded out of the memory after launching to avoid memory leak
                    using (Process process = new Process{}){
                        
                        process.StartInfo = new ProcessStartInfo{

                            FileName = ffmpegOsPlatform,
                            Arguments = String.Format("-i https://stream-cd.univie.ac.at/opencast_default/smil:engage-player_{0}_presentation.smil/playlist.m3u8 -c copy -bsf:a aac_adtstoasc {0}.mp4", streamIDs[i])
                        
                        };
                         //Start the process 
                        process.Start();
                        //Wait until the process is done
                        process.WaitForExit();
                        //Dispose the process
                        process.Dispose();
                        //Repeat
                    }
                
                  
                
 

            }

            //Done.
            Console.WriteLine("Done!");
            Console.ReadLine();

            
        }

    }

    
}
