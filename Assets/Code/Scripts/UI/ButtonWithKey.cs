using UnityEngine;
using UnityEngine.UI;

public class ButtonWithKey : MonoBehaviour
{
    public KeyCode key;

    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            GetComponent<Button>().onClick.Invoke();
        }
    }
}
