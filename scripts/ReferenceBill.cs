using Godot;
using System;

public partial class ReferenceBill : Node2D {
	[Export]
	public float waitTimeTillShow = 1.5f;

	/// <summary>
	/// Marks if the reference bill has been shown for the first time already
	/// </summary>
	private bool _hasBeenShown = false;
	private bool _isShown = false;
	private CollisionObject2D _positionWrapper;
	private AnimationPlayer _billAnimPlayer;
	private AnimationPlayer _tooltipAnimPlayer;
	private Label _tooltip;
	private Timer _helpTimer;
	public override void _Ready() {
		_positionWrapper = GetNode<CollisionObject2D>("PositionWrapper");
		_billAnimPlayer = GetNode<AnimationPlayer>("PositionWrapper/BillAnimationPlayer");
		_tooltipAnimPlayer = GetNode<AnimationPlayer>("PositionWrapper/TooltipAnimationPlayer");
		_tooltip = GetNode<Label>("PositionWrapper/Tooltip");
		_helpTimer = GetNode<Timer>("HelpTimer");

		_positionWrapper.InputEvent += OnInputEvent;
		_helpTimer.Timeout += OnHelpTimerTimeout;	

		_isShown = false;

		_helpTimer.WaitTime = waitTimeTillShow;
		_helpTimer.Start();
	}

    private void OnInputEvent(Node viewport, InputEvent @event, long shapeIdx) {
		if (@event is InputEventMouseButton mouseEvent) {
			if (mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed) {
				if (_isShown) {
					_billAnimPlayer.Play("hide_reference");
				} else {
					_billAnimPlayer.Play("show_reference");
				}		
				_isShown = !_isShown;

				// Disable tooltip after clicking once
				if (!_hasBeenShown) {
					_tooltip.Hide();
					_hasBeenShown = true;
				}
			}
		}
    }

	public void OnHelpTimerTimeout() {
		_billAnimPlayer.Play("start");
	}
}
