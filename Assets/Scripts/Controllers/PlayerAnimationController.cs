using Prime31.MessageKit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour {

    public int playerNumber;
    public Animator animator;
    public AudioClip[] soundEffects;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start() {

        audioSource = GetComponent<AudioSource>();

        if (playerNumber == 0) {
            MessageKit.addObserver(GameEvents.P1_AUTO_HIT, () => {
                PlayConfusionAnimation();
            });
            MessageKit.addObserver(GameEvents.P1_PUNCH, () => {
                PlayPunchAnimation();
            });
            MessageKit.addObserver(GameEvents.P1_DAMAGE, () => {
                PlayDamageAnimation();
            });
            MessageKit.addObserver(GameEvents.P1_DEFENSE, () => {
                PlayDefenseAnimation();
            });
            MessageKit.addObserver(GameEvents.P1_CONFUSION, () => {
                PlayConfusionAnimation();
            });
        } else {
            MessageKit.addObserver(GameEvents.P2_AUTO_HIT, () => {
                PlayConfusionAnimation();
            });
            MessageKit.addObserver(GameEvents.P2_PUNCH, () => {
                PlayPunchAnimation();
            });
            MessageKit.addObserver(GameEvents.P2_DAMAGE, () => {
                PlayDamageAnimation();
            });
            MessageKit.addObserver(GameEvents.P2_DEFENSE, () => {
                PlayDefenseAnimation();
            });
            MessageKit.addObserver(GameEvents.P2_CONFUSION, () => {
                PlayConfusionAnimation();
            });
        }        
    }

    void PlayConfusionAnimation() {
        animator.SetTrigger("Confusion");
    }

    void PlayPunchAnimation() {
        animator.SetTrigger("Punch");
    }

    void PlayDamageAnimation() {
        animator.SetTrigger("Hit");
    }

    void PlayDefenseAnimation() {
        animator.SetTrigger("Defense");
    }

    public void PlaySoundEffect(int index) {
        audioSource.PlayOneShot(soundEffects[index]);

    }
}
