using Godot;
using System;
using System.Linq;

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


    public Texture2D GenerateRandomRealSprite() {
        if (realSprites.Length == 1) return realSprites[0].sprite;

        int totalWeight = realSprites.Sum(sprite => sprite.weight);

        int rng = (int)GD.Randi() % totalWeight;

        int runningWeight = 0;
        foreach (WeightedSprite sprite in realSprites) {
            runningWeight += sprite.weight;
            if (rng < runningWeight) return sprite.sprite;
        }
        
        return realSprites[realSprites.Length - 1].sprite;
    }

    public Texture2D GenerateRandomFakeSprite() {
        if (fakeSprites.Length == 1) return fakeSprites[0].sprite;

        int totalWeight = fakeSprites.Sum(sprite => sprite.weight);

        int rng = (int)GD.Randi() % totalWeight;

        int runningWeight = 0;
        foreach (WeightedSprite sprite in fakeSprites) {
            runningWeight += sprite.weight;
            if (rng < runningWeight) return sprite.sprite;
        }
        
        return fakeSprites[fakeSprites.Length - 1].sprite;
    }


    // 1, 2, 1

    // 4

    // 1

    // 2, 3

    // 1
}
