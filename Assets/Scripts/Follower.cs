using UnityEngine;

public class Follower : MonoBehaviour
{
    public SpriteRenderer pistolSpriteRend;
    public GameObject player;
    public GameObject actualPistol;
    public float espaceEntreJoueurEtPistolet = 0.1f;
    public Sprite gauche;
    public Sprite droite;
    private CharacterController2D _playerController;
    private int _direction = 0;

    private void Start()
    {
        _playerController = player.GetComponent<CharacterController2D>();
    }

    private void Update()
    {
        if (_direction != _playerController.derniereDir)
        {
            Vector3 something = actualPistol.transform.position;
            if (_playerController.derniereDir == 1)
            {
                pistolSpriteRend.sprite = droite;
                something.x += 2 * espaceEntreJoueurEtPistolet;
            }
            else if (_playerController.derniereDir == 3)
            {
                pistolSpriteRend.sprite = gauche;
                something.x -= 2 * espaceEntreJoueurEtPistolet;
            }
            actualPistol.transform.position = something;
            _direction = _playerController.derniereDir;
        }
    }       
}
