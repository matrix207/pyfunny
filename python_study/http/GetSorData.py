import urllib2

# if 0:
	# re = urllib2.Request(r'http://192.168.1.103/GetSorData.cgi')  
	# rs = urllib2.urlopen(re).read()  
# else:
	# rs = urllib2.urlopen('http://192.168.1.103/GetSorData.cgi').read()
	# open('tmp.sor.zip', 'wb').write(rs)
	

httpHandler = urllib2.HTTPHandler(debuglevel=1)
httpsHandler = urllib2.HTTPSHandler(debuglevel=1)
opener = urllib2.build_opener(httpHandler, httpsHandler)
 
urllib2.install_opener(opener)
response = urllib2.urlopen('http://192.168.1.103/GetSorData.cgi')