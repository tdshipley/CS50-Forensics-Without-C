/*
Thomas Shipley (www.mrshipley.com)

recover.go reovers jpeg images from a raw SD card file provided by CS50:
    * https://cdn.cs50.net/2014/fall/psets/4/pset4/pset4.html

It makes some assumptions for the sake of simplicity:

    * The card has been zero wiped before images were taken and deleted.
    * The images are stored in contiguous 512 byte blocks
    * Trailing zeros in recovered images is OK
    * It is OK to hardcode the path to the file

Purpose of redoing this exercise in Go is to learn a little more about the
language by going beyond a Hello World application as a first attempt.

The code here was created with the help of a code sample from:
    * https://gobyexample.com/reading-files

Other resources used:
    * https://golang.org/doc/effective_go.html
    * http://stackoverflow.com/questions/1821811/how-to-read-write-from-to-file
    * https://tour.golang.org
    * https://tour.golang.org/flowcontrol/4
    * http://golang.org/pkg/bufio/#Reader.Read
    * https://golang.org/pkg/fmt/#Println
*/
package main

import (
  "bufio"
  "fmt"
  "io"
  "os"
)

func check(err error)  {
  if err != nil && err != io.EOF {
      panic(err)
  }
}

func isJPEG(fileBlock []byte) (result bool)  {
  result = false
  if (fileBlock[0] == 0xff && fileBlock[1] == 0xd8 && fileBlock[2] == 0xff && (fileBlock[3] == 0xe0 || fileBlock[3] == 0x0e1)) {
    result = true
  }
  return result
}

func main() {
  // 1. Open the file
  file, err := os.Open("card.raw")
  check(err)
  defer func() {
        check(err)
    }()

  fileNumber := 1
  check(err)
  // 2. Continue looping until end of file
  reader := bufio.NewReader(file)
  fileBlock := make([]byte, 512)
  fileName := fmt.Sprintf("%03d.jpg", fileNumber)
  fo, err := os.Create(fileName)
  writer := bufio.NewWriter(fo)

  check(err)
  for {
    fileName := fmt.Sprintf("%03d.jpg", fileNumber)
    // 3. Read 512 bytes of the file.
    byteCount, err := reader.Read(fileBlock)
    if byteCount == 0 {
      fmt.Println("That is it.")
      break
    }
    check(err)

    // 4. Check if first 4 bytes match header of jpeg file
    if isJPEG(fileBlock) {
         // 5. If match: Close last recovered image file (if it exists)
         writer.Flush()

         // 6. If match: Write to a new recovered image file
         fileName = fmt.Sprintf("%03d.jpg", fileNumber)
         fileNumber++

         fo, err := os.Create(fileName)
         check(err)

         writer = bufio.NewWriter(fo)
         writer.Write(fileBlock[:byteCount])
    } else if(writer != nil) {
      // 7. If not match for header write bytes to file
      writer.Write(fileBlock[:byteCount])
    }
  }
}
