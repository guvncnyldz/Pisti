using UnityEngine;

public class GameScene : MonoBehaviour
{
    public static GameScene instance;

    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }    
    }

    public void Restart()
    {
        if (GameAnimation.instance.cancellation == null)
        {
            Debug.LogError("LAAAAAAAN");
            Application.LoadLevel((int)Scenes.Game);
        }
        else
        {
            GameAnimation.instance.isRestart = true;
            UIManager.Instance.OpenRestart();
        }
        
       
        //Application.LoadLevel((int) Scenes.Game);
       
    }

    public void Exit()
    {
        Application.LoadLevel((int) Scenes.Menu);
    }

    public void SettingsPanel()
    {
        UIManager.Instance.OpenPanel();
    }

    public void ClosePanel()
    {
        UIManager.Instance.ClosePanel();
    }

    public void OpenRoundPanel()
    {
        UIManager.Instance.Round();
    }

    public void StartRound()
    {
        UIManager.Instance.StartRound();
    }


}