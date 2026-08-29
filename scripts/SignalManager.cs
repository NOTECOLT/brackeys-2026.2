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


    [Signal]
    public delegate void SetDebugModeEventHandler(bool isDebug);
}
