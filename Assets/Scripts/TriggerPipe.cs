using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TriggerPipe : MonoBehaviour
{
 
    public GameObject player;
    public BoxCollider2D boxCollider;
    private BoxCollider2D _playerCollider;

    public float timeAmount = 10f;
    public Image image;
    public Canvas canvas;
    private float _time;

    private Animator _playerAnimator;
    private static readonly int IsInteracting = Animator.StringToHash("IsInteracting");

    public Animator animator;
    public BoxCollider2D triggerCollider;

    void Start()
    {
        _playerCollider = player.GetComponent<BoxCollider2D>();
        _playerAnimator = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {

        if (boxCollider.IsTouching(_playerCollider))
        {
            if (Input.GetButton("FireGood"))
            {
                _playerAnimator.SetBool(IsInteracting, true);
                if (_time < timeAmount)
                {
                    _time += Time.deltaTime;
                    image.fillAmount = _time / timeAmount;
                }
                else
                {
                    canvas.gameObject.SetActive(false);
                    triggerCollider.enabled = false;
                    GameStats.GoodScore += (int)GameStats.ScoreTable.Pipes;
                }
            }
            else if (Input.GetButton("FireBad"))
            {
                _playerAnimator.SetBool(IsInteracting, true);
                if (_time < timeAmount)
                {
                    _time += Time.deltaTime;
                    image.fillAmount = _time / timeAmount;
                }
                else
                {
                    canvas.enabled = false;
                    triggerCollider.enabled = false;
                    animator.SetTrigger("IsBroken");
                    GameStats.BadScore += (int)GameStats.ScoreTable.Pipes;
                }
            }
            else
            {
                _time = 0f;
                image.fillAmount = _time / timeAmount;
                _playerAnimator.SetBool(IsInteracting, false);
                canvas.gameObject.SetActive(true);
            }
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.Equals(player))
        {
            canvas.gameObject.SetActive(false);
            _playerAnimator.SetBool(IsInteracting, false);
        }
    }
}