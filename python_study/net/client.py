#!/usr/bin/env python
#client

import socket

s = socket.socket()

host = "112.22.179.75"
#host = socket.gethostname()
port = 2600

try:
	s.connect((host, port))
	print s.recv(1024)
except:
	print 'catch except'

