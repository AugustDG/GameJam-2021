using TMPro;
using UnityEngine;

public class TriggerCSplitter : MonoBehaviour
{
    public TMP_Text text;
    public GameObject player;
    public bool isPlayerWaiting;
    public float delay = 0.5f;

    [SerializeField] private BoxCollider2D boxCollider;

    private Animator _playerAnimator;
    private AreaEffector2D _effector2D;
    private static readonly int IsInteracting = Animator.StringToHash("IsInteracting");
    private float timeLast = 0f;

    private void Start()
    {
        _playerAnimator = player.GetComponent<Animator>();
        _effector2D = boxCollider.GetComponent<AreaEffector2D>();
    }

    private void Update()
    {
        if (isPlayerWaiting)
        {
            if (Input.GetButtonDown("FireGood"))
            {
                boxCollider.isTrigger = true;

                _effector2D.forceAngle = 180;
                _effector2D.forceMagnitude = 2;
                _playerAnimator.SetBool(IsInteracting, true);
                timeLast = Time.time;
            }
            else if (Input.GetButtonDown("FireBad"))
            {
                boxCollider.isTrigger = true;

                _effector2D.forceAngle = 0;
                _effector2D.forceMagnitude = 2;
                _playerAnimator.SetBool(IsInteracting, true);
                timeLast = Time.time;
            }
        }

        if (timeLast != 0 && timeLast + delay <= Time.time)
        {
            boxCollider.isTrigger = false;

            _effector2D.forceAngle = 0;
            _effector2D.forceMagnitude = 0;
            _playerAnimator.SetBool(IsInteracting, false);
            timeLast = 0;
        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.Equals(player))
        {
            isPlayerWaiting = true;
            text.text = "Q pour envoyer vers la gauche\nE pour envoyer vers la droite";
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
        /*if (other.gameObject.CompareTag("Box"))
        {
            boxCollider.isTrigger = false;

            _effector2D.forceAngle = 0;
            _effector2D.forceMagnitude = 0;
            _playerAnimator.SetBool(IsInteracting, false);
            
            hasBoxWaiting = false;
        }*/
    }
}