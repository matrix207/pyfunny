#!/usr/bin/env python
#test.py

def square(x):
    '''
    Square a number and returns the result.
    >>> square(2)
    4
    >>> square(3)
    9
    '''
    return x*x

if __name__ == '__main__':
    import doctest, test
    doctest.testmod(test)
