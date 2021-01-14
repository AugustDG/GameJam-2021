using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TriggerCamion : MonoBehaviour
{
    public GameObject canvas;
    public GameObject player;
    public BoxCollider2D boxCollider;
    private BoxCollider2D _playerCollider;

    public float timeAmount = 10f;
    public Image image;
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
                        GameStats.GoodScore += (int)GameStats.ScoreTable.Fuel;
                        _fueledUp = true;
                        actualPistol.SetActive(true);
                        canvas.SetActive(false);
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
                        GameStats.BadScore += (int)GameStats.ScoreTable.Fuel;
                        _fueledUp = true;
                        actualPistol.SetActive(true);
                        canvas.SetActive(false);
                        _playerAnimator.SetBool("FuelingUp", false);
                    }
                }
                else
                {
                    actualPistol.SetActive(true);
                    _time = 0f;
                    image.fillAmount = _time / timeAmount;
                    _playerAnimator.SetBool("FuelingUp", false);
                    canvas.SetActive(true);
                }
            }
            else if (!boxCollider.IsTouching(_playerCollider))
            {
                _time = 0f;
                image.fillAmount = _time / timeAmount;
                canvas.SetActive(false);
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