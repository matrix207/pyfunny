#!/usr/bin/env python
#-*- coding: UTF-8 -*-
# dict.py

phonebook = {'Dennis':'7123', 'Kevin':'8910', 'James':'4187'}

print 'phone book:', phonebook

print "Kevin's phone number:", phonebook['Kevin']
print "James's phone number:", phonebook['James']
print "Dennis's phone number:", phonebook['Dennis']

print "len=", len(phonebook)

dennis_new_num = '3218'
print "set Dennis's phone number as ", dennis_new_num
phonebook['Dennis'] = dennis_new_num
print 'phone book:', phonebook

phonebook['Abel'] = '7575'
print 'Add Abel to phone book'
print 'phone book:', phonebook

print 'Del James from phone book'
del phonebook['James']
print 'phone book:', phonebook

if 'James' in phonebook:
	print 'James in phone book'
else:
	print 'James not in phone book'

print 'Add any type to dict'
phonebook[42] = 'test type'
print 'phone book:', phonebook
