using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [System.Serializable]
    public struct NoteSprite {
        public Sprite sprite;
        public InputNote noteType;
    }

    public Slider moraleSlider;
    public NoteSprite[] buttonSprites;

    [Header("Player 1")]
    public Image player1FillBorderImage;
    public Image[] player1Buttons;

    [Header("Player 2")]
    public Image player2FillBorderImage;
    public Image[] player2Buttons;

    private Dictionary<InputNote, Sprite> buttonSpritesDictionary;

    // Start is called before the first frame update
    void Start() {
        buttonSpritesDictionary = new Dictionary<InputNote, Sprite>();
        for ( int i = 0; i < buttonSprites.Length; i++ ) {
            buttonSpritesDictionary.Add(buttonSprites[i].noteType, buttonSprites[1].sprite);
        }
    }

    // Update is called once per frame
    void Update() {
        
    }

    /// <summary>
    /// From 0 to 1
    /// Max player 1 = 1
    /// Max Player 2 = 0
    /// Middle = 0.5
    /// </summary>
    /// <param name="newValue"></param>
    public void SetMoraleValue(float newValue) {
            moraleSlider.value = newValue;
    }

    /// <summary>
    /// Value of fill from 0 to 1
    /// </summary>
    /// <param name="newValue"></param>
    public void SetBorderFillValue(float newValue) {
        player1FillBorderImage.fillAmount = newValue;
        player2FillBorderImage.fillAmount = newValue;
    }

    public void UpdateButtons(bool isPlayer1, Queue<InputNote> notes) {
        if (isPlayer1) {
            for ( int i = 0; i < player1Buttons.Length; i++ ) {
                player1Buttons[i].sprite = buttonSpritesDictionary[notes.Dequeue()];
            }
        } else {
            for ( int i = 0; i < player2Buttons.Length; i++ ) {
                player2Buttons[i].sprite = buttonSpritesDictionary[notes.Dequeue()];
            }
        }
    }

}
