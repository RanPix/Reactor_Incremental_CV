using System;

namespace Reactor_Incremental_CV;

class InitGame
{
    public static void Init()
    {
        InitConsole();

        InitGraphics();
    }

    private static void InitConsole()
    { // sets console
        Console.InputEncoding = System.Text.Encoding.UTF8;
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.Title = "Reactor Incremental CV";

        Console.SetWindowSize(55, 20);

        Sounds.MusicPlayer();
    }

    public static void InitGraphics()
    { // sets reactor graphics
        GameFuncs.SetBlock(in GameVars.BlockTypeIdx);

        GameFuncs.DisplayReactorInfo();

        Sprite.Write(31, 9, "Keys: W,A,S,D, U, I");
        Sprite.Write(31, 10, "O, N, M, H, SPACE, T");

        for (byte i = 1; i < 19; i++) // draws a reactor borders
            Sprite.Write(30, i, "║");

        for (byte i = 1; i < 19; i++)
            Sprite.Write(0, i, "║");

        Sprite.Write(1, 0, new string('═', 29));
        Sprite.Write(1, 19, new string('═', 29));

        Sprite.Write(0, 0, "╔"); // draws conrers
        Sprite.Write(30, 0, "╗");
        Sprite.Write(0, 19, "╚");
        Sprite.Write(30, 19, "╝");
    }
}
//╣ ║ ╗ ╝ ╚ ╔ ╩ ╦ ╠ ╬ ═