using Godot;
using System;

public partial class MainMenu : CanvasLayer {
	[Export]
	public Button startGameButton;

	private SignalManager _signalMgr;

	public override void _Ready() {
		_signalMgr = GetNode<SignalManager>(SignalManager.PATH);

		_signalMgr.ChangeGameState += OnChangeGameState;
		startGameButton.Pressed += OnStartGamePressed;
	}

	private void OnStartGamePressed() {
		_signalMgr.EmitSignal(SignalManager.SignalName.ChangeGameState, (int)GameState.GAME);
	}

	private void OnChangeGameState(GameState state) {
		if (state == GameState.MAIN_MENU) {
			Visible = true;
		} else {
			Visible = false;
		}
	}
}
