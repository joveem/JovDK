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
using Epic.OnlineServices.P2P;
using PlayEveryWare.EpicOnlineServices;
using TMPro;
using static PlayEveryWare.EpicOnlineServices.EOSManager;

// from company
using JovDK.Debug;
using JovDK.SafeActions;
using JovDK.SerializingTools.Bson;
using JovDK.SerializingTools.Json;
using JovDK.Debug.Events;
using JovDK.Networking.Realtime;

// from project
// ...


public partial class EosP2PTestingShowcase : MonoBehaviour
{

    [Space(5), Header("[ Dependencies ]"), Space(10)]

    [SerializeField] GenericRealtimeServer _realtimeServer;


    // [Space(5), Header("[ State ]"), Space(10)]

    // bool _state;


    [Space(5), Header("[ Parts ]"), Space(10)]

    // auth
    [SerializeField] Transform _authContainer;
    [SerializeField] TMP_InputField _authToolAddressInputField;
    [SerializeField] TMP_InputField _usernameInputField;
    [SerializeField] Button _joinButton;

    // interaction
    [Space(10)]

    [SerializeField] Transform _interactionContainer;
    [SerializeField] TextMeshProUGUI _localUserIdText;
    [SerializeField] Button _copyLocalIdButton;
    [SerializeField] TMP_InputField _messageDestinationIdsListInputField;
    [SerializeField] TMP_InputField _messageContentInputField;
    [SerializeField] Button _sendMessageButton;
    [SerializeField] EventLogList _eventLogList;



    [Space(5), Header("[ Configs ]"), Space(10)]

    string _localUserIdPrefix = "local user id: ";



    // void Awake()
    // {

    // }

    void Start()
    {
        // EOSSingleton eosSingleton = EOSManager.Instance;
        // eosSingleton.Init()
        SetupButtons();
        ApplyInitialState();
        SubscribeAllListeners();
    }

    void OnDestroy()
    {
        UnsubscribeAllListeners();
    }

    // void Update()
    // {

    // }

    // void FixedUpdate()
    // {

    // }

}
