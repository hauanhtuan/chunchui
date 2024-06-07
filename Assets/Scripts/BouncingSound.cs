using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingSound : MonoBehaviour
{
  public void PlayBound()
    {
        SoundController.Instance.PlayBounceFx();
    }
    public void End()
    {
        Debug.Log("End anim");
    }
}
