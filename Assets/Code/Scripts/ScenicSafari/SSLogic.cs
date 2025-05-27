using System;
using System.Collections;
using UnityEngine;

public class SSLogic : MonoBehaviour
{
    public event EventHandler OnSnapPhoto;

    public int currentPhotos;
    public int maxPhotos;

    public bool busy;

    private void Awake()
    {
        busy = false;
        maxPhotos = 12;
        currentPhotos = 0;

        OnSnapPhoto += SnapPhoto;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnSnapPhoto?.Invoke(this, EventArgs.Empty);
        }
    }

    private void SnapPhoto(object sender, EventArgs e)
    {
        if (!busy)
        {
            Debug.Log("Not busy!!");
            busy = true;
            currentPhotos++;
            if (currentPhotos == maxPhotos)
            {
                Debug.Log("Yahoo");
            }
        }
        else
        {
            Debug.Log("Whoops, busy");
        }
    }
}
