using Godot;
using System;

/// <summary>
/// Defines an element that can be randomized
/// 
/// Each RandomizedElement contains data on real & fake counterparts
/// </summary>
[GlobalClass]
public partial class RandomizedElement : Resource {
    [Export]
    public WeightedSprite[] realSprites;

    [Export]
    public WeightedSprite[] fakeSprites;

    public void GenerateRandomFakeSprite() {
        
    }
}
