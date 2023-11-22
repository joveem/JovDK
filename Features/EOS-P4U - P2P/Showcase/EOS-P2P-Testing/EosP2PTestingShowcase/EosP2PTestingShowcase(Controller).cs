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
using Epic.OnlineServices.Auth;
using PlayEveryWare.EpicOnlineServices;
using TMPro;

// from company
using JovDK.Debug;
using JovDK.SafeActions;
using JovDK.SerializingTools.Bson;
using JovDK.SerializingTools.Json;
using JovDK.Networking.Realtime;

// from project
// ...


public partial class EosP2PTestingShowcase : MonoBehaviour
{
    IGenericRealtimeServer GetRealtimeServer()
    {
        IGenericRealtimeServer value = (IGenericRealtimeServer)_realtimeServer;
        return value;
    }

    void TryToConnect()
    {
        string username = _authToolAddressInputField.text;
        string password = _usernameInputField.text;

        string debugText =
            "> ".ToColor(GoodCollors.orange) +
            "TryToConnect" + "\n" +
            "username = " + username + "\n" +
            "password = " + password + "\n" +
            "";
        DebugExtension.DevLog(debugText);

        RealtimeServerConnectionParameters connectionParameters = new RealtimeServerConnectionParameters();

        connectionParameters.ServerType = "EosP2P";
        connectionParameters.LocalUserName = username;
        connectionParameters.LocalUserCredential = password;

        GetRealtimeServer().Connect(connectionParameters);
    }



    void UpdateMembersList()
    {
        string rawDestinationIdsListText = _messageDestinationIdsListInputField.text;

        List<ProductUserId> productUserIdsList = GetProductUserIdsList(rawDestinationIdsListText);
        List<string> idsList = new List<string>();

        foreach (ProductUserId productUserId in productUserIdsList)
            idsList.Add(productUserId.ToString());

        List<GenericNetworkMember> membersList = new List<GenericNetworkMember>();

        foreach (string userId in idsList)
        {
            GenericNetworkMember genericNetworkMember = new GenericNetworkMember(userId);
            membersList.Add(genericNetworkMember);
        }

        GetRealtimeServer().UpdateMembersList(membersList);
    }

    void SendMessageToPlayersList()
    {
        string rawDestinationIdsListText = _messageDestinationIdsListInputField.text;

        List<ProductUserId> productUserIdsList = GetProductUserIdsList(rawDestinationIdsListText);
        List<string> idsList = new List<string>();

        foreach (ProductUserId productUserId in productUserIdsList)
            idsList.Add(productUserId.ToString());

        string messageContent = _messageContentInputField.text;
        byte[] rawMessage = Encoding.UTF8.GetBytes(messageContent);

        GetRealtimeServer().EmitToUsersList(idsList, rawMessage);

        string title = "You";
        string content = messageContent;

        ProductUserId localProductUserId = EOSManager.Instance.GetProductUserId();

        if (localProductUserId != null)
            title += " " + localProductUserId.ToString().ToShortId();
        else
        {
            string debugText =
                "ERROR Trying to CopyUserId!" + "\n" +
                "localProductUserId IS NULL!" +
                "";
            DebugExtension.DevLogError(debugText);
        }

        _eventLogList.AddEventLog(title, content, Color.white);
    }

    List<ProductUserId> GetProductUserIdsList(string rawDestinationIdsListText)
    {
        List<ProductUserId> value = new List<ProductUserId>();

        string[] rawDestinationIdsList = rawDestinationIdsListText.Split(';');

        foreach (var item in rawDestinationIdsList)
        {
            item.DoIfNotNull(() =>
            {
                ProductUserId productUserId = ProductUserId.FromString(item);

                if (productUserId.IsValid())
                    value.Add(productUserId);
            });
        }

        return value;
    }
}
