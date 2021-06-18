using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dic
{
    /// <summary>
    /// 役に対応する柄のディクショナリ
    /// <para>Role :役のこと。列挙型</para>
    /// <para>l :leftの略。左の柄のIDを示す。</para>
    /// <para>c :centerの略。中央の柄のIDを示す。</para>
    /// <para>r :rightの略。右の柄のIDを示す。</para>
    /// </summary>
    public static Dictionary<Role, (int l, int c, int r)> symbolDic = new Dictionary<Role, (int l, int c, int r)>()
    {
        {Role.NONE,(0,0,0) },
        {Role.STRONGCHERRY,(1,1,1)},
        {Role.CHERRY,(1,1,0)  },
        {Role.WEAKCHERRY,(1, 0,0) },
        {Role.WATERMELON,(2, 2, 2) },
        {Role.BELL,(3, 3, 3) },
        {Role.REPLAY,(4, 4, 4) },
        {Role.QUESTION,(5, 5, 5) },
        {Role.REGBONUS,(7, 7, 6) },
        {Role.BIGBONUS,(7, 7, 7) },
        {Role.FREEZE,(7, 7, 7) },
    };
    /// <summary>
    /// int[設定,状態(Condition)]
    /// </summary>
    public static Dictionary<Role, int[,]> dic = new Dictionary<Role, int[,]>()
    {
        {Role.WEAKCHERRY,new int[,]{
            { 1,2,3,4,5},
            { 2,3,4,5,6},
            { 3,4,5,7,8},
        } },
        {Role.CHERRY,new int[,]{
            { 1,2,3,4,5},
            { 2,3,4,5,6},
            { 3,4,5,7,8},
        } },
        {Role.QUESTION,new int[,]{
            { 1,2,3,4,5},
            { 2,3,4,5,6},
            { 3,4,5,7,8},
        } },
        {Role.BELL, new int[,]{
            { 1,2,3,4,5},
            { 2,3,4,5,6},
            { 3,4,5,7,8},
        } },
        {Role.REPLAY, new int[,]{
            { 1,2,3,4,5},
            { 2,3,4,5,6},
            { 3,4,5,7,8},
        } },
        {Role.WATERMELON,new int[,]{
            { 1,2,3,4,5},
            { 2,3,4,5,6},
            { 3,4,5,7,8},
        } },
        {Role.STRONGCHERRY, new int[,]{
            { 1,2,3,4,5},
            { 2,3,4,5,6},
            { 3,4,5,7,8},
        } },
        {Role.FREEZE, new int[,]{
            { 1,2,3,4,5},
            { 2,3,4,5,6},
            { 3,4,5,7,8},
        } },
        {Role.REGBONUS, new int[,]{
            { 1,2,3,4,5},
            { 2,3,4,5,6},
            { 3,4,5,7,8},
        } },
        {Role.BIGBONUS, new int[,]{
            { 1,2,3,4,5},
            { 2,3,4,5,6},
            { 3,4,5,7,8},
        } },
        {Role.NONE, new int[,]{
            { 1,2,3,4,5},
            { 2,3,4,5,6},
            { 3,4,5,7,8},
        } },
    };
    public static Dictionary<Config, Dictionary<Role, int>> prodic = new Dictionary<Config, Dictionary<Role, int>>()
    {
        { Config.LOW,
            new Dictionary<Role, int>{
                {Role.FREEZE,0},
                {Role.BIGBONUS,2 },
                {Role.REGBONUS,8 },
                {Role.STRONGCHERRY,49 },
                {Role.CHERRY,104 },
                {Role.WEAKCHERRY,206 },
                {Role.WATERMELON,274 },
                {Role.QUESTION,550 },
                {Role.BELL,1920 },
                {Role.REPLAY,4651 },
                {Role.NONE,8192 },
            }
        },
        { Config.MIDDLE,
            new Dictionary<Role, int>{
                {Role.FREEZE,0},
                {Role.BIGBONUS,2 },
                {Role.REGBONUS,8 },
                {Role.STRONGCHERRY,49 },
                {Role.CHERRY,104 },
                {Role.WEAKCHERRY,206 },
                {Role.WATERMELON,274 },
                {Role.QUESTION,550 },
                {Role.BELL,1920 },
                {Role.REPLAY,4651 },
                {Role.NONE,8192 },
            }
        },
        { Config.HIGH,
            new Dictionary<Role, int>{
                {Role.FREEZE,0},
                {Role.BIGBONUS,2 },
                {Role.REGBONUS,8 },
                {Role.STRONGCHERRY,49 },
                {Role.CHERRY,104 },
                {Role.WEAKCHERRY,206 },
                {Role.WATERMELON,274 },
                {Role.QUESTION,550 },
                {Role.BELL,1920 },
                {Role.REPLAY,4651 },
                {Role.NONE,8192 },
            }
        }
    };
}
