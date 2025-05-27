using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class SSCamera : MonoBehaviour
{
    public SSLogic logic;
    
    public Camera mainCam;
    public Camera playerCam;
    public GameObject camPreview;
    public RawImage image;

    public TextMeshProUGUI photoTxt;
    public string photoString;
    public int prevPhotoCount;

    public int camCullMask;
    public CameraClearFlags camClearFlags;


    Vector3 downLeftWorld = new Vector3(-6.1f, -3.3f, 0f);
    Vector3 upRightWorld = new Vector3(6.1f, 3.3f, 0f);
    Vector3 downLeftScreen = new Vector3();
    Vector3 upRightScreen = new Vector3();

    private void Start()
    {
        logic = GetComponent<SSLogic>();
        logic.OnSnapPhoto += Logic_OnSnapPhoto;
        downLeftScreen = mainCam.WorldToScreenPoint(downLeftWorld);
        upRightScreen = mainCam.WorldToScreenPoint(upRightWorld);
        UpdateString();
        prevPhotoCount = logic.currentPhotos;
    }

    private void Logic_OnSnapPhoto(object sender, EventArgs e)
    {
        if (!logic.busy)
        {
            Debug.Log("Snap!");
            image.color = new Color(1, 1, 1, 0);
            camCullMask = playerCam.cullingMask;
            camClearFlags = playerCam.clearFlags;
            playerCam.cullingMask = 0;
            playerCam.clearFlags = CameraClearFlags.Nothing;
            UpdateString();
            StartCoroutine(nameof(CameraClick));
        }
    }

    private void Update()
    {
        MoveCamera();
    }

    void MoveCamera()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -20);
        if (mousePos.x <= upRightScreen.x && mousePos.x >= downLeftScreen.x)
        {
            Vector3 posXCam = new Vector3(mainCam.ScreenToWorldPoint(mousePos).x, playerCam.transform.position.y, playerCam.transform.position.z);
            Vector3 posXPrev = new Vector3(Input.mousePosition.x, camPreview.transform.position.y, camPreview.transform.position.z);
            playerCam.transform.position = posXCam;
            camPreview.transform.position = posXPrev;
        }
        if (mousePos.y <= upRightScreen.y && mousePos.y >= downLeftScreen.y)
        {
            Vector3 posYCam = new Vector3(playerCam.transform.position.x, mainCam.ScreenToWorldPoint(mousePos).y, playerCam.transform.position.z);
            Vector3 posYPrev = new Vector3(camPreview.transform.position.x, Input.mousePosition.y, camPreview.transform.position.z);
            playerCam.transform.position = posYCam;
            camPreview.transform.position = posYPrev;
        }
    }

    public IEnumerator CameraClick()
    {
        Debug.Log("Started click");
        yield return new WaitForSeconds(0.1f);
        image.color = Color.white;
        yield return new WaitForSeconds(1);
        playerCam.cullingMask = camCullMask;
        playerCam.clearFlags = camClearFlags;
        logic.busy = false;
        yield break;
    }

    void UpdateString()
    {
        photoString = $"{logic.currentPhotos}/{logic.maxPhotos}";
        photoTxt.text = photoString;
    }
}
