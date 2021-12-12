using System;

namespace Reactor_Incremental_CV;

class Controls
{// allows to control the game by pressing keyboard keys
    static byte CurX = 10; // initializes cursor positions
    static byte CurY = 10;

    public static bool GamePaused = false; // to stop the game cycle if spacebar is pressed

    public static void KeyChecker() 
    {
        Console.SetCursorPosition(CurX, CurY); // to make cursor always stay on its position because other functions that writes something just moveing it
        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo PressedKey = Console.ReadKey(true); // takes the key id that was pressed
            switch (PressedKey.Key) // checkes which key id was used
            {
                //movement keys
                case ConsoleKey.W: // move cursor up
                    CurY--;
                    CurY = Math.Clamp(CurY, (byte)1, (byte)18); // to make cursor stay in the borders

                    Console.SetCursorPosition(CurX, CurY);
                    break;
                case ConsoleKey.S: // move cursor down
                    CurY++;
                    CurY = Math.Clamp(CurY, (byte)1, (byte)18);

                    Console.SetCursorPosition(CurX, CurY);
                    break;
                case ConsoleKey.A: // move cursor left
                    CurX--;
                    CurX = Math.Clamp(CurX, (byte)1, (byte)29);

                    Console.SetCursorPosition(CurX, CurY);
                    break;
                case ConsoleKey.D: // move cursor right
                    CurX++;
                    CurX = Math.Clamp(CurX, (byte)1, (byte)29);

                    Console.SetCursorPosition(CurX, CurY);
                    break;
                //movement keys

                //game interaction keys
                case ConsoleKey.N:// places block
                    GameFuncs.PlaceBlock(CurX, CurY, in GameVars.BlockTypeIdx);
                    break;
                case ConsoleKey.M:// removes block
                    GameFuncs.RemoveBlock(CurX, CurY);
                    break;

                case ConsoleKey.Spacebar: // pausing the game
                    GamePaused = !GamePaused;

                    if (GamePaused)
                        Sprite.Write(31, 19, "Pause", ConsoleColor.Red);
                    else
                        Sprite.Write(31, 19, "     ", ConsoleColor.Black);
                    break;
                case ConsoleKey.H: // takes money from generated power
                    if(!GamePaused)
                        GameVars.ReactorHeat--;
                    //GameVars.ReactorHeat = Math.Clamp(GameVars.ReactorHeat, 0, long.MaxValue);
                    break;
                /*case ConsoleKey.B:
                    GameVars.Money += GameVars.Power;

                    GameVars.Power = 0;
                    break;*/
                //game interaction keys

                //interface keys
                case ConsoleKey.J: // takes the block moving the list to the left
                    GameVars.BlockTypeIdx--;
                    GameVars.BlockTypeIdx = (GameVars.BlockType)Math.Clamp((int)GameVars.BlockTypeIdx, 0, (int)GameVars.BlockType.Count - 1);

                    GameFuncs.SetBlock(in GameVars.BlockTypeIdx);
                    break;
                case ConsoleKey.K: // takes the block moving the list to the right
                    GameVars.BlockTypeIdx++;
                    GameVars.BlockTypeIdx = (GameVars.BlockType)Math.Clamp((int)GameVars.BlockTypeIdx, 0, (int)GameVars.BlockType.Count - 1);

                    GameFuncs.SetBlock(in GameVars.BlockTypeIdx);
                    break;

                case ConsoleKey.U: // saving
                    GameStateSaver.SaveState();

                    Sprite.Write(31, 18, "Saved ", ConsoleColor.Yellow);
                    break;
                case ConsoleKey.I: // loading save
                    GameStateSaver.SaveReader();

                    Sprite.Write(31, 18, "Loaded", ConsoleColor.Yellow);

                    GamePaused = true;

                    Sprite.Write(31, 19, "Pause", ConsoleColor.Red);

                    InitGame.InitGraphics();
                    break;

                case ConsoleKey.O:
                    Sounds.MusicPlayer();
                    break;
                //interface keys
            }
        }
    }
}
