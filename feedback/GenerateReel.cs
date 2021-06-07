using System.Collections;
using System.Collections.Generic;
using System;

public class Slot_Noguchi
{

    private const int div0 = 7;//赤＄＄＄が出るしきい値
    private const int div1 = 15;//＄＄＄が出るしきい値
    private const int div2 = 20;//BARが出るしきい値
    private const int div3 = 40;//bが出るしきい値
    private const int div4 = 55;//0が出るしきい値
    private const int div5 = 80;//wが出るしきい値

    private List<int> low_list = new List<int>(6){
        0, div0, div1, div2, div3, div4
    };

    private List<int> high_list = new List<int>(6);

    public Slot_Noguchi()
    {
        high_list = new List<int>(low_list);
        high_list.RemoveAt(0);
        high_list.Add(div5);
    }

    public static void Main(){
        var test = new Slot_Noguchi();
        var data_list = new List<int>(){
            0, 7, 15, 20, 40, 55, 80, 90,
            1, 8, 16, 21, 41, 56, 81, 91
                    };
        foreach (var data in data_list)
        {
            System.Console.WriteLine(test.GetTmp(data));
        }
    }

    private int GetTmp(int rand)
    {
        var tmp = 0;
        for (var i = 0; i < low_list.Count; i++)
        {
            if (low_list[i] < rand && rand < high_list[i])
            {
                return tmp;
            }
            tmp += 1;
        }
        return tmp;
    }

}
