using Godot;
using System;

public partial class GameOver : CanvasLayer {
	[Export]
	public Button startGameButton;

	private SignalManager _signalMgr;

	public override void _Ready() {
		_signalMgr = GetNode<SignalManager>(SignalManager.PATH);

		_signalMgr.ChangeGameState += OnChangeGameState;
		startGameButton.Pressed += OnStartGamePressed;

		Visible = false;
	}

	private void OnStartGamePressed() {
		_signalMgr.EmitSignal(SignalManager.SignalName.ChangeGameState, (int)GameState.GAME);
	}

	private void OnChangeGameState(GameState state) {
		if (state == GameState.GAME_OVER) {
			Visible = true;
		} else {
			Visible = false;
		}
	}
}
