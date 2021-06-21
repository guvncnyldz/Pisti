using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScene : MonoBehaviour
{
    public void Play()
    {
        Application.LoadLevel((int) Scenes.Game);
    }
}