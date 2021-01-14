using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class InfoPanelContainer : MonoBehaviour
{
    public TMP_Text nameText;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) ColorblindTypeChanged(CustomColorblindness.Instance.CurrentType);
    }

    public void ColorblindTypeChanged(int type)
    {
        StartCoroutine(DisplayInfo(type));
    }

    private IEnumerator DisplayInfo(int type)
    {
        nameText.text = ((ColorblindTypesFrench) type).ToString();
        
        transform.GetComponent<RectTransform>().DoAnchorPosY(-3.9f, 0.8f).Play();

        yield return new WaitForSeconds(1f);
        
        transform.GetComponent<RectTransform>().DoAnchorPosY(-5.2f, 0.8f).Play();
    }
}