using Godot;
using System;

[GlobalClass]
public partial class DebugInt : DebugValue {
    [Export]
    public int value;

    [Export]
    public int defaultValue;
}
