using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Prime31.MessageKit;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

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
    public ParticleSystem[] playersParticles;
    public AudioSource[] playersSuccessSongs;
    public AudioSource[] playersFailureSongs;
    public Color failColor, disabledColor;
    public float resetColorTime;
    public GameObject endGameScreen;
    public TextMesh endgameText;
    public GameObject endGameButton;
    public EventSystem eventSystem;

    private Dictionary<InputNote, Sprite> _buttonSpritesDictionary;


    private void Awake() {

        // Initiazing note to sprite map.
        _buttonSpritesDictionary = new Dictionary<InputNote, Sprite>();
        for ( int i = 0; i < buttonSprites.Length; i++ )
            _buttonSpritesDictionary.Add(buttonSprites[i].noteType, buttonSprites[i].sprite);

            MessageKit.addObserver(GameEvents.MORALE_CHANGED, () => {
            SetMoraleValue(LevelController.Instance.playersMorale);
        });

        MessageKit.addObserver(GameEvents.QUEUE_CHANGED, () => {
            UpdateQueue();
        });

        //MessageKit.addObserver(GameEvents.AUTO_HIT, () => {
        //    PaintNoteRed();
        //});

        MessageKit.addObserver(GameEvents.RESTORE_COLOR, () => {
            ResetButtonColor();
        });

        MessageKit.addObserver(GameEvents.GAME_OVER, () => {
            Debug.Break();
            OnEndGame();
        });
    }

    void PlayPlayer1Success()
    {
        playersParticles[0].gameObject.SetActive(false);
        playersParticles[0].gameObject.SetActive(true);
        playersParticles[0].Play();
        playersSuccessSongs[0].Play();
        // TODO: tocar som
        //Debug.Break();
    }

    void PlayPlayer2Success()
    {
        playersParticles[1].gameObject.SetActive(false);
        playersParticles[1].gameObject.SetActive(true);
        playersParticles[1].Play();
        playersSuccessSongs[1].Play();
        // TODO: tocar som
        //Debug.Break();
    }

    void PlayPlayer1Failure()
    {
        playersFailureSongs[0].Play();
    }

    void PlayPlayer2Failure()
    {
        playersFailureSongs[1].Play();
    }


    private void Start()
    {
        MessageKit.addObserver( GameEvents.P2_DAMAGE, PlayPlayer1Success );
        MessageKit.addObserver( GameEvents.P1_DAMAGE, PlayPlayer2Success );

        MessageKit.addObserver(GameEvents.P1_MISS, PlayPlayer1Failure);
        MessageKit.addObserver(GameEvents.P2_MISS, PlayPlayer2Failure);
    }

    // Update is called once per frame
    void Update() {
        float percent = AudioController.Instance.GetCurrentBeatPercentage();
        
        if ( AudioController.Instance.IsInAcceptZone() )
        {
            for (int i = 0; i < LevelController.NUMBER_OF_PLAYERS; i++)
            {
                if ( LevelController.Instance.playerMiss[i] )
                    playersButtons[i].images[0].color = failColor;
                else
                    playersButtons[i].images[0].color = Color.white;
            }
            //percent = 1f; // forcing slider percent to 1 when in accept zone
        }
        else
        {
            for (int i = 0; i < LevelController.NUMBER_OF_PLAYERS; i++)
            {
                if (LevelController.Instance.playerMiss[i])
                    playersButtons[i].images[0].color = failColor;
                else
                    playersButtons[i].images[0].color = disabledColor;
            }
            
        }
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

    private void ResetButtonColor() {
        for ( int i = 0; i < playersButtons.Count; i++ ) {
            playersButtons[i].images[0].color = Color.white;
        }        
    }

    public void OnEndGame() {
        endGameScreen.SetActive(true);
        eventSystem.SetSelectedGameObject(endGameButton);
    }

    public void OnBackToMenu() {
        SceneManager.LoadScene("MainMenu");
    }

}
