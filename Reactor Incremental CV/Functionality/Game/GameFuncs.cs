using System;

namespace Reactor_Incremental_CV;

class GameFuncs
{
    public static void DisplayReactorInfo()
    {
        Sprite.Write(31, 0, new string(' ', 24)); // erase old string to update it
        Sprite.Write(31, 1, new string(' ', 24));

        GameVars.ReactorHeat = Math.Clamp(GameVars.ReactorHeat, 0, long.MaxValue);

        Sprite.Write(31, 0, $"Money: {GameVars.Money}", ConsoleColor.Yellow); // show info
        Sprite.Write(31, 1, $"Heat: {GameVars.ReactorHeat} / {GameVars.MaxReactorHeat}", ConsoleColor.Yellow);

        if (GameVars.ReactorHeat > GameVars.MaxReactorHeat && !Controls.GamePaused)
            ReactorExplosion();
    }

    public static void ReactorExplosion()
    {
        for (byte i = 1; i < 19; i++) // explosion effect
            Sprite.Write(1, i, new string('*', 29), ConsoleColor.DarkRed);

        System.Threading.Thread.Sleep(500);

        for (byte i = 1; i < 19; i++) // erase explosion
            Sprite.Write(1, i, new string(' ', 29), ConsoleColor.DarkRed);

        GameVars.Cells.Clear(); // delete every block in reactor
        GameVars.Vents.Clear();
        GameVars.Outlets.Clear();
        GameVars.Reflectors.Clear();
        GameVars.Platings.Clear();
    }

    public static void SetBlock(in GameVars.BlockType type) // sets the block that you gonna build with
    {
        switch (type)
        {
            // cells
            case GameVars.BlockType.UraniumCell:
                PreviewBlockInfo.WriteInfo(in GameVars.BlockTypeIdx, '|', ConsoleColor.DarkGreen, 10, 1, 1, 15);
                break;
            case GameVars.BlockType.DoubleUraniumCell:
                PreviewBlockInfo.WriteInfo(in GameVars.BlockTypeIdx, '‖', ConsoleColor.DarkGreen, 25, 4, 8, 20);
                break;

            case GameVars.BlockType.PlutoniumCell:
                PreviewBlockInfo.WriteInfo(in GameVars.BlockTypeIdx, '|', ConsoleColor.DarkYellow, 7000, 150, 150, 60);
                break;
            case GameVars.BlockType.DoublePlutoniumCell:
                PreviewBlockInfo.WriteInfo(in GameVars.BlockTypeIdx, '‖', ConsoleColor.DarkYellow, 14000, 600, 1200, 100);
                break;

            case GameVars.BlockType.ThoriumCell:
                PreviewBlockInfo.WriteInfo(in GameVars.BlockTypeIdx, '|', ConsoleColor.Cyan, 4700000, 7500, 7500, 900);
                break;
            case GameVars.BlockType.DoubleThoriumCell:
                PreviewBlockInfo.WriteInfo(in GameVars.BlockTypeIdx, '‖', ConsoleColor.Cyan, 10400000, 29600, 59200, 1200);
                break;
            // cells 

            // reflectors
            case GameVars.BlockType.BasicReflector:
                PreviewBlockInfo.WriteInfo(in GameVars.BlockTypeIdx, 'H', ConsoleColor.Green, 500, 200);
                break;

            case GameVars.BlockType.AdvancedReflector:
                PreviewBlockInfo.WriteInfo(in GameVars.BlockTypeIdx, 'H', ConsoleColor.Yellow, 5000, 1250);
                break;

            case GameVars.BlockType.SuperReflector:
                PreviewBlockInfo.WriteInfo(in GameVars.BlockTypeIdx, 'H', ConsoleColor.Blue, 50000, 15000);
                break;
            // reflectors

            // platings
            case GameVars.BlockType.BasicPlating:
                PreviewBlockInfo.WriteInfo(in GameVars.BlockTypeIdx, '0', ConsoleColor.DarkGray, 1000, 100);
                break;

            case GameVars.BlockType.AdvancedPlating:
                PreviewBlockInfo.WriteInfo(in GameVars.BlockTypeIdx, '0', ConsoleColor.Gray, 160000, 14000);
                break;

            case GameVars.BlockType.SuperPlating:
                PreviewBlockInfo.WriteInfo(in GameVars.BlockTypeIdx, '0', ConsoleColor.DarkBlue, 2560000, 1960000);
                break;
            // platings

            // outlets
            case GameVars.BlockType.BasicOutlet:
                PreviewBlockInfo.WriteInfo(in GameVars.BlockTypeIdx, '&', ConsoleColor.Cyan, 150, 16);
                break;

            case GameVars.BlockType.AdvancedOutlet:
                PreviewBlockInfo.WriteInfo(in GameVars.BlockTypeIdx, '&', ConsoleColor.DarkCyan, 32000, 1200);
                break;

            case GameVars.BlockType.SuperOutlet:
                PreviewBlockInfo.WriteInfo(in GameVars.BlockTypeIdx, '&', ConsoleColor.Blue, 6400000, 90000);
                break;
            //outlets

            // vents
            case GameVars.BlockType.BasicVent:
                PreviewBlockInfo.WriteInfo(in GameVars.BlockTypeIdx, '#', ConsoleColor.Gray, 50, 4, 40);
                break;

            case GameVars.BlockType.AdvancedVent:
                PreviewBlockInfo.WriteInfo(in GameVars.BlockTypeIdx, '#', ConsoleColor.DarkGray, 12500, 300, 3000);
                break;

            case GameVars.BlockType.SuperVent:
                PreviewBlockInfo.WriteInfo(in GameVars.BlockTypeIdx, '#', ConsoleColor.DarkMagenta, 3125000, 25500, 255000);
                break;
                // vents
        }
    }

    public static void PlaceBlock(byte CurX, byte CurY, in GameVars.BlockType type)
    {
        bool has_cell = GameVars.Cells.Exists(BP => BP.X == CurX && BP.Y == CurY); // BP = Block position
        bool has_vent = GameVars.Vents.Exists(BP => BP.X == CurX && BP.Y == CurY);
        bool has_outlet = GameVars.Outlets.Exists(BP => BP.X == CurX && BP.Y == CurY);
        bool has_reflector = GameVars.Reflectors.Exists(BP => BP.X == CurX && BP.Y == CurY);
        bool has_plating = GameVars.Platings.Exists(BP => BP.X == CurX && BP.Y == CurY);

        if (!has_cell && !has_vent && !has_outlet && !has_reflector && !has_plating) // checks if there is a block on a place where you want to place
        {
            switch (type)
            {
                //cells
                case GameVars.BlockType.UraniumCell: // Uranium Cell
                    if (GameVars.Money < 10)
                        break;
                    
                    GameVars.Money -= 10;

                    Sprite.Write(CurX, CurY, "|", ConsoleColor.DarkGreen); // draws a sprite of a block

                    GameVars.Cells.Add(new CellsInfo(CurX, CurY, 15, 1, 1, "|", ConsoleColor.DarkGreen)); // adds a block to the block list
                    
                    break;

                case GameVars.BlockType.DoubleUraniumCell: // Double Uranium Cell
                    if (GameVars.Money < 25)
                        break;
                   
                    GameVars.Money -= 25;

                    Sprite.Write(CurX, CurY, "‖", ConsoleColor.DarkGreen);

                    GameVars.Cells.Add(new CellsInfo(CurX, CurY, 20, 4, 8, "‖", ConsoleColor.DarkGreen)); 
                    break;

                case GameVars.BlockType.PlutoniumCell: // Plutonium Cell
                    if (GameVars.Money < 7000)
                        break;
                   
                    GameVars.Money -= 7000;

                    Sprite.Write(CurX, CurY, "|", ConsoleColor.DarkYellow);

                    GameVars.Cells.Add(new CellsInfo(CurX, CurY, 60, 150, 150, "|", ConsoleColor.DarkYellow)); 
                    
                    break;

                case GameVars.BlockType.DoublePlutoniumCell: // Double Plutonium Cell
                    if (GameVars.Money < 14000)
                        break;
                   
                    GameVars.Money -= 14000;

                    Sprite.Write(CurX, CurY, "‖", ConsoleColor.DarkYellow);

                    GameVars.Cells.Add(new CellsInfo(CurX, CurY, 100, 600, 1200, "‖", ConsoleColor.DarkYellow)); 
                    break;

                case GameVars.BlockType.ThoriumCell: // Thorium Cell
                    if (GameVars.Money < 4700000)
                        break;

                    GameVars.Money -= 4700000;

                    Sprite.Write(CurX, CurY, "|", ConsoleColor.Cyan);

                    GameVars.Cells.Add(new CellsInfo(CurX, CurY, 900, 7500, 7500, "|", ConsoleColor.Cyan));
                    break;

                case GameVars.BlockType.DoubleThoriumCell: // Double Thorium Cell
                    if (GameVars.Money < 10400000)
                        break;

                    GameVars.Money -= 10400000;

                    Sprite.Write(CurX, CurY, "‖", ConsoleColor.Cyan);

                    GameVars.Cells.Add(new CellsInfo(CurX, CurY, 1200, 29600, 59200, "‖", ConsoleColor.Cyan));
                    break;
                //cells

                //reflectors
                case GameVars.BlockType.BasicReflector: // Basic Reflector
                    if (GameVars.Money < 500)
                        break;

                    GameVars.Money -= 500;

                    Sprite.Write(CurX, CurY, "H", ConsoleColor.Green);

                    GameVars.Reflectors.Add(new ReflectorsInfo(CurX, CurY, 200, ConsoleColor.Green));
                    break;

                case GameVars.BlockType.AdvancedReflector: // Advanced Reflector
                    if (GameVars.Money < 5000)
                        break;

                    GameVars.Money -= 5000;

                    Sprite.Write(CurX, CurY, "H", ConsoleColor.Yellow);

                    GameVars.Reflectors.Add(new ReflectorsInfo(CurX, CurY, 1250, ConsoleColor.Yellow));
                    break;

                case GameVars.BlockType.SuperReflector: // Super Reflector
                    if (GameVars.Money < 50000)
                        break;

                    GameVars.Money -= 50000;

                    Sprite.Write(CurX, CurY, "H", ConsoleColor.Blue);

                    GameVars.Reflectors.Add(new ReflectorsInfo(CurX, CurY, 15000, ConsoleColor.Blue));
                    break;
                //reflectors

                //platings
                case GameVars.BlockType.BasicPlating: // Advanced Plating
                    if (GameVars.Money < 1000)
                        break;

                    GameVars.Money -= 1000;

                    Sprite.Write(CurX, CurY, "0", ConsoleColor.DarkGray);

                    GameVars.Platings.Add(new PlatingsInfo(CurX, CurY, 100, ConsoleColor.DarkGray));
                    break;

                case GameVars.BlockType.AdvancedPlating: // Basic Plating
                    if (GameVars.Money < 160000)
                        break;

                    GameVars.Money -= 160000;

                    Sprite.Write(CurX, CurY, "0", ConsoleColor.Gray);

                    GameVars.Platings.Add(new PlatingsInfo(CurX, CurY, 14000, ConsoleColor.Gray));
                    break;

                case GameVars.BlockType.SuperPlating: // Super Plating
                    if (GameVars.Money < 2560000)
                        break;

                    GameVars.Money -= 2560000;

                    Sprite.Write(CurX, CurY, "0", ConsoleColor.DarkBlue);

                    GameVars.Platings.Add(new PlatingsInfo(CurX, CurY, 1960000, ConsoleColor.DarkBlue));
                    break;
                //platings

                //outlets
                case GameVars.BlockType.BasicOutlet: // Basic Outlet
                    if (GameVars.Money < 150)
                        break;
                   
                    GameVars.Money -= 150;

                    Sprite.Write(CurX, CurY, "&", ConsoleColor.Cyan);

                    GameVars.Outlets.Add(new OutletsInfo(CurX, CurY, 16, ConsoleColor.Cyan));
                    break;

                case GameVars.BlockType.AdvancedOutlet: // Advanced Outlet
                    if (GameVars.Money < 32000)
                        break;
                   
                    GameVars.Money -= 32000;

                    Sprite.Write(CurX, CurY, "&", ConsoleColor.DarkCyan);

                    GameVars.Outlets.Add(new OutletsInfo(CurX, CurY, 1200, ConsoleColor.DarkCyan));
                    break;

                case GameVars.BlockType.SuperOutlet: // Super Outlet
                    if (GameVars.Money < 6400000)
                        break;

                    GameVars.Money -= 6400000;

                    Sprite.Write(CurX, CurY, "&", ConsoleColor.Blue);

                    GameVars.Outlets.Add(new OutletsInfo(CurX, CurY, 90000, ConsoleColor.Blue));
                    break; // 6400000
                //outlets

                //vents
                case GameVars.BlockType.BasicVent: // Basic Vent
                    if (GameVars.Money < 50)
                        break;
                   
                    GameVars.Money -= 50;

                    Sprite.Write(CurX, CurY, "#", ConsoleColor.Gray);

                    GameVars.Vents.Add(new VentsInfo(CurX, CurY, 4, 40, ConsoleColor.Gray)); 
                    break;

                case GameVars.BlockType.AdvancedVent: // Advanced Vent
                    if (GameVars.Money < 12500)
                        break;
                   
                    GameVars.Money -= 12500;

                    Sprite.Write(CurX, CurY, "#", ConsoleColor.DarkGray);

                    GameVars.Vents.Add(new VentsInfo(CurX, CurY, 300, 3000, ConsoleColor.DarkGray));
                    break;

                case GameVars.BlockType.SuperVent: // Super Vent
                    if (GameVars.Money < 3125000)
                        break;

                    GameVars.Money -= 3125000;

                    Sprite.Write(CurX, CurY, "#", ConsoleColor.DarkMagenta);

                    GameVars.Vents.Add(new VentsInfo(CurX, CurY, 22500, 225000, ConsoleColor.DarkMagenta));
                    break;
                    // vents
            }
        }

        DisplayReactorInfo();
    }

    public static void RemoveBlock(byte CurX, byte CurY)
    {
        bool has_cell = GameVars.Cells.Exists(BP => BP.X == CurX && BP.Y == CurY);
        bool has_vent = GameVars.Vents.Exists(BP => BP.X == CurX && BP.Y == CurY);
        bool has_outlet = GameVars.Outlets.Exists(BP => BP.X == CurX && BP.Y == CurY);
        bool has_reflector = GameVars.Reflectors.Exists(BP => BP.X == CurX && BP.Y == CurY);
        bool has_plating = GameVars.Platings.Exists(BP => BP.X == CurX && BP.Y == CurY);

        if (has_cell) // checks if there is the block you want to remove 
            GameVars.Cells.RemoveAt(GameVars.Cells.FindIndex(BP => BP.X == CurX && BP.Y == CurY)); // removes a block from list on index
        else if (has_vent) // remove vent
            GameVars.Vents.RemoveAt(GameVars.Vents.FindIndex(BP => BP.X == CurX && BP.Y == CurY));
        else if (has_outlet) // remove outlet
            GameVars.Outlets.RemoveAt(GameVars.Outlets.FindIndex(BP => BP.X == CurX && BP.Y == CurY));
        else if (has_reflector) // remove reflector
            GameVars.Reflectors.RemoveAt(GameVars.Reflectors.FindIndex(BP => BP.X == CurX && BP.Y == CurY));
        else if (has_plating) // remove plating
            GameVars.Platings.RemoveAt(GameVars.Platings.FindIndex(BP => BP.X == CurX && BP.Y == CurY));

        Sprite.Write(CurX, CurY, " ", ConsoleColor.Black); // erasing sprite
    }
}