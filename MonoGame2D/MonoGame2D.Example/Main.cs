using System;
using System.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;

namespace MonoGame2D.Example.OSX
{
			/// <summary>
/// Main class.
/// </summary>
class MainClass {
    /// <summary>
    /// The entry point of the program, where the program control starts and ends.
    /// </summary>
    /// <param name='args'>
    /// The command-line arguments.
    /// </param>
    static void Main (string [] args) {
        NSApplication.Init ();

        using (var p = new NSAutoreleasePool ()) {
            NSApplication.SharedApplication.Delegate = new AppDelegate ();
            NSApplication.Main (args);
        }
    }
}

/// <summary>
/// App delegate.
/// </summary>
[MonoMac.Foundation.Register("AppDelegate")]
public class AppDelegate : NSApplicationDelegate {
    /// <summary>
    /// The game.
    /// </summary>
    private Director game;

    /// <summary>
    /// Called when Mac app is finished launching.
    /// </summary>
    /// <param name='notification'>
    /// Notification.
    /// </param>
    public override void FinishedLaunching (NSObject notification) {
         game = new Director(800,600);
            game.Script.Invoke(() => {
				game.PushScene(new ExampleScene(), Effects.SceneSwitchEffects.Fade, 2.0f);
            }).Wait(4.0f).Invoke(()=>{
                game.PushScene(new ExampleScene(), Effects.SceneSwitchEffects.Fade, 2.0f);
            }).Wait(4.0f).Invoke(() =>
            {
                game.PushScene(new ExampleScene(), Effects.SceneSwitchEffects.SlideRight, 1f);
            });
            game.Run();
    }

    /// <summary>
    /// Called when the last open window is closed to determine whether or not
    /// the applcation should then terminate.
    /// </summary>
    public override bool ApplicationShouldTerminateAfterLastWindowClosed (NSApplication sender) {
        return true;
    }
}
	
}	

