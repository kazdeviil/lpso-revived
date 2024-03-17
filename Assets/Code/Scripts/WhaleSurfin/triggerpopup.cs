using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class triggerpopup : MonoBehaviour
{
    public GameObject text;
    public float texttimer;
    public float resettimer = 10;
    public string[] happyWords = new string[]
    {
        "Doing Great!",
        "Keep It Up!",
        "Fantastic!",
        "So Agile!",
        "Keep Going!",
        "Wow!",
        "Way to go!",
        "Balance Master!"
    };

    private void Start()
    {
        texttimer = 10;
    }

    void Update()
    {
        texttimer -= Time.deltaTime;
        if (texttimer <= 8)
        {
            text.GetComponent<TextMeshProUGUI>().text = "";
        }
        if (texttimer <= 0 )
        {
            ChangeText();
            texttimer = resettimer;
        }
    }

    void ChangeText()
    {
        text.GetComponent<TextMeshProUGUI>().text = happyWords[Random.Range(0, happyWords.Length)];
    }
}
