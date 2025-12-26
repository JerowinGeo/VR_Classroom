using UnityEngine;


public class AvatarCustomizationUI : MonoBehaviour
{
    public ApplySelectedAvatar targetApply; // optional live preview target


    public void SetColor(Color c)
    {
        // assume avatar root has "Body" mesh with Material
        var app = targetApply;
        if (app == null) return;
        var root = app.avatarRoot;
        if (!root) return;


        var body = root.transform.Find("Body");
        if (body)
        {
            var mr = body.GetComponent<MeshRenderer>();
            if (mr) mr.material.color = c;
        }
    }


    public void ToggleAccessory(string accessoryName, bool on)
    {
        var root = targetApply.avatarRoot;
        if (!root) return;
        var acc = root.transform.Find(accessoryName);
        if (acc) acc.gameObject.SetActive(on);
    }
}