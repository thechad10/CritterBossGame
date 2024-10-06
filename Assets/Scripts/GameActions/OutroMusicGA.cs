using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class OutroMusicGA : GameAction
{
    [SerializeField] private AudioSource m_Source;
    [SerializeField] private AudioClip m_Clip;
    public override void Action()
    {
        m_Source.DOFade(0, .5f);
        StartCoroutine(SwapTrack());
    }
    IEnumerator SwapTrack()
    {
        yield return new WaitForSeconds(.5f);
        Debug.Log("Coroutine Called");
        m_Source.clip = m_Clip;
        m_Source.volume = 1;
        m_Source.loop = false;
        m_Source.Play();
    }
}
