using Godot;
using System;

[GlobalClass]
public partial class DebugDouble : DebugValue {
    [Export]
    public double value;

    [Export]
    public double defaultValue;
}
