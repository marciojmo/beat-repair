using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Prime31.MessageKit;

public class UIController : MonoBehaviour
{

    [System.Serializable]
    public struct NoteSprite
    {
        public InputNote noteType;
        public Sprite sprite;
    }

    [System.Serializable]
    public struct PlayerSequenceUI
    {
        public Image[] images;
    }

    public Slider moraleSlider;
    public NoteSprite[] buttonSprites;
    public List<Image> playersBorders;
    public List<PlayerSequenceUI> playersButtons;

    private Dictionary<InputNote, Sprite> _buttonSpritesDictionary;

    private void Awake() {

        // Initiazing note to sprite map.
        _buttonSpritesDictionary = new Dictionary<InputNote, Sprite>();
        for ( int i = 0; i < buttonSprites.Length; i++ )
            _buttonSpritesDictionary.Add(buttonSprites[i].noteType, buttonSprites[i].sprite);

    }

    // Start is called before the first frame update
    void Start() {

        MessageKit.addObserver(GameEvents.MORALE_CHANGED, () => {
            SetMoraleValue(LevelController.Instance.playersMorale);
        });

        MessageKit.addObserver(GameEvents.QUEUE_CHANGED, () => {
            UpdateQueue();
        });
    }

    // Update is called once per frame
    void Update() {
        float percent = AudioController.Instance.GetCurrentBeatPercentage();
        SetBorderFillValue(percent);
        //SetBorderFillValue(1 - (1 - 0) * Mathf.Log(Time.deltaTime, percent));
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
        for ( int i = 0; i < playersBorders.Count; i++ ) {
            playersBorders[i].fillAmount = newValue;
        }
    }

    private void UpdateQueue() {

        for ( int i = 0; i < LevelController.NUMBER_OF_PLAYERS; i++ ) {
            InputNote[] currentPlayerNotes = LevelController.Instance.playerNotes[i].ToArray();
            for ( int j = 0; j < LevelController.NUMBER_OF_INITIAL_NOTES; j++ ) {
                InputNote currentPlayerNote = currentPlayerNotes[j];
                playersButtons[i].images[j].sprite = _buttonSpritesDictionary[currentPlayerNote];
            }
        }
    }

}
