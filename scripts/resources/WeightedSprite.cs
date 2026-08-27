using Godot;
using System;

/// <summary>
/// Defines a sprite and its weight
/// </summary>
[GlobalClass]
public partial class WeightedSprite : Resource {
    [Export]
    public Texture2D sprite;

    /// <summary>
    /// Positive integer denoting the "weight" of a sprite in randomization.
    /// 
    /// Can be a minimum of 0 and no maximum
    /// </summary>
    [Export(PropertyHint.Range, "0,100,1,or_greater")]
    public int weight = 1;
}
