using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using MonoGame2D.Script;
namespace MonoGame2D
{
    public class Node: INode
    {
        public Node()
        {
            ComputeTransform();
        }


        Node _parent = null;
        ContentManager _content = null;
        List<Node> _children = new List<Node>();
        ///<summary>Location relative to parent node. It defines origin point</summary>
        private Vector2 _location = new Vector2(0,0);
        ///<summary>Origin point, it allows change rotation/scaling point relative to location</summary>
        private Vector2 _origin;
        ///<summary>Element scale. Origin point is location point or (0,0) in node space</summary>
        private Vector2 _scale = Vector2.One;
        ///<summary>Rotation of node around its origin point</summary>
        private float _rotation  = 0;
        ///<summary>Post-computed 2D transform of this node</summary>
        private Matrix _transform;
        //bool _attached = false;

        protected ContentManager Content { get { return _content; } }

        #region Sprites

        List<Sprite> sprites = new List<Sprite>();

        public void AddChild(Sprite sprite)
        {
            sprites.Add(sprite);
        }

        public void RemoveChild(Sprite sprite)
        {
            sprites.Remove(sprite);
        }
        #endregion

        #region Childen

        //public bool Attached { get { return _attached; } set { _attached = value; } }

        public IEnumerable<Node> Children
        {
            get
            {
                return _children;
            }
        }
        

        /// <summary>
        /// Gets the children of the specified type only.
        /// </summary>
        /// <typeparam name="T">Type of child to return</typeparam>
        /// <returns>Enumeration of node children of specified type</returns>
        public IEnumerable<T> GetChildren<T>() where T : Node
        {
            Type returnType = typeof(T);
            foreach (Node node in Children)
            {
                if (returnType.IsInstanceOfType(node))
                {
                    yield return node as T;
                }
            }
        }

        /// <summary>
        /// Parent node for this node
        /// </summary>
        /// <value>The parent node.</value>
        public Node Parent
        {
            get { return _parent; }
            set
            {
                //remove node from parent list first
                if (null != _parent)
                {
                    _parent.RemoveChild(this);
                }
                //add node to new parent or leave abandoned
                if (value != null)
                {
                    value.AddChild(this);
                }
            }
        }

        public void AddChild(Node Node)
        {

            if (Node.Parent != null)
            {
                Node.Parent.RemoveChild(Node);
            }
            _children.Add(Node);
            Node._parent = this;
            if(_content !=null)
                Node.LoadContents(_content);
        }

        public void RemoveChild(Node Node)
        {
            if (_children.Contains(Node))
            {
                _children.Remove(Node);
                Node._parent = null;
                //Node.Attached = false;
                Node.UnloadContents();
            }
        }

        public void RemoveAllChildren()
        {
            foreach (Node node in _children.ToArray())
            {
                RemoveChild(node);
            }
        }

        public void MoveToFront(Node childNode)
        {
            if (_children.Contains(childNode))
            {
                _children.Remove(childNode);
                _children.Add(childNode);
            }
        }


        #endregion


        #region Element Geometry Positioning

        /// <summary>
        /// Gets or sets the location of node relative to its parent.
        /// </summary>
        /// <value>The location of node.</value>
        public virtual Vector2 Location
        {
            get { return _location; }
            set { _location = value; ComputeTransform(); }
        }

        /// <summary>
        /// Gets or sets the X component of location of node relative to its parent.
        /// </summary>
        /// <value>The X component of location</value>
        public virtual float LocationX
        {
            get { return _location.X; }
            set
            {
                _location.X = value;
                ComputeTransform();
            }
        }

        /// <summary>
        /// Gets or sets the Y component of location of node relative to its parent.
        /// </summary>
        /// <value>The Y component of location</value>
        public virtual float LocationY
        {
            get { return _location.Y; }
            set
            {
                _location.Y = value;
                ComputeTransform();
            }
        }

        /// <summary>
        /// Gets or sets the origin point offset which is relative to location (like a correction offset).
        /// </summary>
        /// <value>The origin point offset of node.</value>
        public virtual Vector2 Origin
        {
            get { return _origin; }
            set { _origin = value; ComputeTransform(); }
        }

        /// <summary>
        /// Gets or sets the scale of node against it origin point (node center).
        /// </summary>
        /// <value>The scale of node against it origin point.</value>
        public virtual Vector2 Scale
        {
            get { return _scale; }
            set { _scale = value; ComputeTransform(); }
        }

        /// <summary>
        /// Gets or sets the rotation of node around its origin point.
        /// </summary>
        /// <value>The node rotation around its origin point.</value>
        public virtual float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; ComputeTransform(); }
        }

        /// <summary>
        /// Transformation of current node relative to parent one
        /// </summary>
        /// <value>The node transform.</value>
        public Matrix Transformation
        {
            get { return _transform; }
        }

        /// <summary>
        /// Transformation of current node relative to global screen space. You can use it for transforming local values to screen or reverse - screen to local
        /// </summary>
        /// <value>The node screen transform.</value>
        public Matrix ScreenTransform
        {
            get
            {
                Matrix transform = _transform;
                Node parent = Parent;
                while (parent != null)
                {
                    transform = transform * parent.Transformation;
                    parent = parent.Parent;
                }
                return transform;
            }
        }

        /// <summary>
        /// Updates transform matrix based on all of transformations: scale, rotation and location
        /// </summary>
        private void ComputeTransform()
        {
            //TODO Investigate and optimize!

            Matrix origin, revOrigin, scale, rotation, translation;
            scale = Matrix.CreateScale(_scale.X, _scale.Y, 1);
            Matrix.CreateRotationZ(_rotation, out rotation);
            origin = Matrix.CreateTranslation(_origin.X, _origin.Y, 0);
            revOrigin = Matrix.CreateTranslation(-_origin.X, -_origin.Y, 0);
            translation = Matrix.CreateTranslation(_location.X, _location.Y, 0);

            _transform = revOrigin * scale * rotation * translation * origin;
            //_transform = revOrigin * translation * rotation * scale * origin;
        }

        #endregion


        public virtual void LoadContents(ContentManager Content)
        {
            _content = Content;
            foreach (Node node in _children.ToArray())
            {
                if(node.Content== null)
                    node.LoadContents(Content);
            }
        }

        public virtual void UnloadContents()
        { 
            
        }

        public virtual void Draw(Canvas2D canvas)
        { 
            
        }

        public virtual void DrawForeground(Canvas2D canvas)
        {

        }

        public virtual void DrawContent(Canvas2D canvas)//SpriteBatch graphic, GameTime GameTime, ref Matrix Transform)
        {
                canvas.PushTransform();
                canvas.ApplyTransformBefore(ref _transform);
                Draw(canvas);
                DrawChildren(canvas);
                DrawForeground(canvas);
                canvas.PopTransform();
            
            
        }

        public virtual void DrawChildren(Canvas2D canvas)
        {
            foreach (var child in _children)
            {
                child.DrawContent(canvas);
            }

           // canvas.Begin();
            //canvas.Batch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, canvas.Matrix);
            //graphic.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, 
            //    DepthStencilState.None, RasterizerState.CullClockwise, null,
            //    Transform);
            foreach (var item in sprites)
            {
                item.Draw(canvas.Batch);
            }
            //canvas.End();
            //canvas.Batch.End();
        }

        public virtual void Update(float gameTime)
        {
        }

        //protected float GetTimeDelta(GameTime time)
        //{
        //    return (float)time.ElapsedRealTime.TotalMilliseconds / 1000;
        //}
    }



}
