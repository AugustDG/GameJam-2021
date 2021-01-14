using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public SpriteRenderer pistolSpriteRend;
    public GameObject player;
    public GameObject actualPistol;
    public float espaceEntreJoueurEtPistolet = 0.1f;
    public Sprite gauche;
    public Sprite droite;
    private CharacterController2D playerController;
    private int direction = 0;
    void Start()
    {
        playerController = player.GetComponent<CharacterController2D>();
    }

    void Update()
    {
        if (direction != playerController.derniereDir)
        {
            Vector3 something = actualPistol.transform.position;
            if (playerController.derniereDir == 1)
            {
                pistolSpriteRend.sprite = droite;
                something.x += 2 * espaceEntreJoueurEtPistolet;
            }
            else if (playerController.derniereDir == 3)
            {
                pistolSpriteRend.sprite = gauche;
                something.x -= 2 * espaceEntreJoueurEtPistolet;
            }
            actualPistol.transform.position = something;
            direction = playerController.derniereDir;
        }
    }       
}
