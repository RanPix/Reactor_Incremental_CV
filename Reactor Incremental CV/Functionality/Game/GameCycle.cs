using System;

namespace Reactor_Incremental_CV
{
    class GameCycle
    {
        private static int TickCounter;

        static void Main(string[] args)
        {
            InitGame();

            while (true)
            {
                if (Controls.GamePaused != true)
                    TickCounter++;

                if(Controls.GamePaused != true && TickCounter == 10000)
                {
                    GameFuncs.DisplayReactorInfo();
                    UpdateBlockInfo.BlocksUpdate();
                    TickCounter = 0;
                }

                Controls.KeyChecker();
            }
        }
        //╣ ║ ╗ ╝ ╚ ╔ ╩ ╦ ╠ ╬ ═
        static void InitGame()
        {
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.Title = "Reactor Incremental CV";

            Console.SetWindowSize(35, 26);

            Sprite.Write(0, 19, new string('═', 35), ConsoleColor.White);

            GameFuncs.SetBlock(in GameVars.BlockTypeIdx);

            Sprite.Write(15, 21, "For controls - (T)", ConsoleColor.White); 

            Sounds.MusicPlayer();
        }
    }
}
