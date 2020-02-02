using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    public GameObject buttons;
    public GameObject startButton;
    public GameObject aboutScreen;
    public GameObject aboutBackButton;
    public AudioSource audioSource;
    public AudioClip selectSound;
    public AudioClip clickSound;
    public EventSystem eventSystem;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void OnStart() {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnAbout() {
        aboutScreen.SetActive(true);
        buttons.SetActive(false);
        eventSystem.SetSelectedGameObject(aboutBackButton);
    }

    public void OnExit() {
        Application.Quit();
    }

    public void OnCloseAbout() {
        aboutScreen.SetActive(false);
        buttons.SetActive(true);
        eventSystem.SetSelectedGameObject(startButton);
    }

    public void PlaySelectSound() {
        audioSource.PlayOneShot(selectSound);
    }

    public void PlayClickSound() {
        audioSource.PlayOneShot(clickSound);
    }

}
