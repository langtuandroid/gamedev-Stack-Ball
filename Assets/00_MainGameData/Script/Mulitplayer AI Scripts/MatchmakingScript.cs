using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MatchmakingScript : MonoBehaviour
{
    public static MatchmakingScript instance;
    public GameObject findOpponentPanel;   
    public Image Game_Icon_image;
    public Text Game_Icon_Name;
    public GameObject opponentObject;
    public GameObject scrollView;
    public List<Sprite> profile_ImagesList = new List<Sprite>();
    public List<string> profile_NameList = new List<string>();
    public GameObject btn_Start;
    public int[] bidValueList;
    private int indexBid;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    public void FindMatchmakingPanelOpen()
    {
        Debug.Log("FindMatchmakingPanelOpen");
        findOpponentPanel.SetActive(true);
        ResetMatchMakingPageValues();
        // btn_Start.SetActive(false);
        // UnityEngine.UI.Extensions.UI_InfiniteScroll.instance.Init();         
    }
    public void ResetMatchMakingPageValues()
    {
        Global_MainGame.user_nameB = "";
        UIManager_Multiplayer.instance.SetNameOfUserB();
        UIManager_Multiplayer.instance.SetCoinsBalance();
        UIManager_Multiplayer.instance.ProfileImgB = null;
        UIManager_Multiplayer.instance.SetProfileImageOfUserB();
        Global_MainGame.currentBidCoin = 100;
        UIManager_Multiplayer.instance.SetBidValueOfCoinUserA();
        UIManager_Multiplayer.instance.SetBidValueOfCoinUserB();
        btn_Start.SetActive(true);
        indexBid = 0;
    }
    IEnumerator ChangeRandomProfileIcon()
    {       
        int randomTotal = Random.Range(20, profile_ImagesList.Count);
        for (int i = 0; i < randomTotal; i++)
        {
            yield return new WaitForSeconds(0.07f);
            UIManager_Multiplayer.instance.ProfileImgB = profile_ImagesList[Random.Range(0, profile_ImagesList.Count)];
            UIManager_Multiplayer.instance.SetProfileImageOfUserB();
        }       
        StopMatchmaking();
    }
    public void StopMatchmaking()
    {
        int randomProImg = Random.Range(0, profile_ImagesList.Count);
        UIManager_Multiplayer.instance.ProfileImgB = profile_ImagesList[randomProImg];
        UIManager_Multiplayer.instance.SetProfileImageOfUserB();

        Global_MainGame.user_nameB = profile_NameList[randomProImg];     
        UIManager_Multiplayer.instance.SetNameOfUserB();       
        opponentObject.SetActive(true);
        scrollView.SetActive(false);
        Invoke("StartGameScene", 0.5f);
    }
    public void Back_FromFindMatchMakingPanel()
    {
       
        findOpponentPanel.SetActive(false);        
        SceneManager.LoadScene("MainScene");
    }
    public void Btn_StartGamePlayScreen()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            CheckInternet._instance.InternetPopUpActive(true);
            return;
        }
        btn_Start.SetActive(false);
        StartCoroutine("ChangeRandomProfileIcon");      
    }
    public void StartGameScene()
    {
        PlayerPrefs.SetInt("gamePlayedCount", PlayerPrefs.GetInt("gamePlayedCount")+1);
        findOpponentPanel.SetActive(false);
        DontDestroyObject.instance.SetValueOnProfileAndHeader();
        DontDestroyObject.instance.ProfileHeader_Active(true);
        DontDestroyObject.instance.Header_User.SetActive(true);
        TimerOfGame_Multiplayer.instance.StartTimer(90);
        PlayerPrefs.SetInt("Coin_games", PlayerPrefs.GetInt("Coin_games") - Global_MainGame.currentBidCoin);
        UIManager_Multiplayer.instance.SetCoinsBalance();
        SceneManager.LoadScene("multi_tower_attack_3d");
    }
    public void RetryGameWhenOpponentAgree()
    {
        MultiplayerManagement_AI.instance.ResetGlobalsVariable_WhenTimeOver();
        DontDestroyObject.instance.SetValueOnProfileAndHeader();
        TimerOfGame_Multiplayer.instance.StartTimer(90);       
        SceneManager.LoadSceneAsync("multi_tower_attack_3d");
     
    }

    public void Btn_IncreaseCoinBidValue()
    {
        indexBid++;
        if (indexBid >= bidValueList.Length)
        {
            indexBid = 0;
        }
        
        if (PlayerPrefs.GetInt("Coin_games") >= bidValueList[indexBid])
        {           
            Global_MainGame.currentBidCoin = bidValueList[indexBid];
            UIManager_Multiplayer.instance.SetBidValueOfCoinUserA();
            UIManager_Multiplayer.instance.SetBidValueOfCoinUserB();
            Debug.Log("Global_MainGame.currentBidCoin "+ Global_MainGame.currentBidCoin);
        }
        else
        {            
            UIManager_Multiplayer.instance.MessageLogAlertPopUP("Alert!","Insufficient balance for bet up.");           
        }
    }
}
