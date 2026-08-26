using Godot;
using System;

public partial class RandomSFXPlayer : AudioStreamPlayer2D {
    [Export]
    public AudioStream[] streams;

    public override void _Ready() {
        base._Ready();
    }

    public void PlayRandom() {
        Stream = streams[GD.Randi() % streams.Length];
        Play();
    }
}
