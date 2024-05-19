using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : Singleton<GameController>
{
    [SerializeField] private Phase phase;
    [Header("Message Zone")]
    [SerializeField] private GameObject messageZone;
    [SerializeField] private TextMeshProUGUI txtCurrentTime;
    [SerializeField] private Button btnNotification;
    [SerializeField] private List<MessageItem> messages;
    [Space(20)]
    [Header("Choose Zone")]
    [SerializeField] private GameObject chooseZone;
    [SerializeField] private GameObject line;
    [SerializeField] private GameObject chooseMsg;
    [Space(20)]
    [Header("Milestone Zone")]
    [SerializeField] private Transform milestoneLine;
    [Header("Characters")]
    [SerializeField] private Character chun;
    [SerializeField] private Character chui;
    private bool showingMessages = false;
    private int curMessageIndex = 0;

    [HideInInspector] public Phase Phase => phase;
    [HideInInspector] public Character selectedChar;

    public override void Awake()
    {
        base.Awake();
        btnNotification.onClick.AddListener(OnNotification);
        btnNotification.gameObject.SetActive(false);
        messageZone.SetActive(true);
        chooseZone.SetActive(false);
        milestoneLine.gameObject.SetActive(false) ;
    }
    private IEnumerator Start()
    {
        phase = Phase.Messaging;
        yield return new WaitForSeconds(.5f);
        btnNotification.gameObject.SetActive(true);
    }
    private void OnNotification()
    {
        txtCurrentTime.gameObject.SetActive(false);
        StartShowMessages();
    }
    private void StartShowMessages()
    {
        showingMessages = true;
        messages[curMessageIndex].gameObject.SetActive(true);
    }
    private void Update()
    {

        txtCurrentTime.text = string.Format("{0:00}:{1:00}", DateTime.Now.Hour, DateTime.Now.Minute);
        if (showingMessages)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (curMessageIndex == messages.Count - 1)
                {
                    DoneShowMessages();
                    return;
                }
                messages[curMessageIndex++].ShowHook(false);
                messages[curMessageIndex].gameObject.SetActive(true);
            }
        }
    }
    private void DoneShowMessages()
    {
        Debug.Log("Done show messages");
        showingMessages = false;
        foreach (var item in messages)
        {
            item.Hide();
        }
        DOVirtual.DelayedCall(.25f, () =>
        {
            messageZone.SetActive(false);
            ShowChooseZone();
        });
    }
    private void ShowChooseZone()
    {
        IEnumerator IShowChooseZone()
        {
            phase = Phase.Running;
            Time.timeScale = 1.7f;
            chooseMsg.SetActive(false);
            chooseZone.SetActive(true);
            line.transform.position = Vector2.left * 2000;
            line.transform.DOLocalMoveX(0, 1f);
            chun.transform.position = new Vector3(-8, 3);
            yield return new WaitForSeconds(1);
            chun.SetAnim("move");
            chun.transform.DOMoveX(0, 3).SetEase(Ease.Linear);
            yield return new WaitForSeconds(3);
            chun.SetAnim("idle");
            chui.transform.position = new Vector3(8, -6.5f);
            chui.SetAnim("move");
            chui.transform.DOMoveX(0, 3).SetEase(Ease.Linear);
            yield return new WaitForSeconds(3);
            chui.SetAnim("idle");
            yield return new WaitForSeconds(1);
            chooseMsg.SetActive(true);
            yield return new WaitForSeconds(1);
            chun.transform.DOScale(1.2f, 0.3f).onComplete = () =>
            {
                chun.transform.DOScale(1f, 0.3f);
            };
            yield return new WaitForSeconds(.6f);
            chui.transform.DOScale(1.2f, 0.3f).onComplete = () =>
            {
                chui.transform.DOScale(1, 0.3f);
            };
            phase = Phase.Choosing;

        }
        StartCoroutine(IShowChooseZone());
    }
    public void SetCharacter(Character character)
    {
        selectedChar = character;
        phase = Phase.Zooming;
        StartMilestoneRunning();
    }
    private void StartMilestoneRunning()
    {
        chooseZone.SetActive(false);
        milestoneLine.gameObject.SetActive(true);
        IEnumerator IStartMilestoneRunning()
        {
            yield return new WaitForSeconds(1);
            if (selectedChar == chun)
            {
                milestoneLine.DOMoveY(-3.6f, 2);
                chui.transform.DOMoveY(-8f, 2);
                chui.transform.DOScale(.5f, 2);
                chun.transform.DOMoveY(0, 2);
            }
            else
            {
                milestoneLine.DOMoveY(3.6f, 2);
                chun.transform.DOMoveY(6f, 2);
                chun.transform.DOScale(.5f, 2);
                chui.transform.DOMoveY(-4.5f, 2);
            }
        }
        StartCoroutine(IStartMilestoneRunning());
    }
}
public enum Phase
{
    Messaging,
    Running,
    Choosing,
    Zooming,
    Playing
}