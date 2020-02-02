using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.MessageKit;

public enum LevelState {
    WAITING,
    HITWINDOW
}


public class LevelController : Singleton<LevelController> {

    public static int NUMBER_OF_PLAYERS = 2;
    public static int NUMBER_OF_INITIAL_NOTES = 6;

    // Start is called before the first frame update

    public SongConfig song;
    /// From 0 to 1 /// Max player 1 = 1 /// Max Player 2 = 0 /// Middle = 0.5
    public float playersMorale = 0.5f;
    public Queue<InputNote>[] playerNotes;
    public bool[] playerSuccess;
    private bool[] _ignoreInput;




    public float punchDamage = 0.1f;
    public UIController uiController;

    private void Init() {
        playersMorale = 0.5f;

        playerNotes = new Queue<InputNote>[NUMBER_OF_PLAYERS];
        playerSuccess = new bool[NUMBER_OF_PLAYERS];
        _ignoreInput = new bool[NUMBER_OF_PLAYERS];
        for ( int i = 0; i < NUMBER_OF_PLAYERS; i++ ) {
            playerNotes[i] = new Queue<InputNote>();
        }

        ResetIgnoreInput();
    }


    void Start() {

        // Initialize model data
        Init();

        // Registers an observer for the beat ended event.
        MessageKit.addObserver(GameEvents.BEAT_ENDED, ProcessNotes);

        // Spawn Initial notes
        for ( int p = 0; p < NUMBER_OF_PLAYERS; p++ )
            for ( int i = 0; i < NUMBER_OF_INITIAL_NOTES; i++ )
                playerNotes[p].Enqueue(GetRandomNote());

        // Let the party begin!
        AudioController.Instance.Play(song);
    }

    private void ResetIgnoreInput() {
        for ( int i = 0; i < _ignoreInput.Length; i++ )
            _ignoreInput[i] = false;
    }

    public void OnNoteInput(int playerNumber, InputNote note) {
        print("player " + playerNumber + " pressed " + note);

        if ( AudioController.Instance.IsInAcceptZone() && !_ignoreInput[playerNumber] ) {
            _ignoreInput[playerNumber] = true;
            playerSuccess[playerNumber] = (playerNotes[playerNumber].Peek() == note);
        } else {
            ProcessDirectHit(playerNumber);
        }

        //update notes queue
        //UpdateNoteQueue(playerNumber);
    }
    private void ProcessNotes() {

        float moraleChange = 0f;
        for ( int i = 0; i < NUMBER_OF_PLAYERS; i++ ) {
            if ( playerSuccess[i] == true ) {
                moraleChange += GetMoraleModifier(i) * punchDamage;
                print("acertou o " + i);
            }                

           UpdateNoteQueue(i);
        }

        UpdateMorale(moraleChange);
        ResetIgnoreInput();
    }

    private float GetMoraleModifier(int playerNumber) {
        return playerNumber == 0 ? 1f : -1f;
    }

    //when someone makes a mistake ant takes a hit
    public void ProcessDirectHit(int playerNumber) {
        // Adds a reverse punch damage on morale
        UpdateMorale(-GetMoraleModifier(playerNumber) * punchDamage);
        UpdateNoteQueue(playerNumber);

        // TODO: update ui via messagekit
        MessageKit.post(GameEvents.AUTO_HIT);
    }

    private void UpdateMorale(float moraleChange) {
        playersMorale += moraleChange;
        MessageKit.post(GameEvents.MORALE_CHANGED);
    }

    private void UpdateNoteQueue(int playerNumber) {
        playerNotes[playerNumber].Dequeue();
        playerNotes[playerNumber].Enqueue(GetRandomNote());
        MessageKit.post(GameEvents.QUEUE_CHANGED);
    }

    private InputNote GetRandomNote() {
        return (InputNote)(Random.Range(0, System.Enum.GetNames(typeof(InputNote)).Length));
    }
}
