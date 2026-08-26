using Godot;
using System;

public partial class MainMenu : Control {
	[Export]
	public Button startGameButton;
	public override void _Ready() {
		base._Ready();

		startGameButton.Pressed += OnStartGamePressed;
	}
	
	public override void _Process(double delta) {
		base._Process(delta);
	}

	private void OnStartGamePressed() {
		GetTree().ChangeSceneToFile("res://scenes//game.tscn");
	}
}
