using System;
using UnityEngine;

public class BoxMovement : MonoBehaviour
{
    public TriggerCSplitter splitterTrigger;
    public GameObject box;
    public BoxCollider2D boxCollider;
    public GameObject player;

    public int direction = 2; //0 up, 1 right, 2 bottom, 3 left
    public Rigidbody2D mRigidbody2D;
    public float boxSpeed = 10f;
    [Range(0, .3f)] [SerializeField] private float mMovementSmoothing = .05f;	// How much to smooth out the movement
    private Vector3 _mVelocity = Vector3.zero;
    
    public bool isDecided;

    private string _cornerTag;

    private Animator _playerAnimator;
    private static readonly int IsInteracting = Animator.StringToHash("IsInteracting");

    private void Start()
    {
        _playerAnimator = player.GetComponent<Animator>();
    }

    void Update()
    {
        if (!isDecided)
        {
            if (splitterTrigger.hasBoxWaiting)
            {
                if (box.transform.position.y <= 6.5)
                //checker si le highlight est on, s'il ne l'est pas, on le turn on, et apres on fait un else (s'il n'est pas activé) et on desactive SI il est activé
                {
                    if (Input.GetButtonDown("FireGood"))
                    {
                        _playerAnimator.SetBool(IsInteracting, true);
                        
                        isDecided = true;
                        direction = 3;
                        boxCollider.gameObject.layer = 6;
                    }
                    else if (Input.GetButtonDown("FireBad"))
                    {
                        _playerAnimator.SetBool(IsInteracting, true);
                        
                        isDecided = true;
                        direction = 1;
                        boxCollider.gameObject.layer = 6;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetVelocity = new Vector2(0, -boxSpeed * 10f);
        mRigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

        if (_cornerTag != null && mRigidbody2D.velocity.x < 0.1f && mRigidbody2D.velocity.y < 0.1f)
        {
            switch (_cornerTag)
            {
                case "CornerUp":
                    direction = 0;
                    _cornerTag = null;
                    break;
                case "CornerRight":
                    direction = 1;
                    _cornerTag = null;
                    break;
                case "CornerDown":
                    direction = 2;
                    _cornerTag = null;
                    break;
                case "CornerLeft":
                    direction = 3;
                    _cornerTag = null;
                    break;
            }
        }

        if (isDecided)
        {
            int tempDirection = direction;
            if (box.transform.position.x >= -0.8f && box.transform.position.x <= -0.2f && box.transform.position.y > 5.61f)
            {
                tempDirection = 2;
            }

            switch (tempDirection)
            {
                case 0:
                    targetVelocity = new Vector2(0, boxSpeed * 10f);
                    break;
                case 1:
                    targetVelocity = new Vector2(boxSpeed * 10f, 0);
                    break;
                case 2:
                    targetVelocity = new Vector2(0, -boxSpeed * 10f);
                    break;
                case 3:
                    targetVelocity = new Vector2(-boxSpeed * 10f, 0);
                    break;
            }
        }
        else if (!isDecided && box.transform.position.y <= 6.25)
        {
            mRigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        }
        mRigidbody2D.velocity = Vector3.SmoothDamp(mRigidbody2D.velocity, targetVelocity, ref _mVelocity, mMovementSmoothing);
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag.Contains("Corner"))
        {
            _cornerTag = other.gameObject.tag;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _playerAnimator.SetBool(IsInteracting, false);
    }
}
