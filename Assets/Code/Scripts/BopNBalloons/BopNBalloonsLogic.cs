using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BopNBalloonsLogic : MonoBehaviour
{
    public GameObject balloon;
    public Transform balloonParent;
    public GameObject[] GridX;
    public GameObject[] GridY;
    public Sprite[] balloons;
    int balloonID = 0;

    private void Start()
    {
        balloonID = 0;
    }

    public void SpawnBalloons()
    {
        Debug.Log("Balloon ID default " + balloonID);
        for (int y = 0; y < GridY.Length; y++)
        {
            for (int x = 0; x < GridX.Length; x++)
            {
                Vector3 instPoint = new(GridX[x].transform.position.x, GridY[y].transform.position.y, 0);
                GameObject newballoon = balloon;
                Instantiate(newballoon, instPoint, Quaternion.identity, balloonParent);
                int newID = balloonID;
                newballoon.GetComponent<BnBBalloon>().balloonID = newID;
                Debug.Log(newID + ". Instantiated balloon " + newballoon.GetComponent<BnBBalloon>().balloonID);
                balloonID++;
            }
        }
    }
}
