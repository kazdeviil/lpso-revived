using System.Collections.Generic;
using UnityEngine;

public class GuppyLoad : MonoBehaviour
{
    [SerializeField] private GameObject waveStill;
    [SerializeField] private GameObject waveMoving;

    [SerializeField] private float waveSpeed;
    [SerializeField] private float waveMaxHeight;
    [SerializeField] private float waveMinHeight;
    [SerializeField] private float wavePos;

    [SerializeField] private float waveLayerSpeed;
    [SerializeField] private int offscreenPos;

    [SerializeField] private float waveLayerBobSpeed;
    [SerializeField] private float waveLayerMaxHeight;
    [SerializeField] private float waveLayerMinHeight;
    [SerializeField] private float waveLayerPos;

    [SerializeField] private List<GameObject> topWaveParts = new List<GameObject>();
    [SerializeField] private List<GameObject> bottomWaveParts = new List<GameObject>();
    [SerializeField] private float waveLength;


    private void Start()
    {
        waveLength = topWaveParts[topWaveParts.Count - 1].transform.Find("wave main").GetComponent<SpriteRenderer>().bounds.size.x;
        wavePos = waveMoving.transform.position.y;
        waveMinHeight = wavePos;
    }
    private void Update()
    {
        // WaveLayerBobbing();
        // WaveBobbing();
        // WaveLooping();
    }

    void WaveBobbing()
    {
        if (waveSpeed > 0)
        {
            if (wavePos < waveMaxHeight)
            {
                wavePos += waveSpeed * Time.deltaTime;
            }
            else
            {
                waveSpeed *= -1;
            }
        }
        else
        {
            if (wavePos > waveMinHeight)
            {
                wavePos += waveSpeed * Time.deltaTime;
            }
            else
            {
                waveSpeed *= -1;
            }
        }
        waveMoving.transform.position = new Vector3 (waveMoving.transform.position.x, wavePos, waveMoving.transform.position.z);
    }

    void WaveLooping()
    {
        transform.position = new Vector3(transform.position.x - (waveLayerSpeed * Time.deltaTime), transform.position.y, transform.position.z);
        if (topWaveParts[0].transform.position.x < offscreenPos)
        {
            GameObject wave = topWaveParts[0];
            wave.transform.position = new Vector3 (topWaveParts[topWaveParts.Count - 1].transform.position.x + waveLength, wave.transform.position.y, wave.transform.position.z);
            topWaveParts.RemoveAt(0);
            topWaveParts.Add(wave);

            GameObject wave2 = bottomWaveParts[0];
            wave2.transform.position = new Vector3(bottomWaveParts[bottomWaveParts.Count - 1].transform.position.x + waveLength, wave2.transform.position.y, wave2.transform.position.z);
            bottomWaveParts.RemoveAt(0);
            bottomWaveParts.Add(wave2);
        }
    }

    void WaveLayerBobbing()
    {
        if (waveLayerBobSpeed > 0)
        {
            if (waveLayerPos < waveLayerMaxHeight)
            {
                waveLayerPos += waveLayerBobSpeed * Time.deltaTime;
            }
            else
            {
                waveLayerBobSpeed *= -1;
            }
        }
        else
        {
            if (waveLayerPos > waveLayerMinHeight)
            {
                waveLayerPos += waveLayerBobSpeed * Time.deltaTime;
            }
            else
            {
                waveLayerBobSpeed *= -1;
            }
        }
        transform.position = new Vector3(transform.position.x, waveLayerPos, transform.position.z);
    }

}
