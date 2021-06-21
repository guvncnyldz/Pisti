using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;

    private void Start()
    {
        UpdateImage();
    }

    public void SoundChange()
    {
        int soundState = PlayerPrefs.GetInt("sound",1);

        if (soundState == 0)
        {
            PlayerPrefs.SetInt("sound",1);
        }
        else
        {
            PlayerPrefs.SetInt("sound",0);
        }
        
        AudioManager.instance.Button();
        UpdateImage();
    }

    void UpdateImage()
    {
        int soundState = PlayerPrefs.GetInt("sound",1);

        transform.GetChild(0).GetComponent<Image>().sprite = sprites[soundState];
    }
}
