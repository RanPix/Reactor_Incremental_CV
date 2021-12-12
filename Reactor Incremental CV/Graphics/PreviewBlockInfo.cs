using System;

namespace Reactor_Incremental_CV;

class PreviewBlockInfo // bad naming here
{
    public static void WriteInfo(in GameVars.BlockType type, in char bSprite, in ConsoleColor bColor, in int bPrice, in int bInfo, in int bInfo2 = 0, in int bInfo3 = 0)
    {
        for (byte i = 3; i <= 7; i++) // erases old info
            Sprite.Write(31, i, new string(' ', 24));

        Sprite.Write(31, 3, $"<(J) [ {bSprite} ] (K)>", bColor);

        switch (type)
        { // checking which block info to show
            // cells block info
            case GameVars.BlockType.UraniumCell:
            case GameVars.BlockType.DoubleUraniumCell:
            case GameVars.BlockType.PlutoniumCell:
            case GameVars.BlockType.DoublePlutoniumCell:
            case GameVars.BlockType.ThoriumCell:
            case GameVars.BlockType.DoubleThoriumCell:
                Sprite.Write(31, 4, $"Power Gen: {bInfo}", ConsoleColor.Red); // writes info
                Sprite.Write(31, 5, $"Heat Gen: {bInfo2}", ConsoleColor.Red);
                Sprite.Write(31, 6, $"Durability: {bInfo3}", ConsoleColor.Red);
                Sprite.Write(31, 7, $"Price: {bPrice}", ConsoleColor.Red);
                break;
            // cells block info

            // reflectors block info
            case GameVars.BlockType.BasicReflector:
            case GameVars.BlockType.AdvancedReflector:
            case GameVars.BlockType.SuperReflector:
                Sprite.Write(31, 4, $"Durability: {bInfo}", ConsoleColor.Red);
                Sprite.Write(31, 5, $"Power Gen Boost: 10%", ConsoleColor.Red);
                Sprite.Write(31, 7, $"Price: {bPrice}", ConsoleColor.Red);
                break;
            // reflectors block info

            // platings block info
            case GameVars.BlockType.BasicPlating:
            case GameVars.BlockType.AdvancedPlating:
            case GameVars.BlockType.SuperPlating:
                Sprite.Write(31, 4, $"Heat Cap: {bInfo}", ConsoleColor.Red);
                Sprite.Write(31, 7, $"Price: {bPrice}", ConsoleColor.Red);
                break;
            // platings block info

            // outlet block info
            case GameVars.BlockType.BasicOutlet:
            case GameVars.BlockType.AdvancedOutlet:
            case GameVars.BlockType.SuperOutlet:
                Sprite.Write(31, 4, $"Heat Transfer: {bInfo}", ConsoleColor.Red);
                Sprite.Write(31, 7, $"Price: {bPrice}", ConsoleColor.Red);
                break;
            // outlet block info

            // vents block info
            case GameVars.BlockType.BasicVent:
            case GameVars.BlockType.AdvancedVent:
            case GameVars.BlockType.SuperVent:
                Sprite.Write(31, 4, $"Heat Vent: {bInfo}", ConsoleColor.Red);
                Sprite.Write(31, 5, $"Heat Cap: {bInfo2}", ConsoleColor.Red);
                Sprite.Write(31, 7, $"Price: {bPrice}", ConsoleColor.Red);
                break;
            // vents block info
        }
    }
}
