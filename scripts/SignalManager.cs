using Godot;
using System;

public partial class SignalManager : Node {

    public static string PATH = "/root/SignalManager";
    
    /// <summary>
    /// Sent when the reference bill is shown or hidden
    /// </summary>
    /// <param name="isShown">True if the reference is shown, false otherwise</param>
	[Signal]
	public delegate void ReferenceShowEventHandler(bool isShown);

    /// <summary>
    /// Sent whenever the game state is changed
    /// </summary>
    /// <param name="state"></param>
    [Signal]
    public delegate void ChangeGameStateEventHandler(GameState state);

    /// <summary>
    /// Sent whenever the game is set into debug mode
    /// </summary>
    /// <param name="isDebug"></param>
    [Signal]
    public delegate void SetDebugModeEventHandler(bool isDebug);

    /// <summary>
    /// Carries debug info regarding each generated bill
    /// </summary>
    /// <param name="log"></param>
    [Signal]
    public delegate void SendBillDebugEventHandler(string log);

    /// <summary>
    /// Updates the game score
    /// </summary>
    /// <param name="score"></param>
    [Signal]
    public delegate void UpdateScoreEventHandler(int score);
}
