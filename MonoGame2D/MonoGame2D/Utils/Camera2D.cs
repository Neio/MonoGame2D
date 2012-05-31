using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame2D.Utils {

	/// <summary>
	/// Event handler for camera change events
	/// </summary>
	public delegate void Camera2DChangedHandler(Camera2D camera);

	/// <summary>
	/// Represent simple 2D camera with scaling/rotation support. It projects world rectangle to screen rectangle.
	/// </summary>
	public class Camera2D : ITransform{
		private Vector2 _location;
		private float _zoom;
		private float _aspectRatio;
		private float _rotationAngle;
		private Rect _screenRect;

		///<summary>Final transform</summary>
		private Transform2D _transform;

		private Transform2D _scaling;
		private Transform2D _rotation;
		private Transform2D _translation;

		/// <summary>
		/// Initializes a new instance of the <see cref="Camera2D"/> class.
		/// </summary>
		/// <param name="screenRect">The target screen rectangle for camera projection.</param>
		public Camera2D(Rect screenRect) {
			_screenRect = screenRect;
			_zoom = 1.0f;
			_aspectRatio = 1.0f;
			UpdateTransform();
		}

		#region Public Properties

		/// <summary>
		/// Gets or sets the location. Camera location is the central point of camera world rectangle.
		/// </summary>
		/// <value>The camera location.</value>
		public Vector2 Location {
			get { return _location; }
			set { _location = value; UpdateTransform(); }
		}

		/// <summary>
		/// Gets or sets the zoom. Zoom is scaling between world and screen units.
		/// </summary>
		/// <value>The zoom ratio.</value>
		public float Zoom {
			get { return _zoom; }
			set { _zoom = value; UpdateTransform(); }
		}

		/// <summary>
		/// Gets or sets the camera rotation angle.
		/// </summary>
		/// <value>The rotation angle in radians.</value>
		public float Angle {
			get { return _rotationAngle; }
			set { _rotationAngle = value; UpdateTransform(); }
		}

		/// <summary>
		/// Gets or sets the aspect ratio. Aspect ratio is addition ratio between width and height when transforming world to screen rect.
		/// </summary>
		/// <value>The aspect ratio.</value>
		public float AspectRatio {
			get { return _aspectRatio; }
			set { _aspectRatio = value; UpdateTransform(); }
		}

		/// <summary>
		/// Gets or sets the screen rect. Screen rect is target rectangle in screen units for camera projection
		/// </summary>
		/// <value>The screen rect.</value>
		public Rect ScreenRect {
			get { return _screenRect; }
			set { _screenRect = value; UpdateTransform(); }
		}

		/// <summary>
		/// Gets the current camera transform.
		/// </summary>
		/// <value>The camera transform.</value>
		public Transform2D Transform {
			get { return _transform; }
		}

		/// <summary>
		/// Occurs when camera location, zoom, aspect ratio is changed.
		/// </summary>
		public event Camera2DChangedHandler Changed;

		#endregion

		/// <summary>
		/// Updates the camera transform.
		/// </summary>
		protected void UpdateTransform() {
			Vector2 negLocation = -_location;
			Vector2 scaling = new Vector2(_zoom * _aspectRatio, _zoom);
			Vector2 screenTranslate = _screenRect.Center;

			Transform2D screenTranslation;

			Transform2D.CreateRotation(_rotationAngle, out _rotation);
			Transform2D.CreateTranslation(ref negLocation, out _translation);
			Transform2D.CreateScaling(ref scaling, out _scaling);
			Transform2D.CreateTranslation(ref screenTranslate, out screenTranslation);

			_transform = _translation * _scaling * _rotation * screenTranslation;

			//send events
			if (null != Changed) {
				Changed(this);
			}
		}

		/// <summary>
		/// Fits camera to contain fully visible world rectangle without changing aspect ratio .
		/// </summary>
		/// <param name="worldRect">The world rect to fit.</param>
		public void FitTo(Rect worldRect) {
			_rotationAngle = 0;
			float hScale = _screenRect.Width / worldRect.Width, vScale = _screenRect.Height / worldRect.Height;
			_zoom = Math.Min(hScale, vScale);
			_location = worldRect.Center;
			UpdateTransform();
		}

		/// <summary>
		/// Stretches camera to match world rectangle.
		/// </summary>
		/// <param name="worldRect">The world rect to stretch.</param>
		public void StretchTo(Rect worldRect) {
			_rotationAngle = 0;
			float hScale = _screenRect.Width / worldRect.Width, vScale = _screenRect.Height / worldRect.Height;
			_zoom = vScale;
			_aspectRatio = hScale / vScale;
			_location = worldRect.Center;
			UpdateTransform();
		}

		/// <summary>
		/// Converts point from world units to screen units.
		/// </summary>
		/// <param name="vector">The vector to convert in world units.</param>
		/// <returns>Vector point in screen units</returns>
		public Vector2 WorldToScreen(Vector2 vector) {
			return _transform.Multiply(vector);
		}

		/// <summary>
		/// Converts rectangle from world coords to screen ones. It converts rect corners, result will be incorrect if rotation <. 0
		/// </summary>
		/// <param name="worldRect">The world rectangle.</param>
		/// <returns>Transformed rectangle</returns>
		public Rect WorldToScreen(Rect worldRect) {
			return new Rect(
				_transform.Multiply(worldRect.LeftTop),
				_transform.Multiply(worldRect.RightBottom)
			);
		}

		/// <summary>
		/// Converts point from screen units to world units.
		/// </summary>
		/// <param name="vector">The vector to convert in screen units.</param>
		/// <returns>Vector point in world units</returns>
		public Vector2 ScreenToWorld(Vector2 vector) {
			return _transform.ReverseMultiply(vector);
		}

		/// <summary>
		/// Converts rectangle from screen coords to world ones. It converts rect corners, result will be incorrect if rotation <. 0
		/// </summary>
		/// <param name="worldRect">The screen rectangle.</param>
		/// <returns>Transformed rectangle</returns>
		public Rect ScreenToWorld(Rect screenRect) {
			return new Rect(
				_transform.ReverseMultiply(screenRect.LeftTop),
				_transform.ReverseMultiply(screenRect.RightBottom)
			);
		}

		/// <summary>
		/// Moves camera for a specified offset in screen units.
		/// </summary>
		/// <param name="deltaX">The offset X in screen units.</param>
		/// <param name="deltaY">The offset Y in screen units.</param>
		public void OffsetScreen(float offsetX, float offsetY) {
			OffsetScreen(new Vector2(offsetX, offsetY));
		}

		/// <summary>
		/// Moves camera for a specified offset in screen units.
		/// </summary>
		/// <param name="delta">The offset units.</param>
		public void OffsetScreen(Vector2 offset) {
			Location = Location + (_rotation * _scaling).ReverseMultiply(offset);
		}

		/// <summary>
		/// Zooms camera at screen point. Very useful for providing mouse zoom at cursor point.
		/// </summary>
		/// <param name="screenPointX">The screen point X coord.</param>
		/// <param name="screenPointY">The screen point Y coord.</param>
		/// <param name="zoomMultiplier">The zoom multiplier.</param>
		public void ZoomAtScreenPoint(float screenPointX, float screenPointY, float zoomMultiplier) {
			ZoomAtScreenPoint(new Vector2(screenPointX, screenPointY), zoomMultiplier);
		}

		/// <summary>
		/// Zooms camera at screen point. Very useful for providing mouse zoom at cursor point.
		/// </summary>
		/// <param name="screenPoint">The screen point.</param>
		/// <param name="zoomMultiplier">The zoom multiplier.</param>
		public void ZoomAtScreenPoint(Vector2 screenPoint, float zoomMultiplier) {
			ZoomAtWorldPoint(ScreenToWorld(screenPoint), zoomMultiplier);
		}

		/// <summary>
		/// Zooms camera at world point.
		/// </summary>
		/// <param name="worldPointX">The world point X coord.</param>
		/// <param name="worldPointY">The world point Y coor.</param>
		/// <param name="zoomMultiplier">The zoom multiplier.</param>
		public void ZoomAtWorldPoint(float worldPointX, float worldPointY, float zoomMultiplier) {
			ZoomAtWorldPoint(new Vector2(worldPointX, worldPointY), zoomMultiplier);
		}

		/// <summary>
		/// Zooms camera at world point.
		/// </summary>
		/// <param name="worldPoint">The world point.</param>
		/// <param name="zoomMultiplier">The zoom multiplier.</param>
		public void ZoomAtWorldPoint(Vector2 worldPoint, float zoomMultiplier) {
			if (zoomMultiplier != 0) {
				Vector2 toCenterVector = Location - worldPoint;
				Zoom = Zoom * zoomMultiplier;
				Location = worldPoint + toCenterVector / zoomMultiplier;
			} else {
				throw new ArgumentException("Zoom factor can't be zero", "zoomMultiplier");
			}
		}

		#region ITransform Members

		/// <summary>
		/// Gets the transform which represents camera.
		/// </summary>
		/// <param name="transform">The transform represented by camera.</param>
		public void GetTransform(out Transform2D transform) {
			transform = _transform;
		}

		#endregion
	}
}
