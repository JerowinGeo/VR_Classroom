using UnityEngine;
using System.Collections.Generic;


public class AvatarPreviewController : MonoBehaviour
{
    [System.Serializable]
    public struct PreviewItem { public string id; public GameObject avatarPrefab; }


    public List<PreviewItem> previews = new List<PreviewItem>();


    // Assign the preview points (matching the previews list length) in inspector
    public Transform[] previewPoints;


    void Start()
    {
        for (int i = 0; i < previews.Count && i < previewPoints.Length; i++)
        {
            var p = previews[i];
            if (p.avatarPrefab == null) continue;
            var inst = Instantiate(p.avatarPrefab, previewPoints[i], false);
            inst.transform.localPosition = Vector3.zero;
            inst.transform.localRotation = Quaternion.identity;


            // disable runtime-only components if necessary (network, input)
            // Use a reflection-safe approach: search for any Behaviour named 'NetworkIdentity' and disable it if found.
            var behaviours = inst.GetComponentsInChildren<Behaviour>(true);
            foreach (var b in behaviours)
            {
                if (b == null) continue;
                var tname = b.GetType().Name;
                if (tname == "NetworkIdentity" || b.GetType().FullName == "UnityEngine.Networking.NetworkIdentity")
                {
                    b.enabled = false;
                    break;
                }
            }
        }
    }


    void Update()
    {
        // simple rotate for all preview points
        foreach (var pt in previewPoints)
            if (pt) pt.Rotate(Vector3.up, 10f * Time.deltaTime, Space.World);
    }
}