using System.Collections.Generic;
using UnityEngine;

public class DanceChallengeLogic : MonoBehaviour
{
    public List<GameObject> LeftArrows = new();
    public List<GameObject> RightArrows = new();
    public List<GameObject> UpArrows = new();
    public List<GameObject> DownArrows = new();

    public void HitArrow()
    {
        print("Hit");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
