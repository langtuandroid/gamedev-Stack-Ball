using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Playerfra : MonoBehaviour {

	[MenuItem("Delete/Delete All PlayerPrefs")]
	static void init()
	{
		PlayerPrefs.DeleteAll ();
		Debug.Log("Deleted All PlayerPrefs Data are deleted ,, OK");
	}
}
