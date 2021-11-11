using System;
using System.Threading.Tasks;

namespace Reactor_Incremental_CV
{
    class Sounds
    {
        static Random rand = new Random();

        static WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();
        public static void MusicPlayer()
        {
            player.settings.volume = 2;

            player.URL = $@"C:\Users\lenovo\source\repos\Reactor Incremental CV\Reactor Incremental CV\data\music\g{rand.Next(1, 10)}.mp3";
            player.controls.play();
        }
    }
}
