using Godot;
using System;

public partial class GameHUD : CanvasLayer {
	private Label _timer;
	private Label _score;

	private WrongIndicator _wrongIndicator;
	// private TextureButton _zoomButton;

	[Export]
	public GameManager gameManager;

	public override void _Ready() {
		base._Ready();

		_timer = GetNode<Label>("Timer");
		_score = GetNode<Label>("Score");
		_wrongIndicator = GetNode<WrongIndicator>("WrongIndicator");
		// _zoomButton = GetNode<TextureButton>("ZoomButton");

		gameManager.ScoreUpdate += OnScoreUpdate;
		gameManager.BillWrong += OnBillWrong;
		// _zoomButton.Pressed += OnZoomButtonPressed;
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

	// Disabled for now ~ (will use right click to activate zoom)
	// private void OnZoomButtonPressed() {
	// 	gameManager.toggleIsZoomed();
	// }

	private void OnBillWrong() {
		_wrongIndicator.PlayWrongAnimation();
	}
}
