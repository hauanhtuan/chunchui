using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : Singleton<SoundController>
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioSource bgmsource;
    [SerializeField] private AudioClip messengerFx;
    [SerializeField] private AudioClip hutFx;
    [SerializeField] private AudioClip bounceFx;
    [SerializeField] private AudioClip selectedFx;
    [SerializeField] private AudioClip eatFx;
    [SerializeField] private AudioClip appearFx;
    [SerializeField] private AudioClip flipFx;
    [SerializeField] private AudioClip flopFx;
    [SerializeField] private AudioClip transfigFx;
    [SerializeField] private AudioClip helloNamFx;
    [SerializeField] private AudioClip helloNuFx;
    [SerializeField] private AudioClip bgmClip;
    [SerializeField] private AudioClip bgm2Clip;
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    public void PlayMessFx()
    {
        source.PlayOneShot(messengerFx);
    }
    public void PlayHutFx()
    {
        source.PlayOneShot(hutFx);
    }
    public void PlayBounceFx()
    {
        source.PlayOneShot(bounceFx);
    }
    public void PlaySelectedFx()
    {
        source.PlayOneShot(selectedFx);
    }
    public void PlayEatFx()
    {
        source.PlayOneShot(eatFx);
    }
    public void PlayAppearFx() {
        source.PlayOneShot(appearFx);
    }
    public void PlayFlipFlopFx(bool flip)
    {
        source.PlayOneShot(flip?flipFx:flopFx);

    }
    public void PlayTranfigFx()
    {
        source.PlayOneShot(transfigFx);
    }
    public void PlayHelloNamFx()
    {
        source.PlayOneShot(helloNamFx);
    }
    public void PlayHelloNuFx()
    {
        source.PlayOneShot(helloNuFx);
    }
    public void PlayBGM()
    {
        bgmsource.volume = .5f;
        bgmsource.clip = bgmClip;
        bgmsource.loop = true;
        bgmsource.Play();
    }
    public void PlayBGM2()
    {
        bgmsource.volume = 1f;
        bgmsource.clip = bgm2Clip;
        bgmsource.loop = true;
        bgmsource.Play();
    }
}
