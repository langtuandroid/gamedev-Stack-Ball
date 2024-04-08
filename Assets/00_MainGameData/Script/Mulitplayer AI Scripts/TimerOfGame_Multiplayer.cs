using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerOfGame_Multiplayer : MonoBehaviour
{
    public static TimerOfGame_Multiplayer instance;
    public float timer, timer1;
    public Image filledImage, filledImage1;
    public Text txtTimer, txtTimer1;
    public GameObject timerGo;
    public AudioSource ticTicMusic;
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
    bool isPaused = false;
    bool isCalledFirst;
    void OnGUI()
    {
        if (isPaused)
        {
            //GUI.Label(new Rect(100, 100, 50, 30), "Game paused");
        }
        else
        {
            /*if(filledImage.fillAmount < 0)
            {
                StopTimer();
                UIManager_Multiplayer.instance.WhileTimeOverOFGamePlay();
                TimeOverOfGame();                
            }*/
        }
    }

    void OnApplicationFocus(bool hasFocus)
    {
        isPaused = !hasFocus; // not paused
       /* if (filledImage.fillAmount <= 0)
        {
            StopTimer();
            UIManager_Multiplayer.instance.WhileTimeOverOFGamePlay();
            TimeOverOfGame();
        }*/
    }

    void OnApplicationPause(bool pauseStatus)
    {
        isPaused = pauseStatus; //
    }
    public void StartTimer(float time)
    {
        ScoreManager_Multiplayer.instance.ResetValues();
        StartCoroutine(StartTimerCoroutine(time));       
        UserB_AI_Management.instance.StartAutoIncrementScoreB();
       
    }
    public void StopTimer()
    {
        StopAllCoroutines();
        StopCoroutine("StartTimerCoroutine");
        UserB_AI_Management.instance.StopAutoIncrementScoreB();
        Debug.Log("Stopo Timer Main Game");
    }
    private IEnumerator StartTimerCoroutine(float time)
    {       
        float starttime = time;
        timer = time;
        while (time > 0)
        {           
            time -= Time.deltaTime;
            filledImage.fillAmount = time / starttime;
            timer = Convert.ToInt32(time);           
            if (timer >= 0)
            {
                MultiplayerManagement_AI.timerRef = timer;
                txtTimer.text = timer.ToString();
                if(timer == 10)
                {
                    StartCoroutine("ScaleInOutTimer");
                }
            }
            yield return null;
        }
        if (timer == 0)
        {
            StopCoroutine("ScaleInOutTimer");
            TimeOverOfGame();
        }
    }
    IEnumerator ScaleInOutTimer()
    {
        ticTicMusic.Play(0);
        yield return new WaitForSeconds(0.5f);
        timerGo.transform.localScale = new Vector3(1.1f,1.1f,1.1f);
        yield return new WaitForSeconds(0.5f);
        timerGo.transform.localScale = new Vector3(1f, 1f, 1f);
        StartCoroutine("ScaleInOutTimer");
    }
    public void TimeOverOfGame()
    {
        StopTimer();
        UIManager_Multiplayer.instance.WhileTimeOverOFGamePlay();
        // DontDestroyObject.instance.ChangeScene_GoMainScreen("MainScene");
        SceneManager.LoadScene("MainScene");
    }

    /// <summary>
    /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>
   
    public void StartTimerForPlayAgain(float time)
    {       
        StartCoroutine(StartTimerCoroutineForPlayAgain(time));
    }
    private IEnumerator StartTimerCoroutineForPlayAgain(float time)
    {
        UIManager_Multiplayer.instance.txtMsgOppReady.text = "WAITING FOR OPPONENT...";
        float starttime = time;
        timer1 = time;
        while (time > 0f)
        {
            time -= Time.deltaTime;
            filledImage1.fillAmount = time / starttime;
            timer1 = Convert.ToInt32(time);
            if (timer1 >= 0)
            {
                txtTimer1.text = timer1.ToString();
                if (timer1 == 3)
                {
                    if (1 == UnityEngine.Random.Range(0, 3))
                     {
                        opponenetReady = true;
                        UIManager_Multiplayer.instance.txtMsgOppReady.text = Global_MainGame.user_nameB + " is ready!!";
                     }
                }
            }
            yield return null;
        }
       
        if (timer1 == 0)
        {
            TimeOverOfPlayAgainRequest();           
        }
    }
    bool opponenetReady;
    public void TimeOverOfPlayAgainRequest()
    {
        StopCoroutine("StartTimerCoroutineForPlayAgain");
        UIManager_Multiplayer.instance.timerWaitingForOppo.SetActive(false);
        if (opponenetReady)
        {
            opponenetReady = false;
            UIManager_Multiplayer.instance.DeactivePanel();
            MatchmakingScript.instance.RetryGameWhenOpponentAgree();
        }
        else
        {
            UIManager_Multiplayer.instance.playAgainBtn.interactable = true;
        }
    }

}
