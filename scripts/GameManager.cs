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


    // Signal sent on timer end
    [Signal]
    public delegate void TimerEndEventHandler();
    
    public override void _Ready() {
        base._Ready();

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

        AddChild(newBill);
    }
}
