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
     * 
*/

namespace Recover
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
