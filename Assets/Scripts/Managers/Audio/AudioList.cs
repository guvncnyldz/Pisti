using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio", menuName = "ScriptableObjects/Audio")]
public class AudioList : ScriptableObject
{
    public AudioClip button;
    [Range(0, 1)] public float buttonV;
    public AudioClip cardDeal;
    [Range(0, 1)] public float cardDealV;
    public AudioClip cardPlace;
    [Range(0, 1)] public float cardPlaceV;
    public AudioClip pisti;
    [Range(0, 1)] public float pistiV;
    public AudioClip shortApplause;
    [Range(0, 1)] public float shortApplauseV;
    public AudioClip aww;
    [Range(0, 1)] public float awwV;
    public AudioClip woosh;
    [Range(0, 1)] public float wooshV;

}