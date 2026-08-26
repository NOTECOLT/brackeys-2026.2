using Godot;
using System;

public partial class CameraZoom : Camera2D {
	[Export]
	public float unZoomedValue = 1;

	[Export]
	public Vector2 unZoomedPos = Vector2.Zero;

	[Export]
	public float zoomedValue = 1.25f;

	[Export]
	public GameManager gameManager;

	private bool _isZoomed = false;

	private Vector2 _lastMousePos;
	public override void _Ready() {
		gameManager.SetZoom += OnZoomToggled;
	}


	public override void _Process(double delta) {
		// Camera Movement when zoomed
		if (_isZoomed) {
			Vector2 currentMousePos = GetLocalMousePosition();
			Vector2 mousePosDiff = _lastMousePos - currentMousePos;

			Position -= mousePosDiff * 0.5f;

			_lastMousePos = currentMousePos;
		}
	}

	public void OnZoomToggled(bool isZoomed) {
		_isZoomed = isZoomed;

		if (_isZoomed) {
			Zoom = new Vector2(zoomedValue, zoomedValue);
			_lastMousePos = GetLocalMousePosition(); // Record of position of mouse on zoom for camera movement
			Position = GetGlobalMousePosition();;
		} else {
			Zoom = new Vector2(unZoomedValue, unZoomedValue);
			Position = unZoomedPos;
		}
	}
}
