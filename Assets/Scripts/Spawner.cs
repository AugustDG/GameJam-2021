using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Spawner : MonoBehaviour
{
    public GameObject boxPrefab;
    public AudioClip audioClip;
    public int boxesPerWave = 10;
    public float timeBetweenWaves = 15;
    public float timeBetweenBoxes = 1f;
    public float timeBeforeFirst = 2f;
    private float lastTime = 0f;
    private bool firstPassed = false;

    public int boxesLeft = 0;
    void Start()
    {
        lastTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (((!firstPassed && lastTime + timeBeforeFirst < Time.time) || (firstPassed && lastTime + timeBetweenWaves < Time.time)) && boxesLeft <= 0)
        {
            firstPassed = true;
            StartCoroutine(Wave());
            lastTime = Time.time;
            boxesLeft = boxesPerWave;
            Siren();
        }
    }

    IEnumerator Wave()
    {
        for (int i = 0; i < boxesPerWave; i++)
        {
            GameObject newBox = Instantiate(boxPrefab) as GameObject;
            newBox.transform.position = this.transform.position;
            yield return new WaitForSeconds(timeBetweenBoxes);
        }
    }

    void Siren()
    {
        GetComponent<AudioSource>().PlayOneShot(audioClip, 0.5f);
    }
}
