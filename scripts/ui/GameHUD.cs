using Godot;
using System;

public partial class GameHUD : Control {
    private Label _timer;

    [Export]
    public GameManager gameManager;

    public override void _Ready() {
        base._Ready();

        _timer = GetNode<Label>("Timer");
    }

    public override void _Process(double delta) {
        base._Process(delta);

        setTimerLabel(gameManager.timeLeft);

    }


    public void setTimerLabel(double time) {
        double min = Math.Abs(Math.Round(time / 60d));
        double sec = Math.Abs(Math.Round(time % 60d));
        _timer.Text = $"{min}:{sec:00}";
    }
}
