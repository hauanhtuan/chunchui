using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : Singleton<SoundController>
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip messengerFx;
    [SerializeField] private AudioClip hutFx;
    [SerializeField] private AudioClip bounceFx;
    [SerializeField] private AudioClip selectedFx;
    [SerializeField] private AudioClip eatFx;
    [SerializeField] private AudioClip bgmClip;
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
    public void PlayBGM()
    {
        source.clip = bgmClip;
        source.loop = true;
        source.Play();
    }
}
