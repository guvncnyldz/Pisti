using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class User : Player
{
    private PushHandler callBack;

    public async override void PushCard(Table table, PushHandler callBack, Player enemy)
    {
        await Task.Delay(500);
        this.callBack = callBack;
        UIManager.Instance.SetClickable(true);
    }

    public void EndCard(Card card)
    {
        AudioManager.instance.CardPlace();
        UIManager.Instance.SetClickable(false);
        GameAnimation.instance.PushCard(this, card);

        callBack(card);
        ClassicGame.instance.Move();
    }
}