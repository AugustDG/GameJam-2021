using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TriggerCamion : MonoBehaviour
{
    public TMP_Text text;
    public GameObject player;
    public BoxCollider2D boxCollider;
    private BoxCollider2D _playerCollider;

    public float timeAmount = 10f;
    public Image image;
    public Canvas canvas;
    private float _time;

    private Animator _playerAnimator;

    public TriggerPistol triggerPistolScript;
    public GameObject actualPistol;
    private bool _fueledUp = false;

    private void Start()
    {
        _playerCollider = player.GetComponent<BoxCollider2D>();
        _playerAnimator = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!_fueledUp)
        {
            if (boxCollider.IsTouching(_playerCollider) && triggerPistolScript.pistolPickedUp)
            {
                Debug.Log("We're in");
                if (Input.GetButton("FireGood"))
                {
                    _playerAnimator.SetBool("FuelingUp", true);
                    if (_time < timeAmount)
                    {
                        actualPistol.SetActive(false);
                        _time += Time.deltaTime;
                        image.fillAmount = _time / timeAmount;
                    }
                    else
                    {
                        _fueledUp = true;
                        actualPistol.SetActive(true);
                        canvas.enabled = false;
                        _playerAnimator.SetBool("FuelingUp", false);
                    }
                }
                else if (Input.GetButton("FireBad"))
                {
                    _playerAnimator.SetBool("FuelingUp", true);
                    if (_time < timeAmount)
                    {
                        actualPistol.SetActive(false);
                        _time += Time.deltaTime;
                        image.fillAmount = _time / timeAmount;
                    }
                    else
                    {
                        _fueledUp = true;
                        actualPistol.SetActive(true);
                        canvas.enabled = false;
                        _playerAnimator.SetBool("FuelingUp", false);
                    }
                }
                else
                {
                    actualPistol.SetActive(true);
                    _time = 0f;
                    image.fillAmount = _time / timeAmount;
                    _playerAnimator.SetBool("FuelingUp", false);
                    text.text = "Q pour faire le plein\nE pour verser de l'essence par terre";
                }
            }
            else if (boxCollider.IsTouching(_playerCollider))
            {
                text.text = "Il vous manque le pistolet de carburant!";
            }
            else if (!boxCollider.IsTouching(_playerCollider))
            {
                _time = 0f;
                image.fillAmount = _time / timeAmount;
                text.text = "";
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