using Godot;
using System;

public partial class DebugHUD : CanvasLayer {
	private SignalManager _signalMgr;

    private bool _isDebug;

    private GameState _state;

    private Label _billData;

	public override void _Ready() {
		_signalMgr = GetNode<SignalManager>(SignalManager.PATH);
        _billData = GetNode<Label>("./BillData");

		_signalMgr.ChangeGameState += OnChangeGameState;
        _signalMgr.SetDebugMode += OnSetDebugMode;
        _signalMgr.SendBillDebug += OnSendBillDebug;

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
		if (_state == GameState.GAME && _isDebug) {
			Visible = true;
		} else {
			Visible = false;
		}
    }

    private void OnSendBillDebug(string log) {
        _billData.Text = log;
    }
}
