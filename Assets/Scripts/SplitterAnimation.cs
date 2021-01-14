using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitterAnimation : MonoBehaviour
{
    public SectionAnimation section2;
    public SectionAnimation section4;
    public Animator splitterAnimator;
    
    // Update is called once per frame
    void Update()
    {
        splitterAnimator.SetBool("isLeft", section4.isActivated);
        splitterAnimator.SetBool("isRight", section2.isActivated);
        Debug.Log(section4.isActivated + " " + section2.isActivated);
    }
}
