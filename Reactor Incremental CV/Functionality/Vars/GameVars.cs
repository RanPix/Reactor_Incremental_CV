using System;
using System.Collections.Generic;

namespace Reactor_Incremental_CV;

class GameVars
{
    public static Random rand = new Random();

    public static List<CellsInfo> Cells = new List<CellsInfo>();

    public static List<VentsInfo> Vents = new List<VentsInfo>();

    public enum BlockType // for switching between the blocks that you can place
    {
        UraniumCell,
        DoubleUraniumCell,
        PlutoniumCell,
        DoublePlutoniumCell,
        BasicVent,
        AdvancedVent,
        Count
    }
    public static BlockType BlockTypeIdx; // to store which block you gonna build with

    public static bool CellsNotColliding = true;
}

public class CellsInfo
{
    public byte X;
    public byte Y;

    public byte CellLife;

    public int CellPowerGen;
    public int CellHeatGen;

    public char CellSprite; // for loading save 
    public ConsoleColor CellColor;

    public CellsInfo(byte sCellX, byte sCellY, byte sCellLife, int sCellPowerGen, int sCellHeatGen, char sCellSprite, ConsoleColor sCellColor)
    {// 's' in variables means "set"
        this.X = sCellX;
        this.Y = sCellY;

        this.CellLife = sCellLife;

        this.CellPowerGen = sCellPowerGen;
        this.CellHeatGen = sCellHeatGen;

        this.CellSprite = sCellSprite;
        this.CellColor = sCellColor;
    }

    public void Update(int ListIndex, ref List<CellsInfo> CellType)
    {
        this.CellLife--; // removes 1 life from object of *THIS* index
 
        if (this.CellLife <= 0)
        { // removes the object from list but places sprite of died cell
            CellType.RemoveAt(ListIndex);
            Sprite.Draw(this.X, this.Y, this.CellSprite, ConsoleColor.DarkGray);
        }

        WhenColliding(ref CellType, 1, 0); // checks the colliding in 4 directions
        WhenColliding(ref CellType, 0, 1);
        WhenColliding(ref CellType, -1, 0);
        WhenColliding(ref CellType, 0, -1);

        if(GameVars.CellsNotColliding == true)
        {
            GameFuncs.Money += this.CellPowerGen;
            GameFuncs.ReactorHeat += this.CellHeatGen;
        }

        GameFuncs.ReactorHeat = Math.Clamp(GameFuncs.ReactorHeat, 0, 999999);

        GameVars.CellsNotColliding = true;
    }

    void WhenColliding(ref List<CellsInfo> CellType, int dirX, int dirY)
    {
        if (CellType.Exists(CBT => this.X + dirX == CBT.X && this.Y + dirY == CBT.Y))
        {
            GameFuncs.Money += this.CellPowerGen * 2;
            GameFuncs.ReactorHeat += this.CellHeatGen * 4;

            GameVars.CellsNotColliding = false;
        }
    }
}

public class VentsInfo
{
    public byte X;
    public byte Y;

    public int VentingPower;

    public ConsoleColor VentColor; // for loading save

    public VentsInfo(byte sVentX, byte sVentY, int sVentingPower, ConsoleColor sVentColor)
    {// 's' in variables means "set"
        this.X = sVentX;
        this.Y = sVentY;

        this.VentingPower = sVentingPower;

        this.VentColor = sVentColor;
    }

    public void Update(ref List<VentsInfo> VentType)
    {
        GameFuncs.ReactorHeat -= this.VentingPower;

        GameFuncs.ReactorHeat = Math.Clamp(GameFuncs.ReactorHeat, 0, 999999);
    }
}

/*public class CellsSpecInf
{
    public int HeatGen;
    public int PowerGen;

    public int CellPrice;
    public int CellLife;

    public char CellSprite;
    public ConsoleColor CellColor;

    public CellsSpecInf (int sHeatGen, int sPowerGen, int sCellPrice, int sCellLife, char sCellSprite, ConsoleColor sCellColor)
    {
    }
}

public class VentsSpecInf
{
    public int VentLife;

    public int VentPrice;

    public char VentSprite;
    public ConsoleColor VentColor;

    public VentsSpecInf(int sVentLife, int sVentPrice, char sVentSprite, ConsoleColor sVentColor)
    {
        this.VentLife = sVentLife;

        this.VentPrice = sVentPrice;

        this.VentSprite = sVentSprite;
        this.VentColor = sVentColor;
    }
}*/
