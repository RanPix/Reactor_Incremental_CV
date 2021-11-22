namespace Reactor_Incremental_CV;

class UpdateBlockInfo
{
    public static void BlocksUpdate()
    {
        for (int i = 0; i < GameVars.Cells.Count; i++)
            GameVars.Cells[i].Update(i, ref GameVars.Cells);

        for (int i = 0; i < GameVars.Vents.Count; i++)
            GameVars.Vents[i].Update(ref GameVars.Vents);
    }
}
