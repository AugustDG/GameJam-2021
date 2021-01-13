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
        Collider2D[] colliders = new Collider2D[50];
        int nb = GetComponent<BoxCollider2D>().OverlapCollider(new ContactFilter2D().NoFilter(), new Collider2D[50]);
        //Debug.Log(nb);
        isActivated = false;
        for (int i = 0; i < nb; i++)
        {
            if (colliders[i].gameObject.tag != null)
                isActivated = isActivated || colliders[i].gameObject.tag.Equals("Box");
        }
        for (int i = 0; i < nb; i++)
        {
            belts[i].GetComponentInChildren<Animator>().SetBool("isIdle", !isActivated);
        }

    }
}
