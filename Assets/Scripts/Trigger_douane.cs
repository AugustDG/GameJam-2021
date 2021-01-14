using TMPro;
using UnityEngine;

public class TriggerDouane : MonoBehaviour
{
    public BoxCollider2D mur;
    public BoxCollider2D tourelle;
    public BoxCollider2D playerCollider;
    public GameObject actualPistol;
    public TMP_Text text;

    private void Update()
    {
        if (playerCollider.IsTouching(tourelle) && actualPistol.activeSelf)
        {
            mur.enabled = true;
            text.text = "Veuillez ranger le pistolet de carburant";
        } else
        {
            mur.enabled = false;
            text.text = "";
        }
    }
}
