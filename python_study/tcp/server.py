import os
import SocketServer


class MyTCPHandler(SocketServer.BaseRequestHandler):
    """
    The RequestHandler class for our server.

    It is instantiated once per connection to the server, and must
    override the handle() method to implement communication to the
    client.
    """

    def handle(self):
        # self.request is the TCP socket connected to the client
        print ("Enter handle function\n")
        self.data = self.request.recv(1024).strip()
        print "%s wrote:" % self.client_address[0]
		#out_file = open('tmp.sor', 'wb')
		#out_file.close()
		# file_object = open('thefile.txt')
		# try:
			 # all_the_text = file_object.read( )
		# finally:
			 # file_object.close( )
        #self.request.send(self.data)
        # just send back the same data, but upper-cased
        # self.request.send(self.data.upper())

	def _recieveFile(self, path):
		LENGTH_SIZE = 4 # length is a 4 byte int.
		# Recieve the file from the client
		#writefile = open(path, 'wb')
		length = decode_length(self.con.read(LENGTH_SIZE) # Read a fixed length integer, 2 or 4 bytes
		# while (length):
			# rec = self.con.recv(min(1024, length))
			# writefile.write(rec)
			# length -= sizeof(rec)

    self.con.send(b'A') # single character A to prevent issues with buffering
	
if __name__ == "__main__":
    HOST, PORT = "localhost", 8080

    # Create the server, binding to localhost on port 8080
    server = SocketServer.TCPServer((HOST, PORT), MyTCPHandler)

    # Activate the server; this will keep running until you
    # interrupt the program with Ctrl-C
    server.serve_forever()
