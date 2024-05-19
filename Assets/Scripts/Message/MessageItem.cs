using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageItem : MonoBehaviour
{
    [SerializeField] private GameObject hook;
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void ShowHook(bool show)
    {
        hook.SetActive(show);
    }
    public void Hide()
    {
        anim.Play("Out");
    }
}
