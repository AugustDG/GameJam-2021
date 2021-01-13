using UnityEngine;

public class BoxMovement : MonoBehaviour
{
    public Trigger_CSplitter splitterTrigger;
    public BoxCollider2D splitterCollider;
    public GameObject box;
    public BoxCollider2D boxCollider;

    public int direction = 2; //0 up, 1 right, 2 bottom, 3 left
    public Rigidbody2D m_Rigidbody2D;
    public float boxSpeed = 10f;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
    private Vector3 m_Velocity = Vector3.zero;
    
    public bool isDecided = false;
    public bool facingLeft = false;

    private string cornerTag = null;

    void Update()
    {
        if (!isDecided)
        {
            if (splitterTrigger.isActivated)
            {
                if (box.transform.position.y <= 7)
                //checker si le highlight est on, s'il ne l'est pas, on le turn on, et apres on fait un else (s'il n'est pas activé) et on desactive SI il est activé
                {
                    if (Input.GetButtonDown("FireGood"))
                    {
                        isDecided = true;
                        direction = 3;
                    }
                    else if (Input.GetButtonDown("FireBad"))
                    {
                        isDecided = true;
                        direction = 1;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetVelocity = new Vector2(0, -boxSpeed * 10f);
        m_Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

        if (cornerTag != null && m_Rigidbody2D.velocity.x < 0.1f && m_Rigidbody2D.velocity.y < 0.1f)
        {
            switch (cornerTag)
            {
                case "CornerUp":
                    direction = 0;
                    cornerTag = null;
                    break;
                case "CornerRight":
                    direction = 1;
                    cornerTag = null;
                    break;
                case "CornerDown":
                    direction = 2;
                    cornerTag = null;
                    break;
                case "CornerLeft":
                    direction = 3;
                    cornerTag = null;
                    break;
            }
        }

        if (isDecided)
        {
            int tempDirection = direction;
            if (box.transform.position.x >= 0.75 && box.transform.position.x <= 1.25 && box.transform.position.y > 6)
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
        else if (!isDecided && box.transform.position.y <= 6.75)
        {
            m_Rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        }
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag.Contains("Corner"))
        {
            cornerTag = other.gameObject.tag;
        }
    }
}
