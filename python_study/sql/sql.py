#!/usr/bin/python
# -*- coding: UTF-8 -*-
import os, sys
import MySQLdb
# �������ݿ�
try:
    conn = MySQLdb.connect(host='192.168.1.168',user='sa',passwd='1234',db='Test')       
except Exception, e:
    print e
    sys.exit()
cursor = conn.cursor()

sql = "insert into address(name, address) values (%s, %s)" 
values  = (("��С��", "����������"), ("��С��", "����������"), ("��С��", "����������"))
try:
    cursor.executemany(sql, values) # �����������
except Exception, e:
    print e

sql = "select count(*) from country"
cursor.execute(sql)					# ��ѯ����
data = cursor.fetchall()
if data:
    for x in data:
        print x[0], x[1]
cursor.close()						# �ر��α�
conn.close()						# �ر����ݿ�