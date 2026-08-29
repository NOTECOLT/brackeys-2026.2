using Godot;
using System;

public partial class GameOver : CanvasLayer {
	[Export]
	public Button startGameButton;

	[Export]
	public Label scoreLabel;

	private SignalManager _signalMgr;

	public override void _Ready() {
		_signalMgr = GetNode<SignalManager>(SignalManager.PATH);

		_signalMgr.ChangeGameState += OnChangeGameState;
		_signalMgr.UpdateScore += OnUpdateScore;
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

	private void OnUpdateScore(int score) {
		scoreLabel.Text = $"Score: {score}";
	}
}
