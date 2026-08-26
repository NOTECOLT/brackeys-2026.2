using Godot;
using System;

public partial class WrongIndicator : ColorRect {
    private AnimationPlayer _animPlayer;

    public override void _Ready() {
        base._Ready();

        _animPlayer =  GetNode<AnimationPlayer>("AnimationPlayer");
    }

    public void PlayWrongAnimation() {
        _animPlayer.Play("wrong");
    }
}
