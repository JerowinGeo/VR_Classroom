using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginUI : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_Dropdown roleDropdown;

    public void OnLoginPressed()
    {
        if (string.IsNullOrWhiteSpace(usernameInput.text))
        {
            Debug.Log("Username is required!");
            return;
        }

        PlayerData.Username = usernameInput.text;
        PlayerData.Role = roleDropdown.options[roleDropdown.value].text;

        // Move to avatar selection screen
        SceneManager.LoadScene("AvatarSelectionScene");
    }
}
