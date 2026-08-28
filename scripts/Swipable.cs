using Godot;
using System;

public partial class Swipable : Node {
	[Export]
	public float minSwipeDistance = 10;

	[Export]
	public float swipeForceFactor = 3000;

	private Vector2 _mouseStartPosition;
	private Vector2 _mouseCurrentPosition;
	private bool _isSwiping = false;
	private RigidBody2D _rigidBody;
	private bool _isSwipable = false;
	private RandomSFXPlayer _sfxPlayer;

	private SignalManager _signalMgr;
    private bool _isReferenceShown = false;
	public override void _Ready() {
		_signalMgr = GetNode<SignalManager>(SignalManager.PATH);
		_rigidBody = GetNode<RigidBody2D>("..");
		_sfxPlayer = GetNode<RandomSFXPlayer>("../MovementSFX");

		_signalMgr.ReferenceShow += OnReferenceShown;
	}

	public override void _Process(double delta) {
		// Play a random sfx on zoom
		if (Input.IsActionJustPressed("zoom")) {
			_sfxPlayer.PlayRandom();
		}

		/* -- Swiping -- */
		// Detect Swipe Start
		if (Input.IsActionJustPressed("press") && _isSwipable && !_isReferenceShown) {
			if (!_isSwiping) {
				_isSwiping = true;
				_mouseStartPosition = _rigidBody.GetGlobalMousePosition();
			}
		}

		// Swiping Action
		if (Input.IsActionPressed("press") && !_isReferenceShown) {
			if (_isSwiping) {
				_mouseCurrentPosition = _rigidBody.GetGlobalMousePosition();

				// Swipe must cover minimum distance
				if (_mouseStartPosition.DistanceTo(_mouseCurrentPosition) > minSwipeDistance) {
					float directionSwiped = Mathf.Round(_mouseStartPosition.DirectionTo(_mouseCurrentPosition).X);

					// Only detect horizontal swipes
					if (Mathf.Abs(directionSwiped) > 0.7f) {
						_isSwiping = false;

						// Push the bill
						_rigidBody.ApplyCentralImpulse(new Vector2(directionSwiped * swipeForceFactor, 0));
						GD.Print($"Direction Swiped {directionSwiped}");		
					}
				}
			}
		}

		if (Input.IsActionJustReleased("press") && _isSwiping) {
			_isSwiping = false;
		}
	}

	/// <summary>
	/// Called in animation player. Animation dictates when a bill is swipable.
	/// </summary>
	public void SetIsSwipable() {
		_isSwipable = true;
	}

	private void OnReferenceShown(bool isShown) {
		_isReferenceShown = isShown;
	}
}
