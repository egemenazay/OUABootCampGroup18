using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class MyNetworkRoomPlayer : NetworkRoomPlayer
{
    [SyncVar]
    public int characterIndex = -1;

    public void SelectCharacter(int index)
    {
        if (isLocalPlayer)
        {
            CmdSelectCharacter(index);
        }
    }

    [Command]
    void CmdSelectCharacter(int index)
    {
        characterIndex = index;
    }
}
