using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DontDestroyObject : MonoBehaviour
{
    public static DontDestroyObject instance; 
    public GameObject ProfilHeader;
    public GameObject ExitPanel_go;
    public Text exitMsgText;
    public GameObject bannerbgImageBottom, bannerbgImageTop;
    public GameObject Header_User;
  
    void Awake()
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
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Scene scene = SceneManager.GetActiveScene();
            Debug.Log(scene.name);
            if (scene.name != "MainScene")
            {
                ExitPanel_go.SetActive(true);
                exitMsgText.text = "Are you sure you want to go home page?";
                
            }
            if (scene.name == "MainScene")
            {
                ExitPanel_go.SetActive(true);
                exitMsgText.text = "Are you sure you want to quit the game?";                
            }                 
        }
    }
    public void ProfileHeader_Active(bool value)
    {
        ProfilHeader.SetActive(value);
    }
    public void SetValueOnProfileAndHeader()
    {
      Header_User.SetActive(true);       
    }
   
    public void ChangeScene_GoMainScreen(string sceneName)
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "MainScene")
        {          
                Application.Quit();           
        }
        else
        {
            Time.timeScale = 1;
            Reset_ObjectWhenGOtoMainScreen();
           // GoogleAdMobController.instance.DestroyBannerAds();
            PlayerPrefs.SetInt("_isFistTimeFrameRate", 0);
            UIManager_Multiplayer.instance.Back_ResultPanelMult();               
            SceneManager.LoadScene(sceneName);
        }
    }
    public void Reset_ObjectWhenGOtoMainScreen()
    {
        UserB_AI_Management.instance.StopAllCoroutines(); 
        TimerOfGame_Multiplayer.instance.StopAllCoroutines();
        ScoreManager_Multiplayer.instance.ResetValues();
        UIManager_Multiplayer.instance.RetryPanel.SetActive(false);
        Header_User.SetActive(false);
    }
}
