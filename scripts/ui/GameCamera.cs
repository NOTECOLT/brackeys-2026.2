using Godot;
using System;

public partial class GameCamera : Node2D {
	[Export]
	public float unZoomedValue = 1;

	[Export]
	public Vector2 unZoomedPos = Vector2.Zero;

	[Export]
	public float zoomedValue = 1.25f;

	[Export]
	public GameManager gameManager;

	private Camera2D _camera;

	private bool _isZoomed = false;

	private Vector2 _lastMousePos;

	private AnimationPlayer _animPlayer;

	public override void _Ready() {
		_camera = GetNode<Camera2D>("Camera2D");
		_animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

		gameManager.SetZoom += OnZoomToggled;
		gameManager.BillWrong += OnBillWrong;
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

	private void OnZoomToggled(bool isZoomed) {
		_isZoomed = isZoomed;

		if (_isZoomed) {
			_camera.Zoom = new Vector2(zoomedValue, zoomedValue);
			_lastMousePos = GetLocalMousePosition(); // Record of position of mouse on zoom for camera movement
			Position = GetGlobalMousePosition();;
		} else {
			_camera.Zoom = new Vector2(unZoomedValue, unZoomedValue);
			Position = unZoomedPos;
		}
	}

	private void OnBillWrong() {
		_animPlayer.Play("wrong");
	}
}
