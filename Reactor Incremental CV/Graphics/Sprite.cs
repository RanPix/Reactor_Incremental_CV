using System;

namespace Reactor_Incremental_CV;

class Sprite
{ 
    public static void Write(byte CurX, byte CurY, string Model, ConsoleColor SpriteColor = ConsoleColor.White, ConsoleColor BGColor = ConsoleColor.Black)
    {
        Console.SetCursorPosition(CurX, CurY);

        Console.ForegroundColor = SpriteColor;
        Console.BackgroundColor = BGColor;

        Console.Write(Model);

        Console.SetCursorPosition(CurX, CurY);

        Console.ForegroundColor = ConsoleColor.White;
    }
}
