import urllib2

#CGI_CMD = 'http://192.168.1.103/GetDeviceStatus.cgi'
CGI_CMD = 'http://192.168.1.103/SetScanParmExt.cgi?Device=3&Channel=1&Mode=3&Route=RDFMCRST00000010000001000001&Range=30&Time=6&Wavelength=1310&Pulsewidth=10&IOR=14770&Sensitivity=1&Trace=2&IP=192.168.1.109&Port=8080'

response = urllib2.urlopen(CGI_CMD)
html = response.read()
print html
