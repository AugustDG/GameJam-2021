using TMPro;
using UnityEngine;

public class Trigger_pipe : MonoBehaviour
{
    public TMP_Text text;
    public GameObject player;
    public BoxCollider2D itemCollider;
    private BoxCollider2D playerCollider;

    void Start()
    {
        playerCollider = player.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (itemCollider.IsTouching(playerCollider))
        {
            if (Input.GetButtonDown("FireGood"))
            {
                itemCollider.enabled = false;
            }
            else if (Input.GetButtonDown("FireBad"))
            {
                itemCollider.enabled = false;
            }
            else
                text.text = "Q pour arranger le tuyau\nE pour inonder";
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.Equals(player))
        {
            text.text = "";
        }
    }

    IEnumerator loading (ProgressbarRef, duration)
    {
        float time = 0.0f;
        while (time < duration)
        {
            ProgressbarRef.progress = time / Mathf.Max(duration, SmallEpsilon);
            time += Time.deltaTime;
            yield return new WaitEndOfFrame();
        }
    }
}