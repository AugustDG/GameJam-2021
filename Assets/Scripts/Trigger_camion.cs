using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Trigger_camion : MonoBehaviour
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

    void Start()
    {
        _playerCollider = player.GetComponent<BoxCollider2D>();
        _playerAnimator = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (boxCollider.IsTouching(_playerCollider))
        {
            if (Input.GetButton("FireGood"))
            {
                _playerAnimator.SetBool("FuelingUp", true);
                if (_time < timeAmount)
                {
                    _time += Time.deltaTime;
                    image.fillAmount = _time / timeAmount;
                }
                else
                {
                    canvas.enabled = false;
                    _playerAnimator.SetBool("FuelingUp", false);
                }
            }
            else if (Input.GetButton("FireBad"))
            {
                _playerAnimator.SetBool("FuelingUp", true);
                if (_time < timeAmount)
                {
                    _time += Time.deltaTime;
                    image.fillAmount = _time / timeAmount;
                }
                else
                {
                    canvas.enabled = false;
                    _playerAnimator.SetBool("FuelingUp", false);
                }
            }
            else
            {
                _time = 0f;
                image.fillAmount = _time / timeAmount;
                _playerAnimator.SetBool("FuelingUp", false);
                text.text = "Q pour faire le plein\nE pour verser de l'essence par terre";
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