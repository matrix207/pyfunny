#!/usr/bin/env python
#-*- coding: gb2312 -*-
# Dennis 2011-12-20

import re,urllib2
from subprocess import Popen, PIPE

localIP = "local IP: " + re.search('\d+\.\d+\.\d+\.\d+',Popen('ipconfig', stdout=PIPE).stdout.read()).group(0)
print localIP

wanIP   = "wan   IP: " + re.search('\d+\.\d+\.\d+\.\d+',urllib2.urlopen("http://www.whereismyip.com").read()).group(0)
#wanIP   = "wan   IP: " + re.search('\d+\.\d+\.\d+\.\d+',urllib2.urlopen("http://www.ip138.com/ip2city.asp").read()).group(0)
print wanIP

f = open('getIP.log', 'w')
f.write(localIP+"\n"+wanIP)
f.close()
	
raw_input("Press any key to exit")
