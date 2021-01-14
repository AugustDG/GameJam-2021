using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info : MonoBehaviour
{
    public bool left = false;
    public Sprite[] spritesLeft;
    public Sprite[] spritesRight;
    public SpriteRenderer boxRenderer;

    void Start()
    {
        if (Random.Range(0,2) == 1)
        {
            left = true;
        }
        if (left)
        {
            boxRenderer.sprite = spritesLeft[Random.Range(0, spritesLeft.Length - 1)];
        }
        else
        {
            boxRenderer.sprite = spritesRight[Random.Range(0, spritesRight.Length - 1)];
        }
    }

}
