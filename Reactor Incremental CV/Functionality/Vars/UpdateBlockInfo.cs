namespace Reactor_Incremental_CV;

class UpdateBlockInfo
{
    public static void BlocksUpdate()
    {
        GameVars.MaxReactorHeat = 1000; // to make platings work correctly

        for (int i = 0; i < GameVars.Platings.Count; i++)
            GameVars.Platings[i].Update();

        for (int i = 0; i < GameVars.Cells.Count; i++)
            GameVars.Cells[i].Update();

        for (int i = 0; i < GameVars.Reflectors.Count; i++)
            GameVars.Reflectors[i].Update();

        for (int i = 0; i < GameVars.Outlets.Count; i++)
            GameVars.Outlets[i].Update();

        for (int i = 0; i < GameVars.Vents.Count; i++)
            GameVars.Vents[i].Update();

    }
}
