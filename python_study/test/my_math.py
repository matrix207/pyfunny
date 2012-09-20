#!/usr/bin/env python
#my_math.py

def square(x):
    '''
    Square a number and returns the result.
    >>> square(2)
    4
    >>> square(3)
    9
    '''
    return x*x

def product(x, y):
    '''
    Multip two number and returns the result.
    >>> product(2, 3)
    6
    >>> product(3, 4)
    12
    '''
    return x*y

if __name__ == '__main__':
    import doctest, my_math
    doctest.testmod(my_math)
