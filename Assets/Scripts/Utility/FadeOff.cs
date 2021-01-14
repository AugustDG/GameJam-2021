using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeOff : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<RawImage>().DoFade(0f, 5f).Play();   
    }

    // Update is called once per frame
    void Update()
    {
    }
}
