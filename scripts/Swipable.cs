using Godot;
using System;

public partial class Swipable : RigidBody2D {
	private Vector2 _mouseStartPosition;
	private Vector2 _mouseCurrentPosition;
	private bool _isSwiping = false;

	[Export]
	public float minSwipeDistance = 10;

	[Export]
	public float swipeForceFactor = 3000;

	public override void _Ready() {
	}

	public override void _Process(double delta) {
		// Detect Swipe Start
		if (Input.IsActionJustPressed("press")) {
			if (!_isSwiping) {
				_isSwiping = true;
				_mouseStartPosition = GetGlobalMousePosition();

				GD.Print($"Swipe Start {_mouseStartPosition}");
			}
		}

		// Swiping Action
		if (Input.IsActionPressed("press")) {
			if (_isSwiping) {
				_mouseCurrentPosition = GetGlobalMousePosition();

				// Swipe must cover minimum distance
				if (_mouseStartPosition.DistanceTo(_mouseCurrentPosition) > minSwipeDistance) {
					float directionSwiped = Mathf.Round(_mouseStartPosition.DirectionTo(_mouseCurrentPosition).X);

					// Only detect horizontal swipes
					if (Mathf.Abs(directionSwiped) > 0.7f) {
						_isSwiping = false;

						ApplyCentralImpulse(new Vector2(directionSwiped * swipeForceFactor, 0));
						GD.Print($"Direction Swiped {directionSwiped}");		
					}
				}
			}
		}

		if (Input.IsActionJustReleased("press") && _isSwiping) {
			_isSwiping = false;
		}
	}
}
