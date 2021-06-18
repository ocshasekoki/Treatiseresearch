using System;
using System.Windows;
using System.Windows.Controls;

public class Program
{
    [STAThread]
    static void Main()
    {
        var button = new Button { Content = "ここを押せ" };
        button.Click += (sender, e) => MessageBox.Show("ようこそ");

        var win = new Window
        {
            Title = "サンプルプログラム",
            Width = 300,
            Height = 200,
            Content = button,
        };

        var app = new Application();
        app.Run(win);
    }
}

public class Hello
{
    public static void Main()
    {
        Console.WriteLine("hello world");
    }
}

// コメントを入力する
using System;
public class Program
{
    public static void Main()
    {
        Console.WriteLine("hello world1"); //文字を表示
        /*Console.WriteLine("hello world2");
        Console.WriteLine("hello world3");*/
    }
}

// HTMLを表示する
using System;
public class Program
{
    public static void Main()
    {
        Console.Write("<h1>hello world</h1>");
        Console.Write("<p>世界の皆さん、");
        Console.Write("<b>こんにちは</b></p>");
    }
}
s