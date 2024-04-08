using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AssetBundleDownload : MonoBehaviour   
{                                                  
    public static AssetBundleDownload instance ;
    public static string multipleGamePath; 
    private AssetBundle myLoadedAssetBundle;
    private string[] scenePaths;
    public List<string> sceneURL;
    public List<string> sceneName;
    public List<string> gameNameList = new List<string>();
    bool isBundleLoaded = false;    
    public static string currentScenenName;
    public int assetVersion;
    //public AudioClip music_loading;
   
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    
    public void DownloadGameFromServer(string gameName, int ver)  
    {
        assetVersion = ver;
        currentScenenName = gameName;

        Loading_Download_scene.instance.loadingUI.SetActive(true);
        UIManager_MainGame.instance.winCash_regular_panel.SetActive(false);
        Loading_Download_scene.instance.loadingProgbar.value = 0.0f;
        Loading_Download_scene.instance.StartCoroutine("LoadingStartAnim");       
        StartCoroutine("DownloadGameFromServer_Courutine", gameName);
        StartCoroutine("CancleBtnOnIfTakeMoreTime");
        
    }
    IEnumerator CancleBtnOnIfTakeMoreTime()
    {
        yield return new WaitForSeconds(75f);
        if (currentScenenName == "MainScene")
        {
            UIManager_MainGame.instance.cancleBtnGo_loadingScreen.SetActive(true);
        }
    }
    public void StopDownloadingAssets() 
    {
        Loading_Download_scene.instance.loadingUI.SetActive(false);
        Loading_Download_scene.instance.StopCoroutine("LoadingStartAnim");
        Loading_Download_scene.instance.loadingProgbar.value = 0;
        StopCoroutine("DownloadGameFromServer_Courutine"); 
        Destroy(GameObject.Find(currentScenenName));
    }

    public IEnumerator DownloadGameFromServer_Courutine(string gameName)
    {
        Debug.Log("DownloadGameFromServer_Courutine");
        //yield return new WaitForSeconds(190f);
        if (gameNameList.Contains(gameName))
        {
            isBundleLoaded = true;               
        }
        else
        {
            isBundleLoaded = false;
           
        }
        
        if (isBundleLoaded == false)
        {
            // oneTimeClicked = 1;
            while (!Caching.ready)
                yield return null;

            Debug.Log(Application.persistentDataPath);
           
            //  var bundleWWW = UnityWebRequestAssetBundle.GetAssetBundle(multipleGamePath + gameName);
            //  yield return bundleWWW.SendWebRequest();
            multipleGamePath = "" + assetVersion + "/";          

            Debug.Log(multipleGamePath + gameName);
            WWW bundleWWW1 = WWW.LoadFromCacheOrDownload(multipleGamePath +""+ gameName, assetVersion); 
            yield return bundleWWW1;

            Debug.Log("bundleWWW1" + bundleWWW1);
            if (!string.IsNullOrEmpty(bundleWWW1.error))
            {
                Debug.Log(bundleWWW1.error);
                if (Application.internetReachability == NetworkReachability.NotReachable)
                {
                    CheckInternet._instance.InternetPopUpActive(true);
                    StopDownloadingAssets();
                }
                else
                {
                    StopDownloadingAssets();
                   // QuizPuzzleAPI.Instance.TextValidationAlertPopup("Sorry for interrupting to start this game, because we are working on improvements. Please try after some time.");
                }
                yield return null;
            }
            else
            {
                myLoadedAssetBundle = bundleWWW1.assetBundle;               
                if (myLoadedAssetBundle.isStreamedSceneAssetBundle)
                {
                    if (myLoadedAssetBundle.Contains(gameName))
                    {
                        StopCoroutine("CancleBtnOnIfTakeMoreTime");
                        yield return new WaitForSeconds(0.2f);
                        if (!gameNameList.Contains(gameName))
                        {
                            gameNameList.Add(gameName);
                        }
                        Loading_Download_scene.instance.ChangeScene(gameName);
                    }
                    else
                    {
                        Debug.Log("scene is not available in your asset bundle");
                    }
                }
            }         
        }
        else {
            Debug.Log("isBundleLoaded " + isBundleLoaded);
            StopCoroutine("CancleBtnOnIfTakeMoreTime");
            Loading_Download_scene.instance.ChangeScene(gameName);
        }
    }  
}
