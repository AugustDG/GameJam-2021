using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{

    public float timeAmount = 10f;
    private float time = 0;
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = this.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (time < timeAmount)
        {
            time += Time.deltaTime;
            image.fillAmount = time / timeAmount;
        }
    }
}
