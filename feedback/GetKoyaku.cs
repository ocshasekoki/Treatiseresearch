using System;
using System.Collections.Generic;
public class Slot_Mizui{
    public static void Main(){
        // Your code here!
        
        var test = new Slot_Mizui();
        var dataList = new List<int>(){
            -1,1000, 0, 770, 895, 945, 985, 993, 997, 999, 894, 944, 984, 992, 996, 998
        };
        foreach (var data in dataList)
        {
            System.Console.WriteLine(data);
            System.Console.WriteLine(test.GetKoyaku(data));
        }
    }
    
    public int GetKoyaku(int lottery)
        {
            if (lottery <= 769 || lottery > 999) return 0;

            var result = 1;
            var conditionLists = new List<List<int>>(6){
                new List<int>(1){770, 894},
                new List<int>(1){895,944},
                new List<int>(1){945,984},
                new List<int>(1){985,992},
                new List<int>(1){993,996},
                new List<int>(1){997,998},
            };

            foreach( var conditionList in conditionLists)
            {
                if (lottery >= conditionList[0] && lottery <= conditionList[1]) return result;
                result += 1;
            }

            return result;

        }
}
