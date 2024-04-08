using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Chat_Manager : MonoBehaviour
{
    public GameObject ChatPanel;
    public GameObject chatPack1, chatPack2;
    public Sprite[] chatPack1_Icons;
    public Sprite[] chatPack2_Icons;
    public Image chatMsgA, chatMsgB;
    public void Btn_OpenChatPanel()
    {
        ChatPanel.SetActive(true);
        PauseCurrentGamePlay();
    }
    public void Btn_CloseChatPanel()
    {
        ChatPanel.SetActive(false);
        ResumeCurrentGamePlay();
    }
    public void Btn_OpenChatPack1()
    {
        chatPack1.SetActive(true);
        chatPack2.SetActive(false);
    }
    public void Btn_OpenChatPack2()
    {
        chatPack1.SetActive(false);
        chatPack2.SetActive(true);
    }
    public void Btn_ChatPack1Icon(int no)
    {
        ResumeCurrentGamePlay();
        ChatPanel.SetActive(false);
        chatMsgA.sprite = chatPack1_Icons[no];
        chatMsgA.DOFade(1, 1);
        chatMsgA.DOFade(0, 1).SetDelay(3);
        StartCoroutine(ChatMsgForUserB(Random.Range(0,2)));
    }
    public void Btn_ChatPack2Icon(int no)
    {
        ResumeCurrentGamePlay();
        ChatPanel.SetActive(false);
        chatMsgA.sprite = chatPack2_Icons[no];
        chatMsgA.DOFade(1, 1);
        chatMsgA.DOFade(0, 1).SetDelay(3);
        StartCoroutine(ChatMsgForUserB(Random.Range(0, 2)));
    }
    IEnumerator ChatMsgForUserB(int rndValue)
    {
        yield return new WaitForSeconds(Random.Range(1, 5));
        int canChatRep = Random.Range(1, 5);
        if (rndValue == 0 && canChatRep != 2)
        {
            chatMsgB.sprite = chatPack1_Icons[Random.Range(0, chatPack1_Icons.Length)];
            chatMsgB.DOFade(1, 1);
            chatMsgB.DOFade(0, 1).SetDelay(3);
        }
        else if (rndValue == 1 && canChatRep != 2)
        {            
            chatMsgB.sprite = chatPack2_Icons[Random.Range(0, chatPack2_Icons.Length)];
            chatMsgB.DOFade(1, 1);
            chatMsgB.DOFade(0, 1).SetDelay(3);
        }
    }
    public void PauseCurrentGamePlay()
    {        
        Time.timeScale = 0;        
    }
    public void ResumeCurrentGamePlay()
    {
        Time.timeScale = 1;
    }
}
