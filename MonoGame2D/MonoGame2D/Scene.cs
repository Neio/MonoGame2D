using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Resources;
using Microsoft.Xna.Framework.Content;

namespace MonoGame2D
{
    public class Scene : ScriptNode, INode
    {
        public Scene()
        {
        }

        protected bool UseEmbeddedResouce = false;
        protected ResourceManager resourceManager = null;
        protected String RootDirectory = "Content";

        public ContentManager Content { get; protected set; }

        internal IServiceProvider BindService { get; set; }

        /// <summary>
        /// Load Resource when activated
        /// </summary>
        internal void ActivateResource() {
            if (UseEmbeddedResouce)
            {
                Content = new ResourceContentManager(BindService, resourceManager);
            }
            else {
                Content = new ContentManager(BindService, RootDirectory);
            }
            LoadContents(Content);
        }

        /// <summary>
        /// Unload Resource when Deactivated
        /// </summary>
        internal void DeactivateResource() {
            if (Content != null)
            {
                Content.Unload();
                Content.Dispose();
                Content = null;
                
            }
            UnloadContents();
        }

        public virtual void Activate()
        { }

        public virtual void Deactivate() 
        { }
    }
}
