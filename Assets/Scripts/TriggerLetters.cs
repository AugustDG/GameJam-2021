using TMPro;
using UnityEngine;

public class TriggerLetters : MonoBehaviour
{
    public GameObject canvas;
    public GameObject player;
    public BoxCollider2D boxCollider;
    private BoxCollider2D _playerCollider;

    private Animator _playerAnimator;
    public bool pistolPickedUp;
    public SpriteRenderer pistolSprite;
    public GameObject actualPistol;

    private void Start()
    {
        _playerCollider = player.GetComponent<BoxCollider2D>();
        _playerAnimator = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {

        if (boxCollider.IsTouching(_playerCollider))
        {
            if (Input.GetButtonDown("FireGood") || Input.GetButtonDown("FireBad"))
            {
                pistolPickedUp = !pistolPickedUp;
                if (pistolPickedUp)
                {
                    _playerAnimator.SetBool("HasPickedUp", true);
                    pistolSprite.enabled = false;
                    actualPistol.SetActive(true);
                }
                else
                {
                    _playerAnimator.SetBool("HasPickedUp", false);
                    pistolSprite.enabled = true;
                    actualPistol.SetActive(false);
                }
            }
            else
            {
                canvas.SetActive(true);
            }
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.Equals(player))
        {
            canvas.SetActive(false);
        }
    }
}