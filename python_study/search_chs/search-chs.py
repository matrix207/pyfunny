#coding=utf-8 
import imp 
import sys 
imp.reload(sys) 
sys.setdefaultencoding('utf-8') #设置默认编码,只能是utf-8,下面\u4e00-\u9fa5要求的
#  Regular Expression
import re   
pchinese=re.compile('([\u4e00-\u9fa5]+)+?') #判断是否为中文的正则表达式
f=open("UTF8.txt") #打开要提取的文件
fw=open("getdata.txt","w")#打开要写入的文件
for line in f.readlines():   #循环读取要读取文件的每一行
    m=pchinese.findall(str(line)) #使用正则表达获取中文
    if m:
        str1='|'.join(m)#同行的中文用竖杠区分
        str2=str(str1)
        fw.write(str2)#写入文件
        fw.write("\n")#不同行的要换行
f.close()
fw.close()#打开的文件记得关闭哦!