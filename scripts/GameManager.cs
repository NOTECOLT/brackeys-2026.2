using Godot;
using System;

public partial class GameManager : Node {
    /* -- Public Values used for HUD & Game State -- */ 
    [Export]
    public double timeLeft;

    /// <summary>
    /// Used for dynamic, point at which audio will start to change into frantic mode
    /// </summary>
    [Export]
    public double timeFrantic = 12.0f;

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
    public PackedScene floatingNumber;

    [Export]
    public SwipeZone realBillZone;

    [Export]
    public SwipeZone fakeBillZone;

    [Export]
    public DynamicAudio dynamicAudio;

    /* -- Signals -- */
    /// <summary>
    /// Signal sent on timer end
    /// </summary>
    [Signal]
    public delegate void TimerEndEventHandler();

    [Signal]
    public delegate void ScoreUpdateEventHandler(int score);

    [Signal]
    public delegate void BillWrongEventHandler();

    /* -- Other Private Game Variables used for Game State-- */
    private int _score = 0;

    private bool _isZoomed = false;

    private AudioStreamPlayer2D _correctSFX;
    private AudioStreamPlayer2D _wrongSFX; 

    public override void _Ready() {
        base._Ready();

        _correctSFX = GetNode<AudioStreamPlayer2D>("CorrectSFX");
        _wrongSFX = GetNode<AudioStreamPlayer2D>("WrongSFX");


        /* -- Bill Spawning & Game Logic -- */

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

        /* -- Timer Processing -- */
        if (timeLeft > 0) {
            timeLeft -= delta;

            if (timeLeft < timeFrantic) {
                dynamicAudio.SetActiveStream(1);
            } else {
                dynamicAudio.SetActiveStream(0);
            }
        } else {
            GetTree().ChangeSceneToFile("res://scenes//game_over.tscn");
        }
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
        else BillSwipedWrong();
        OnBillSwiped();
    }

    private void OnFakeBillSwiped(bool billIsReall) {
        if (!billIsReall) BillSwipedCorrect();
        else BillSwipedWrong();
        OnBillSwiped();
    }
    
    private void OnBillSwiped() {
        SpawnBill();
    }

    private void BillSwipedCorrect() {
        timeLeft += timeBonus;
        _score += 1;
        EmitSignal(SignalName.ScoreUpdate, _score);

        Node2D newFloatingNumber = floatingNumber.Instantiate<Node2D>();
        Label label = newFloatingNumber.GetNode<Label>("./Label");
        label.Text = $"+{(int)timeBonus}s";

        _correctSFX.Play();

        // CallDeferred pushes the function call to the end of the current frame.
        // Godot forbids physics state alterations (add new node) while it processes collisions
        CallDeferred(MethodName.AddChild, newFloatingNumber);
    }

    private void BillSwipedWrong() {
        EmitSignal(SignalName.BillWrong);

        _wrongSFX.Play();
    }
}
