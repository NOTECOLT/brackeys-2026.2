using Godot;
using System;

public partial class PointLight : Node {
    private SignalManager _signalMgr;
    private AnimationPlayer _animPlayer;

    public override void _Ready() {
        _signalMgr = GetNode<SignalManager>(SignalManager.PATH);
        _animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

        _signalMgr.ChangeGameState += OnChangeGameState;
    }

    private void OnChangeGameState(GameState state) {
        if (state == GameState.GAME_OVER) {
            _animPlayer.Play("shut_off");
        } else if (state == GameState.GAME) {
            _animPlayer.Play("turn_on");
        }
    }

}
