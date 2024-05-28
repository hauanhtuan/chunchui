using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : Singleton<SoundController>
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip messengerFx;
    [SerializeField] private AudioClip hutFx;
    [SerializeField] private AudioClip bounceFx;
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
}
