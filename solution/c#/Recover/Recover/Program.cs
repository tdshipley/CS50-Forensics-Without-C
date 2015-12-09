using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 Thomas Shipley (www.mrshipley.com)

 Recover reovers jpeg images from a raw SD card file provided by CS50:
  * https://cdn.cs50.net/2014/fall/psets/4/pset4/pset4.html

 It makes some assumptions for the sake of simplicity:

  * The card has been zero wiped before images were taken and deleted.
  * The images are stored in contiguous 512 byte blocks
  * Trailing zeros in recovered images is OK
  * It is OK to hardcode the path to the file

 Purpose of redoing this exercise in C# is to showcase some of the basics
 of the language by going beyond a Hello World application as a first attempt.
 It also serves as a good language to compare against others I am less familar in.

   Resources used:
    * http://stackoverflow.com/questions/2030847/best-way-to-read-a-large-file-into-a-byte-array-in-c 
    * https://msdn.microsoft.com/en-us/library/system.io.file.readallbytes(v=vs.110).aspx
    * https://msdn.microsoft.com/en-us/library/kt8btd95(v=vs.110).aspx
    * http://stackoverflow.com/questions/6865890/how-can-i-read-stream-a-file-without-loading-the-entire-file-into-memory
    * http://stackoverflow.com/questions/411592/how-do-i-save-a-stream-to-a-file-in-c
*/

namespace Recover
{
    class Program
    {
        static void Main(string[] args)
        {
            //1. Set the root path of the project & current project path
            const string CARD_ROOT_PATH = @"F:\code\CS50-Forensics-Without-C\";
            string projectPath = Environment.CurrentDirectory;

            Console.WriteLine("Finding images...");
            Console.WriteLine("Images will be output to: " + projectPath);

            //2. Set the size of a block
            const int BLOCK_SIZE = 512;

            //3. Create an uninitalised output stream var to write data
            // and a image counter.
            FileStream outputFile = null;
            int imageCounter = 0;

            //4. Open the card image in the root of the project
            using (var file = File.OpenRead(CARD_ROOT_PATH + "card.raw"))
            {
                //5. Create a byte array to store the block of data
                byte[] block = new byte[BLOCK_SIZE];

                //6. Keep reading from the input image in 512 byte blocks or less
                // until no data left
                while (file.Read(block, 0, block.Length) > 0)
                {
                    //7. If the block contains the header of a jpeg
                    if(IsJpegMatch(block))
                    {
                        //8. Close any image currently being written too.
                        if (outputFile != null)
                        {
                            outputFile.Close();
                        }

                        //9. Increase the image found counter
                        imageCounter++;

                        //10. Create a new image file with a incremented file name
                        // and write block to it.
                        string imageNumber = imageCounter.ToString();
                        string imageFileName = imageNumber.PadLeft(3, '0') + ".jpg";
                        outputFile = File.Create(projectPath + imageFileName);
                        outputFile.Write(block, 0, BLOCK_SIZE);

                    }
                    else if(outputFile != null)
                    {
                        //11. Write block to currently found image.
                        outputFile.Write(block, 0, BLOCK_SIZE);
                    }
                }
            }

            Console.WriteLine(imageCounter.ToString() + " images found. Press any key to exit.");
            Console.ReadLine();
        }

        /// <summary>
        /// Given a block of at least four bytes read the first four
        /// to see if the block contains the start of a jpeg
        /// </summary>
        /// <param name="block">Array of bytes representing a block of data</param>
        /// <returns></returns>
        private static bool IsJpegMatch(byte[] block)
        {
            bool match = false;
            if(block[0].Equals(0xff) &&
               block[1].Equals(0xd8) &&
               block[2].Equals(0xff))
            {
                if(block[3].Equals(0xe0) || block[3].Equals(0xe1))
                {
                    match = true;
                }
            }

            return match;
        }

        // My first ill fated attempt at the code to recover images. Kept here for educational reasons.
        // Highlighting how hard you can make something for yourself! And to write about it in the blog post.

        /*while(offset < fileBytes.Length)
        {
            byte[] block = new byte[BLOCK_SIZE];
            Buffer.BlockCopy(fileBytes, offset, block, 0, BLOCK_SIZE);
                
            //4. Check if block is start of jpeg
            if(IsJepgMatch(block))
            {
                imageCounter++;

                if (outputFileBytes != null)
                {
                    string imageNumber = imageCounter.ToString();
                    string imageFileName = imageNumber.PadLeft(3, '0') + ".jpg";
                    File.WriteAllBytes(CARD_ROOT_PATH + imageFileName, outputFileBytes.ToArray());
                }

                outputFileBytes = new List<byte>();
                block.ToList().ForEach(x => outputFileBytes.Add(x));

            }
            else if(outputFileBytes != null)
            {
                block.ToList().ForEach(x => outputFileBytes.Add(x));
            }

            offset += BLOCK_SIZE;
        }*/
    }
}
