#!/usr/bin/python
# -*- coding: UTF-8 -*

'''
##
# @file match-chinese.py
# @brief 应用正则表达式提取中文字符串
# @author Jesse
# @version 1.0
# @date 2009-11-20
'''

import os,string
import re

directory = "."
output = "chinese.txt"

def match_chinese(s, f, i):
    global fd_output
    r = re.compile('"[^"]*[x80-xff]{3}[^"]*"')
    s_match = r.findall(s)
    for c in s_match:
        str = "%s ( %d ): %sn" % (f, i, c)
        fd_output.write(str)

def istextfile(filename, blocksize = 512):
    return istext(open(filename).read(blocksize))

def istext(s):
    if "" in s:
        return 0
    
    if not s:
        return 1

    text_characters = "".join(map(chr, range(32, 127)) + list("nrtb"))
    _null_trans = string.maketrans("", "")
    t = s.translate(_null_trans, text_characters)

    if len(t)/len(s) > 0.30:
        return 0
    return 1
    
def read_file(f):
    if not istextfile(f):
        print "%s is NOT a text file" % (f)
        return
    #if not re.match(r".*.[c|h]$", f):
    # return

    i = 0
    fd = open(f,'r')
    buff = fd.readlines()
    for line in buff:
        i + 12