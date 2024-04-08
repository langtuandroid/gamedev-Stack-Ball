using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinteScroll : MonoBehaviour
{
    public Color newcol;
    float scrollspeed = 7f;
    Vector3 startpos;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * scrollspeed);
        //transform.rotation = Quaternion.identity;
        //transform.rotation = Quaternion.Euler(90f, 0, 0);

    }
}
