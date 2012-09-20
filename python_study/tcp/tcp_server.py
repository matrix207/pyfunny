import socket
import os

host = ''
port = 8080
address = (host, port)

tcpserver = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
tcpserver.bind(address)
tcpserver.listen(5)

while True:

    print ('waiting for connection...')
    tcpclient, addr = tcpserver.accept()
    print ('...connected from:',addr)
    #data = (tcpclient.recv(1024)).decode('utf-8')
	data = tcpclient.recv(1024)
    #print ('...recvice file: %s \n\r' % data)
    file = os.open('tmp.sor', os.O_WRONLY | os.O_CREAT | os.O_EXCL |os.O_BINARY)

    while True:
        rdata = tcpclient.recv(1024)
        if not rdata:
            break
        os.write(file, rdata)
    os.close(file)
    tcpclient.close()
    print ('The client is closed ....')