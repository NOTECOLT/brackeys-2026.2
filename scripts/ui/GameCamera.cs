using Godot;
using System;
public partial class GameCamera : Node2D {
	[Export]
	public float zoomSpeed = 0.2f;

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

	private Vector2 _mousePosOnZoom;

	private AnimationPlayer _animPlayer;

	private SignalManager _signalMgr;

	private bool _canZoom = false;

	public override void _Ready() {
		_camera = GetNode<Camera2D>("Camera2D");
		_animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		_signalMgr = GetNode<SignalManager>(SignalManager.PATH);

		gameManager.BillWrong += OnBillWrong;
		_signalMgr.ChangeGameState += OnChangeGameState;

		_camera.Zoom = Vector2.One * unZoomedValue;
	}

	public override void _Process(double delta) {

		if (_canZoom) {
			if (Input.IsActionJustPressed("zoom")) {
				toggleZoom(true);
			}

			if (Input.IsActionJustReleased("zoom")) {
				toggleZoom(false);
			}		
		}


		if (_isZoomed) {
			// Camera zoom in;
			if (_camera.Zoom.X < zoomedValue) {
				_camera.Zoom += Vector2.One * zoomSpeed * (float)delta;

				if (_camera.Zoom.X > zoomedValue) _camera.Zoom = Vector2.One * zoomedValue;
				

				// Have Camera move to the where the mouse was on clicking zoom
				//		at the same speed at which the camera is zooming
				// Distance between Camera & initial Mouse * the percentage the camera zoomed within the last frame
				Position = Position.MoveToward(_mousePosOnZoom,	
					Position.DistanceTo(_mousePosOnZoom) * (zoomSpeed / (zoomedValue - _camera.Zoom.X)) * (float)delta);
			}

			
			// Camera Movement when zoomed
			Vector2 currentMousePos = GetLocalMousePosition();
			Vector2 mousePosDiff = _lastMousePos - currentMousePos;

			Position -= mousePosDiff * 0.5f;

			_lastMousePos = currentMousePos;

		} else {
			// Camera zoom out;
			if (_camera.Zoom.X > unZoomedValue) {
				_camera.Zoom -= Vector2.One * zoomSpeed * (float)delta;

				if (_camera.Zoom.X < unZoomedValue) _camera.Zoom = Vector2.One * unZoomedValue;

				// Have Camera move to unZoomedPosition
				//		at the same speed at which the camera is zooming
				// Distance between Camera & initial Mouse * the percentage the camera zoomed within the last frame
				Position = Position.MoveToward(unZoomedPos,	
					Position.DistanceTo(unZoomedPos) * (zoomSpeed / (zoomedValue - _camera.Zoom.X)) * (float)delta);
			}
		}
	}

	private void toggleZoom(bool isZoomed) {
		_isZoomed = isZoomed;

		if (_isZoomed) {
			// Record of position of mouse on zoom for camera movement
			_mousePosOnZoom = _lastMousePos = GetLocalMousePosition(); 
		}
	}

	private void OnBillWrong() {
		_animPlayer.Play("wrong");
	}

	private void OnChangeGameState(GameState state) {
		if (state == GameState.GAME) {
			_canZoom = true;
		} else {
			_canZoom = false;
		}
	}
}
