#coding=utf-8 
import imp 
import sys 
imp.reload(sys) 
sys.setdefaultencoding('utf-8') #����Ĭ�ϱ���,ֻ����utf-8,����\u4e00-\u9fa5Ҫ���
#  Regular Expression
import re   
pchinese=re.compile('([\u4e00-\u9fa5]+)+?') #�ж��Ƿ�Ϊ���ĵ�������ʽ
f=open("UTF8.txt") #��Ҫ��ȡ���ļ�
fw=open("getdata.txt","w")#��Ҫд����ļ�
for line in f.readlines():   #ѭ����ȡҪ��ȡ�ļ���ÿһ��
    m=pchinese.findall(str(line)) #ʹ���������ȡ����
    if m:
        str1='|'.join(m)#ͬ�е���������������
        str2=str(str1)
        fw.write(str2)#д���ļ�
        fw.write("\n")#��ͬ�е�Ҫ����
f.close()
fw.close()#�򿪵��ļ��ǵùر�Ŷ!