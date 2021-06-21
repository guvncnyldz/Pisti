using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cards", menuName = "ScriptableObjects/Cards")]

public class CardSprites : ScriptableObject
{
    public Sprite[] front;
    public Sprite back;
}