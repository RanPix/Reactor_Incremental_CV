using System;
using System.Collections.Generic;

namespace Reactor_Incremental_CV;

class Sprite
{ 
    public static void Draw(byte CurX, byte CurY, char Sprite, ConsoleColor Color)
    {
        Console.SetCursorPosition(CurX, CurY);

        Console.ForegroundColor = Color;

        Console.Write(Sprite);

        Console.SetCursorPosition(CurX, CurY);

        Console.ForegroundColor = ConsoleColor.White;
    }

    public static void Write(byte CurX, byte CurY, string Model, ConsoleColor Color)
    {
        Console.SetCursorPosition(CurX, CurY);

        Console.ForegroundColor = Color;

        Console.Write(Model);

        Console.SetCursorPosition(CurX, CurY);

        Console.ForegroundColor = ConsoleColor.White;
    }
}
