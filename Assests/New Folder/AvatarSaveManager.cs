using UnityEngine;


public static class AvatarSaveManager
{
    private const string PREF_KEY = "selected_avatar_id_v1";


    public static void SaveSelectedAvatar()
    {
        PlayerPrefs.SetString(PREF_KEY, AvatarSelectionManager.SelectedAvatarID);
        PlayerPrefs.Save();
        Debug.Log($"[AvatarSave] Saved {AvatarSelectionManager.SelectedAvatarID}");
    }


    public static string LoadSelectedAvatar()
    {
        if (PlayerPrefs.HasKey(PREF_KEY)) return PlayerPrefs.GetString(PREF_KEY);
        return null;
    }


    public static void Clear()
    {
        PlayerPrefs.DeleteKey(PREF_KEY);
    }
}