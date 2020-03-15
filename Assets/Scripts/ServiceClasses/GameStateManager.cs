using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStateManager
{
    public static BoolEvent onGameStateChanged = new BoolEvent();

    private static float pausedTime = 0;
    public static float LastPauseTime = 0;
    public static float TimeSinceLastPause => Time.time - LastPauseTime;

    public static void SwitchGameState(bool isPlaying)
    {
        if (!isPlaying) pausedTime = Time.time;
        onGameStateChanged?.Invoke(isPlaying);
        Debug.Log("is playing = " + isPlaying);
    }
}