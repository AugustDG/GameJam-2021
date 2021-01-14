using UnityEngine;

public class SectionAnimation : MonoBehaviour
{
    public GameObject[] belts;
    public bool isActivated;
    

    // Update is called once per frame
    private void FixedUpdate()
    {
        for (int i = 0; i < belts.Length; i++)
        {
            belts[i].GetComponentInChildren<Animator>().SetBool("isIdle", !isActivated);
        }
        LayerMask mask = LayerMask.GetMask("Box");
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(mask);
        int nb = GetComponent<BoxCollider2D>().OverlapCollider(filter, new Collider2D[50]);
        if (nb == 0) isActivated = false;
        
    }

    private void OnTriggerStay2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            //holy fuck let's activate
            isActivated = true;
        }
    }
}