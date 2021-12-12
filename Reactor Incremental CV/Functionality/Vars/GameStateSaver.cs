using System;
using System.IO;

namespace Reactor_Incremental_CV;

class GameStateSaver // bad name
{
    private static string SaveFile = Directory.GetCurrentDirectory() + @"\data\save\Save.txt"; // for releases
    //private const string SaveFile = @"C:\Users\lenovo\source\repos\Reactor Incremental CV\Reactor Incremental CV\data\save\Save.txt"; // for testing (put here your directory path)

    public static void SaveState()
    { // IF YOU WANT TO CHANGE SAVES FOLLOW THE ORDER OF WRITING AND READING THE FILE
        StreamWriter fileW = new StreamWriter(SaveFile);

        fileW.WriteLine(GameVars.ReactorHeat);
        fileW.WriteLine(GameVars.Money);

        fileW.WriteLine(GameVars.Cells.Count);
        fileW.WriteLine(GameVars.Reflectors.Count);
        fileW.WriteLine(GameVars.Platings.Count);
        fileW.WriteLine(GameVars.Vents.Count);
        fileW.WriteLine(GameVars.Outlets.Count);

        foreach (var Cell in GameVars.Cells)
        { // WRITES FUEL CELLS INFO TO THE FILE
            fileW.WriteLine(Cell.X);
            fileW.WriteLine(Cell.Y);

            fileW.WriteLine(Cell.CellLife);

            fileW.WriteLine(Cell.CellPowerGen);
            fileW.WriteLine(Cell.CellHeatGen);

            fileW.WriteLine(Cell.CellSprite);
            fileW.WriteLine((int)Cell.CellColor);
        }

        foreach (var Reflector in GameVars.Reflectors)
        { // WRITES REFLECTORS INFO TO THE FILE
            fileW.WriteLine(Reflector.X);
            fileW.WriteLine(Reflector.Y);

            fileW.WriteLine(Reflector.ReflectorLife);

            fileW.WriteLine((int)Reflector.ReflectorColor);
        }

        foreach (var Plating in GameVars.Platings)
        { // WRITES PLATINGS INFO TO THE FILE
            fileW.WriteLine(Plating.X);
            fileW.WriteLine(Plating.Y);

            fileW.WriteLine(Plating.PlatingHeatCap);

            fileW.WriteLine((int)Plating.PlatingColor);
        }

        foreach (var Vent in GameVars.Vents)
        { // WRITES VENTS INFO TO THE FILE
            fileW.WriteLine(Vent.X);
            fileW.WriteLine(Vent.Y);

            fileW.WriteLine(Vent.VentingPower);
            fileW.WriteLine(Vent.VentLife);

            fileW.WriteLine((int)Vent.VentColor);
        }

        foreach (var Outlet in GameVars.Outlets)
        { // WRITES OUTLETS INFO TO THE FILE
            fileW.WriteLine(Outlet.X);
            fileW.WriteLine(Outlet.Y);

            fileW.WriteLine(Outlet.TransferCap);

            fileW.WriteLine((int)Outlet.OutletColor);
        }

        fileW.Close();
    }

    public static void SaveReader()
    {
        for (byte i = 1; i < 18; i++)
            Sprite.Write(1, i, new string(' ', 28), ConsoleColor.DarkRed);

        GameVars.Cells.Clear();
        GameVars.Reflectors.Clear();
        GameVars.Platings.Clear();
        GameVars.Vents.Clear();
        GameVars.Outlets.Clear();

        StreamReader fileR = new StreamReader(SaveFile);

        GameVars.ReactorHeat = long.Parse(fileR.ReadLine());
        GameVars.Money = long.Parse(fileR.ReadLine());

        int cells_saved_lenght = int.Parse(fileR.ReadLine());
        int reflectors_saved_lenght = int.Parse(fileR.ReadLine());
        int platings_saved_length = int.Parse(fileR.ReadLine());
        int vents_saved_lenght = int.Parse(fileR.ReadLine());
        int outlets_saved_lenght = int.Parse(fileR.ReadLine());

        for (int i = 0; i < cells_saved_lenght; i++)
        { // READS FUEL CELLS INFO FROM THE FILE
            GameVars.Cells.Add(new CellsInfo(
                byte.Parse(fileR.ReadLine()), // CELL X
                byte.Parse(fileR.ReadLine()), // CELL Y

                int.Parse(fileR.ReadLine()), // CELL LIFE

                int.Parse(fileR.ReadLine()), // CELL POWER GENERATION
                int.Parse(fileR.ReadLine()), // CELL HEAT GENERATION

                fileR.ReadLine(), // CELL SPRITE
                (ConsoleColor)byte.Parse(fileR.ReadLine()) // CELL SPRITE COLOR
                ));

            Sprite.Write(GameVars.Cells[i].X, GameVars.Cells[i].Y, Convert.ToString(GameVars.Cells[i].CellSprite), GameVars.Cells[i].CellColor);
        }

        for (int i = 0; i < reflectors_saved_lenght; i++)
        { // READS REFLECTORS INFO FROM THE FILE
            GameVars.Reflectors.Add(new ReflectorsInfo(
                byte.Parse(fileR.ReadLine()), // REFLECTOR X
                byte.Parse(fileR.ReadLine()), // REFLECTOR Y

                int.Parse(fileR.ReadLine()), // REFLECTOR LIFE

                (ConsoleColor)byte.Parse(fileR.ReadLine()) // REFLECTOR SPRITE COLOR
                ));

            Sprite.Write(GameVars.Reflectors[i].X, GameVars.Reflectors[i].Y, "H", GameVars.Reflectors[i].ReflectorColor);
        }

        for (int i = 0; i < platings_saved_length; i++)
        { // READS PLATINGS INFO FORM THE FILE
            GameVars.Platings.Add(new PlatingsInfo(
                byte.Parse(fileR.ReadLine()), // PLATING X
                byte.Parse(fileR.ReadLine()), // PLATING Y

                long.Parse(fileR.ReadLine()), // PLATING HEAT CAP

                (ConsoleColor)byte.Parse(fileR.ReadLine()) // PLATING SPRITE COLOR
                ));

            Sprite.Write(GameVars.Platings[i].X, GameVars.Platings[i].Y, "0", GameVars.Platings[i].PlatingColor);
        }

        for (int i = 0; i < vents_saved_lenght; i++)
        { // READS VENTS INFO FROM FILE
            GameVars.Vents.Add(new VentsInfo(
                byte.Parse(fileR.ReadLine()), // VENT X
                byte.Parse(fileR.ReadLine()), // VENT Y

                int.Parse(fileR.ReadLine()), // VENT POWER
                int.Parse(fileR.ReadLine()), // VENT LIFE

                (ConsoleColor)byte.Parse(fileR.ReadLine()) // VENT SPRITE COLOR
                ));

            Sprite.Write(GameVars.Vents[i].X, GameVars.Vents[i].Y, "#", GameVars.Vents[i].VentColor);
        }

        for (int i = 0; i < outlets_saved_lenght; i++)
        { // READS OUTLETS INFO FROM FILE
            GameVars.Outlets.Add(new OutletsInfo(
                byte.Parse(fileR.ReadLine()), // OUTLET X
                byte.Parse(fileR.ReadLine()), // OUTLET Y

                int.Parse(fileR.ReadLine()), // OUTLET TRANSFER CAP

                (ConsoleColor)byte.Parse(fileR.ReadLine()) // OUTLET SPRITE COLOR
                ));

            Sprite.Write(GameVars.Outlets[i].X, GameVars.Outlets[i].Y, "&", GameVars.Outlets[i].OutletColor);
        }

        fileR.Close();
    }
}
