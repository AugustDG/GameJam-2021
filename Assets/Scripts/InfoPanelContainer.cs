using System;
using System.Collections;
using System.Linq;
using DG.Tweening;
using SOHNE.Accessibility.Colorblindness;
using TMPro;
using UnityEngine;

public class InfoPanelContainer : MonoBehaviour
{
    public TMP_Text nameText;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) ColorblindTypeChanged(Colorblindness.Instance.currentType);
    }

    public void ColorblindTypeChanged(int type)
    {
        StartCoroutine(DisplayInfo(type));
    }

    IEnumerator DisplayInfo(int type)
    {
        nameText.text = ((ColorblindTypesFrench) type).ToString();
        
        transform.GetComponent<RectTransform>().DOAnchorPosY(-3.9f, 0.8f).Play();

        yield return new WaitForSeconds(1f);
        
        transform.GetComponent<RectTransform>().DOAnchorPosY(-5.2f, 0.8f).Play();
    }
}

public enum ColorblindTypesFrench
{
    Normal,
    Protanopie,
    Protanomalie,
    Deutéronome,
    Deutéranomalie,
    Tritanopie,
    Tritanomalie,
    Achromatopsie,
    Achromatomalie,
}