using System;
using TMPro;
using UnityEngine;

public class TriggerCSplitter : MonoBehaviour
{
    public TMP_Text text;
    public GameObject player;
    public bool isPlayerWaiting;
    public bool hasBoxWaiting;

    [SerializeField] private BoxCollider2D boxCollider;

    private Animator _playerAnimator;
    private AreaEffector2D _effector2D;
    private static readonly int IsInteracting = Animator.StringToHash("IsInteracting");

    private void Start()
    {
        _playerAnimator = player.GetComponent<Animator>();
        _effector2D = boxCollider.GetComponent<AreaEffector2D>();
    }

    private void Update()
    {
        if (isPlayerWaiting && hasBoxWaiting)
        {
            if (Input.GetButtonDown("FireGood"))
            {
                boxCollider.isTrigger = true;

                _effector2D.forceAngle = 180;
                _effector2D.forceMagnitude = 2;
                _playerAnimator.SetBool(IsInteracting, true);
            }
            else if (Input.GetButtonDown("FireBad"))
            {
                boxCollider.isTrigger = true;

                _effector2D.forceAngle = 0;
                _effector2D.forceMagnitude = 2;
                _playerAnimator.SetBool(IsInteracting, true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.Equals(player))
        {
            isPlayerWaiting = true;
            text.text = "Q pour envoyer vers la gauche\nE pour envoyer vers la droite";
        }
        if (other.gameObject.CompareTag("Box"))
        {
            hasBoxWaiting = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        print(gameObject.name);
        
        if (other.gameObject.Equals(player))
        {
            isPlayerWaiting = false;
            text.text = "";
        }
        if (other.gameObject.CompareTag("Box"))
        {
            boxCollider.isTrigger = false;

            _effector2D.forceAngle = 0;
            _effector2D.forceMagnitude = 0;
            _playerAnimator.SetBool(IsInteracting, false);
            
            hasBoxWaiting = false;
        }
    }
}