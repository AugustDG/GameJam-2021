using TMPro;
using UnityEngine;

public class Trigger_CSplitter : MonoBehaviour
{
    public TMP_Text text;
    public GameObject player;
    public BoxCollider2D itemCollider;
    private BoxCollider2D playerCollider;
    public bool isActivated = false;

    void Start()
    {
        playerCollider = player.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isActivated = itemCollider.IsTouching(playerCollider);
        if (isActivated)
        {
            text.text = "Q pour envoyer vers la gauche\nE pour envoyer vers la droite";
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