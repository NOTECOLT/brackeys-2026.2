using Godot;
using System;

public partial class DebugMenu : CanvasLayer {
	private SignalManager _signalMgr;

    private bool _isDebug;

    private GameState _state;

	public override void _Ready() {
		_signalMgr = GetNode<SignalManager>(SignalManager.PATH);

		_signalMgr.ChangeGameState += OnChangeGameState;
        _signalMgr.SetDebugMode += OnSetDebugMode;

        _isDebug = false;
        _state = GameState.MAIN_MENU;
        Visible = false;
	}

	private void OnChangeGameState(GameState state) {
        _state = state;
        UpdateVisibility();
	}

    private void OnSetDebugMode(bool isDebug) {
        _isDebug = isDebug;
        UpdateVisibility();
    }

    private void UpdateVisibility() {
		if ((_state == GameState.MAIN_MENU || _state == GameState.GAME_OVER) && _isDebug) {
			Visible = true;
		} else {
			Visible = false;
		}
    }
}
