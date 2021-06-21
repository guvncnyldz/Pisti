using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioList audioList;
    private AudioSource audioSource;

    public static AudioManager instance;

    private void Awake()
    {
        audioSource = FindObjectOfType<AudioSource>();
        audioSource = gameObject.AddComponent< AudioSource>();
        instance = this;
    }

    public void Button()
    {
        if (IsSoundOn())
            audioSource.PlayOneShot(audioList.button,audioList.buttonV);
    }

    public void CardDeal()
    {
        if (IsSoundOn())
            audioSource.PlayOneShot(audioList.cardDeal, audioList.cardDealV);
    }

    public void CardPlace()
    {
        if (IsSoundOn())
            audioSource.PlayOneShot(audioList.cardPlace, audioList.cardPlaceV);
    }

    public void Woosh()
    {
        if (IsSoundOn())
            audioSource.PlayOneShot(audioList.woosh, audioList.wooshV);
    }

    public void Pisti()
    {
        if (IsSoundOn())
            audioSource.PlayOneShot(audioList.pisti, audioList.pistiV);
    }

    public void ShortApllause()
    {
        if (IsSoundOn())
            audioSource.PlayOneShot(audioList.shortApplause, audioList.shortApplauseV);
    }

    public void LoseRound()
    {
        if (IsSoundOn())
            audioSource.PlayOneShot(audioList.aww, audioList.awwV);
    }

    bool IsSoundOn()
    {
        if (PlayerPrefs.GetInt("sound", 1) == 1)
            return true;

        return false;
    }
}