using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class LoadingScene : MonoBehaviour
{
    public string[] loadingTexts, botNames;
    public string botName;
    int index = 0;

    public TextMeshProUGUI loadingText;
    // Start is called before the first frame update
    void Start()
    {
        RandomName();

        LoadText();
    }

    void RandomName()
    {
        int random = UnityEngine.Random.Range(0, botNames.Length);

        botName = botNames[random];

        PlayerPrefs.SetString("name", botName);
    }

    void LoadText()
    {
        index = 0;

        loadingText.text = loadingTexts[index];
        float randomTime = UnityEngine.Random.Range(1, 2f);
        StartCoroutine(LoadingTextChange(randomTime));
    }

    IEnumerator LoadingTextChange(float randomTime)
    {
        float parse = randomTime * 5;
        for (int i = 0; i < parse; i++)
        {
            yield return new WaitForSeconds(0.2f);

            if (index == 2)
            {
                loadingText.text = botName + " " + loadingTexts[index];
            }
            else
            {
                loadingText.text += ".";

                if (i % 4 == 0)
                {
                    loadingText.text = loadingTexts[index];
                }
            }
        }

        index++;

        if (index == 3)
        {
            loadingText.text = loadingTexts[index];
        }

        if (index > 3)
        {
            // YENİ SAHNE
            SceneManager.LoadScene("Game", LoadSceneMode.Single);
        }
        else
        {
            float randomTime2 = UnityEngine.Random.Range(1, 2f);
            StartCoroutine(LoadingTextChange(randomTime2));
        }

    }
}
