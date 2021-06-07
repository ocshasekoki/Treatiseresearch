#!/usr/bin/env python3
# -*- coding: utf_8 -*-
"""
   チームAの課題フィードバックプログラム
"""

__author__ = 'Taku Ikegami'
__version__ = '1.0.1'
__date__ = '2021/06/07 (Created: 2021/06/07)'

def GetKoyaku(lottery):
    
    if lottery <= 769 or lottery > 999:
        return 0

    low_high_tuples = (
        (770,894),
        (895,944),
        (945,984),
        (985,992),
        (993,996),
        (997,998),
    )
    result = 1
    for low, high in [low_high_tuple for low_high_tuple in low_high_tuples]:
        if low <= lottery <= high:
            return result
        result += 1
    return result

def main():
    for i in (-1,1000, 0, 770, 895, 945, 985, 993, 997, 999, 894, 944, 984, 992, 996, 998):
        print(i)
        print('result = {}'.format(str(GetKoyaku(i))))

if __name__ == "__main__":
    main()
