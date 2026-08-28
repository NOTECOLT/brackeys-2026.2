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

    /// <summary>
    /// Percentage that an element can go missing.
    /// </summary>
    [Export]
    public float chanceToGoMissing = 0.4f;

    public Texture2D GenerateRandomRealSprite() {
        // If theres only one sprite, then just return that
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
        // If theres element can go missing, do not render it.
        if (chanceToGoMissing > 0 && GD.Randf() < chanceToGoMissing)
            return null;

        // If theres only one sprite, then just return that
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
