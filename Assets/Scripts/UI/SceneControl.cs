using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class SceneControl : MonoBehaviour
{
    public GameObject inputField;
    string newName;

    public TextMeshProUGUI rank;

    private void Start()
    {
        newName = PlayerPrefs.GetString("playerName", "You");

        inputField.GetComponent<TMP_InputField>().text = newName;

        rank.text = PlayerPrefs.GetInt("rank", 1).ToString();
    }

    public void Play()
    {
        PlayerPrefs.SetString("playerName", inputField.GetComponent<TMP_InputField>().text);

        SceneManager.LoadScene("LoadingScreen",LoadSceneMode.Single);

    }
}
