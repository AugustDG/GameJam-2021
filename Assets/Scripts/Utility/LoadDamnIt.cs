using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadDamnIt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().fillAmount += 1f * Time.deltaTime;

        if (GetComponent<Image>().fillAmount >= 1f) GetComponent<Image>().fillAmount = 0;
    }
}
