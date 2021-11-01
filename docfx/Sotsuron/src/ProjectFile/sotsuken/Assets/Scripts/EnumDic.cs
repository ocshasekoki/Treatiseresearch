namespace EnumDic
{

    /// <summary>
    /// 柄の列挙型
    /// <para>NONE：役無し</para>
    /// <para>CHERRY：チェリー</para>
    /// <para>WATERMELON：スイカ</para>
    /// <para>BELL：ベル</para>
    /// <para>REPLAY：リプレイ</para>
    /// <para>QUESTION：問題</para>
    /// <para>BAR：バー</para>
    /// <para>SEVEN：７</para>
    /// </summary>
    public enum Symbol
    {
        NONE = 0,
        CHERRY = 1,
        WATERMELON = 2,
        BELL = 3,
        REPLAY = 4,
        QUESTION = 5,
        BAR = 6,
        SEVEN = 7,
    }


    /// <summary>
    /// 役の列挙型
    /// <para>FREEZE：フリーズ 1/8192</para>
    /// <para>BIGBONUS：ビッグボーナス 1/4096</para>
    /// <para>REGBONUS：レギュラーボーナス 1/2048</para>
    /// <para>STRONGCHERRY：強チェリー 1/250</para>
    /// <para>CHERRY：中チェリー 1/150</para>
    /// <para>WEAKCHERRY：弱チェリー 1/80</para>
    /// <para>WATERMELON：スイカ 1/120</para>
    /// <para>QUESTION：問題出題 1/30</para>
    /// <para>REPLAY：リプレイ 1/5</para>
    /// <para>NONE：役無し</para>
    /// </summary>

    public enum Role
    {
        FREEZE = 0,
        BIGBONUS = 1,
        REGBONUS = 2,
        STRONGCHERRY = 3,
        CHERRY = 4,
        WEAKCHERRY = 5,
        WATERMELON = 6,
        QUESTION = 7,
        BELL = 8,
        REPLAY = 9,
        NONE = 10,
    }

    /// <summary> Condition:状態の列挙型 </summary>
    public enum Condition
    {
        NOMAL = 0,
        CZ = 1,
        BONUS = 2,
        BIGBONUS = 3,
        FREEZE = 4,
        AT = 5,
    }

    /// <summary>Config:設定の列挙</summary>
    public enum Config
    {
        LOW = 0,
        MIDDLE = 1,
        HIGH = 2
    }

    /// <summary>Position:リールの列 </summary>
    public enum Position
    {
        RIGHT = 0,
        MIDDLE = 1,
        LEFT = 2
    }

    public enum Real
    {
        NOBET=0,
        BET=1,
        ROTATE=2,
        ONESTOP=3,
        TWOSTOP=4,
        ALLSTOP=5
    }
}
