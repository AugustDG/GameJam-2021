using DG.Tweening;
using UnityEngine;

public class RotateContinusously : MonoBehaviour
{
    public float speed = 2f;
    
    // Update is called once per frame
    void Update()
    {
        var rectTrans = GetComponent<RectTransform>();
        rectTrans.DOLocalRotate(rectTrans.rotation.eulerAngles + new Vector3(0, 0, 1f) * speed * Time.deltaTime, Time.deltaTime,
            RotateMode.FastBeyond360).Play();
    }
}