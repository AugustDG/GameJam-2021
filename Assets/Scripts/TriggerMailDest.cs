using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TriggerMailDest : MonoBehaviour
{
    public GameObject canvas;
    public GameObject player;
    public BoxCollider2D boxCollider;
    public BoxCollider2D colliderLettresAutres;
    private BoxCollider2D _playerCollider;

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
                GameStats.GoodScore += (int)GameStats.ScoreTable.Mail;
                lettres.SetActive(false);
                canvas.SetActive(false);
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
                GameStats.BadScore += (int)GameStats.ScoreTable.Mail;
                lettres.SetActive(false);
                canvas.SetActive(false);
                poubelleRenderer.sprite = poubellePleine;
                GameStats.BadScore += (int)GameStats.ScoreTable.Mail;
                boxCollider.enabled = false;
                colliderLettresAutres.enabled = false;
                player.GetComponent<Animator>().SetBool("HasPickedUp", false);
            }
            else
            {
                canvas.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.Equals(player))
        {
            canvas.SetActive(false);
        }
    }
}