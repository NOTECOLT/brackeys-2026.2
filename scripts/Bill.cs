using Godot;
using System;

public partial class Bill : RigidBody2D {
    /// <summary>
    /// True if the bill is real, false if fake
    /// </summary>
    [Export]
    public bool isReal;

    [Export]
    public Texture2D _realTempSprite;

    [Export]
    public Texture2D[] _fakeTempSprites;


    private Sprite2D _tempBillSprite;

    public override void _Ready() {
        base._Ready();

        _tempBillSprite = GetNode<Sprite2D>("PositionWrapper/Sprite2D");
        if (isReal) {
            _tempBillSprite.Texture = _realTempSprite;
        } else {
            GD.Randomize();

            _tempBillSprite.Texture = _fakeTempSprites[GD.Randi() % _fakeTempSprites.Length];
        }
    }
}
