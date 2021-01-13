using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Trigger_pipe : MonoBehaviour
{
    public TMP_Text text;
    public GameObject player;
    public BoxCollider2D collider;
    private BoxCollider2D playerCollider;

    void Start()
    {
        playerCollider = player.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (collider.IsTouching(playerCollider))
        {
            if (Input.GetButtonDown("FireGood"))
            {
                collider.enabled = false;
            }
            else if (Input.GetButtonDown("FireBad"))
            {
                collider.enabled = false;
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
}