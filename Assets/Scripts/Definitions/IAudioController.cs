using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAudioController
{
    /// <summary>
    /// Returns the time percentage of current beat to be "ready to press".
    /// </summary>
    /// <returns>The time percentage of the current beat.</returns>
    float GetCurrentBeatPercentage();

    /// <summary>
    /// Returns whether or not the current beat is in the "accept zone".
    /// </summary>
    /// <returns>True if the current beat is in accept zone, False otherwise</returns>
    bool IsInAcceptZone();
}
