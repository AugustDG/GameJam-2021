using System.Collections;
using Cinemachine;
using DG.Tweening;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private RectTransform upTransform;
    [SerializeField] private RectTransform downTransform;
    
    private TMP_Text _textComp;
    private CinemachineVirtualCamera _mainVirtualCam;

    public void Awake()
    {
        GameEvents.CutsceneStarted += ActivateCutscene;
    }

    public void Start()
    {
        _textComp = GetComponent<TMP_Text>();
        _mainVirtualCam = Camera.current.GetComponent<CinemachineBrain>().ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();
    }

    private void ActivateCutscene(object sender, string[] dial)
    {
        DOTween.To(() => _mainVirtualCam.m_Lens.OrthographicSize, x => _mainVirtualCam.m_Lens.OrthographicSize = x, 2f,
            2).OnComplete(()=> StartCoroutine(CycleCutscene(dial)));

        upTransform.DoAnchorPosY(upTransform.anchoredPosition.y - 50, 2f, true);
        downTransform.DoAnchorPosY(downTransform.anchoredPosition.y + 50, 2f, true);

        DOTween.PlayAll();
    }

    private IEnumerator CycleCutscene(string[] dial)
    {
        var iteration = 0;
        
        yield return new WaitForSeconds(0.1f);

        while (iteration < dial.Length)
        {
            _textComp.text += dial[iteration];
            iteration++;

            yield return new WaitForSeconds(2f);
        }
    }
}
