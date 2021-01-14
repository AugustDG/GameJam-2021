using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Trigger_pistol : MonoBehaviour
{
    public TMP_Text text;
    public GameObject player;
    public BoxCollider2D boxCollider;
    private BoxCollider2D _playerCollider;

    private Animator _playerAnimator;
    public bool pistolPickedUp;
    public Sprite pistolLa;
    public Sprite pistolPasLa;
    public SpriteRenderer pistolSprite;

    void Start()
    {
        _playerCollider = player.GetComponent<BoxCollider2D>();
        _playerAnimator = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (boxCollider.IsTouching(_playerCollider))
        {
            if (Input.GetButtonDown("FireGood") || Input.GetButtonDown("FireBad"))
            {
                pistolPickedUp = !pistolPickedUp;
                if (pistolPickedUp)
                {
                    _playerAnimator.SetTrigger("PickUpFuel");
                    _playerAnimator.SetBool("HasFuel", true);
                    pistolSprite.sprite = pistolPasLa;
                }
                else
                {
                    _playerAnimator.SetBool("HasFuel", false);
                    pistolSprite.sprite = pistolLa;
                }
            }
            else
            {
                text.text = "Q ou E pour intéragir avec le pistolet à gaz";
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