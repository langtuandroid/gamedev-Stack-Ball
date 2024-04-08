using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading_Download_scene : MonoBehaviour
{
    public static Loading_Download_scene instance;
	public AsyncOperation sceneAO;
	public GameObject loadingUI;
	public Slider loadingProgbar;
	public Text loadingText;
	public Text gameNameText;
	public Image Icon_image;
	// the actual percentage while scene is fully loaded
	private const float LOAD_READY_PERCENTAGE = 0.9f;
	

	public void Awake()
    {
        instance = this;
    }
    public void ChangeScene(string sceneName)
	{
		
		loadingText.text = "LOADING...";
		
		StartCoroutine(LoadingSceneRealProgress(sceneName));
	}
	
	public IEnumerator LoadingStartAnim()
	{
		yield return new WaitForSeconds(0.05f);
		loadingProgbar.value += 0.002f;
		loadingText.text = "LOADING..." + Mathf.CeilToInt(loadingProgbar.value * 100).ToString() + "%";
		StartCoroutine("LoadingStartAnim");
		if (loadingProgbar.value >= 0.9f)
		{
			//StopCoroutine("LoadingStartAnim");
		}
		
	}
	IEnumerator LoadingSceneRealProgress(string sceneName)
	{
		yield return new WaitForSeconds(0);				
		sceneAO = SceneManager.LoadSceneAsync(sceneName);
		sceneAO.allowSceneActivation = false;	// disable scene activation while loading to prevent auto load
		while (!sceneAO.isDone)
		{			
            loadingText.text = "LOADING..." + Mathf.CeilToInt(sceneAO.progress * 100).ToString() + "%";
            if (sceneAO.progress >= 0.9f)
			{
				//GoogleAdMobController.instance.gameName = sceneName;
				
				Debug.Log("Game start");
				
				AssetBundleDownload.instance.StopCoroutine("Music_BG_LoadingPlay");
				loadingProgbar.value = 1f;                
                loadingText.text = "LOADING...100%";				
			  
			  // if(Analytics_Manager.game_type == "multi_player")
               // {	
					MatchmakingScript.instance.FindMatchmakingPanelOpen(); // Indirect karva mate. matchmaking panel nahi ave
					loadingUI.SetActive(false);
				//}			
				break;
			}
			yield return null;
		}
		
	}
}
