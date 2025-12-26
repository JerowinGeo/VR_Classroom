using Unity.Collections;
using Unity.Netcode;
using UnityEngine;


public class NetworkAvatarSync_NGO : NetworkBehaviour
{
    public NetworkVariable<FixedString128Bytes> AvatarId = new NetworkVariable<FixedString128Bytes>("default_avatar");


    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            var chosen = AvatarSaveManager.LoadSelectedAvatar();
            if (string.IsNullOrEmpty(chosen)) chosen = AvatarSelectionManager.SelectedAvatarID;
            AvatarId.Value = chosen;
        }


        AvatarId.OnValueChanged += OnAvatarChanged;
    }


    void OnAvatarChanged(FixedString128Bytes oldVal, FixedString128Bytes newVal)
    {
        var app = GetComponent<ApplySelectedAvatar>();
        if (app) app.SendMessage("ApplyAvatar", newVal.ToString(), SendMessageOptions.DontRequireReceiver);
    }
}