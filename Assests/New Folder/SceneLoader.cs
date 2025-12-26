using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    public string sceneToLoad = "VRMultiplayerRoom";


    public void LoadMainScene()
    {
        // Save before load
        AvatarSaveManager.SaveSelectedAvatar();
        SceneManager.LoadScene(sceneToLoad);
    }
}