using System;

namespace Reactor_Incremental_CV
{
    class GameFuncs
    {
        public static long ReactorHeat = 0; // for displaying reactor heat and for advanced reactor builds

        public static long Money = 10; // money

        public static void DisplayReactorInfo()
        {
            Sprite.Write(0, 25, new string(' ', 35), ConsoleColor.Black); // erase old string to update it

            Sprite.Write(0, 25, $"Money: {Money} Heat: {ReactorHeat} / 1000", ConsoleColor.Yellow);

            if (ReactorHeat >= 1000)
                ReactorExplosion();
        }

        public static void ReactorExplosion()
        {
            for (int i = 0; i < 19; i++)
            { // explosion effect
                Console.SetCursorPosition(0, i);

                Console.ForegroundColor  = ConsoleColor.DarkRed;

                Console.WriteLine(new string('*', 35));
            }

            System.Threading.Thread.Sleep(500);

            for (int i = 0; i < 19; i++)
            {
                Console.SetCursorPosition(0, i);

                Console.WriteLine(new string(' ', 35));
            }

            GameVars.Cells.RemoveRange(0, GameVars.Cells.Count);
            GameVars.Vents.RemoveRange(0, GameVars.Vents.Count);
        }

        public static void SetBlock(in GameVars.BlockType type) // sets the block that you gonna build with
        {
            switch (type) 
            { // shit begins B)
                case GameVars.BlockType.UraniumCell:
                    Sprite.Write(0, 20, $"<(J) [ | ] (K)>", ConsoleColor.DarkGreen); // shows which block you choosed

                    Sprite.Write(0, 21, new string(' ', 35) + '\n' + new string(' ', 35) + '\n' + new string(' ', 35) + '\n' + new string(' ', 35), ConsoleColor.Black); // erase old string to update it
                    Sprite.Write(0, 21, $"Power Gen: 1\nHeat Gen: 1\nDurability: 15\nPrice: 10", ConsoleColor.Red); // shows info about the block
                    break;
                case GameVars.BlockType.DoubleUraniumCell:
                    Sprite.Write(0, 20, $"<(J) [ ‖ ] (K)>", ConsoleColor.DarkGreen); 

                    Sprite.Write(0, 21, new string(' ', 35) + '\n' + new string(' ', 35) + '\n' + new string(' ', 35) + '\n' + new string(' ', 35), ConsoleColor.Black); 
                    Sprite.Write(0, 21, $"Power Gen: 4\nHeat Gen: 8\nDurability: 20\nPrice: 25", ConsoleColor.Red); 
                    break;
                case GameVars.BlockType.PlutoniumCell:
                    Sprite.Write(0, 20, $"<(J) [ | ] (K)>", ConsoleColor.DarkYellow);

                    Sprite.Write(0, 21, new string(' ', 35) + '\n' + new string(' ', 35) + '\n' + new string(' ', 35) + '\n' + new string(' ', 35), ConsoleColor.Black);
                    Sprite.Write(0, 21, $"Power Gen: 150\nHeat Gen: 150\nDurability: 60\nPrice: 7000", ConsoleColor.Red);
                    break;
                case GameVars.BlockType.DoublePlutoniumCell:
                    Sprite.Write(0, 20, $"<(J) [ ‖ ] (K)>", ConsoleColor.DarkYellow);

                    Sprite.Write(0, 21, new string(' ', 35) + '\n' + new string(' ', 35) + '\n' + new string(' ', 35) + '\n' + new string(' ', 35), ConsoleColor.Black);
                    Sprite.Write(0, 21, $"Power Gen: 600\nHeat Gen: 600\nDurability: 100\nPrice: 14000", ConsoleColor.Red);
                    break;
                case GameVars.BlockType.BasicVent:
                    Sprite.Write(0, 20, $"<(J) [ # ] (K)>", ConsoleColor.Gray);

                    Sprite.Write(0, 21, new string(' ', 35) + '\n' + new string(' ', 35) + '\n' + new string(' ', 35) + '\n' + new string(' ', 35), ConsoleColor.Black);
                    Sprite.Write(0, 21, $"Heat Venting: 3\nPrice: 50", ConsoleColor.Red);
                    break;
                case GameVars.BlockType.AdvancedVent:
                    Sprite.Write(0, 20, $"<(J) [ # ] (K)>", ConsoleColor.DarkRed);

                    Sprite.Write(0, 21, new string(' ', 35) + '\n' + new string(' ', 35) + '\n' + new string(' ', 35) + '\n' + new string(' ', 35), ConsoleColor.Black);
                    Sprite.Write(0, 21, $"Heat Venting: 300\nPrice: 12500", ConsoleColor.Red);
                    break;
            }  
        }

        public static void PlaceBlock(byte CurX, byte CurY, in GameVars.BlockType type)
        {
            bool has_cell = GameVars.Cells.Exists(BP => BP.X == CurX && BP.Y == CurY); // BP = Block position
            bool has_vent = GameVars.Vents.Exists(BP => BP.X == CurX && BP.Y == CurY);

            if (!has_cell && !has_vent) // checks if there is a block on a place where you want to place
            {
                switch(type) 
                {
                    case GameVars.BlockType.UraniumCell:
                        Sprite.Draw(CurX, CurY, '|', ConsoleColor.DarkGreen); // draws a sprite of a block

                        if (Money >= 10)
                            Money -= 10;

                        GameVars.Cells.Add(new CellsInfo(CurX, CurY, 15, 1, 1, '|', ConsoleColor.DarkGreen)); // Uranium cell
                        break;
                    case GameVars.BlockType.DoubleUraniumCell:
                        Sprite.Draw(CurX, CurY, '‖', ConsoleColor.DarkGreen); // draws a sprite of a block

                        if (Money >= 25)
                            Money -= 25;

                        GameVars.Cells.Add(new CellsInfo(CurX, CurY, 20, 4, 8, '‖', ConsoleColor.DarkGreen)); // Double uranium cell
                        break;
                    case GameVars.BlockType.PlutoniumCell:
                        Sprite.Draw(CurX, CurY, '|', ConsoleColor.DarkYellow);

                        if (Money >= 7000)
                            Money -= 7000;

                        GameVars.Cells.Add(new CellsInfo(CurX, CurY, 60, 150, 150, '|', ConsoleColor.DarkGreen));
                        break;
                    case GameVars.BlockType.DoublePlutoniumCell:
                        Sprite.Draw(CurX, CurY, '‖', ConsoleColor.DarkYellow);

                        if (Money >= 14000)
                            Money -= 14000;

                        GameVars.Cells.Add(new CellsInfo(CurX, CurY, 100, 600, 1200, '‖', ConsoleColor.DarkGreen)); 
                        break;

                    case GameVars.BlockType.BasicVent:
                        Sprite.Draw(CurX, CurY, '#', ConsoleColor.Gray);

                        if (Money >= 50)
                            Money -= 50;

                        GameVars.Vents.Add(new VentsInfo(CurX, CurY, 4, ConsoleColor.Gray));
                        break;
                    case GameVars.BlockType.AdvancedVent:
                        Sprite.Draw(CurX, CurY, '#', ConsoleColor.DarkRed);

                        if (Money >= 12500)
                            Money -= 12500;

                        GameVars.Vents.Add(new VentsInfo(CurX, CurY, 300, ConsoleColor.DarkRed));
                        break;
                }
            }
        }

        public static void RemoveBlock(byte CurX, byte CurY)
        {
            bool has_cell = GameVars.Cells.Exists(BP => BP.X == CurX && BP.Y == CurY);
            bool has_vent = GameVars.Vents.Exists(BP => BP.X == CurX && BP.Y == CurY);

            if (has_cell) // checks if there is a block on a place where you want to place
            {
                GameVars.Cells.RemoveAt(GameVars.Cells.FindIndex(BP => BP.X == CurX && BP.Y == CurY)); // removes a block from list on index

                Sprite.Draw(CurX, CurY, ' ', ConsoleColor.Black); // erases sprite
            }
            else if (has_vent)
            {
                GameVars.Vents.RemoveAt(GameVars.Vents.FindIndex(BP => BP.X == CurX && BP.Y == CurY)); 

                Sprite.Draw(CurX, CurY, ' ', ConsoleColor.Black);
            }
        }
    }
}