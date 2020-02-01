using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelState {
    WAITING,
    HITWINDOW
}


public class LevelController : MonoBehaviour {
    // Start is called before the first frame update

    Queue<InputNote> _player1Notes, _player2Notes;
    bool _player1Success, _player2Success;
    /// From 0 to 1 /// Max player 1 = 1 /// Max Player 2 = 0 /// Middle = 0.5
    float _playersMorale = 0.5f;
    LevelState _currentState = LevelState.WAITING;

    public float punchDamage = 0.1f;
    public UIController uiController;
    

    void Start() {
        _player1Notes = new Queue<InputNote>();
        _player2Notes = new Queue<InputNote>();

        for ( int i = 0; i < 6; i++ ) {
            _player1Notes.Enqueue(GetRandomNote());
            _player2Notes.Enqueue(GetRandomNote());
        }
    }

    // Update is called once per frame
    void Update() {

        switch ( _currentState ) {
            case LevelState.WAITING:
                //updates fill barrs
                //uiController.SetBorderFillValue( porcentagem de progresso ate o proximo beat );
                break;
            case LevelState.HITWINDOW:
                break;
            default:
                break;
        }
    }

    public void OnNoteInput(int playerNumber, InputNote note) {
        print("player " + playerNumber + " pressed " + note);

        switch ( _currentState ) {
            case LevelState.WAITING:
                //process wrong note
                ProcessDirectHit(playerNumber);
                break;
            case LevelState.HITWINDOW:
                bool isNoteRight = CheckNote(playerNumber, note);
                RegisterNote(playerNumber, isNoteRight);                      
                break;
            default:
                break;
        }
        //update notes queue
        UpdateNoteQueue(playerNumber);

    }

    private bool CheckNote(int playerNumber, InputNote note) {
        if ( playerNumber == 1 ) {
            if ( note == _player1Notes.Peek() ) {
                return true;
            }
            return false;
        } else {
            if ( playerNumber == 2 ) {
                if ( note == _player2Notes.Peek() ) {
                    return true;
                }
                return false;
            }
        }
        return false;
    }

    private void RegisterNote(int playerNumber, bool isRight) {
        if ( playerNumber == 1 ) {
            if ( isRight ) {
                _player1Success = true;
            } else _player1Success = false;
        }
        if ( playerNumber == 2 ) {
            if ( isRight ) {
                _player2Success = true;
            } else _player2Success = false;
        }
    }

    private void ProcessNotes() {
        if (_player1Success && _player2Success) {
            //ambos acertaram, empate
            //animate
        }
        if ( _player1Success && !_player2Success ) {
            //player 1 acertou
            _playersMorale += punchDamage;
            //animate
        }
        if ( !_player1Success && _player2Success ) {
            //player 2 acertou
            _playersMorale -= punchDamage;
            //animate
        }
        if ( !_player1Success && !_player2Success ) {
            //todos erraram, empate
            //animate
        }
        //update ui
        uiController.SetMoraleValue(_playersMorale);
    }

    //when someone makes a mistake ant takes a hit
    public void ProcessDirectHit(int playerNumber) {
        if ( playerNumber == 1 ) {
            _playersMorale -= punchDamage;
            //animate
        } else {
            _playersMorale += punchDamage;
            //animate
        }
        //update ui
        uiController.SetMoraleValue(_playersMorale);
    }

    private void UpdateNoteQueue(int playerNumber) {
        InputNote newNote = GetRandomNote();

        if ( playerNumber == 1 ) {
            _player1Notes.Dequeue();
            _player1Notes.Enqueue(newNote);
            uiController.UpdateButtons(true, _player1Notes);
        } else {
            _player2Notes.Dequeue();
            _player2Notes.Enqueue(newNote);
            uiController.UpdateButtons(false, _player2Notes);
        }
    }

    private InputNote GetRandomNote() {
        return (InputNote)(Random.Range(0, System.Enum.GetNames(typeof(InputNote)).Length));
    }
}
