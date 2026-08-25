using Godot;
using System;

public partial class Bill : RigidBody2D {
    /// <summary>
    /// True if the bill is real, false if fake
    /// </summary>
    [Export]
    public bool isReal;


    private Label _tempBillLabel;

    public override void _Ready() {
        base._Ready();

        _tempBillLabel = GetNode<Label>("PositionWrapper/TempBillLabel");
        _tempBillLabel.Text = isReal ? "REAL" : "FAKE";
    }
}
