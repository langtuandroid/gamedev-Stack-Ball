using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AutoDeactiveGO : MonoBehaviour
{
    public float duration;

    private void OnEnable()
    {		
        StartCoroutine("ActiveThisGameObject");
    }
    public IEnumerator ActiveThisGameObject()
    {
        yield return new WaitForSeconds(duration);
        this.gameObject.SetActive(false);
    }
}
