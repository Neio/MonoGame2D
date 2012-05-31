#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.AppKit;
using MonoMac.Foundation;


#endregion

namespace MonoGame2D.Example
{
#if MACOS

#else
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        private static Director game;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            game = new Director(800,600);
            game.Script.Invoke(() => {
                game.PushScene(new ExampleScene(), Effects.SceneSwitchEffects.Fade,2.0f);
            }).Wait(4.0f).Invoke(()=>{
                game.PushScene(new ExampleScene(), Effects.SceneSwitchEffects.Fade, 2.0f);
            }).Wait(4.0f).Invoke(() =>
            {
                game.PushScene(new ExampleScene(), Effects.SceneSwitchEffects.SlideRight, 1f);
            });
            game.Run();
        }
    }
#endif
	
}
