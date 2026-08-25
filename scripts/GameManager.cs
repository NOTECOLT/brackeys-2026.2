using Godot;
using System;

public partial class GameManager : Node {

    private double _timeLeft;

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

        // Add Signal Triggers
        realBillZone.BillSwiped += OnRealBillSwiped;
        fakeBillZone.BillSwiped += OnFakeBillSwiped;

        _timeLeft = timeLimit;
        SpawnBill();
    }

    public override void _Process(double delta) {
        base._Process(delta);

        if (_timeLeft > 0) {
            _timeLeft -= delta;
        } else {
            GD.Print("Game Over");
        }
    }

    public void SpawnBill() {
        Node2D newBill = bill.Instantiate<Node2D>();

        // CallDeferred pushes the function call to the end of the current frame.
        // Godot forbids physics state alterations (add new node) while it processes collisions
        CallDeferred(MethodName.AddChild, newBill);
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
