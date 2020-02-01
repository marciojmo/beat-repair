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

    // Start is called before the first frame update
    void Start() {
       
    }

    // Update is called once per frame
    void Update() {

    }

    public void OnNoteXButton(CallbackContext value) {
        //print("player " + playerNumber + "pressed X");
        inputEvent.Invoke(playerNumber, InputNote.NOTE_X);
    }

    public void OnNoteYButton(CallbackContext value) {
        //print("player " + playerNumber + "pressed Y");
        inputEvent.Invoke(playerNumber, InputNote.NOTE_Y);
    }

    public void OnNoteAButton(CallbackContext value) {
        //print("player " + playerNumber + "pressed A");
        inputEvent.Invoke(playerNumber, InputNote.NOTE_A);
    }

    public void OnNoteBButton(CallbackContext value) {
        //print("player " + playerNumber + "pressed B");
        inputEvent.Invoke(playerNumber, InputNote.NOTE_B);
    }
}
