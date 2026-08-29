using Godot;
using System;
using System.Diagnostics;

public partial class Bill : RigidBody2D {
    [Export]
    public DebugInt totalFakeElements;

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

    /// <summary>
    /// log summary of bill containing data of which sprites were faked for debugging purposes
    /// </summary>    
    private string _debugSpriteLog;

    public override void _Ready() {
        _signalMgr = GetNode<SignalManager>(SignalManager.PATH);
        _layerParent = GetNode<Node2D>("PositionWrapper");

        _signalMgr.ChangeGameState += OnChangeGameState;
        
        _debugSpriteLog = "[SpawnBill]\n";

        // The number of fake elements to be generated IF the bill isnt real
        int fakeRemaining = 0;
        if (!isReal) {
            fakeRemaining = Mathf.Max(1, ((int)GD.Randi() % totalFakeElements.value) + 1);
        }

        _debugSpriteLog += $"{fakeRemaining} fake elements\n";
        
        int elementsRemaining = billElements.Length;
        foreach (RandomizedElement billElement in billElements) {
            Node2D newBillLayer = billLayer.Instantiate<Node2D>();
            Sprite2D layerSprite = newBillLayer.GetNode<Sprite2D>(".");

            bool wasFaked = false;
            if (!isReal && fakeRemaining > 0) {
                // if the bill isn't real, then randomly decide which elements will be faked

                if (GD.Randf() < (float)fakeRemaining / elementsRemaining) {
                    WeightedSprite weightedSprite = billElement.GenerateRandomFakeSprite();

                    if (weightedSprite != null) {
                        layerSprite.Texture = weightedSprite.sprite;
                        _debugSpriteLog += weightedSprite.ResourcePath + "\n";
                    } else {
                        _debugSpriteLog += billElement.ResourcePath + " EMPTY\n";
                    }
                    
                    fakeRemaining--;
                    wasFaked = true;
                }
            }

            // If bill is real or the specific element wasnt faked, then generate real sprite
            if (isReal || !wasFaked) {
                layerSprite.Texture = billElement.GenerateRandomRealSprite().sprite;
            }

            if (layerSprite.Texture != null) _layerParent.AddChild(newBillLayer);
            elementsRemaining--;
        }

        _signalMgr.EmitSignal(SignalManager.SignalName.SendBillDebug, _debugSpriteLog);
    }

    public override void _ExitTree() {
        _signalMgr.ChangeGameState -= OnChangeGameState;
    }

    public void OnChangeGameState(GameState state) {
        QueueFree();
    }
}
