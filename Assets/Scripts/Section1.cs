using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section1 : MonoBehaviour
{
    public GameObject[] belts;
    public bool isActivated = false;

    // Update is called once per frame
    void Update()
    {
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(LayerMask.GetMask("Box"));
        //Debug.Log(GetComponent<BoxCollider2D>().OverlapCollider(filter, new Collider2D[50]));
        if (GetComponent<BoxCollider2D>().OverlapCollider(filter,new Collider2D[50]) > 0)
        {
            isActivated = true;
        } else
        {
            isActivated = false;
        }

        for (int i = 0; i < belts.Length; i++)
        {
            belts[i].GetComponentInChildren<Animator>().SetBool("isIdle", !isActivated);
        }

    }
}
