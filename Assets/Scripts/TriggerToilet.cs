using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TriggerToilet : MonoBehaviour
{
    public TMP_Text text;
    public GameObject player;
    public BoxCollider2D boxCollider;
    private BoxCollider2D _playerCollider;

    public float timeAmount = 10f;
    private float _time = 0;
    public Image image;
    public Canvas canvas;

    void Start()
    {
        _playerCollider = player.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (boxCollider.IsTouching(_playerCollider))
        {
            if (Input.GetButton("FireGood"))
            {
                if (_time < timeAmount)
                {
                    _time += Time.deltaTime;
                    image.fillAmount = _time / timeAmount;
                }
                else
                {
                    canvas.enabled = false;
                }
            }
            else if (Input.GetButton("FireBad"))
            {
                if (_time < timeAmount)
                {
                    _time += Time.deltaTime;
                    image.fillAmount = _time / timeAmount;
                }
                else
                {
                    canvas.enabled = false;
                }
            }
            else
            {
                _time = 0f;
                image.fillAmount = _time / timeAmount;
                text.text = "Q pour fermer le robinet\nE pour boucher le lavabo";
            }
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