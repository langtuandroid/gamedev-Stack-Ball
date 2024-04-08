using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserB_AI_Management : MonoBehaviour
{
    public static UserB_AI_Management instance;
    public static bool isCalled = false;
    private int scoreBTemp;
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
    public void StartAutoIncrementScoreB()
    {
        scoreBTemp = 0;
        StartCoroutine("StartIncrementUserScoreB");
    }
    private IEnumerator StartIncrementUserScoreB()
    {
       // if (AssetBundleDownload.currentScenenName == "multi_tower_attack_3d" || AssetBundleDownload.currentScenenName == "multi_firetank_game")
       // {
            if (!isCalled)
            {
                isCalled = true;
                scoreBTemp = 1;
                ScoreManager_Multiplayer.instance.SetScore_UserB(scoreBTemp); // starting level number 1 thi thase etle
            }
            yield return new WaitForSecondsRealtime(Random.Range(15, 30));
            scoreBTemp++;
            ScoreManager_Multiplayer.instance.SetScore_UserB(scoreBTemp);
            StartCoroutine("StartIncrementUserScoreB");
        /*}
        else if (AssetBundleDownload.currentScenenName == "multi_injection_hitman")
        {
            if (!isCalled)
            {
                isCalled = true;
                scoreBTemp = 1;
                ScoreManager_Multiplayer.instance.SetScore_UserB(scoreBTemp); // starting level number 1 thi thase etle
            }
            yield return new WaitForSecondsRealtime(Random.Range(9, 17));
            scoreBTemp++;
            ScoreManager_Multiplayer.instance.SetScore_UserB(scoreBTemp);
            StartCoroutine("StartIncrementUserScoreB");
        }
        else if (AssetBundleDownload.currentScenenName == "multi_hunterman_assassin")
        {
            yield return new WaitForSecondsRealtime(Random.Range(2, 7));
            scoreBTemp++;
            ScoreManager_Multiplayer.instance.SetScore_UserB(scoreBTemp);
            StartCoroutine("StartIncrementUserScoreB");
        }
        else if (AssetBundleDownload.currentScenenName == "multi_twistycolorroad")
        {
            if (!isCalled)
            {
                isCalled = true;
                ScoreManager_Multiplayer.instance.SetScore_UserB(0);
                yield return new WaitForSecondsRealtime(9);
            }
            yield return new WaitForSecondsRealtime(Random.Range(1, 6));
            if (Random.Range(1, 4) == 1)
            {
                scoreBTemp = scoreBTemp + 4;
            }
            else
            {
                scoreBTemp++;
            }
            ScoreManager_Multiplayer.instance.SetScore_UserB(scoreBTemp);
            StartCoroutine("StartIncrementUserScoreB");
        }
        else if (AssetBundleDownload.currentScenenName == "multi_savetheballoon")
        {
            yield return new WaitForSecondsRealtime(Random.Range(0, 2));
            scoreBTemp++;
            ScoreManager_Multiplayer.instance.SetScore_UserB(scoreBTemp);
            StartCoroutine("StartIncrementUserScoreB");
        }
        else if (AssetBundleDownload.currentScenenName == "multi_cannonball3d")
        {
            if (!isCalled)
            {
                isCalled = true;
                scoreBTemp = 1;
                ScoreManager_Multiplayer.instance.SetScore_UserB(scoreBTemp); // starting level number 1 thi thase etle
            }
            yield return new WaitForSecondsRealtime(Random.Range(12, 25));
            scoreBTemp++;
            ScoreManager_Multiplayer.instance.SetScore_UserB(scoreBTemp);
            StartCoroutine("StartIncrementUserScoreB");
        }*/
    }

    public void StopAutoIncrementScoreB()
    {
        StopCoroutine("StartIncrementUserScoreB");
    }
}
