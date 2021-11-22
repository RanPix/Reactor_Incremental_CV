using System.IO;

namespace Reactor_Incremental_CV;
class Sounds
{
    static WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();
    public static void MusicPlayer()
    {
        player.settings.volume = 5;

        player.URL = Directory.GetCurrentDirectory() + $@"\data\music\g{GameVars.rand.Next(1, 10)}.mp3"; // for game
        //player.URL = $@"C:\Users\lenovo\source\repos\Reactor Incremental CV\Reactor Incremental CV\data\music\g{GameVars.rand.Next(1, 10)}.mp3"; // for dev
        player.controls.play();
    }
}
