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

        
		//public ResourceCollection Resources {get; protected set;}

        internal IServiceProvider BindService { get; set; }

        /// <summary>
        /// Load Resource when activated
        /// </summary>
        internal void ActivateResource() {
            if (UseEmbeddedResouce)
            {
                Resources = new ResourceCollection(BindService, resourceManager);
            }
            else {
                Resources = new ResourceCollection(BindService, RootDirectory);
            }
            LoadContents(Resources);
        }

        /// <summary>
        /// Unload Resource when Deactivated
        /// </summary>
        internal void DeactivateResource() {
            if(Resources!=null){
				((ResourceCollection)Resources).Dispose();
				Resources = null;
			}
            UnloadContents();
        }

        public virtual void Activate()
        { }

        public virtual void Deactivate() 
        { }
    }
}
