using Godot;
using System;

public partial class GameManager : Node {
    /* -- Public Values used for HUD & Game State -- */ 
    [Export]
    public double timeLeft;

    /* -- Game Settings -- */

    /// <summary>
    /// Time Limit expressed in seconds
    /// </summary>
    [Export]
    public double timeLimit = 30.0d;

    /// <summary>
    /// Increase in time left whenever a bill is swiped correctly
    /// </summary>
    [Export]
    public double timeBonus = 2.0d;

    /* -- Referenced Objects -- */
    [Export]
    public PackedScene bill;

    [Export]
    public SwipeZone realBillZone;

    [Export]
    public SwipeZone fakeBillZone;

    /* -- Signals -- */
    /// <summary>
    /// Signal sent on timer end
    /// </summary>
    [Signal]
    public delegate void TimerEndEventHandler();

    [Signal]
    public delegate void ScoreUpdateEventHandler(int score);
    
    [Signal]
    public delegate void SetZoomEventHandler(bool isZoomed);

    /* -- Other Private Game Variables used for Game State-- */
    private int _score = 0;

    private bool _isZoomed = false;

    public override void _Ready() {
        base._Ready();

        // Set Random Seed based on time
        GD.Randomize();

        // Add Signal Triggers
        realBillZone.BillSwiped += OnRealBillSwiped;
        fakeBillZone.BillSwiped += OnFakeBillSwiped;

        // Set initial game values
        timeLeft = timeLimit;
        _score = 0;
        _isZoomed = false;

        // Spawn first bill
        SpawnBill();
    }

    public override void _Process(double delta) {
        base._Process(delta);

        /* -- Zoom Control -- */
        if (Input.IsActionJustPressed("zoom")) {
            toggleIsZoomed(true);
        }

        if (Input.IsActionJustReleased("zoom")) {
            toggleIsZoomed(false);
        }

        /* -- Timer Processing -- */
        if (timeLeft > 0) {
            timeLeft -= delta;
        } else {
            GetTree().ChangeSceneToFile("res://scenes//game_over.tscn");
        }
    }

    /// <summary>
    /// Toggles isZoomed. Currently referenced in GameHUD when the zoom button is pressed.
    /// </summary>
    public void toggleIsZoomed(bool isZoomed) {
        _isZoomed = isZoomed;
        EmitSignal(SignalName.SetZoom, _isZoomed);
    }

    private void SpawnBill() {
        Node2D newBillNode = bill.Instantiate<Node2D>();

        Bill newBill = newBillNode.GetNode<Bill>(".");

        // Randomly generate real or fake bill
        int isReal = (int)(GD.Randi() % 2); // Generates 0 or 1
        newBill.isReal = isReal == 0;

        // CallDeferred pushes the function call to the end of the current frame.
        // Godot forbids physics state alterations (add new node) while it processes collisions
        CallDeferred(MethodName.AddChild, newBillNode);
    }

    private void OnRealBillSwiped(bool billIsReall) {
        if (billIsReall) BillSwipedCorrect();
        OnBillSwiped();
    }

    private void OnFakeBillSwiped(bool billIsReall) {
        if (!billIsReall) BillSwipedCorrect();
        OnBillSwiped();
    }
    
    private void OnBillSwiped() {
        SpawnBill();
    }

    private void BillSwipedCorrect() {
        timeLeft += timeBonus;
        _score += 1;
        EmitSignal(SignalName.ScoreUpdate, _score);
    }
}
