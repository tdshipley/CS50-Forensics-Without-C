using System;
using System.Collections.Generic;
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
*/

namespace Recover
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        /// <summary>
        /// Given a block of at least four bytes read the first four
        /// to see if the block contains the start of a jpeg
        /// </summary>
        /// <param name="block">Array of bytes representing a block of data</param>
        /// <returns></returns>
        private static bool IsJepgMatch(byte[] block)
        {
            bool match = false;
            if(block[0].Equals(0xFF) &&
               block[1].Equals(0xD8) &&
               block[2].Equals(0xFF))
            {
                if(block[3].Equals(0xE0) || block[3].Equals(0xE1))
                {
                    match = true;
                }
            }

            return match;
        }
    }
}
