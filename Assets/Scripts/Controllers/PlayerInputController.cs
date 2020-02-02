using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public enum InputNote {
    NOTE_A,
    NOTE_B,
    NOTE_X,
    NOTE_Y
}

[System.Serializable]
public class InputEvent : UnityEvent<int, InputNote> {
}

public class PlayerInputController : MonoBehaviour {

    public int playerNumber;

    public InputEvent inputEvent;

    bool[] _pressedButton;

    // Start is called before the first frame update
    void Start() {
        _pressedButton = new bool[4]{ false, false, false, false};
    }

    // Update is called once per frame
    void Update() {

    }

    public void OnNoteXButton(CallbackContext value) {
        if( !_pressedButton[0]) 
            inputEvent.Invoke(playerNumber, InputNote.NOTE_X);
        _pressedButton[0] = !_pressedButton[0];
    }

    public void OnNoteYButton(CallbackContext value) {
        if ( !_pressedButton[1] )
            inputEvent.Invoke(playerNumber, InputNote.NOTE_Y);
        _pressedButton[1] = !_pressedButton[1];
    }

    public void OnNoteAButton(CallbackContext value) {
        if ( !_pressedButton[2] )
            inputEvent.Invoke(playerNumber, InputNote.NOTE_A);
        _pressedButton[2] = !_pressedButton[2];
    }

    public void OnNoteBButton(CallbackContext value) {
        if ( !_pressedButton[3] )
            inputEvent.Invoke(playerNumber, InputNote.NOTE_B);
        _pressedButton[3] = !_pressedButton[3];
    }
}
