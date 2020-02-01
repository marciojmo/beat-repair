using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "song-config", menuName = "Song Config")]
public class SongConfig : ScriptableObject
{
    public AudioClip clip;
    public float bpm;
    public List<string> beats;


    public static float StrTimeToMilis( string time )
    {
        string[] timeSplit = time.Split(':');
        // not checking here.. is jam
        return int.Parse( timeSplit[0] ) * 60000f + int.Parse( timeSplit[1] ) * 1000f + int.Parse( timeSplit[2] );
    }
}
