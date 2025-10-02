using UnityEngine;
using UnityEngine.UI;

public class DanceChallengeButton : MonoBehaviour
{
    public GameObject great1;
    public GameObject great2;
    public GameObject perfect;
    public GameObject okay;
    public GameObject miss;

    public Image thisButton;
    public Sprite idle;
    public Sprite pressing;

    public KeyCode key;

    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            GetComponent<Button>().onClick.Invoke();
            thisButton.sprite = pressing;
        }
        if (Input.GetKeyUp(key))
        {
            thisButton.sprite = idle;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thisButton = GetComponent<Image>();
    }
}
