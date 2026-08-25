using Godot;
using System;

public partial class SwipeZone : Area2D {
	[Signal]
	public delegate void BillSwipedEventHandler();
	public override void _Ready() {
	}

	public override void _Process(double delta) {
	}

	/// <summary>
	/// When a bill enteres the Swipe zone, destroy the bill and emit a signal for game manager
	/// </summary>
	/// <param name="body"></param>
	public void OnBodyEntered(Node2D body) {
		body.QueueFree();
		EmitSignal(SignalName.BillSwiped);
	}
}
