using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public float timeAmount = 10f;
    private float _time = 0;
    private Image _image;

    // Start is called before the first frame update
    private void Start()
    {
        _image = GetComponent<Image>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (_time < timeAmount)
        {
            _time += Time.deltaTime;
            _image.fillAmount = _time / timeAmount;
        }
    }
}
