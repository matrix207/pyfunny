#!/usr/bin/env python
#creator.py

def flatten(nested):
	for sublist in nested:
		for element in sublist:
			yield element

nested = [[1, 2], [3, 4], [5]]

#for num in flatten(nested):
#	print num

#print list(flatten(nested))

def flatten_1(nested):
	try:
		for sublist in nested:
			for element in flatten_1(sublist):
				yield element
	except TypeError:
		yield nested

#for num in flatten_1(nested):
#	print num

print list(flatten_1([[1, 2], 3, 4, [5,[6,7],8]]))

