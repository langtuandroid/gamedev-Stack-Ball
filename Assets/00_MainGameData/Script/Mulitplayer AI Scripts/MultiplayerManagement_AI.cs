using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerManagement_AI : MonoBehaviour
{
    public static MultiplayerManagement_AI instance;
    public static float timerRef;
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
    public void ScoreUpdate_UserA(int score)
    {
        ScoreManager_Multiplayer.instance.SetScore_UserA(score);
    }
    public void GameOver()
    {
        if (timerRef > 1)
        {
            OpenRetryPanel_BeforeTimeOver();
        }
        else
        {
            //TimerOfGame_Multiplayer.instance.StopTimer();
            //UIManager_Multiplayer.instance.WhileTimeOverOFGamePlay();
        }
    }
    public void OpenRetryPanel_BeforeTimeOver()
    {       
        UIManager_Multiplayer.instance.RetryPanel.SetActive(true);
       // FB_AdManager.instance.SecondChangeBeforeTimeOver();
    }  
    public void ResetGlobalsVariable_WhenTimeOver()
    {
        multi_tower_attack_3d.GLOBALS_StackBall.levelSkinChange = 1;       
        /*multi_firetank_game.GLOBALS_FireTank.levelSkinChange = 1;
        multi_injection_hitman.OnefallGames.StateManager.levelSkinChange = 1;
        multi_hunterman_assassin.GameManager_Assassin.killedEnemy = 0;
        mulit_cannonball3d.UIManager_cannonBall.levelOfcannon = 1;*/
        UserB_AI_Management.isCalled = false;
    }
}
