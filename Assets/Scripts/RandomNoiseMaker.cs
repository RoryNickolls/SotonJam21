using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNoiseMaker : MonoBehaviour
{
    [SerializeField]
    private float minTime = 4f;

    [SerializeField] private float maxTime = 15f;

    private float time;
    private float timer = 0.0f;

    private void Start()
    {
        time = Random.Range(minTime, maxTime);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= time)
        {
            timer = 0.0f;
            GetComponent<AudioSource>().Play();
            time = Random.Range(minTime, maxTime);
        }
    }
}
