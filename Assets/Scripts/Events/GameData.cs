using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static int PlayCounter
    {
        get => PlayerPrefs.GetInt("play_count", 0);
        set => PlayerPrefs.SetInt("play_count", value);
    }
}
