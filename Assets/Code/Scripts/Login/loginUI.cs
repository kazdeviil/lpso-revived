using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class loginUI : MonoBehaviour
{

    public TMP_InputField usernameField;
    public TMPro.TextMeshProUGUI placeholderUsername;
    public TMPro.TextMeshProUGUI username;

    public Button loginButton;

    public void CheckUsername()
    {
        if (usernameField.text.Length > 0)
        {
            Debug.Log(username.text);
            GameDataManager.Instance.displayName = username.text;
            LoginSuccess();
        }
        else
        {
            placeholderUsername.text = "Please enter a username";
            placeholderUsername.color = new Color32(253, 33, 33, 255);
        }
    }

    public void LoginSuccess()
    {
        SceneManager.LoadScene("CreateAPet");
    }

}
