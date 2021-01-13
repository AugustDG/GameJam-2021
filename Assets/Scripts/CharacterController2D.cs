using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
    public float runSpeed = 40f;
    private Rigidbody2D m_Rigidbody2D;
    private Vector3 m_Velocity = Vector3.zero;
    public int direction = 4; //0 en haut, 1 a droite, 2 en bas, 3 a gauche, 4 idle haut, 5 idle droite, 6 idle bas, 7 idle gauche

    private Animator _animator;
    private static readonly int Direction = Animator.StringToHash("Direction");

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButton("Up"))
        {
            if (direction >= 4)
                direction = 0;
        }
        else if (Input.GetButton("Down"))
        {
            if (direction >= 4)
                direction = 2;
        }
        else if (Input.GetButton("Right"))
        {
            if (direction >= 4)
                direction = 1;
        }
        else if (Input.GetButton("Left"))
        {
            if (direction >= 4)
                direction = 3;
        }
        else
        {
            switch (direction)
            {
                case 0:
                    direction = 4;
                    break;
                case 1:
                    direction = 5;
                    break;
                case 2:
                    direction = 6;
                    break;
                case 3:
                    direction = 7;
                    break;
            }
        }
        if (Input.GetButtonUp("Up"))
        {
            if (direction == 0)
                direction = 4;
        }
        else if (Input.GetButtonUp("Down"))
        {
            if (direction == 2)
                direction = 4;
        }
        else if (Input.GetButtonUp("Right"))
        {
            if (direction == 1)
                direction = 4;
        }
        else if (Input.GetButtonUp("Left"))
        {
            if (direction == 3)
                direction = 4;
        }
    }

    void FixedUpdate()
    {
        Vector3 targetVelocity = new Vector2(0, 0);
        
        _animator.SetInteger(Direction, direction);
        
        switch (direction)
        {
            case 0:
                targetVelocity = new Vector2(0, runSpeed * 10f);
                break;
            case 1:
                targetVelocity = new Vector2(runSpeed * 10f, 0);
                break;
            case 2:
                targetVelocity = new Vector2(0, -runSpeed * 10f);
                break;
            case 3:
                targetVelocity = new Vector2(-runSpeed * 10f, 0);
                break;
        }

        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
    }
    
}
