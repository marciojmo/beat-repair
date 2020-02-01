using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.MessageKit;

public class DebuggerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MessageKit.addObserver(GameEvents.BEAT_ENDED, () =>
        {
            Debug.Break();
        });

        //AudioController.Instance.Play();

    }

    // Update is called once per frame
    void Update()
    {
        print("Beat percentage: " + AudioController.Instance.GetCurrentBeatPercentage());
        //print("Accepting beats: " + AudioController.Instance.IsInAcceptZone());
    }



}
