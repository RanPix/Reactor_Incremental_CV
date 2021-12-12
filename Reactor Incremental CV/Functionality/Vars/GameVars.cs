using System;
using System.Collections.Generic;

namespace Reactor_Incremental_CV;

class GameVars
{
    public static long ReactorHeat = 0; // for displaying reactor heat
    public static long MaxReactorHeat = 1000; // yes

    //public static long MaxPowerCap = 100; // can be expanded with power capacitors
    //public static long Power = 0; // sellable power
    public static long Money = 10; // money

    public static Random rand = new Random();

    public static List<CellsInfo> Cells = new List<CellsInfo>();
    public static List<ReflectorsInfo> Reflectors = new List<ReflectorsInfo>();
    public static List<PlatingsInfo> Platings = new List<PlatingsInfo>();
    public static List<VentsInfo> Vents = new List<VentsInfo>();
    public static List<OutletsInfo> Outlets = new List<OutletsInfo>();

    public enum BlockType // for switching between the blocks that you can place
    {
        // cells
        UraniumCell,
        DoubleUraniumCell,

        PlutoniumCell,
        DoublePlutoniumCell,

        ThoriumCell,
        DoubleThoriumCell,
        // cells

        // reflectors
        BasicReflector,
        AdvancedReflector,
        SuperReflector,
        // reflectors

        // platings
        BasicPlating,
        AdvancedPlating,
        SuperPlating,
        // platings

        // outlets
        BasicOutlet,
        AdvancedOutlet,
        SuperOutlet,
        // outlets

        // vents
        BasicVent,
        AdvancedVent,
        SuperVent,
        // vents

        Count // yes
    }
    public static BlockType BlockTypeIdx; // to store which block you gonna build with

    public static bool CellsColliding = false;
}

// SHOULD I DO SEPERATE SCRIPT FOR EACH BLOCK INFO????

public class CellsInfo // class for fuel cells
{
    public byte X;
    public byte Y;

    public int CellLife;

    public int CellPowerGen;
    public int CellHeatGen;

    public string CellSprite; // for loading save 
    public ConsoleColor CellColor;

    public CellsInfo(byte sCellX, byte sCellY, int sCellLife, int sCellPowerGen, int sCellHeatGen, string sCellSprite, ConsoleColor sCellColor)
    {// 's' in variables means "set"
        this.X = sCellX;
        this.Y = sCellY;

        this.CellLife = sCellLife;

        this.CellPowerGen = sCellPowerGen;
        this.CellHeatGen = sCellHeatGen;

        this.CellSprite = sCellSprite;
        this.CellColor = sCellColor;
    }

    public void Update() // vent functionality
    {
        this.CellLife--; // removes 1 life from object of *THIS* index
 
        if (this.CellLife <= 0)
        { // removes the object from list but places sprite of died cell
            GameVars.Cells.Remove(this);
            Sprite.Write(this.X, this.Y, this.CellSprite, ConsoleColor.DarkGray);
        }

        WhenColliding(1, 0, ref GameVars.Cells, ref GameVars.Reflectors); // checks the colliding in 4 directions
        WhenColliding(0, 1, ref GameVars.Cells, ref GameVars.Reflectors);
        WhenColliding(-1, 0, ref GameVars.Cells, ref GameVars.Reflectors);
        WhenColliding(0, -1, ref GameVars.Cells, ref GameVars.Reflectors);

        if(!GameVars.CellsColliding)
        {
            GameVars.Money += this.CellPowerGen;
            GameVars.ReactorHeat += this.CellHeatGen;
        }

        GameVars.CellsColliding = false;
    }

    private void WhenColliding(int dirX, int dirY, ref List<CellsInfo> CellType, ref List<ReflectorsInfo> ReflectorType)
    {
        if (CellType.Exists(CBT => this.X + dirX == CBT.X && this.Y + dirY == CBT.Y))
        {
            GameVars.Money += this.CellPowerGen * 2;
            GameVars.ReactorHeat += this.CellHeatGen * 4;

            GameVars.CellsColliding = true;
        }

        if (ReflectorType.Exists(RBT => this.X + dirX == RBT.X && this.Y + dirY == RBT.Y))
        {
            GameVars.Money += (long)(this.CellPowerGen * 1.1);
            GameVars.ReactorHeat += this.CellHeatGen;

            int index = ReflectorType.FindIndex(RBT => this.X + dirX == RBT.X && this.Y + dirY == RBT.Y);
            ReflectorType[index].ReflectorLife--;

            GameVars.CellsColliding = true;
        }
    }
}

public class ReflectorsInfo // class for neutron reflectors
{
    public byte X;
    public byte Y;

    public int ReflectorLife; 

    public ConsoleColor ReflectorColor;

    public ReflectorsInfo(byte sReflectorX, byte sReflectorY, int sReflectorLife, ConsoleColor sReflectorColor)
    {
        this.X = sReflectorX;
        this.Y = sReflectorY;

        this.ReflectorLife = sReflectorLife;

        this.ReflectorColor = sReflectorColor;
    }

    public void Update()
    {
        if (ReflectorLife <= 0)
        {
            GameVars.Reflectors.Remove(this);
            Sprite.Write(this.X, this.Y, " ");
        }
    }
}

public class PlatingsInfo // class for reactor platings
{
    public byte X;
    public byte Y;

    public long PlatingHeatCap;

    public ConsoleColor PlatingColor;

    public PlatingsInfo(byte sPlatingX, byte sPlatingY, long sPlatingHeatCap, ConsoleColor sPlatingColor)
    {
        this.X = sPlatingX;
        this.Y = sPlatingY;

        this.PlatingHeatCap = sPlatingHeatCap;

        this.PlatingColor = sPlatingColor;
    }

    public void Update()
    {
        GameVars.MaxReactorHeat += this.PlatingHeatCap;
    } 
}

/*public class CapacitorsInfo // class for energy capacitors
{
    public byte X;
    public byte Y;

    public long CapacitorStorage;

    public int CapacitorBuff; // the % of auto selling the power

    public ConsoleColor CapacitorColor;

    public CapacitorsInfo(byte sCapacitorX, byte sCapacitorY, long sCapacitorStorage, int sCapacitorBuff, ConsoleColor sCapacitorColor)
    {
        this.X = sCapacitorX;
        this.Y = sCapacitorY;

        this.CapacitorStorage = sCapacitorStorage;

        this.CapacitorBuff = sCapacitorBuff;

        this.CapacitorColor = sCapacitorColor;
    }

    public void Update()
    {

    }
}*/

public class OutletsInfo // class for heat outlets 
{ 
    public byte X;
    public byte Y;

    public int TransferCap;

    public ConsoleColor OutletColor;

    public OutletsInfo(byte sOutletX, byte sOutletY, int sTransferCap, ConsoleColor sOutletColor)
    {
        this.X = sOutletX;
        this.Y = sOutletY;

        this.TransferCap = sTransferCap;

        this.OutletColor = sOutletColor;
    }

    public void Update()
    {
        WhenColliding(1, 0, ref GameVars.Vents);
        WhenColliding(0, 1, ref GameVars.Vents);
        WhenColliding(-1, 0, ref GameVars.Vents);
        WhenColliding(0, -1, ref GameVars.Vents);
    }

    private void WhenColliding(int dirX, int dirY, ref List<VentsInfo> VentType)
    {
        if (VentType.Exists(VBT => this.X + dirX == VBT.X && this.Y + dirY == VBT.Y))
        {
            int blockIndex = VentType.FindIndex(VBT => this.X + dirX == VBT.X && this.Y + dirY == VBT.Y);

            GameVars.ReactorHeat -= VentType[blockIndex].VentingPower + this.TransferCap;
            VentType[blockIndex].touchingOutletsCount++;
        }
    }
}

public class VentsInfo // class for heat vents
{
    public byte X;
    public byte Y;

    public int VentingPower;

    public int VentLife; // vents current life, vent life == venting power * 10
    private int VML; // vent max life

    public ConsoleColor VentColor; // for loading save

    public byte touchingOutletsCount;

    public VentsInfo(byte sVentX, byte sVentY, int sVentingPower, int sVentLife, ConsoleColor sVentColor)
    {// 's' in variables means "set"
        this.X = sVentX;
        this.Y = sVentY;

        this.VentingPower = sVentingPower;

        this.VentLife = sVentLife;
        this.VML = sVentLife;

        this.VentColor = sVentColor;
    }

    public void Update() // vent functionality // re write нахуй (maybe)
    {
        if (GameVars.ReactorHeat > 0) // if heat is more than it should be starting to destroy the vent
            this.VentLife -= VML / 7;

        WhenColliding(1, 0, GameVars.Cells);
        WhenColliding(0, 1, GameVars.Cells);
        WhenColliding(-1, 0, GameVars.Cells);
        WhenColliding(0, -1, GameVars.Cells);

        if (GameVars.ReactorHeat == 0 && this.VentLife > 0)
        {
            this.VentLife += VML / 7;
            this.VentLife = Math.Clamp(VentLife, 0, VML);
        }

        VentHeatPrev(VML);

        if (touchingOutletsCount > 1)
        {
            GameVars.Vents.Remove(this);
            Sprite.Write(this.X, this.Y, " ");
        }
        this.touchingOutletsCount = 0;

        if (this.VentLife <= 0)
        {
            GameVars.Vents.Remove(this);
            Sprite.Write(this.X, this.Y, " ");
        }
    }

    private void WhenColliding(int dirX, int dirY, List<CellsInfo> CellType)
    {
        if (CellType.Exists(CBT => this.X + dirX == CBT.X && this.Y + dirY == CBT.Y)) // CBT = cell block type
        {
            this.VentLife -= CellType[CellType.FindIndex(CBT => this.X + dirX == CBT.X && this.Y + dirY == CBT.Y)].CellHeatGen;
            GameVars.ReactorHeat -= this.VentingPower;
        }
    }

    private void VentHeatPrev(int VML) // previewing the life of the vent on the console 
    { 
        if (this.VentLife >= VML * 0.75) // shit, re write нахуй
            Sprite.Write(this.X, this.Y, "#", this.VentColor);
        else if (this.VentLife <= VML * 0.35)
            Sprite.Write(this.X, this.Y, "#", this.VentColor, ConsoleColor.Red);
    }
}