using Godot;
using System;

public partial class Bill : RigidBody2D {
    [Export]
    public int totalFakeElements = 5;

    /// <summary>
    /// True if the bill is real, false if fake
    /// </summary>
    [Export]
    public bool isReal;

    /// <summary>
    /// Actual game object holding bill elements. These are spawned dynamically
    /// </summary>
    [Export]
    public PackedScene billLayer;

    /// <summary>
    /// Each RandomizedElement contains data on real & fake counterparts
    /// </summary>
    [Export]
    public RandomizedElement[] billElements;

    private Node2D _layerParent;

    private SignalManager _signalMgr;

    public override void _Ready() {
        _signalMgr = GetNode<SignalManager>(SignalManager.PATH);
        _layerParent = GetNode<Node2D>("PositionWrapper");

        _signalMgr.ChangeGameState += OnChangeGameState;

        // The number of fake elements to be generated IF the bill isnt real
        int fakeRemaining = 0;
        if (!isReal) {
            fakeRemaining = ((int)GD.Randi() % totalFakeElements) + 1;
        }
        
        int elementsRemaining = billElements.Length;
        foreach (RandomizedElement billElement in billElements) {
            Node2D newBillLayer = billLayer.Instantiate<Node2D>();
            Sprite2D layerSprite = newBillLayer.GetNode<Sprite2D>(".");

            bool wasFaked = false;
            if (!isReal && fakeRemaining > 0) {
                // if the bill isn't real, then randomly decide which elements will be faked

                if (GD.Randf() < (float)fakeRemaining / elementsRemaining) {

                    layerSprite.Texture = billElement.GenerateRandomFakeSprite();
                    fakeRemaining--;
                    wasFaked = true;
                }
            }

            // If bill is real or the specific element wasnt faked, then generate real sprite
            if (isReal || !wasFaked) {
                layerSprite.Texture = billElement.GenerateRandomRealSprite();
            }

            if (layerSprite.Texture != null) _layerParent.AddChild(newBillLayer);
            elementsRemaining--;
        }
    }

    public override void _ExitTree() {
        _signalMgr.ChangeGameState -= OnChangeGameState;
    }

    public void OnChangeGameState(GameState state) {
        QueueFree();
    }
}
