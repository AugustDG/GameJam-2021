using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TreeEditor;
using UnityEngine;

public class LoopConstinuously : MonoBehaviour
{
    public RectTransform[] rectTransforms = new RectTransform[2];
    public float speed = 2f;

    // Update is called once per frame
    void Update()
    {
        foreach (var trans in rectTransforms)
        {
            trans.DOAnchorPosX(trans.anchoredPosition.x + 1f * speed * Time.deltaTime, Time.deltaTime).Play();

            if (trans.anchoredPosition.x >= 1301)
            {
                var transAnchoredPosition = trans.anchoredPosition;
                transAnchoredPosition.x = -1301;
                trans.anchoredPosition = transAnchoredPosition;
            }
        }
    }
}
