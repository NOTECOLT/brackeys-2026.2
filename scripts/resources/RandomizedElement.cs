using Godot;
using System;

/// <summary>
/// Defines an element that can be randomized
/// </summary>
[GlobalClass]
public partial class RandomizedElement : Resource {
    [Export]
    public Texture2D[] realSprites;

    [Export]
    public Texture2D[] fakeSprites;
}
