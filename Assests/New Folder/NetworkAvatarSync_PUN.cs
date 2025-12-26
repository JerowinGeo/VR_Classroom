using Photon.Pun;
using UnityEngine;


public class NetworkAvatarSync_PUN : MonoBehaviourPun
{
    public string avatarId;


    void Start()
    {
        if (photonView.IsMine)
        {
            avatarId = AvatarSaveManager.LoadSelectedAvatar();
            if (string.IsNullOrEmpty(avatarId)) avatarId = AvatarSelectionManager.SelectedAvatarID;


            photonView.RPC("RPC_SetAvatar", RpcTarget.AllBuffered, avatarId);
        }
    }


    [PunRPC]
    void RPC_SetAvatar(string id)
    {
        avatarId = id;
        // apply visuals locally (could call ApplySelectedAvatar.ApplyAvatar via reference)
        var app = GetComponent<ApplySelectedAvatar>();
        if (app) app.SendMessage("ApplyAvatar", id, SendMessageOptions.DontRequireReceiver);
    }
}