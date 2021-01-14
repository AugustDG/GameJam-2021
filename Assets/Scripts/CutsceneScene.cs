using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneScene : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            DOTween.KillAll();
            SceneManager.LoadSceneAsync(2);
        }
    }
}
