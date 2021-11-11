using System;
using System.IO;

namespace Reactor_Incremental_CV
{
    class GameStateSaver
    {
        //const string GameFile = "./../../Game.txt";
        const string SaveFile = @"./../../../data\save\Save.txt";

        public static void SaveFiles()
        {
            StreamWriter fileW = new StreamWriter(SaveFile);

            fileW.WriteLine(GameFuncs.ReactorHeat);
            fileW.WriteLine(GameFuncs.Money);

            fileW.WriteLine((int)GameVars.BlockTypeIdx);

            fileW.WriteLine(GameVars.Cells.Count);
            fileW.WriteLine(GameVars.Vents.Count);

            foreach (var Cell in GameVars.Cells)
            {
                fileW.WriteLine(Cell.X);
                fileW.WriteLine(Cell.Y);

                fileW.WriteLine(Cell.CellLife);

                fileW.WriteLine(Cell.CellPowerGen);
                fileW.WriteLine(Cell.CellHeatGen);

                fileW.WriteLine(Cell.CellSprite);
                fileW.WriteLine((int)Cell.CellColor);
            }

            foreach (var Vent in GameVars.Vents)
            {
                fileW.WriteLine(Vent.X);
                fileW.WriteLine(Vent.Y);

                fileW.WriteLine(Vent.VentingPower);

                fileW.WriteLine((int)Vent.VentColor);
            }

            fileW.Close();
        }

        public static void SaveReader()
        {
            GameVars.Cells.RemoveRange(0, GameVars.Cells.Count);
            GameVars.Vents.RemoveRange(0, GameVars.Vents.Count);

            StreamReader fileR = new StreamReader(SaveFile);

            GameFuncs.ReactorHeat = long.Parse(fileR.ReadLine());
            GameFuncs.Money = long.Parse(fileR.ReadLine());

            GameVars.BlockTypeIdx = (GameVars.BlockType)int.Parse(fileR.ReadLine());

            GameFuncs.SetBlock(GameVars.BlockTypeIdx);

            int cells_saved_lenght = int.Parse(fileR.ReadLine());
            int vents_saved_lenght = int.Parse(fileR.ReadLine());

            for (int i = 0; i < cells_saved_lenght; i++)
            {
                GameVars.Cells.Add(new CellsInfo(
                    byte.Parse(fileR.ReadLine()), // CELL X
                    byte.Parse(fileR.ReadLine()), // CELL Y

                    byte.Parse(fileR.ReadLine()), // CELL LIFE

                    int.Parse(fileR.ReadLine()), // CELL POWER GENERATION
                    int.Parse(fileR.ReadLine()), // CELL HEAT GENERATION

                    char.Parse(fileR.ReadLine()), // CELL SPRITE
                    (ConsoleColor)int.Parse(fileR.ReadLine()) // CELL COLOR
                    ));
                
                Sprite.Draw(GameVars.Cells[i].X, GameVars.Cells[i].Y, GameVars.Cells[i].CellSprite, GameVars.Cells[i].CellColor);
            }

            for (int i = 0; i < vents_saved_lenght; i++)
            {
                GameVars.Vents.Add(new VentsInfo(
                    byte.Parse(fileR.ReadLine()), // VENT X
                    byte.Parse(fileR.ReadLine()), // VENT Y

                    int.Parse(fileR.ReadLine()), // VENT POWER

                    (ConsoleColor)byte.Parse(fileR.ReadLine()) // VENT COLOR
                    ));

                Sprite.Draw(GameVars.Vents[i].X, GameVars.Vents[i].Y, '#', GameVars.Vents[i].VentColor);
            }

            fileR.Close();

            GameFuncs.DisplayReactorInfo();

            Controls.GamePaused = true;
            Sprite.Write(30, 20, "Pause", ConsoleColor.Red);
        }
    }
}
