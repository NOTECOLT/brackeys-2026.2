using Godot;
using System;

public partial class ReferenceBill : Area2D {
	[Export]
	public float waitTimeTillShow = 1.5f;

	/// <summary>
	/// Marks if the reference bill has been shown for the first time already
	/// </summary>
	private bool _hasBeenShown = false;
	private bool _isShown = false;
	private AnimationPlayer _billAnimPlayer;
	private AnimationPlayer _tooltipAnimPlayer;
	private Label _tooltip;
	private Timer _helpTimer;
	public override void _Ready() {
		_billAnimPlayer = GetNode<AnimationPlayer>("BillAnimationPlayer");
		_tooltipAnimPlayer = GetNode<AnimationPlayer>("TooltipAnimationPlayer");

		_tooltip = GetNode<Label>("Tooltip");

		_helpTimer = GetNode<Timer>("../HelpTimer");
		_helpTimer.Timeout += OnHelpTimerTimeout;

		_isShown = false;

		_helpTimer.WaitTime = waitTimeTillShow;
		_helpTimer.Start();
	}

    public override void _InputEvent(Viewport viewport, InputEvent @event, int shapeIdx) {
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
