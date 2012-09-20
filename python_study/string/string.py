#!/usr/bin/env python
#-*- coding: UTF-8 -*-
# string.py

# output d*e*n*n*i*s
print '*'.join("dennis")

# output d*e*n*n*i*s
print ''.join("d enn is")

dir = '', 'usr', 'bin', 'env'
print '/'.join(dir)
print 'C:' + '**'.join(dir)

name = 'dennis', 'kevin'
print 'C:' + '**'.join('dennis')
print 'C:' + '**'.join(name)
