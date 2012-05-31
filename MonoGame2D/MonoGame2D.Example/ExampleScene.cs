using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame2D.Example
{
    class ExampleScene : Scene
    {
        public ExampleScene() {
            TimeLine.Wait(0.5f).Invoke( () =>
            {
                var eNode = new ExampleNode();

                AddChild(eNode);

                TimeLine.Repeat(20.0f, t =>
                {
                    eNode.Location = new Microsoft.Xna.Framework.Vector2( 500f * t.Progress);
                    eNode.Rotation = (float)Math.PI * 5 * t.Progress;
                });
            });
        }

    }
}
