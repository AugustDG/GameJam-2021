using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eater : MonoBehaviour
{
    public bool eatsLeft = true;
    public Spawner spawner;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            spawner.boxesLeft--;
            if (other.gameObject.GetComponent<Info>().left == eatsLeft)
            {
                //yum
                GameStats.GoodScore += (int)GameStats.ScoreTable.Box;
            }
            else
            {
                //eyk
                GameStats.BadScore += (int)GameStats.ScoreTable.Box;
            }
            Destroy(other.transform.parent.gameObject);
        }
    }
}
