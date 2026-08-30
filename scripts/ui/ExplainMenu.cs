using Godot;
using System;

public partial class ExplainMenu : CanvasLayer {
	[Export]
	public Button startGameButton;

	private SignalManager _signalMgr;

	private AnimationPlayer _animPlayer;
	public override void _Ready() {
		_signalMgr = GetNode<SignalManager>(SignalManager.PATH);
		_animPlayer = GetNode<AnimationPlayer>("Explainer/AnimationPlayer");

		_signalMgr.ChangeGameState += OnChangeGameState;
		startGameButton.Pressed += OnStartGamePressed;

		Visible = false;
	}

	private void OnStartGamePressed() {
		_signalMgr.EmitSignal(SignalManager.SignalName.ChangeGameState, (int)GameState.GAME);	
	}

	private void OnChangeGameState(GameState state) {
		if (state == GameState.TUTORIAL) {
			Visible = true;
			_animPlayer.Play("open");
		} else {
			Visible = false;
		}
	}
}
