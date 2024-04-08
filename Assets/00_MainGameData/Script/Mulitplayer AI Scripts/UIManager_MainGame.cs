using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager_MainGame : MonoBehaviour
{
    public static UIManager_MainGame instance;
    public GameObject cancleBtnGo_loadingScreen;
    public Text dailyRewardText, txtCoinsBalanceHome;
    public Button dailyRewardBtn;
    public GameObject timer_Img;   
    public Text WalletBalance_Total_text, WalletBalance_Total_text2, WalletBalance_Total_text3;
    public GameObject panel_Profile;
    public GameObject GlowParticle;
    public GameObject AppVersionPanel;
    public GameObject skipAppVersionBtn;
    public static bool _isSkipedAppVersionPanel;
    public static int _isMainSceneLoadFirstTime;
    public GameObject winCash_regular_panel;
    public GameObject FlappyBirdGame_panel;
    public GameObject walletBalance_panel;
    public GameObject[] RsObjects;
    public Text[] playerCountText;

    public GameObject CashWinnerListPanel;
    public GameObject WinnerContentPanel;
    public GameObject CashWinner_ListItem;
    public Text totalAmountGiven;
    

    private void Awake()
    {
        instance = this;
        Physics.gravity = new Vector3(0, -9.81f, 0);
        
    }   
    void Start()
    {
        Physics2D.queriesStartInColliders = true;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        UIManager_Multiplayer.instance.SetCoinsBalance();
    }
    public void Btn_Update_AppVersion()
    {
        _isSkipedAppVersionPanel = true;
        Application.OpenURL("https://play.google.com/store/apps/details?id=" + Application.identifier);
    }
    public void Btn_Skip_AppVersion()
    {
        _isSkipedAppVersionPanel = true;
        AppVersionPanel.SetActive(false);
        GlowParticle.SetActive(true);
    }
    public void Btn_OpenProfilePanel()
    {       
        panel_Profile.SetActive(true);
        Global_MainGame._isFromProfilePanel = true;
    }
    public void Btn_CloseProfilePanel()
    {       
        panel_Profile.SetActive(false);
        Global_MainGame._isFromProfilePanel = false;
        GlowParticle.SetActive(true);
    }
    public void Btn_Cancle_Downloading()
    {
        //FB_AdManager.instance.DestroyBannerAds();
        AssetBundleDownload.instance.StopDownloadingAssets();
    }
    public void DailyRewardBtn()
    {
        DailyReward_maingame.Instance.ResetNextRewardTime();
      
    }   
    public void Update()
    {
        txtCoinsBalanceHome.text = PlayerPrefs.GetInt("Coin_games").ToString();
        WalletBalance_Total_text.text = PlayerPrefs.GetFloat("WalletBalanceTotalGC").ToString();
        WalletBalance_Total_text2.text = PlayerPrefs.GetFloat("WalletBalanceTotalGC").ToString();
        WalletBalance_Total_text3.text = PlayerPrefs.GetFloat("WalletBalanceTotalGC").ToString();

        if (DailyReward_maingame.Instance.CanRewardNow())
        {
            dailyRewardText.text = "Ready..";
            dailyRewardBtn.interactable = true;
            timer_Img.SetActive(false);
        }
        else
        {
            string hours = DailyReward_maingame.Instance.TimeUntilNextReward.Hours.ToString();
            string minutes = DailyReward_maingame.Instance.TimeUntilNextReward.Minutes.ToString();
            string seconds = DailyReward_maingame.Instance.TimeUntilNextReward.Seconds.ToString();
            dailyRewardText.text = hours + ":" + minutes + ":" + seconds;
            dailyRewardBtn.interactable = false;
            timer_Img.SetActive(true);
        }
    }

    public void Btn_Regular()
    {
        AssetBundleDownload.instance.DownloadGameFromServer(Global_MainGame.gameName, Global_MainGame.assetVersion);
    }
    public void Btn_Multiplayer()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            CheckInternet._instance.internetPopup.SetActive(true);
        }
        else
        {
            if (PlayerPrefs.GetInt("Coin_games") >= 100)
            {
                AssetBundleDownload.instance.DownloadGameFromServer("multi_"+Global_MainGame.gameName, Global_MainGame.assetVersion);
            }
            else
            {
                UIManager_Multiplayer.instance.MessageLogAlertPopUP("Alert!", "Insufficient balance for this bet. Required minimum 100 Coins for multiplayer game.");
                UIManager_Multiplayer.instance.getExtraCoinbtn.SetActive(true);
            }
        }
    }
    
    public void Btn_WalletPanel()
    {
        walletBalance_panel.SetActive(true);
    }
    public void Btn_CloseWalletPanel()
    {
        walletBalance_panel.SetActive(false);
    }
    public void Rs_Object_Active(bool value)
    {
        for (int i = 0; i < RsObjects.Length; i++)
        {
            RsObjects[i].SetActive(value);
        }
    }
   /* public void Btn_OpenCashWinnerScreen()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            CheckInternet._instance.InternetPopUpActive(true);
            return;
        }
        if (WinnerContentPanel.transform.childCount == 0 & QuizPuzzleAPI.Instance.CashWinnersClassObj.data.Count > 0)
        {
            totalAmountGiven.text = "₹ " + QuizPuzzleAPI.Instance.CashWinnersClassObj.TotalAmount;
            for (int i = 0; i < QuizPuzzleAPI.Instance.CashWinnersClassObj.data.Count; i++)
            {
                CashWinnerListPanel.SetActive(true);
                GameObject winnerGO = Instantiate(CashWinner_ListItem) as GameObject;
                winnerGO.GetComponent<CashWinnerListItem>().SetValueInCashWinnerListItem(QuizPuzzleAPI.Instance.CashWinnersClassObj.data[i].name, QuizPuzzleAPI.Instance.CashWinnersClassObj.data[i].mobile, QuizPuzzleAPI.Instance.CashWinnersClassObj.data[i].amount);
                winnerGO.transform.parent = WinnerContentPanel.transform;
                winnerGO.transform.localScale = Vector3.one;
            }
        }
        else if (WinnerContentPanel.transform.childCount > 0)
        {
            CashWinnerListPanel.SetActive(true);
        }
        else
        {            
            QuizPuzzleAPI.Instance.GetCashWinner_UsersList_API();
        }
    }*/
    public void Btn_OtherCountry()
    {
        PlayerPrefs.SetString("country_name","Iran");
    }
    public void Btn_IndiaCountry()
    {
        PlayerPrefs.SetString("country_name", "India");
    }

}
