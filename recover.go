/*
Thomas Shipley

recover.go reovers jpeg images from a raw SD card file provided by CS50:
https://cdn.cs50.net/2014/fall/psets/4/pset4/pset4.html

It makes some assumptions for the sake of simplicity:

    * The card has been zero wiped before images were taken and deleted.
    * The images are stored in contiguous 512 byte blocks
    * Trailing zeros in recovered images is OK
    * It is OK to hardcode the path to the file

Purpose of redoing this exercise in Go is to learn a little more about the
language by going beyond a Hello World application as a first attempt.

The code here was created with the help of a code sample from:
https://gobyexample.com/reading-files
*/
package main

import (
  "bufio"
  "fmt"
  "io"
  "io/ioutil"
  "os"
)

func check(e error)  {
  if(e != nil) {
    panic(e)
  }
}

package func main() {
  // 1. Open the file
  // 2. Continue looping until end of file
  // 3. Read 512 bytes of the file.
  // 4. Check if first 4 bytes match header of jpeg file
  // 5. If match: Close last recovered image file (if it exists)
  // 6. If match: Write to a new recovered image file
  // 7. Move to next 512 bytes in loop
}
