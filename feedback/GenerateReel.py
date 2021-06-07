#!/usr/bin/env python3
# -*- coding: utf_8 -*-
"""
   チームAの課題フィードバックプログラム
"""

__author__ = 'Taku Ikegami'
__version__ = '1.0.1'
__date__ = '2021/06/07 (Created: 2021/06/07)'


import random

def generate_reel():

    def get_tmp(rand,low_list, high_list):
        tmp = 0
        for low, high in zip(low_list, high_list):
            if low < rand < high:
                return tmp
            tmp += 1
        return tmp

    low_list = [0, 7, 15, 20, 40, 55]
    high_list = low_list[1:] + [80]
    for count in range(0, 6):
        rand = random.randint(0, 91)
        tmp = get_tmp(rand, low_list, high_list)
        print("rand = {}".format(str(rand)))
        print("tmp = {}".format(str(tmp)))



def main():
    generate_reel()

if __name__ == "__main__":
    main()