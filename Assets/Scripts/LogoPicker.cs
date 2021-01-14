using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoPicker : MonoBehaviour
{
    public Sprite logo;
    public GameObject[] places;

    void Start()
    {
        places[Random.Range(0, places.Length)].GetComponent<SpriteRenderer>().sprite = logo;
    }

}
