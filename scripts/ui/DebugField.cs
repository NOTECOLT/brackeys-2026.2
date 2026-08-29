using Godot;
using System;

public partial class DebugField : Label {
    [Export]
    public string fieldName;

    [Export]
    public DebugValue field;

    [Export]
    public DebugType type;

    public LineEdit _lineEdit;
    public override void _Ready() {
        Text = fieldName;

        _lineEdit = GetNode<LineEdit>("LineEdit");
        _lineEdit.TextSubmitted += OnTextSubmitted;

        _lineEdit.Text = ((DebugDouble)field).defaultValue.ToString();
    }

    private void OnTextSubmitted(string newText) {
        switch (type) {
            case DebugType.DOUBLE:
                if (double.TryParse(newText, out double newValue)) {
                    ((DebugDouble)field).value = newValue;  
                } else {
                    _lineEdit.Text = ((DebugDouble)field).defaultValue.ToString();
                }
                break;
            default:
                break;
        }
    }
}
