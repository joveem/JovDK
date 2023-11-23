// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// third
using Epic.OnlineServices;
using TMPro;

// from company
using JovDK.Debug;
using JovDK.SafeActions;
using JovDK.SerializingTools.Bson;
using JovDK.SerializingTools.Json;
using PlayEveryWare.EpicOnlineServices;

// from project
// ...


public partial class EosP2PTestingShowcase : MonoBehaviour
{
    void SetupButtons()
    {
        _joinButton.SetOnClickIfNotNull(JoinButton);
        _copyLocalIdButton.SetOnClickIfNotNull(CopyUserIdButton);
        _sendMessageButton.SetOnClickIfNotNull(SendMessageButton);
    }

    void JoinButton()
    {
        DebugExtension.DevLog("#> ".ToColor(GoodColors.Orange) + "JoinButton");

        TryToConnect();
    }

    void CopyUserIdButton()
    {
        DebugExtension.DevLog("#> ".ToColor(GoodColors.Orange) + "CopyUserIdButton");
        ProductUserId localProductUserId = EOSManager.Instance.GetProductUserId();

        if (localProductUserId != null)
            GUIUtility.systemCopyBuffer = localProductUserId.ToString();
        else
        {
            string debugText =
                "ERROR Trying to CopyUserId!" + "\n" +
                "localProductUserId IS NULL!" +
                "";
            DebugExtension.DevLogError(debugText);
        }
    }

    void SendMessageButton()
    {
        DebugExtension.DevLog("#> ".ToColor(GoodColors.Orange) + "SendMessageButton");

        UpdateMembersList();
        SendMessageToPlayersList();
    }

}
