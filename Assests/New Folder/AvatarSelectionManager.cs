using UnityEngine;


public class AvatarSelectionManager : MonoBehaviour
{
    public static string SelectedAvatarID { get; private set; } = "default_avatar";


    // Optional: store a friendly display name
    public static string SelectedAvatarName { get; private set; } = "Default";


    public void SelectAvatar(string avatarID)
    {
        SelectedAvatarID = avatarID;
        Debug.Log($"[AvatarSelection] Selected: {avatarID}");
    }


    public void SelectAvatar(string avatarID, string avatarName)
    {
        SelectedAvatarID = avatarID;
        SelectedAvatarName = avatarName;
        Debug.Log($"[AvatarSelection] Selected: {avatarID} ({avatarName})");
    }
}