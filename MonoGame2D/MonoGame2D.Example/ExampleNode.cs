using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame2D.Script;
using Microsoft.Xna.Framework.Content;

namespace MonoGame2D.Example
{
    class ExampleNode : ScriptNode
    {
        bool _load=false;
		string _asset;
		public ExampleNode(String AssetName)
		{
			_asset = AssetName;

		}
        
        public override void LoadContents(IResourceCollection Context)
        {
            base.LoadContents(Context);
            if (!_load )
            {
                var sp = new Sprite(Context.Get<Texture2D>(_asset));
                AddChild(sp);
                _load = true;
            }
        }

        

    }


    public class DynamicElement : ScriptNode
    {
        public DynamicElement(Node node)
        {
            _adjust = 0.1f;
            _node = node;
            AddChild(_node);
            TimeLine.Repeat(0.2f, t =>
            {
                var p = t.Progress;
                if (p > 1.0f) p = 1.0f;
                _adjust = 0.1f + 0.9f * p;
            }).Invoke(() =>{
                Repeat();
            });
        }

        float _adjust = 1.0f;
        Node _node;

        private void Repeat()
        {
            TimeLine.Repeat(1.0f, t =>
            {
                var p = t.Progress;
                if (p > 1.0f) p = 1.0f;
                _adjust = 1.0f + (float)Math.Sin((Math.PI * 2 * p)) * 0.1f;
            })
            .Invoke(() =>
            {
                TimeLine = new Script.TimeLine();
                Repeat();
            });
        }

        public void RemoveSelf()
        {
            TimeLine = new TimeLine();
            TimeLine.Repeat(0.2f, t =>
            {
                var p = t.Progress;
                if (p > 1.0f) p = 1.0f;
                _adjust = 0.1f + 0.9f * (1 - p);
            }).Invoke(() =>
            {
                Parent.RemoveChild(this);
            });
        }

        public override void Update(float timeDelta)
        {
            base.Update(timeDelta);
            if (_node != null)
                _node.Scale = new Vector2(_adjust);
        }
    }
}
