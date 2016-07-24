"""
Thomas Shipley (www.mrshipley.com)

recover.py reovers jpeg images from a raw SD card file provided by CS50:
    * https://cdn.cs50.net/2014/fall/psets/4/pset4/pset4.html

It makes some assumptions for the sake of simplicity:

   * The card has been zero wiped before images were taken and deleted.
   * The images are stored in contiguous 512 byte blocks
   * Trailing zeros in recovered images is OK
   * It is OK to hardcode the path to the file

Purpose of redoing this exercise in Python is to learn a little more about the
language by going beyond a Hello World application as a first attempt.

   Resources used:

     * https://learnxinyminutes.com/docs/python3/
     * http://stackoverflow.com/questions/159720/what-is-the-naming-convention-in-python-for-variable-and-function-names
     * https://docs.python.org/3/tutorial/inputoutput.html
     * http://stackoverflow.com/questions/25465792/python-binary-eof
     * http://stackoverflow.com/questions/3754240/declare-function-at-end-of-file-in-python
     * http://stackoverflow.com/questions/6624453/whats-the-correct-way-to-convert-bytes-to-a-hex-string-in-python-3
     * http://stackoverflow.com/questions/664294/is-it-possible-only-to-declare-a-variable-without-assigning-any-value-in-python
"""
import binascii

def main():
    file_number = 0
    relative_raw_card_file_path = '../../card.raw'
    raw_card_file = open(relative_raw_card_file_path, 'rb')
    output_file = None

    # Until end of file read 512 bytes of raw card file
    with open(relative_raw_card_file_path, 'rb') as raw_card_file:
        block = raw_card_file.read(512)
        while block:
            if(is_a_jpeg(block)):
                if(output_file):
                    output_file.close()
                file_number += 1
                output_file = open(get_filename(file_number), 'wb')
                print("Found new file - No: " + str(file_number))
            if(output_file):
                output_file.write(block)
            block = raw_card_file.read(512)

def is_a_jpeg(block):
    if len(block) < 3:
        return False

    first_byte_as_hex = binascii.hexlify(block[0])
    second_byte_as_hex = binascii.hexlify(block[1])
    third_byte_as_hex = binascii.hexlify(block[2])
    forth_byte_as_hex = binascii.hexlify(block[4])

    return (
                first_byte_as_hex == 'ff'  and
                second_byte_as_hex == 'd8' and
                third_byte_as_hex == 'ff'  and
                (forth_byte_as_hex == 'e0' or '0e1')
            )

def get_filename(file_number):
    return 'image_' + str(file_number) + '.jpeg'

if __name__ == '__main__':
    main()