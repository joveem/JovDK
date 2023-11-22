// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// third
using Epic.OnlineServices;
using Epic.OnlineServices.Auth;
using Epic.OnlineServices.Connect;
using Epic.OnlineServices.P2P;
using PlayEveryWare.EpicOnlineServices;
using TMPro;
using static PlayEveryWare.EpicOnlineServices.EOSManager;
using AuthLoginCallbackInfo = Epic.OnlineServices.Auth.LoginCallbackInfo;
using ConnectLoginCallbackInfo = Epic.OnlineServices.Connect.LoginCallbackInfo;

// from company
using JovDK.Debug;
using JovDK.SafeActions;
using JovDK.SerializingTools.Bson;
using JovDK.SerializingTools.Json;
using JovDK.Services;

// from project
// ...


namespace JovDK.Networking.Realtime
{
    public partial class EosP2PServer : GenericRealtimeServer
    {
        ulong _connectionNotificationId = 0;

        void TryToAuthenticate(string username, string password)
        {
            LoginCredentialType loginCredentialType = LoginCredentialType.Developer;

#if UNITY_ANDROID && !UNITY_EDITOR
            loginCredentialType = LoginCredentialType.AccountPortal;
#endif

            string debugText =
                "StartPersistentLogin 02-05" + "\n" +
                "loginCredentialType = " + loginCredentialType.ToString() + "\n" +
                "username = " + username + "\n" +
                "password = " + password + "\n" +
                "";
            DebugExtension.DevLog(debugText);

            _popUpService.ShowLoadingCover();
            EOSManager.Instance.StartLoginWithLoginTypeAndToken(
                        loginCredentialType,
                        username,
                        password,
                        OnAuthenticationResponse);
        }

        void OnAuthenticationResponse(AuthLoginCallbackInfo loginCallbackInfo)
        {
            DebugExtension.DevLog(
                "> ".ToColor(GoodColors.Orange) + "OnAuthenticationResponse" + "\n" +
                "loginCallbackInfo.ResultCode = " + loginCallbackInfo.ResultCode.ToString());

            _popUpService.HideLoadingCover();

            switch (loginCallbackInfo.ResultCode)
            {
                // TODO: REVIEW THIS
                // no internet connection
                case Result.NoConnection:
                    break;

                case Result.Success:
                    {
                        ConnectAuthentication(loginCallbackInfo);
                        break;
                    }
            }

        }

        void ConnectAuthentication(AuthLoginCallbackInfo loginCallbackInfo)
        {
            DebugExtension.DevLog(
                "> ".ToColor(GoodColors.Orange) + "ConnectAuthentication 01" + "\n" +
                "loginCallbackInfo.ResultCode = " + loginCallbackInfo.ResultCode.ToString());

            _popUpService.ShowLoadingCover();
            EOSManager.Instance.StartConnectLoginWithEpicAccount(
                loginCallbackInfo.LocalUserId,
                (connectLoginCallbackInfo) =>
                {
                    DebugExtension.DevLog(
                        "> ".ToColor(GoodColors.Orange) + "ConnectAuthentication 02" + "\n" +
                        "connectLoginCallbackInfo.ResultCode = " + connectLoginCallbackInfo.ResultCode.ToString());

                    _popUpService.HideLoadingCover();
                    if (connectLoginCallbackInfo.ResultCode == Result.Success)
                    {
                        print("Connect Login Successful. [" + connectLoginCallbackInfo.ResultCode + "]");
                        _isConnected = true;
                        OnConnectionSucceed();

                        // _localProductUserId = EOSManager.Instance.GetProductUserId();
                        // DebugLocalProductUserId(_localProductUserId);

                        RefreshNATType();
                        SubscribeToConnectionRequest();
                    }
                    else if (connectLoginCallbackInfo.ResultCode == Result.InvalidUser)
                    {
                        // ask user if they want to connect; sample assumes they do
                        DebugExtension.DevLogWarning(
                            "$ > ".ToColor(GoodColors.Pink) +
                            "InvalidUser!" + " " +
                            "Creating new user...");

                        string messageTitle = "Invalid User!";
                        string messageDescription = "You need to create an user to continue...";
                        string positiveButtonText = "Create User";
                        string negativeButtonText = "No";

                        Action positiveCallback =
                            () =>
                            {
                                _popUpService.ShowLoadingCover();
                                EOSManager.Instance.CreateConnectUserWithContinuanceToken(
                                    connectLoginCallbackInfo.ContinuanceToken,
                                    (CreateUserCallbackInfo createUserCallbackInfo) =>
                                    {
                                        DebugExtension.DevLog("Creating new connect user...");
                                        EOSManager.Instance.StartConnectLoginWithEpicAccount(loginCallbackInfo.LocalUserId, (ConnectLoginCallbackInfo retryConnectLoginCallbackInfo) =>
                                        {
                                            DebugExtension.DevLog(
                                                "> ".ToColor(GoodColors.Orange) + "ConnectAuthentication 03" + "\n" +
                                                "retryConnectLoginCallbackInfo.ResultCode = " + retryConnectLoginCallbackInfo.ResultCode.ToString());

                                            _popUpService.HideLoadingCover();

                                            if (retryConnectLoginCallbackInfo.ResultCode == Result.Success)
                                            {
                                                DebugExtension.DevLog("Connect Login Successful. [" + retryConnectLoginCallbackInfo.ResultCode + "]");
                                                _isConnected = true;
                                                OnConnectionSucceed();

                                                // _localProductUserId = EOSManager.Instance.GetProductUserId();
                                                // DebugLocalProductUserId(_localProductUserId);

                                                RefreshNATType();
                                                SubscribeToConnectionRequest();
                                            }
                                            else
                                            {
                                                // unexpected error!

                                                // For any other error, re-enable the login procedure
                                                // ConfigureUIForLogin();
                                                _popUpService.ShowPopUpInformation("Unexpected error! :(");
                                            }
                                        });
                                    });
                            };

                        _popUpService.ShowPopUpConfirmation(
                            messageTitle,
                            messageDescription,
                            positiveButtonText,
                            negativeButtonText,
                            positiveCallback);
                    }
                }
                );
        }

        private void RefreshNATType()
        {
            var options = new QueryNATTypeOptions();
            _p2pHandler.QueryNATType(ref options, null, OnRefreshNATTypeFinished);
        }

        private void OnRefreshNATTypeFinished(ref OnQueryNATTypeCompleteInfo data)
        {
            if (!data.Equals(default))
            {
                if (data.ResultCode == Result.Success)
                    UnityEngine.Debug.Log("P2p (OnRefreshNATTypeFinished): RefreshNATType Completed");
                else
                    UnityEngine.Debug.LogErrorFormat("P2p (OnRefreshNATTypeFinished): RefreshNATType error: {0}", data.ResultCode);
            }
            else
            {
                DebugExtension.DevLogError(
                    "$ > ".ToColor(GoodColors.Pink) +
                    "data IS NULL!");
            }
        }

        void DebugLocalProductUserId(ProductUserId productUserId)
        {
            try
            {
                if (productUserId != null)
                {
                    if (productUserId.IsValid())
                    {
                        DebugExtension.DevLog(
                            "productUserId = " +
                            productUserId.ToString());
                    }
                    else
                    {
                        DebugExtension.DevLogWarning(
                            "$$$ > ".ToColor(GoodColors.Pink) +
                            "productUserId IS INVALID!" + "\n" +
                            "productUserId = " +
                            productUserId.ToString());
                    }
                }
                else
                {
                    DebugExtension.DevLogWarning(
                        "$$$ > ".ToColor(GoodColors.Pink) +
                        "productUserId IS NULL!");
                }
            }
            catch (Exception exception)
            {
                DebugExtension.DevLogError(
                    "$$$ > ".ToColor(GoodColors.Pink) +
                    "ERROR trying to DebugLocalProductUserId!" + "\n" +
                    "exception =" + "\n" +
                    exception);
            }
        }

        private void SubscribeToConnectionRequest()
        {
            DebugExtension.DevLog("> ".ToColor(GoodColors.Orange) + "SubscribeToConnectionRequest");

            if (_connectionNotificationId == 0)
            {
                SocketId socketId = new SocketId()
                {
                    SocketName = "CHAT"
                };

                AddNotifyPeerConnectionRequestOptions options = new AddNotifyPeerConnectionRequestOptions()
                {
                    LocalUserId = EOSManager.Instance.GetProductUserId(),
                    SocketId = socketId
                };

                _connectionNotificationId = _p2pHandler.AddNotifyPeerConnectionRequest(ref options, null, OnIncomingConnectionRequest);
                if (_connectionNotificationId == 0)
                {
                    UnityEngine.Debug.Log("EOS P2PNAT SubscribeToConnectionRequests: could not subscribe, bad notification id returned.");
                }
            }
        }

        private void OnIncomingConnectionRequest(ref OnIncomingConnectionRequestInfo data)
        {
            DebugExtension.DevLog(
                "> ".ToColor(GoodColors.Orange) + "OnIncomingConnectionRequest" + "\n" +
                "data =" + "\n" +
                data.SerializeObjectToJSON(true));

            //if (data == null)
            //{
            //    Debug.LogError("P2P (OnIncomingConnectionRequest): data is null");
            //    return;
            //}

            bool isValidSocketId = false;
            string socketName = "UNDEFINED";

            if (data.SocketId != null)
            {
                socketName = data.SocketId?.SocketName;
                isValidSocketId = true;
            }

            if (!isValidSocketId)
            {
                DebugExtension.DevLogError(
                    "$ > ".ToColor(GoodColors.Pink) +
                    "INVALID SocketId" + "\n" +
                    "socketName = " + socketName + "\n" +
                    "data =" + "\n" +
                    data.SerializeObjectToJSON(true));

                return;
            }

            SocketId socketId = new SocketId()
            {
                SocketName = "CHAT"
            };

            AcceptConnectionOptions options = new AcceptConnectionOptions()
            {
                LocalUserId = EOSManager.Instance.GetProductUserId(),
                RemoteUserId = data.RemoteUserId,
                SocketId = socketId
            };

            Result result = _p2pHandler.AcceptConnection(ref options);

            if (result != Result.Success)
            {
                UnityEngine.Debug.LogErrorFormat("P2p (OnIncomingConnectionRequest): error while accepting connection, code: {0}", result);
            }
        }
    }
}
