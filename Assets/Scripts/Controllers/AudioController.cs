using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Prime31;
using Prime31.MessageKit;

[RequireComponent(typeof(AudioSource))]
public class AudioController : Singleton<AudioController>, IAudioController
{
    // A reference to the sound that should be played
    public SongConfig song;

    // The beat GAP (area in which the player can press the sample)
    public float gapMilis = 0.5f;

    // A reference to the audio source component
    private AudioSource _audioSource;
    // The current beat index (in sound config)
    private int _currentBeatIndex = 0;
    // The inverse of the song frequency
    private float _songFrequencyInverse = 1f;
    // The beats in samples
    private List<int> _beatsSamples;
    private List<float> _beats;
    private float _milisPerSample;
    public void Awake()
    {
        // Initializing references...
        _audioSource = GetComponent<AudioSource>();
        Assert.IsNotNull(_audioSource, string.Format("Missing {0} on {1}", typeof(AudioSource).Name, typeof(AudioController).Name));
    }

    public void Play()
    {
        _audioSource.clip = song.clip;
        _beats = new List<float>();
        _currentBeatIndex = 0;
        _songFrequencyInverse = 1f / song.clip.frequency;

        // Converting string time to samples
        for (int i = 0; i < song.beats.Count; i++)
        {
            float beatMilis = SongConfig.StrTimeToMilis(song.beats[i]);
            _beats.Add(beatMilis);
        }
        _audioSource.Play();
    }


    //private int TimeToSample( float beatMilis )
    //{
    //    return (int) (beatMilis / _milisPerSample );

    //}

    /// <summary>
    /// Returns the percentage of "readiness" of the current beat
    /// </summary>
    /// <returns>A number between zero and one representing the load of the current beat.</returns>
    public float GetCurrentBeatPercentage()
    {

        if ( _currentBeatIndex >= _beats.Count)
            return 0f;


        // Last beat sample starts at the begining of the sound (sample 0) or
        // at the sample of the previous beat.
        // TODO: Add GAP?
        float lastBeatTime = 0f;
        if (_currentBeatIndex > 0)
            lastBeatTime = _beats[_currentBeatIndex - 1];

        // Current beat sample starts at the end of the sound (clip.samples)
        // or at the sample of the current beat
        // TODO: Add GAP?
        float currentBeatTime = song.clip.length;
        if ( _currentBeatIndex < _beats.Count )
            currentBeatTime = _beats[_currentBeatIndex] + 0.00001f;

        float currentSongTime = GetCurrentSongTimeMilis();

        // Returns the relative distance for the current beat.
        float totalDistance = Mathf.Abs( currentBeatTime - lastBeatTime);
        float currentDistance = Mathf.Abs( currentSongTime - lastBeatTime );
        return Mathf.Clamp( currentDistance / totalDistance, 0f, 1f );
    }

    /// <summary>
    /// Indicates whether or not the current beat is in the accept zone
    /// </summary>
    /// <returns>True if the current beat is in the accept zone, False othewise.</returns>
    public bool IsInAcceptZone()
    {

        if ( _currentBeatIndex >= _beats.Count )
            return false;

        float currentBeatSample = _beats[_currentBeatIndex];
        float songBeatSample = GetCurrentSongTimeMilis();
        return ( songBeatSample >= currentBeatSample - gapMilis && songBeatSample <= currentBeatSample + gapMilis );
    }


    private float GetCurrentSongTimeMilis()
    {
        return _audioSource.timeSamples * _songFrequencyInverse * 1000f;
    }

    public void Update()
    {
        //Debug.Log("Current Audio Sample: " + _audioSource.timeSamples);
        if ( _currentBeatIndex >= _beats.Count )
            return;

        float currentSongTimeMilis = GetCurrentSongTimeMilis();
        float currentBeatTimeMilis = _beats[_currentBeatIndex];
        float currentBeatWindowMaxMilis = currentBeatTimeMilis + gapMilis;

        //print("Current beat: " + currentBeatTimeMilis);
        //print("Current time: " + currentSongTimeMilis );

        if ( currentSongTimeMilis > currentBeatWindowMaxMilis )
        {
            _currentBeatIndex++;
            MessageKit.post(GameEvents.BEAT_ENDED);
        }
    }



}
