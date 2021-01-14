using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TriggerMailDest : MonoBehaviour
{
    public TMP_Text text;
    public GameObject player;
    public BoxCollider2D boxCollider;
    public BoxCollider2D colliderLettresAutres;
    private BoxCollider2D _playerCollider;

    public Canvas canvas;

    public GameObject lettres;
    public GameObject lettresArtistiquesPasCellesQuiTeSuivent;
    public SpriteRenderer poubelleRenderer;
    public Sprite poubellePleine;

    private void Start()
    {
        _playerCollider = player.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {

        if (boxCollider.IsTouching(_playerCollider) && lettres.activeSelf)
        {
            if (Input.GetButton("FireGood"))
            {
                lettres.SetActive(false);
                canvas.enabled = false;
                Vector3 newPos = new Vector3(-9.97f, -5.22f, 0f);
                Vector3 newScale = new Vector3(0.76f, 0.76f, 0.76f);
                lettresArtistiquesPasCellesQuiTeSuivent.transform.position = newPos;
                lettresArtistiquesPasCellesQuiTeSuivent.transform.localScale = newScale;
                lettresArtistiquesPasCellesQuiTeSuivent.GetComponent<SpriteRenderer>().enabled = true;
                GameStats.GoodScore += (int)GameStats.ScoreTable.Mail;
                boxCollider.enabled = false;
                colliderLettresAutres.enabled = false;
                player.GetComponent<Animator>().SetBool("HasPickedUp", false);
            }
            else if (Input.GetButton("FireBad"))
            {
                lettres.SetActive(false);
                canvas.enabled = false;
                poubelleRenderer.sprite = poubellePleine;
                GameStats.BadScore += (int)GameStats.ScoreTable.Mail;
                boxCollider.enabled = false;
                colliderLettresAutres.enabled = false;
                player.GetComponent<Animator>().SetBool("HasPickedUp", false);
            }
            else
            {
                text.text = "Q pour déposer le courrier\nE pour le jeter à la poubelle";
            }
        }
        else if (boxCollider.IsTouching(_playerCollider))
        {
            text.text = "Il vous manque les lettres!";
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.Equals(player))
        {
            text.text = "";
        }
    }
}