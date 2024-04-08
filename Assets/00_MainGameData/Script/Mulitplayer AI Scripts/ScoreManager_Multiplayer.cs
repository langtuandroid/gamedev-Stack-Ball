using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager_Multiplayer : MonoBehaviour
{
    public static ScoreManager_Multiplayer instance;    
    public int ScoreA, ScoreB;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }        
    }
    public void ResetValues()
    {
        ScoreB = ScoreA = 0;
        UIManager_Multiplayer.instance.txtScoreUserA.text = "";
        UIManager_Multiplayer.instance.txtScoreUserB.text = "";        
       // UIManager_Multiplayer.instance.DeactivePanel();
        MultiplayerManagement_AI.instance.ResetGlobalsVariable_WhenTimeOver();
    }

    public void SetScore_UserA(int score)
    {
        ScoreA = score;  
        //if (AssetBundleDownload.currentScenenName == "multi_tower_attack_3d")
        //{            
            UIManager_Multiplayer.instance.txtScoreUserA.text = "LEVEL " + ScoreA.ToString();
            SetMessageForRetryPanel("Avoid black stacks!");
        /*}else if(AssetBundleDownload.currentScenenName == "multi_firetank_game")
        {
            UIManager_Multiplayer.instance.txtScoreUserA.text = "LEVEL " + ScoreA.ToString();
            SetMessageForRetryPanel("Don't fire on vehicles!");
        }
        else if (AssetBundleDownload.currentScenenName == "multi_injection_hitman")
        {
            UIManager_Multiplayer.instance.txtScoreUserA.text = "LEVEL " + ScoreA.ToString();
            SetMessageForRetryPanel("Don't hit injection or diamonds!");
        }
        else if (AssetBundleDownload.currentScenenName == "multi_hunterman_assassin")
        {
            UIManager_Multiplayer.instance.txtScoreUserA.text = "KILLED " + ScoreA.ToString();
            SetMessageForRetryPanel("Save yourself from enemies!");
        }
        else if (AssetBundleDownload.currentScenenName == "multi_twistycolorroad")
        {
            UIManager_Multiplayer.instance.txtScoreUserA.text = "Score " + ScoreA.ToString();
            SetMessageForRetryPanel("Don't swipe at different color of ball");
        }
        else if (AssetBundleDownload.currentScenenName == "multi_savetheballoon")
        {
            UIManager_Multiplayer.instance.txtScoreUserA.text = "Score " + ScoreA.ToString();
            SetMessageForRetryPanel("Protect balloon from obstacles!");
        }
        else if (AssetBundleDownload.currentScenenName == "multi_cannonball3d")
        {
            UIManager_Multiplayer.instance.txtScoreUserA.text = "Level " + ScoreA.ToString();
            SetMessageForRetryPanel("Fill the bucket with required ball");
        }*/
    }
    public void SetScore_UserB(int score)
    {
        ScoreB = score;
        //if (AssetBundleDownload.currentScenenName == "multi_tower_attack_3d"  )
       // {
            UIManager_Multiplayer.instance.txtScoreUserB.text = "LEVEL " + ScoreB.ToString();
       /* }
        else if (AssetBundleDownload.currentScenenName == "multi_firetank_game")
        {
            UIManager_Multiplayer.instance.txtScoreUserB.text = "LEVEL " + ScoreB.ToString();
        }
        else if (AssetBundleDownload.currentScenenName == "multi_injection_hitman")
        {
            UIManager_Multiplayer.instance.txtScoreUserB.text = "LEVEL " + ScoreB.ToString();
        }
        else if(AssetBundleDownload.currentScenenName == "multi_hunterman_assassin")
        {
            UIManager_Multiplayer.instance.txtScoreUserB.text = "KILLED " + ScoreB.ToString();
        }
        else if (AssetBundleDownload.currentScenenName == "multi_twistycolorroad")
        {
            UIManager_Multiplayer.instance.txtScoreUserB.text = "Score " + ScoreB.ToString();
        }
        else if (AssetBundleDownload.currentScenenName == "multi_savetheballoon")
        {
            UIManager_Multiplayer.instance.txtScoreUserB.text = "Score " + ScoreB.ToString();
        }
        else if (AssetBundleDownload.currentScenenName == "multi_cannonball3d")
        {
            UIManager_Multiplayer.instance.txtScoreUserB.text = "Level " + ScoreB.ToString();           
        }*/
    }
    public void SetMessageForRetryPanel(string str)
    {
        UIManager_Multiplayer.instance.txtMsgRetry.text = str;
    }
}
