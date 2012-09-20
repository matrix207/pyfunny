#!/usr/bin/env python

def process(line):
    '''process function
    '''
    print line

try:
    for line in open('tmp.txt', 'r'):
        process(line)
except:
    print 'catch fileinput exception'

try:
    f = open('tmp.txt', 'r')
    while True:
        line = f.readline()
        if not line:
            break
        #process(line)
    f.close()
except:
    print "catch error"
