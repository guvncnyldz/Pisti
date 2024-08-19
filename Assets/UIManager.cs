using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject deck;

    public GameObject cardPrefab;
    public GameObject botObject;

    public Transform centerPlace;
    public Transform backCard;
    public Transform myDeck;
    public Transform hand;
    public Player user, bot;

    public Sprite cardBackSprite;
    public Sprite cardFrontSprite;

    public Image background, wonGameImg, wonTourImg, loseGameImg, loseTourImg;
    public GameObject settingsPanel, pistiPanel, roundPanel, gamePanel, tourPanel, treeCardPanel, restartPanel;
    public TextMeshProUGUI roundTxt, loseTourTxt, loseGameTxt;

    Coroutine co;

    private void Start()
    {
        Instance = this;
        deck = GameObject.Find("[CARDBACK]");
        deck.SetActive(true);
        //Debug.LogError("UIMANAGER START");
    }

    private void Update()
    {
        Card test = new Card(2, CardType.Sinek);
        test.imageBack = cardBackSprite;
        test.imageFront = cardFrontSprite;
    }

    public void Pisti()
    {
        pistiPanel.transform.DOScale(Vector3.one, .2f);
        pistiPanel.transform.DOScale(Vector3.zero, .1f).SetDelay(.5f);
    }

    //    public IEnumerator AnimationTest()
    //    {
    //        Card test = new Card(2, CardType.Club);
    //        test.imageBack = cardBackSprite;
    //        test.imageFront = cardFrontSprite;
    //
    //        Animation1(test);
    //        yield return new WaitForSeconds(.4f);
    //        Animation2(user, new[] {test, test, test, test});
    //        yield return new WaitForSeconds(.6f);
    //        Animation3(bot);
    //        MyDeckRally();
    //    }

    //Başlangıçtaki 4 ortaya 4 ilk oyuncuya 4 diğer oyuncuya kart dağıtma aniamsyonu
    public void Animation1(Card selectedCard)
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject card = Instantiate(cardPrefab, backCard.transform.position, Quaternion.identity);
            card.transform.DOScale(new Vector3(.8f, .8f, .8f), 0);
            card.transform.parent = centerPlace.transform;
            card.transform.DORotate(new Vector3(0, 0, Random.Range(30, 180)), 0).SetDelay(.1f * i);
            card.GetComponent<RectTransform>().DOPivot(new Vector2(Random.Range(.4f, .6f), Random.Range(.4f, .6f)), 0)
                .SetDelay(.1f * i);
            int cardID = i;
            card.transform.DOMove(centerPlace.transform.position, .1f).SetDelay(.1f * i).OnComplete(() =>
            {
                if (cardID == 3)
                {
                    card.GetComponent<Card>().SetImage(selectedCard.imageFront, selectedCard.imageBack);
                    card.GetComponent<Card>().FlipCard(true);
                }
            });
        }
    }

    public void SetClickable(bool isClicakbe)
    {
        for (int i = 0; i < myDeck.childCount; i++)
        {
            Transform card = myDeck.transform.GetChild(i);
            card.GetComponent<Card>().IsPushable = isClicakbe;
        }
    }

    public void Animation2(Player player, List<Card> selectedCard)
    {

        for (int i = 0; i < 4; i++)
        {
            GameObject card = Instantiate(cardPrefab, backCard.transform.position, Quaternion.identity);
            card.transform.DOScale(new Vector3(.8f, .8f, .8f), 0);
            card.transform.parent = myDeck.transform;
            card.transform.DORotate(new Vector3(0, 0, Random.Range(30, 180)), 0).SetDelay(.1f * i);
            card.GetComponent<RectTransform>().DOPivot(new Vector2(Random.Range(.4f, .6f), Random.Range(.4f, .6f)), 0)
                .SetDelay(.1f * i);
            int cardID = i;
            card.GetComponent<Card>().SetImage(selectedCard[i].imageFront, selectedCard[i].imageBack);
            card.GetComponent<Card>().value = selectedCard[i].value;
            card.GetComponent<Card>().type = selectedCard[i].type;
            card.AddComponent<Button>().onClick.AddListener(() =>
            {
                UIManager.Instance.ThrowAtMyCard(card.GetComponent<Card>());
            });

            SetClickable(false);
            card.transform.DOMove(hand.transform.position, .2f).SetDelay(.1f * i);
            //card.transform.DORotate(new Vector3(myDeck.rotation.x, myDeck.rotation.y, myDeck.rotation.z), 0);
            //card.transform.DORotate(new Vector3(0, 0, 50f), 0);
        }
    }

    public void Animation3(Player player)
    {
        myDeck.transform.eulerAngles = new Vector3(0, 0, 0);
        for (int i = 0; i < 4; i++)
        {
            GameObject card = Instantiate(cardPrefab, backCard.transform.position, Quaternion.identity);
            card.transform.DOScale(new Vector3(.8f, .8f, .8f), 0);
            //card.transform.parent = toplanmaAlani.parent;
            card.transform.parent = botObject.transform;
            card.transform.DORotate(new Vector3(0, 0, Random.Range(30, 180)), 0).SetDelay(.1f * i);
            card.GetComponent<RectTransform>().DOPivot(new Vector2(Random.Range(.4f, .6f), Random.Range(.4f, .6f)), 0)
                .SetDelay(.1f * i);
            int cardID = i;
            card.transform.DOMove(player.cardPlace.transform.position, .2f).SetDelay(.1f * i);
        }
    }

    //Ortadaki kartları kazanan kullanıcıya verir
    public void Animation4(Player player)
    {
        List<Transform> cards = new List<Transform>();
        foreach (Transform variable in centerPlace.transform)
        {
            cards.Add(variable);
        }

        foreach (var VARIABLE in cards)
        {
            VARIABLE.DOMove(player.cardPlace.transform.position, .2f).OnComplete(() =>
            {
                if (cards.Count != 0)
                {
                    foreach (Transform card in cards)
                    {
                        Destroy(card.gameObject);
                    }
                }
            });
        }
    }


    public void ThrowAtMyCard(Card selectedCard)
    {
        FindObjectOfType<User>().EndCard(selectedCard);

        selectedCard.transform.parent = centerPlace;


        selectedCard.GetComponent<RectTransform>()
            .DOPivot(new Vector2(Random.Range(.4f, .6f), Random.Range(.4f, .6f)), 0);
        selectedCard.transform.DORotate(new Vector3(0, 0, Random.Range(30, 180)), 0);


        selectedCard.transform.DOMove(centerPlace.transform.position, .2F);
        selectedCard.transform.DOScale(new Vector3(1.4f, 1.4f, 1.4f), .1f);
        selectedCard.transform.DOScale(new Vector3(.8f, .8f, .8f), .1f).SetDelay(.1f);

        Destroy(selectedCard.GetComponent<Button>());
        MyDeckRally();
    }

    public void ThrowAtCard(Player player, Card selectedCard)
    {
        AudioManager.instance.CardPlace();

        GameObject card = Instantiate(cardPrefab, player.cardPlace.transform.position, Quaternion.identity);
        card.transform.parent = centerPlace;

        card.GetComponent<Card>().imageFront = selectedCard.imageFront;
        card.GetComponent<Card>().FlipCard(true);
        card.GetComponent<RectTransform>().DOPivot(new Vector2(Random.Range(.4f, .6f), Random.Range(.4f, .6f)), 0);
        card.transform.DORotate(new Vector3(0, 0, Random.Range(30, 180)), 0);


        card.transform.DOMove(centerPlace.transform.position, .2F);
        card.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), .1f);
        card.transform.DOScale(new Vector3(.8f, .8f, .8f), .1f).SetDelay(.1f);
    }

    public void MyDeckRally()
    {
        int counter = myDeck.transform.childCount / 2 * -1;
        foreach (Transform card in myDeck.transform)
        {
            card.transform.SetSiblingIndex(0);
            card.GetComponent<RectTransform>().DOPivot(new Vector2(.5f, .0f), 0);
            card.transform.DOScale(Vector3.one, .2f);
            card.transform.DORotate(new Vector3(0, 0, counter * 30), .2f);
            card.transform.DOMove(hand.transform.position, .2f);
            card.GetComponent<Card>().FlipCard(true);
            counter++;
        }
    }

    public void RotateHand()
    {
        myDeck.transform.DORotate(new Vector3(0, 0, myDeck.transform.rotation.z + 15f), 1);
    }

    public void OpenRestart()
    {
        //restartPanel.GetComponent<Image>().enabled = true;
        restartPanel.SetActive(true);
        /*
        background.DOColor(new Color(0, 0, 0, .7f), .2f);
        background.raycastTarget = true;
        restartPanel.transform.DOScale(Vector3.one, .2F).SetEase(Ease.Flash);
    */
    }
    public void OpenPanel()
    {
        background.DOColor(new Color(0, 0, 0, .7f), .2f);
        background.raycastTarget = true;
        settingsPanel.transform.DOScale(Vector3.one, .2F).SetEase(Ease.Flash);

    }

    public void ClosePanel()
    {
        background.DOColor(new Color(0, 0, 0, .0f), .2f);
        background.raycastTarget = false;
        settingsPanel.transform.DOScale(Vector3.zero, .2F).SetEase(Ease.Flash);
    }

    public void OpenThreeCardPanel()
    {
        background.DOColor(new Color(0, 0, 0, .7f), .2f);
        background.raycastTarget = true;
        treeCardPanel.transform.DOScale(Vector3.one, .2F).SetEase(Ease.Flash);
    }

    public void CloseThreeCardPanel()
    {
        background.DOColor(new Color(0, 0, 0, .0f), .2f);
        background.raycastTarget = false;
        treeCardPanel.transform.DOScale(Vector3.zero, .2F).SetEase(Ease.Flash);
    }

    public void Round()
    {
        background.DOColor(new Color(0, 0, 0, .7f), .2f);
        background.raycastTarget = true;
        roundPanel.transform.DOScale(Vector3.one, .2F).SetEase(Ease.Flash);

        co = StartCoroutine(WaitRoundPanel());
        //Debug.LogError("AFTER BEKLEME");
    }

    public void Tour()
    {
        //Debug.LogError("END TOUR");
        background.DOColor(new Color(0, 0, 0, .7f), .2f);
        background.raycastTarget = true;
        tourPanel.transform.DOScale(Vector3.one, .2F).SetEase(Ease.Flash);
        co = StartCoroutine(WaitTourPanel());
    }

    public void End()
    {
         background.DOColor(new Color(0, 0, 0, .7f), .2f);
        background.raycastTarget = true;
        tourPanel.transform.DOScale(Vector3.one, .2F).SetEase(Ease.Flash);
        co = StartCoroutine(WaitEndPanel());
    }

    public void Game()
    {
        background.DOColor(new Color(0, 0, 0, .7f), .2f);
        background.raycastTarget = true;
        gamePanel.transform.DOScale(Vector3.one, .2F).SetEase(Ease.Flash);
    }

    public void StartRound()
    {
        //Debug.LogError("START ROUND");
        background.DOColor(new Color(0, 0, 0, .0f), .2f);
        background.raycastTarget = false;

        roundPanel.transform.DOScale(Vector3.zero, .2F).SetEase(Ease.Flash);
        gamePanel.transform.DOScale(Vector3.zero, .2F).SetEase(Ease.Flash);
        tourPanel.transform.DOScale(Vector3.zero, .2F).SetEase(Ease.Flash);

        wonTourImg.enabled = false;
        wonGameImg.enabled = false;
        loseTourImg.enabled = false;
        loseGameImg.enabled = false;
    }

    public void StopWaiting()
    {
        StopCoroutine(co);
        Debug.LogError("STOPPPPPP");
    }

    IEnumerator WaitRoundPanel()
    {
        //Debug.LogError("Before BEKLEME");
        yield return new WaitForSeconds(1.5f);
        //Debug.LogError("İçerde BEKLEME");
        StartRound();
        ClassicGame.instance.StartGame();
    }

    IEnumerator WaitTourPanel()
    {
        //Debug.LogError("Before TOUR BEKLEME");
        yield return new WaitForSeconds(2);
        StartRound();
        ClassicGame.instance.EndTour();
        //Debug.LogError("İçerde TOUR BEKLEME");
    }

    IEnumerator WaitEndPanel()
    {
        //Debug.LogError("Before TOUR BEKLEME");
        yield return new WaitForSeconds(2);
        ClassicGame.instance.EndTour();
        //Debug.LogError("İçerde TOUR BEKLEME");
    }

    public IEnumerator WaitReplayButton()
    {
        yield return new WaitForSeconds(1);
        Application.LoadLevel((int)Scenes.Game);
    }
}

public enum Panel
{
    Settings,
    ScoreBoard
}