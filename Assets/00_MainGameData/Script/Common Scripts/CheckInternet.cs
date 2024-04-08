using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInternet : MonoBehaviour {

	public static CheckInternet _instance;
	// Internet available
	public static bool internetAvailabelStatus = true;
	public static bool internetConnectionLow = false;

	public GameObject internetPopup;

	// Use this for initialization
	void Start () 
	{
		if (_instance == null) {
		
			_instance = this;
		}
		//InvokeRepeating ("CheckInternetTest", 0, 3f);

//		CheckInternetTest ();
//		Invoke("CheckInternetTest",1f);

	}

	// Check internet every 1sec.
	void CheckInternetTest()
	{
//		StopCoroutine (InternetAvailibility ());
		StartCoroutine("InternetAvailibility");
	}

	IEnumerator InternetAvailibility()
	{
		/*WWW www = new WWW("http://www.google.co.in/");
		yield return www;

		if (www.error == null) {
			internetConnectionLow = false;
		} else {
			internetConnectionLow = true;

		}
		if(string.IsNullOrEmpty(www.error) && www.bytes.Length > 500)
			internetAvailabelStatus = true;
			else
			internetAvailabelStatus = false;*/
		
		if (Application.internetReachability == NetworkReachability.NotReachable) {
			internetConnectionLow = true;
			internetAvailabelStatus = false;
			InternetPopUpActive(true);
		} else {
			internetConnectionLow = false;
			internetAvailabelStatus = true;
			InternetPopUpActive (false);
		}
		yield return new WaitForSeconds(0);
	}

	public void InternetPopUpActive(bool value)
	{
		internetPopup.SetActive(value);
	}

}
