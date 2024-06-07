using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Material matGrey;
    [SerializeField] private Material matDefault;
    Tween twScale;
    private void Start()
    {
        matDefault = sr.material;
    }
    public void SetAnim(string name)
    {
        anim.Play(name);
    }
    
    private void OnMouseOver()
    {
        if (GameController.Instance.Phase != Phase.Choosing)
            return;
        if (twScale != null )
        {
            twScale.Kill();
        }
       twScale= transform.DOScale(1.2f, 0.3f);

    }
    private void OnMouseExit()
    {
        if (GameController.Instance.Phase != Phase.Choosing)
            return;
        if (twScale != null)
        {
            twScale.Kill();
        }
       twScale= transform.DOScale(1f, 0.3f);
    }
    private void OnMouseUp()
    {
        if (GameController.Instance.Phase != Phase.Choosing)
            return;
        GameController.Instance.SetCharacter(this);
        SoundController.Instance.PlaySelectedFx();
        PlaySelectAnim();
    }
    private void PlaySelectAnim()
    {
        if (twScale != null)
        {
            twScale.Kill();
        }
        transform.DOScale(1, .1f).onComplete = () =>
        {
            transform.DOScale(1.25f, .4f).onComplete = () =>
            {
                transform.DOScale(1, .2f);
            };
        };
    }
    public void Scale(float scale, float time, Action callback = null)
    {
        transform.DOScale(scale, time).onComplete = () =>
        {
            callback.Invoke();
        };
    }
    public void Jump()
    {
        SetAnim("jump");

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;
        switch(collision.tag)
        {
            case "Wavez":
                if (name == "Chui")
                    GameController.Instance.OnChuiTouchWavez();
                else
                    GameController.Instance.OnChunTouchWavez();
                break;
            case "Instax":
                GameController.Instance.NextItem();
        SoundController.Instance.PlayEatFx();
                break;
        }
    }
}
