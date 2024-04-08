using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTagAndLayer : MonoBehaviour {

	public string tagName;
	public string layerName;
	void Awake()
	{
		if (tagName == "") {
			this.gameObject.tag = "Untagged";
		} else {
			this.gameObject.tag = tagName;
		}
		if (layerName == "") {
			this.gameObject.layer = LayerMask.NameToLayer ("Default");
		} else {
			this.gameObject.layer = LayerMask.NameToLayer (layerName);
		}

	}
	void Start () {
		if (tagName == "") {
			this.gameObject.tag = "Untagged";
		} else {
			this.gameObject.tag = tagName;
		}
		if (layerName == "") {
			this.gameObject.layer = LayerMask.NameToLayer ("Default");
		} else {
			this.gameObject.layer = LayerMask.NameToLayer (layerName);
		}
	}
	
	void OnEnable(){
		if (tagName == "") {
			this.gameObject.tag = "Untagged";
		} else {
			this.gameObject.tag = tagName;
		}
		if (layerName == "") {
			this.gameObject.layer = LayerMask.NameToLayer ("Default");
		} else {
			this.gameObject.layer = LayerMask.NameToLayer (layerName);
		}
	}
}
