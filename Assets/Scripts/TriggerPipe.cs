using TMPro;
using UnityEngine;

public class TriggerPipe : MonoBehaviour
{
    public TMP_Text text;
    public GameObject player;
    public BoxCollider2D itemCollider;
    private Animator _playerAnimator;
    private BoxCollider2D _playerCollider;
    private static readonly int IsInteracting = Animator.StringToHash("IsInteracting");

    private void Start()
    {
        _playerCollider = player.GetComponent<BoxCollider2D>();
        _playerAnimator = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (itemCollider.IsTouching(_playerCollider))
        {
            if (Input.GetButtonDown("FireGood"))
            {
                _playerAnimator.SetBool(IsInteracting, true);
                itemCollider.enabled = false;
            }
            else if (Input.GetButtonDown("FireBad"))
            {
                _playerAnimator.SetBool(IsInteracting, true);
                itemCollider.enabled = false;
            }
            else
            {
                _playerAnimator.SetBool(IsInteracting, false);
                text.text = "Q pour arranger le tuyau\nE pour inonder";
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.Equals(player)) text.text = "";
    }
}