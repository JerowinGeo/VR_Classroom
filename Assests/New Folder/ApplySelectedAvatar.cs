using UnityEngine;


public class ApplySelectedAvatar : MonoBehaviour
{
    [Header("Reference to the OvrAvatar2Entity component on the player prefab")]
    public UnityEngine.GameObject avatarRoot; // root that contains OvrAvatar2Entity


    void Awake()
    {
        // If saved to PlayerPrefs, prefer that
        string chosen = AvatarSaveManager.LoadSelectedAvatar();
        if (string.IsNullOrEmpty(chosen)) chosen = AvatarSelectionManager.SelectedAvatarID;


        ApplyAvatar(chosen);
    }


    void ApplyAvatar(string id)
    {
        if (string.IsNullOrEmpty(id)) return;


        // This depends on how you've built avatars. Two patterns:
        // 1) Each id corresponds to a prefab under Resources/Avatars/<id>
        // 2) Use OvrAvatar API to load presets


        // Example 1: instantiate prefab under avatarRoot
        var prefab = Resources.Load<GameObject>("Avatars/" + id);
        if (prefab != null && avatarRoot != null)
        {
            // remove existing children
            for (int i = avatarRoot.transform.childCount - 1; i >= 0; i--) Destroy(avatarRoot.transform.GetChild(i).gameObject);


            var inst = Instantiate(prefab, avatarRoot.transform);
            inst.transform.localPosition = Vector3.zero;
            inst.transform.localRotation = Quaternion.identity;
            return;
        }


        // Example 2: If using OvrAvatar2 API (don't reference SDK types directly so project compiles when SDK absent)
        if (avatarRoot != null)
        {
            // Search for a component whose type name looks like an Ovr avatar driver and try to call LoadPreset(string)
            Component found = null;
            var comps = avatarRoot.GetComponentsInChildren<Component>(true);
            foreach (var c in comps)
            {
                if (c == null) continue;
                var tname = c.GetType().Name;
                if (tname.Contains("OvrAvatarDriver") || tname.Contains("OvrAvatar") || tname.Contains("OvrAvatar2"))
                {
                    found = c;
                    break;
                }
            }

            if (found != null)
            {
                var mi = found.GetType().GetMethod("LoadPreset", new[] { typeof(string) });
                if (mi != null)
                {
                    try
                    {
                        mi.Invoke(found, new object[] { id });
                        return;
                    }
                    catch (System.Exception ex)
                    {
                        Debug.LogWarning("ApplySelectedAvatar: failed to invoke LoadPreset on " + found.GetType().Name + ": " + ex.Message);
                    }
                }
                else
                {
                    Debug.Log("ApplySelectedAvatar: found avatar component '" + found.GetType().Name + "' but no LoadPreset(string) method.");
                    return;
                }
            }
        }


        Debug.LogWarning("ApplySelectedAvatar: no prefab or API found for id=" + id);
    }
}