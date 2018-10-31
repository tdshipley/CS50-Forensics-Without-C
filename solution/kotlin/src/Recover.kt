import java.io.File
//
// Thomas Shipley (www.mrshipley.com)
//
// Recover.kt recovers jpeg images from a raw SD card file provided by CS50:
//    * https://cdn.cs50.net/2014/fall/psets/4/pset4/pset4.html

// It makes some assumptions for the sake of simplicity:
//
//   * The card has been zero wiped before images were taken and deleted.
//   * The images are stored in contiguous 512 byte blocks
//   * Trailing zeros in recovered images is OK
//   * It is OK to hardcode the path to the file
//
// Purpose of redoing this exercise in Kotlin is to learn a little more about the
// language by going beyond a Hello World application as a first attempt.
//
//   Resources used:
//
//      * https://try.kotlinlang.org
//      * https://dotnetvibes.com/2018/01/03/intellij-idea-error-cannot-run-program-no-such-file-or-directory/
//      * https://stackoverflow.com/questions/37837706/kotlin-foreachblock-example

fun main(args: Array<String>) {
    val rawCard = File("../../card.raw")
    var fileNumber = 0
    var outFile:File? = null

    rawCard.forEachBlock(512) {
        bytes, _ ->

        if(isJpeg(bytes)) {
            fileNumber+=1

            outFile = File("out/$fileNumber.jpeg")
            outFile?.writeBytes(bytes)
            println("New file number $fileNumber started.")

        } else {
            outFile?.appendBytes(bytes)
        }
    }

    println("Finished")
    println("Files in: ${System.getProperty("user.dir")}")
}

fun isJpeg(block: ByteArray) : Boolean{
    val jpegHeaderPart  = 0xff.toByte()

    return block[0] == jpegHeaderPart &&
            block[1] == 0xd8.toByte() &&
            block[2] == jpegHeaderPart &&
            (block[3] == 0xe0.toByte() || block[3] == 0xe1.toByte())
}

