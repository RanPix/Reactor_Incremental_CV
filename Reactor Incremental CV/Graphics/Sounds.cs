using System;
using System.IO;

namespace Reactor_Incremental_CV
{
    class Sounds
    {
        static WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();
        public static void MusicPlayer()
        {
            player.settings.volume = 1;

            player.URL = Directory.GetCurrentDirectory() + $@"\data\music\g{GameVars.rand.Next(1, 10)}.mp3";
            player.controls.play();
        }
    }
}
