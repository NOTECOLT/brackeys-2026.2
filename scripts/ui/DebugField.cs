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

        switch (type) {
            case DebugType.DOUBLE:
                _lineEdit.Text = ((DebugDouble)field).defaultValue.ToString();
                break;
            case DebugType.INT:
                _lineEdit.Text = ((DebugInt)field).defaultValue.ToString();
                break;
            default:
                break;
        }
        
    }

    private void OnTextSubmitted(string newText) {
        switch (type) {
            case DebugType.DOUBLE:
                if (double.TryParse(newText, out double newValueDouble)) {
                    ((DebugDouble)field).value = newValueDouble;  
                } else {
                    _lineEdit.Text = ((DebugDouble)field).defaultValue.ToString();
                }
                break;
            case DebugType.INT:
                if (int.TryParse(newText, out int newValueInt)) {
                    ((DebugInt)field).value = newValueInt;  
                } else {
                    _lineEdit.Text = ((DebugInt)field).defaultValue.ToString();
                }
                break;
            default:
                break;
        }
    }
}
