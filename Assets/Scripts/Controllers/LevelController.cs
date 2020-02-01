using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {
    // Start is called before the first frame update

    Queue<InputNote> player1Notes, player2Notes;

    void Start() {
        player1Notes = new Queue<InputNote>();
        player2Notes = new Queue<InputNote>();

        for ( int i = 0; i < 10; i++ ) {
            player1Notes.Enqueue(GetRandomNote());
            player2Notes.Enqueue(GetRandomNote());
        }
    }

    // Update is called once per frame
    void Update() {

    }

    public void OnNoteInput(int playerNumber, InputNote note) {
        print("player " + playerNumber + " pressed " + note);
    }

    private InputNote GetRandomNote() {
        return (InputNote)(Random.Range(0, System.Enum.GetNames(typeof(InputNote)).Length));
    }
}
