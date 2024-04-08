using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScaleAnimation : MonoBehaviour
{
    public RectTransform btnObject;
    public float scaleSize;
    public float delay;
    public float duration=1.0f;
    void Start()
    {
        btnObject.DOScale(new Vector3(scaleSize, scaleSize, scaleSize), duration).SetLoops(-1, LoopType.Yoyo).SetDelay(delay);
    }
    
}
