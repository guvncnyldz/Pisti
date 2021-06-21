using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class MainMenuControl : MonoBehaviour
{

    public GameObject yardimPanel, sayfa1, sayfa2;
    public Image previousButton, nextButton;

    public void OpenHelpPanel()
    {
        yardimPanel.transform.DOScale(Vector3.one, .2F).SetEase(Ease.Flash);
    }

    public void CloseHelpPanel()
    {
        yardimPanel.transform.DOScale(Vector3.zero, .2F).SetEase(Ease.Flash);
    }

    public void NextPage()
    {
        nextButton.enabled = false;
        sayfa1.transform.DOScale(Vector3.zero, .2F).SetEase(Ease.Flash).OnComplete(() =>
        {
            sayfa2.transform.DOScale(Vector3.one, .2F).SetEase(Ease.Flash);
            previousButton.enabled = true;
        });

    }

    public void PreviousPage()
    {
        previousButton.enabled = false;
        sayfa2.transform.DOScale(Vector3.zero, .2F).SetEase(Ease.Flash).OnComplete(() =>
        {
            sayfa1.transform.DOScale(Vector3.one, .2F).SetEase(Ease.Flash);
            nextButton.enabled = true;
        });
    }
}
