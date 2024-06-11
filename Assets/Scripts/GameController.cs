using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField] private GameObject smokePrefab;
    [SerializeField] private GameObject milestoneZone;
    [SerializeField] private GameObject milestoneDate;
    [SerializeField] private RectTransform milestoneDate2;
    [SerializeField] private TextMeshProUGUI txtMilestoneDate;
    [SerializeField] private TextMeshProUGUI txtMilestoneDate2;
    [SerializeField] private Transform chunMilestone;
    [SerializeField] private MovingObject[] chunMoving;
    [SerializeField] private Transform chuiMilestone;
    [SerializeField] private MovingObject[] chuiMoving;
    [SerializeField] private SpriteRenderer[] chunMsSprites;
    [SerializeField] private SpriteRenderer[] chuiMsSprites;
    [SerializeField] private GameObject chunWavez;
    [SerializeField] private GameObject chuiWavez;
    [Header("Our Milestone")]
    [SerializeField] private Transform ourBg;
    [SerializeField] private Transform ourMilestone;
    [SerializeField] private MovingObject[] ourMoving;
    [SerializeField] private MovingObject instax;
    [Header("Tutorial")]
    [SerializeField] private GameObject hand;
    [SerializeField] private MessageItem tutTxt;
    [Header("Items")]
    [SerializeField] private Transform polaroid;
    [SerializeField] private Image polaroidImg;
    [SerializeField] private MovingObject[] items;
    [SerializeField] private MessageItem[] descriptions;
    [SerializeField] private string[] daysText;
    [SerializeField] private Sprite[] imgs;
    [Header("Last event")]
    [SerializeField] private MessageItem msg1;
    [SerializeField] private MessageItem msg2;
    [SerializeField] private RectTransform thiep;
    [Header("Characters")]
    [SerializeField] private Character chun;
    [SerializeField] private Character chui;
    private bool showingMessages = false;
    private int curMessageIndex = 0;
    private bool canJump = true;
    private int countItem = 0;

    [HideInInspector] public Phase Phase => phase;
    [HideInInspector] public Character selectedChar;

    public override void Awake()
    {
        base.Awake();
        btnNotification.onClick.AddListener(OnNotification);
        btnNotification.gameObject.SetActive(false);
        messageZone.SetActive(true);
        chooseZone.SetActive(false);
        milestoneZone.SetActive(false);
        milestoneLine.gameObject.SetActive(false);
        chunMsSprites = chunMilestone.GetComponentsInChildren<SpriteRenderer>();
        chuiMsSprites = chuiMilestone.GetComponentsInChildren<SpriteRenderer>();
        chunMoving = chunMilestone.GetComponentsInChildren<MovingObject>();
        chuiMoving = chuiMilestone.GetComponentsInChildren<MovingObject>();
        chunMilestone.gameObject.SetActive(false);
        chuiMilestone.gameObject.SetActive(false);
        chunWavez.SetActive(false);
        chuiWavez.SetActive(false);
        ourMoving = ourMilestone.GetComponentsInChildren<MovingObject>();
        ourMilestone.gameObject.SetActive(false);
        hand.SetActive(false);
        tutTxt.gameObject.SetActive(false);
        foreach (var d in descriptions)
            d.gameObject.SetActive(false);
        polaroid.gameObject.SetActive(false);
        thiep.gameObject.SetActive(false);

    }
    private IEnumerator Start()
    {
        phase = Phase.Messaging;
        yield return new WaitForSeconds(.5f);
        btnNotification.gameObject.SetActive(true);
        SoundController.Instance.PlayMessFx();
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
        if (Input.GetKeyDown(KeyCode.A))
            StartOurMilestone();
        if (Input.GetKeyDown(KeyCode.R))
                Debug.Log(thiep.anchoredPosition + " " + thiep.anchorMin + " " + thiep.anchorMax);
        if (Input.GetKeyDown(KeyCode.S))
            SceneManager.LoadScene("Gameplay");
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
        else if (phase == Phase.Playing && Input.GetMouseButtonDown(0) && canJump)
        {
            canJump = false;
            Jump();
        }
    }
    private void Jump()
    {
        IEnumerator IJump()
        {
            chun.Jump();
            SoundController.Instance.PlayBounceFx();
            yield return new WaitForSeconds(.1f);
            chui.Jump();
            yield return new WaitForSeconds(1);
            canJump = true;
        }
        StartCoroutine(IJump());
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
            yield return new WaitForSeconds(1);
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
            SetColor(chunMsSprites, new Color(1, 1, 1, 0), 0);
            SetColor(chuiMsSprites, new Color(1, 1, 1, 0), 0);
            yield return new WaitForSeconds(1);
            if (selectedChar == chun)
            {
                milestoneLine.DOMoveY(-3.6f, 2);
                milestoneDate2.anchoredPosition = new Vector3(20, -1345);
                chui.transform.DOMoveY(-8f, 2);
                chui.transform.DOScale(.375f, 2);
                chun.transform.DOMoveY(0, 2);
                chunMilestone.transform.position = new Vector3(-5.5f, -1.9f, -.25f + 2f);
                chunMilestone.transform.localScale = Vector3.one;
                chuiMilestone.transform.position = new Vector3(-5.5f, -10.75f, -.26f + 2f);
                chuiMilestone.transform.localScale = Vector3.one * .55f;
            }
            else
            {
                milestoneLine.DOMoveY(3.6f, 2);
                milestoneDate2.anchoredPosition = new Vector3(20, -625);
                chun.transform.DOMoveY(6f, 2);
                chun.transform.DOScale(.375f, 2);
                chui.transform.DOMoveY(-4.5f, 2);
                chunMilestone.transform.position = new Vector3(-5.5f, 5.3f, -.25f + 2f);
                chunMilestone.transform.localScale = Vector3.one * .375f;
                chuiMilestone.transform.position = new Vector3(-5.5f, -9.3f, -.26f + 2f);
                chuiMilestone.transform.localScale = Vector3.one;
            }
            yield return new WaitForSeconds(2.2f);
            var smoke1 = Instantiate(smokePrefab);
            var smoke2 = Instantiate(smokePrefab);
            var chunPos = chun.transform.position;
            chunPos.y -= .5f;
            chunPos.z = -3;
            var chuiPos = chui.transform.position;
            chuiPos.y -= .5f;
            chuiPos.z = -3;
            smoke1.transform.position = chunPos;
            smoke2.transform.position = chuiPos;
            smoke1.transform.localScale = Vector3.one * (selectedChar == chun ? 2.5f : 1.5f);
            smoke2.transform.localScale = Vector3.one * (selectedChar == chun ? 1.5f : 2.5f);
            yield return new WaitForSeconds(2.583f / 3f);
            smoke1.SetActive(false);
            smoke2.SetActive(false);
            chun.gameObject.SetActive(false);
            chui.gameObject.SetActive(false);
            chunMilestone.gameObject.SetActive(true);
            SetColor(chunMsSprites, new Color(1, 1, 1, 1), 1);
            chuiMilestone.gameObject.SetActive(true);
            SetColor(chuiMsSprites, new Color(1, 1, 1, 1), 1);
            yield return new WaitForSeconds(1);

            milestoneZone.SetActive(true);
            milestoneDate.SetActive(true);
            milestoneDate2.gameObject.SetActive(false);
            txtMilestoneDate.text = "1995";
            chunPos = chun.transform.position;
            chunPos.x = -3.5f;
            chun.transform.position = chunPos;
            chunPos.y -= .5f;
            chunPos.z = -3;
            yield return new WaitForSeconds(1.5f);
            chun.gameObject.SetActive(true);
            smoke1.transform.position = chunPos;
            smoke1.SetActive(true);
            yield return new WaitForSeconds(2.583f / 3f);
            smoke1.SetActive(false);
            foreach (var m in chunMoving)
                m.moving = true;
            foreach (var m in chuiMoving)
                m.moving = true;
            var pos = chui.transform.position;
            pos.x = 7.5f;
            //   chui.transform.position= pos;
            //   chui.transform.DOMoveX(-3.5f, 11/2f).SetEase(Ease.Linear);
            yield return new WaitForSeconds(3);
            milestoneDate2.gameObject.SetActive(true);
            txtMilestoneDate.text = "1996";
            txtMilestoneDate2.text = "1996";
            yield return new WaitForSeconds(1.5f);
            smoke2.gameObject.SetActive(true);
            chui.gameObject.SetActive(true);
            chuiPos = chui.transform.position;
            chuiPos.x = -3.5f;
            chuiPos.y = selectedChar == chui ? -5.5f : -8.65f;
            chui.transform.position = chuiPos;
            chuiPos.y -= .5f;
            chuiPos.z = -3;
            smoke2.transform.position = chuiPos;
            smoke2.SetActive(true);
            yield return new WaitForSeconds(2.583f / 3f);
            smoke2.SetActive(false);

            var chuiWPos = chuiWavez.transform.localPosition;
            chuiWPos.x = selectedChar == chui ? 12f : 21f;
            chuiWPos.y = 4.5f;
            chuiWavez.gameObject.SetActive(true);
            chuiWavez.transform.localPosition = chuiWPos;
            chuiWavez.transform.SetParent(chuiMoving[1].transform);

        }
        StartCoroutine(IStartMilestoneRunning());
    }
    public void OnChuiTouchWavez()
    {
        txtMilestoneDate.text = "2/2022";
        txtMilestoneDate2.text = "2/2022";
        OnChunWavezSetup();
        chuiWavez.SetActive(false);
        SoundController.Instance.PlayEatFx();
    }
    public void OnChunTouchWavez()
    {
        Debug.Log("OnChunTouchWavez");
        txtMilestoneDate.text = "9/2022";
        txtMilestoneDate2.text = "9/2022";
        chunWavez.SetActive(false);
        SoundController.Instance.PlayEatFx();
        StartOurMilestone();
    }
    private void OnChunWavezSetup()
    {
        var chunWPos = chunWavez.transform.localPosition;
        chunWPos.x = selectedChar == chun ? 12f : 19f;
        chunWPos.y = 2.5f;
        chunWavez.gameObject.SetActive(true);
        chunWavez.transform.localPosition = chunWPos;
        chunWavez.transform.SetParent(chunMoving[1].transform);
    }
    private void StartOurMilestone()
    {
        IEnumerator IStart()
        {
            Time.timeScale = 1.7f;
            yield return new WaitForSeconds(1);
            milestoneDate.GetComponent<MessageItem>().Hide();
            milestoneDate2.GetComponent<MessageItem>().Hide();
            ourBg.DOMoveY(0, 3);
            yield return new WaitForSeconds(4);
            ourBg.DOMoveY(-19.2f, 3);
            chuiMilestone.gameObject.SetActive(false);
            chunMilestone.gameObject.SetActive(false);
            ourMilestone.gameObject.SetActive(true);
            milestoneLine.gameObject.SetActive(false);
            chui.transform.localScale = Vector3.one * .7f;
            chun.transform.localScale = Vector3.one * .7f;
            Vector3 pos = new Vector3(-4.5f, -4.5f, -1f);
            chui.transform.position = pos;
            pos.x += 1.5f;
            chun.transform.position = pos;
            foreach (var m in ourMoving)
                m.moving = true;
            instax.moving = true;
            Debug.Log(Time.time);
            yield return new WaitForSeconds(14f);
            foreach (var m in ourMoving)
                m.moving = false;
            instax.moving = false;
            hand.SetActive(true);
            tutTxt.gameObject.SetActive(true);
            phase = Phase.Playing;
            yield return new WaitUntil(() => !canJump);
            foreach (var m in ourMoving)
                m.moving = true;
            instax.moving = true;
            hand.SetActive(false);
            tutTxt.gameObject.SetActive(false);

        }
        StartCoroutine(IStart());
    }
    public void NextItem()
    {
        IEnumerator INextItem()
        {
            if (countItem == 0)
            {
                tutTxt.Hide();
                milestoneZone.SetActive(true);
                milestoneDate2.gameObject.SetActive(false);
                milestoneDate.SetActive(false);
                yield return null;
                milestoneDate.SetActive(true);
            }

            txtMilestoneDate.text = daysText[countItem];
            polaroid.gameObject.SetActive(true);
            polaroid.localScale = new Vector3(0, .5f, 1);
            Destroy(items[countItem].gameObject);
            var itemshow = descriptions[countItem];
            itemshow.gameObject.SetActive(true);
            
                yield return new WaitForSeconds(4);
                var iteminside = itemshow.transform.GetChild(itemshow.transform.childCount - 1);
                iteminside.DOScaleX(0, .3f);
                yield return new WaitForSeconds(.3f);
                polaroidImg.sprite = imgs[countItem];
                polaroid.DOScaleX(.5f, .3f);
                yield return new WaitForSeconds(4);
                polaroid.DOScaleX(0, .3f);
            //yield return new WaitForSeconds(5);
            descriptions[countItem].Hide();
            countItem++;
            if (countItem >= descriptions.Length)
            {
                Debug.Log("Done");
                yield return new WaitForSeconds(1);

                Marrige();
            }
            else
                items[countItem].moving = true;
        }
        StartCoroutine(INextItem());
    }
    private void Marrige()
    {
        IEnumerator IMarrige()
        {
            phase = Phase.Marring;
            foreach (var o in ourMoving)
                o.moving = false;
            chui.transform.DOMoveX(1, 4.5f).SetEase(Ease.Linear);
            chun.transform.DOMoveX(-1, 2.5f).SetEase(Ease.Linear);
            yield return new WaitForSeconds(4.5f);
            chun.Stand();
            chui.Stand();
            yield return new WaitForSeconds(1f);
            msg1.gameObject.SetActive(true);
            yield return new WaitForSeconds(7);
            msg1.Hide();
            yield return new WaitForSeconds(1);
            chun.ShowAccessory();
            chui.ShowAccessory();
            yield return new WaitForSeconds(7);
            msg2.gameObject.SetActive(true);
            yield return new WaitForSeconds(7);
            msg2.Hide();
            thiep.anchoredPosition = new Vector2(0, -1920);
            thiep.gameObject.SetActive(true);
            yield return new WaitForSeconds(1);

            thiep.DOAnchorPos(Vector2.zero,3);
        }
        StartCoroutine(IMarrige());
    }
    private void SetColor(SpriteRenderer[] sprites, Color color, float duration, Action callback = null)
    {
        foreach (var sprite in sprites)
        {
            sprite.DOColor(color, duration);
        }
        if (callback != null)
            DOVirtual.DelayedCall(duration, () =>
            {
                callback.Invoke();
            });
    }
}
public enum Phase
{
    Messaging,
    Running,
    Choosing,
    Zooming,
    Playing,
    Marring
}