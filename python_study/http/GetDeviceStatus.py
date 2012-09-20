#!/usr/bin/env python

import urllib2
response = urllib2.urlopen('http://192.168.1.103/GetDeviceStatus.cgi')
html = response.read()
print html