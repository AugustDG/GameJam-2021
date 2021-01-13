using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Trigger_toilet : MonoBehaviour
{
    public TMP_Text text;
    public GameObject player;
    public BoxCollider2D collider;
    private BoxCollider2D playerCollider;

    public float timeAmount = 10f;
    private float time = 0;
    public Image image;
    public Canvas canvas;

    void Start()
    {
        playerCollider = player.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (collider.IsTouching(playerCollider))
        {
            if (Input.GetButton("FireGood"))
            {
                if (time < timeAmount)
                {
                    time += Time.deltaTime;
                    image.fillAmount = time / timeAmount;
                }
                else
                {
                    canvas.enabled = false;
                }
            }
            else if (Input.GetButton("FireBad"))
            {
                if (time < timeAmount)
                {
                    time += Time.deltaTime;
                    image.fillAmount = time / timeAmount;
                }
                else
                {
                    canvas.enabled = false;
                }
            }
            else
            {
                time = 0f;
                image.fillAmount = time / timeAmount;
                text.text = "Q pour fermer le robinet\nE pour boucher le lavabo";
            }
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