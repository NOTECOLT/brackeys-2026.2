using Godot;
using System;

public partial class GameHUD : CanvasLayer {
	private Label _timer;
	private Label _score;
	private Button _zoomButton;

	[Export]
	public GameManager gameManager;

	public override void _Ready() {
		base._Ready();

		_timer = GetNode<Label>("Timer");
		_score = GetNode<Label>("Score");
		_zoomButton = GetNode<Button>("ZoomButton");

		gameManager.ScoreUpdate += OnScoreUpdate;
		_zoomButton.Pressed += OnZoomButtonPressed;
	}

	public override void _Process(double delta) {
		base._Process(delta);

		SetTimerLabel(gameManager.timeLeft);
	}

	private void OnScoreUpdate(int score) {
		_score.Text = $"Score: {score}";
	}

	private void SetTimerLabel(double time) {
		double min = Math.Abs(Math.Floor(time / 60d));
		double sec = Math.Abs(Math.Floor(time % 60d));
		_timer.Text = $"{min}:{sec:00}";
	}

	private void OnZoomButtonPressed() {
		
	}
}
