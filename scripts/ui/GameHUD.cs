using Godot;
using System;

public partial class GameHUD : CanvasLayer {
	[Export]
	public GameManager gameManager;

	private Label _timer;
	private Label _score;

	private WrongIndicator _wrongIndicator;
	private SignalManager _signalMgr;

	public override void _Ready() {
		_signalMgr = GetNode<SignalManager>(SignalManager.PATH);
		_timer = GetNode<Label>("Timer");
		_score = GetNode<Label>("Score");
		_wrongIndicator = GetNode<WrongIndicator>("WrongIndicator");
		
		_signalMgr.ChangeGameState += OnChangeGameState;
		gameManager.ScoreUpdate += OnScoreUpdate;
		gameManager.BillWrong += OnBillWrong;

		Visible = false;
	}

	public override void _Process(double delta) {
		SetTimerLabel(gameManager.timeLeft);
	}

	private void OnChangeGameState(GameState state) {
		if (state == GameState.GAME) {
			Visible = true;
		} else {
			Visible = false;
		}
	}

	private void OnScoreUpdate(int score) {
		_score.Text = $"Score: {score}";
	}

	private void SetTimerLabel(double time) {
		double min = Math.Abs(Math.Floor(time / 60d));
		double sec = Math.Abs(Math.Floor(time % 60d));
		_timer.Text = $"{min}:{sec:00}";
	}

	private void OnBillWrong() {
		_wrongIndicator.PlayWrongAnimation();
	}
}
