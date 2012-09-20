#!/usr/bin/env python

import pagerank

rank = pagerank.get_pagerank('http://www.google.com')
print 'google:',rank
print "baidu:", pagerank.get_pagerank('http://www.baidu.com')
print pagerank.get_pagerank('http://www.csdn.net')
print pagerank.get_pagerank('http://www.codeproject.com')