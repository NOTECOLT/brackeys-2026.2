using Godot;
using System;

public partial class GameManager : Node {
    [Export]
    public double timeLeft;

    /// <summary>
    /// Time Limit expressed in seconds
    /// </summary>
    [Export]
    public double timeLimit = 30.0d;

    [Export]
    public PackedScene bill;
    [Export]
    public SwipeZone realBillZone;
    [Export]
    public SwipeZone fakeBillZone;

    // Signal sent on timer end
    [Signal]
    public delegate void TimerEndEventHandler();
    
    public override void _Ready() {
        base._Ready();

        // Set Random Seed based on time
        GD.Randomize();

        // Add Signal Triggers
        realBillZone.BillSwiped += OnRealBillSwiped;
        fakeBillZone.BillSwiped += OnFakeBillSwiped;

        timeLeft = timeLimit;
        SpawnBill();
    }

    public override void _Process(double delta) {
        base._Process(delta);

        if (timeLeft > 0) {
            timeLeft -= delta;
        } else {
            GetTree().ChangeSceneToFile("res://scenes//game_over.tscn");
        }
    }

    public void SpawnBill() {
        Node2D newBillNode = bill.Instantiate<Node2D>();

        Bill newBill = newBillNode.GetNode<Bill>(".");

        // Randomly generate real or fake bill
        int isReal = (int)(GD.Randi() % 2); // Generates 0 or 1
        newBill.isReal = isReal == 0;

        // CallDeferred pushes the function call to the end of the current frame.
        // Godot forbids physics state alterations (add new node) while it processes collisions
        CallDeferred(MethodName.AddChild, newBillNode);
    }

    public void OnRealBillSwiped() {
        OnBillSwiped();
    }

    public void OnFakeBillSwiped() {
        OnBillSwiped();
    }


    public void OnBillSwiped() {
        SpawnBill();
    }
}
