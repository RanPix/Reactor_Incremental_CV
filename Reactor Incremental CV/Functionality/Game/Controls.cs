using System;

namespace Reactor_Incremental_CV
{
    // allows to control the game by pressing keyboard keys
    class Controls
    {
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
                        CurY = Math.Clamp(CurY, (byte)0, (byte)18); // to make cursor stay in the borders

                        Console.SetCursorPosition(CurX, CurY);
                        break;
                    case ConsoleKey.S: // move cursor down
                        CurY++;
                        CurY = Math.Clamp(CurY, (byte)0, (byte)18);

                        Console.SetCursorPosition(CurX, CurY);
                        break;
                    case ConsoleKey.A: // move cursor left
                        CurX--;
                        CurX = Math.Clamp(CurX, (byte)0, (byte)34);

                        Console.SetCursorPosition(CurX, CurY);
                        break;
                    case ConsoleKey.D: // move cursor right
                        CurX++;
                        CurX = Math.Clamp(CurX, (byte)0, (byte)34);

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
                            Sprite.Write(30, 20, "Pause", ConsoleColor.Red);
                        else
                            Sprite.Write(30, 20, "     ", ConsoleColor.Black);

                        break;
                    //game interaction keys
                    //interface keys
                    case ConsoleKey.H: // takes money from generated power
                        GameFuncs.ReactorHeat--;
                        GameFuncs.ReactorHeat = Math.Clamp(GameFuncs.ReactorHeat, 0, 999999);
                        break;

                    case ConsoleKey.J: // takes the block moving the list to the left
                        GameVars.BlockTypeIdx--;
                        GameVars.BlockTypeIdx = (GameVars.BlockType)Math.Clamp((int)GameVars.BlockTypeIdx, 0, (int)GameVars.BlockType.Count - 1);
                        
                        GameFuncs.SetBlock(in GameVars.BlockTypeIdx);  // re write нахуй 
                        break;
                    case ConsoleKey.K: // takes the block moving the list to the right
                        GameVars.BlockTypeIdx++;
                        GameVars.BlockTypeIdx = (GameVars.BlockType)Math.Clamp((int)GameVars.BlockTypeIdx, 0, (int)GameVars.BlockType.Count - 1);

                        GameFuncs.SetBlock(in GameVars.BlockTypeIdx);
                        break;

                    case ConsoleKey.U: // saving
                        GameStateSaver.SaveFiles();

                        Sprite.Write(23, 20, "Saved ", ConsoleColor.Yellow);
                        break;
                    case ConsoleKey.I: // loading save
                        GameStateSaver.SaveReader();

                        Sprite.Write(23, 20, "Loaded", ConsoleColor.Yellow);

                        GameFuncs.DisplayReactorInfo();

                        GamePaused = true;

                        Sprite.Write(30, 20, "Pause", ConsoleColor.Red);

                        GameFuncs.SetBlock(GameVars.BlockTypeIdx);
                        break;

                    case ConsoleKey.T:
                        Sprite.Write(15, 21, "Controls: W, A, S, D", ConsoleColor.White);
                        Sprite.Write(15, 22, "U, I, N, M, H, SPACE", ConsoleColor.White);
                        break;
                }
            }
        }
    }
}
