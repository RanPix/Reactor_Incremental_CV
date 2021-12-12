namespace Reactor_Incremental_CV;

class GameCycle
{
    private static int TickCounter;

    static void Main(string[] args)
    {
        InitGame.Init();
        
        while (true) 
        {
            if (!Controls.GamePaused) // if game paused != true
                TickCounter++;

            Controls.KeyChecker();

            if (!Controls.GamePaused && TickCounter == 9000)
            {
                UpdateBlockInfo.BlocksUpdate();
                GameFuncs.DisplayReactorInfo();
                TickCounter = 0;
            }

        }
    }
}
