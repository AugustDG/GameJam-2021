using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovement : MonoBehaviour
{
    public Trigger_CSplitter splitter;
    public int direction = 2; //0 up, 1 right, 2 bottom, 3 left
    public Rigidbody2D m_Rigidbody2D;
    public float boxSpeed = 10f;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
    private Vector3 m_Velocity = Vector3.zero;
    
    public bool isDecided = false;

    void Update()
    {
        if (splitter.isActivated)
        {
            //checker si le highlight est on, s'il ne l'est pas, on le turn on, et apres on fait un else (s'il n'est pas activé) et on desactive SI il est activé


            if (Input.GetButtonDown("FireGood"))
            {

            }
            else if (Input.GetButtonDown("FireBad"))
            {

            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetVelocity = new Vector2(0, 0);
        switch (direction)
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
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
    }
   
}
