#
# Thomas Shipley (www.mrshipley.com)
#
# recover.go reovers jpeg images from a raw SD card file provided by CS50:
#    * https://cdn.cs50.net/2014/fall/psets/4/pset4/pset4.html

# It makes some assumptions for the sake of simplicity:
#
#   * The card has been zero wiped before images were taken and deleted.
#   * The images are stored in contiguous 512 byte blocks
#   * Trailing zeros in recovered images is OK
#   * It is OK to hardcode the path to the file
#
# Purpose of redoing this exercise in Ruby is to learn a little more about the
# language by going beyond a Hello World application as a first attempt.
#
#   Resources used:
#
#     * http://stackoverflow.com/questions/16815308/ruby-comparing-hex-value-to-string
#     * http://ruby-doc.org/core-1.9.3/String.html#method-i-rjust
#     * http://stackoverflow.com/questions/7911669/create-file-in-ruby

# @param [Object] block A 512 byte block to check if it contains a jpeg header in first 4 bytes
def isJPEG?(block)
  # Encodings are diff between reading an image file and strings so block[0] == 0xFF wont work
  # http://stackoverflow.com/questions/16815308/ruby-comparing-hex-value-to-string
  jpeg_header_part = ['FFD8FF'].pack('H*')
  return block[0,3] == jpeg_header_part && (block[3] == 0xe0.chr || block[3] == 0x0e1.chr)
end

def getFilename(file_number)
  file_number.to_s.rjust 3, '0'
end


filenumber = 0
out_file = nil

# Open the file
file = File.open 'card.raw'

until file.eof?
  block = file.read 512

  if isJPEG? block
    unless out_file.nil?
      out_file.close
    end

    filenumber = filenumber.next
    puts "#{getFilename(filenumber)}.jpeg"
    out_file = File.open("#{getFilename(filenumber)}.jpeg", 'w')
    out_file.write block
  else
    unless out_file.nil?
      out_file.write block
    end
  end
end

out_file.close
file.close
