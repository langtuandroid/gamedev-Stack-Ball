using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager_Multiplayer : MonoBehaviour
{
    public static UIManager_Multiplayer instance;
    public GameObject ResultPanelMultiplayer, timerWaitingForOppo;
    public GameObject UserA_go, UserB_go;
    public GameObject RetryPanel;
    public GameObject _2xCoinBtn, messageLogAlertPopUp;
    public Sprite ProfileImgA, ProfileImgB;
    public Image[] IconUserA;
    public Image[] IconUserB;
    public Image TajUserA, TajUserB;
    public Text[] nameUserA;
    public Text[] nameUserB;
    public Text[] coinPriceA;
    public Text[] coinPriceB;
    public Text[] coinsBalance;
    public Text txtBid2XCoin;
    public Text statusA, statusB;
    public Text txtScoreUserA, txtScoreUserB;
    public Text txtScoreUserARes, txtScoreUserBRes , txtMsgOppReady;
    public Text txtMsgTitle, txtMsgDescription, txtMsgRetry;
    public Button playAgainBtn;
    public GameObject getExtraCoinbtn;
    public GameObject winImage,loseImage,tiedImage;

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
        if (PlayerPrefs.GetInt("IsFirstTimeGameStart") == 0)
        {
            PlayerPrefs.SetInt("IsFirstTimeGameStart", 1);
            PlayerPrefs.SetInt("Coin_games", 1000);
        }

    }
    private void Start()
    {
        SetCoinsBalance();
    }
    public void DeactivePanel()
    {
        DontDestroyObject.instance.Header_User.SetActive(false);
        ResultPanelMultiplayer.SetActive(false);
        RetryPanel.SetActive(false);
        timerWaitingForOppo.SetActive(false);
    }
    public void MessageLogAlertPopUP(string title,string description)
    {
       // getExtraCoinbtn.SetActive(false);
        messageLogAlertPopUp.SetActive(true);
        txtMsgTitle.text = title;
        txtMsgDescription.text = description;
    }
    
    public void SetNameOfUserA()
    {
        for (int i = 0; i < nameUserA.Length; i++)
        {
            nameUserA[i].text = Global_MainGame.user_nameA;
        }       
    }
    public void SetNameOfUserB()
    {
        for (int i = 0; i < nameUserB.Length; i++)
        {
            nameUserB[i].text = Global_MainGame.user_nameB;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////// ///////////////////////////////////////////////////////////////////////////////////
    public void SetProfileImageOfUserA()
    {
        for (int i = 0; i < IconUserA.Length; i++)
        {
            IconUserA[i].sprite = ProfileImgA;
        }
    }
    public void SetProfileImageOfUserB()
    {
        for (int i = 0; i < IconUserB.Length; i++)
        {
            IconUserB[i].sprite = ProfileImgB;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////// ///////////////////////////////////////////////////////////////////////////////////
    public void SetBidValueOfCoinUserA()
    {
        for (int i = 0; i < coinPriceA.Length; i++)
        {
            coinPriceA[i].text = Global_MainGame.currentBidCoin.ToString();
        }
    }
    public void SetBidValueOfCoinUserB()
    {
        for (int i = 0; i < coinPriceB.Length; i++)
        {
            coinPriceB[i].text = Global_MainGame.currentBidCoin.ToString();
        }
    }
    /////////////////////////////////////////////////////////////////////////////////// ///////////////////////////////////////////////////////////////////////////////////
    public void SetCoinsBalance()
    {
        for (int i = 0; i < coinsBalance.Length; i++)
        {
            coinsBalance[i].text = PlayerPrefs.GetInt("Coin_games").ToString();           
        }
    }
   
    public void WhileTimeOverOFGamePlay()
    {
        DeactivePanel();
        ResultPanelMultiplayer.SetActive(true);
        playAgainBtn.interactable = true; 
        txtScoreUserARes.text = ScoreManager_Multiplayer.instance.ScoreA.ToString();
        txtScoreUserBRes.text = ScoreManager_Multiplayer.instance.ScoreB.ToString();
        
        if (ScoreManager_Multiplayer.instance.ScoreA > ScoreManager_Multiplayer.instance.ScoreB)
        {
            TajUserA.gameObject.SetActive(true);
            TajUserB.gameObject.SetActive(false);
            statusA.text = "YOU WIN";
            statusB.text = "LOSE";
            winImage.SetActive(true);
            loseImage.SetActive(false);
            tiedImage.SetActive(false);
            WinCoinsAdd();
            DoubleCoinGameObjectEnable();
        }
        else if (ScoreManager_Multiplayer.instance.ScoreA < ScoreManager_Multiplayer.instance.ScoreB)
        {
            TajUserB.gameObject.SetActive(true);
            TajUserA.gameObject.SetActive(false);
            statusA.text = "YOU LOSE";
            statusB.text = "WIN";
            winImage.SetActive(false);
            loseImage.SetActive(true);
            tiedImage.SetActive(false);
            _2xCoinBtn.SetActive(false);
        }
        else if (ScoreManager_Multiplayer.instance.ScoreA == ScoreManager_Multiplayer.instance.ScoreB)
        {
            TajUserA.gameObject.SetActive(false);
            TajUserB.gameObject.SetActive(false);
            statusA.text = "TIED GAME";
            statusB.text = "TIED GAME";
            winImage.SetActive(false);
            loseImage.SetActive(false);
            tiedImage.SetActive(true);
            _2xCoinBtn.SetActive(false);
        }
    }
    public void Play_Again()
    {
        TimerOfGame_Multiplayer.instance.StartTimerForPlayAgain(10);
        timerWaitingForOppo.SetActive(true);
        playAgainBtn.interactable = false;
        winImage.SetActive(false);
        loseImage.SetActive(false);
        tiedImage.SetActive(false);
    }
    public void Back_ResultPanelMult()
    {
        DeactivePanel();
        ScoreManager_Multiplayer.instance.ResetValues();
        TimerOfGame_Multiplayer.instance.StopAllCoroutines();
        MatchmakingScript.instance.findOpponentPanel.SetActive(true);
        IconUserB[0].GetComponent<Image>().sprite = null;
        nameUserB[0].text = "";
        MatchmakingScript.instance.btn_Start.SetActive(true);

        SceneManager.LoadSceneAsync("MainScene");
    }
  
    public void Btn_Retry_BeforeTimeOver()
    {
        RetryPanel.SetActive(false);
        // FB_AdManager.instance.SecondChangeBeforeTimeOver();
        multi_tower_attack_3d.UI.instance.ReviveSuccessful_And_Level_NotFailed();       
    }
    
    public void WinCoinsAdd()
    {
        PlayerPrefs.SetInt("Coin_games", PlayerPrefs.GetInt("Coin_games") + Global_MainGame.currentBidCoin * 2);
        SetCoinsBalance();
    }
    
    public void DoubleCoinGameObjectEnable()
    {
        txtBid2XCoin.text = (Global_MainGame.currentBidCoin * 2).ToString();
        _2xCoinBtn.SetActive(true);
    }
    
    public void ReviveSuccessful_And_Add_2xCoins()
    {
        _2xCoinBtn.SetActive(false);
        MessageLogAlertPopUP("Congratulation!", "Wow! "+ Global_MainGame.currentBidCoin * 2+" Coins, You have successfully got the double coins!");
        PlayerPrefs.SetInt("Coin_games", PlayerPrefs.GetInt("Coin_games") + Global_MainGame.currentBidCoin * 2);
        SetCoinsBalance();
    }
    
    public void ReviveSuccessful_And_Add_GcCoins()
    {
        getExtraCoinbtn.SetActive(false);
        MessageLogAlertPopUP("Congratulation!", "Wow! " + 400 + " Coins, You have successfully got the coins!");
        PlayerPrefs.SetInt("Coin_games", PlayerPrefs.GetInt("Coin_games") + 400);
        SetCoinsBalance();        
    }
}
