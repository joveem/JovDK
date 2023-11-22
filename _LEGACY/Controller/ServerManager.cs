using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

using Newtonsoft.Json;

using JovDK.Debug;
using JovDK.SerializingTools.Json;
using JovDK.Models;
using JovDK.LEGACY.Localization;


public class ServerManager : MonoBehaviour
{

    public static ServerManager instance;
    private string baseApi = "http://localhost:8029/Api/v1/";
    private string apiUrl = "";
    private string socketUrl = "";

    private void Awake()
    {

        apiUrl = baseApi;

        if (ServerManager.instance == null)
        {

            instance = this;
        }
        else
        {

            DebugExtension.DevLogWarning("one or more Server Managers instaces has been detected!");
            Destroy(this);

        }

    }

    private IEnumerator ApiRequest(string _endPoint, string _data, Action<int, string> _response, string _requestMethod = "GET", bool _showConnectionError = true)
    {


        byte[] _formData = System.Text.Encoding.UTF8.GetBytes(_data);


        UploadHandler _uploadHandler = new UploadHandlerRaw(_formData);
        _uploadHandler.contentType = "application/json";

        DownloadHandlerBuffer _downloadHandler = new DownloadHandlerBuffer();

        UnityWebRequest _request = new UnityWebRequest(apiUrl + _endPoint, _requestMethod);
        _request.uploadHandler = _uploadHandler;
        _request.downloadHandler = _downloadHandler;

        PanelsManager.instance.ShowLoadingPanel();

        UnityWebRequestAsyncOperation _requestOperation = _request.SendWebRequest();
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        DebugExtension.DevLog(("CALLING API ( " + apiUrl + _endPoint + " )").ToColor(GoodColors.Green) + ("[" + _requestMethod + "]").ToColor(GoodColors.Green) + "\n\ndata =\n\n" + _data.ToColor(GoodColors.Red));
#endif

        while (!_requestOperation.isDone)
        {

            PanelsManager.instance.progress = (_request.uploadProgress + _request.downloadProgress) / 2;

            DebugExtension.DevLog("waiting.....".ToColor(GoodColors.Red) + " | up = " + _request.uploadProgress + " down = " + _request.downloadProgress);
            yield return null;

        }
        DebugExtension.DevLog("DONE!".ToColor(GoodColors.Red) + " | up = " + _request.uploadProgress + " down = " + _request.downloadProgress);

        PanelsManager.instance.HideLoadingPanel();

        switch (_request.result)
        {
            case UnityWebRequest.Result.ConnectionError:
                {
                    if (_showConnectionError)
                    {
                        ShowConnectionError();
                    }
                    DebugExtension.DevLog("CONNECTION ERROR".ToColor(GoodColors.Red));
                    break;

                }

            case UnityWebRequest.Result.DataProcessingError:
                {

                    ShowDataProcessingError();
                    DebugExtension.DevLog("DATA PROCESSING ERROR".ToColor(GoodColors.Red));
                    break;

                }

            default:
                {

                    DebugExtension.DevLog("...");
                    break;

                }

        }


#if UNITY_EDITOR || DEVELOPMENT_BUILD
        DebugExtension.DevLog(("API RESPONSE ( " + apiUrl + _endPoint + " )").ToColor(GoodColors.Yellow) + ("[" + _requestMethod + "]").ToColor(GoodColors.Yellow) + "\n\ncode = " + _requestOperation.webRequest.responseCode.ToString().ToColor(GoodColors.Red) + "\n\ndata =\n\n" + (string.IsNullOrWhiteSpace(_requestOperation.webRequest.downloadHandler.text) ? "NULL / EMPTY" : _requestOperation.webRequest.downloadHandler.text).ToColor(GoodColors.Red) + "\n\n");
#endif

        _response((int)_request.responseCode, _request.downloadHandler.text);

    }

    private void ShowConnectionError()
    {

        PanelsManager.instance.ShowInformationPopup(LocalizationService.GetTextById("ServerManager.Connection.Error.Title.Text"), LocalizationService.GetTextById("ServerManager.Connection.Error.Description.Text"));

    }
    private void ShowDataProcessingError()
    {

        PanelsManager.instance.ShowInformationPopup(LocalizationService.GetTextById("ServerManager.DataProcessing.Error.Title.Text"), LocalizationService.GetTextById("ServerManager.DataProcessing.Error.Description.Text"));

    }


    private void CallAPI(string _endPoint, string _data, Action<int, string> _response, string _requestMethod = "GET", bool _showConnectionError = true)
    {

        StartCoroutine(ApiRequest(_endPoint, _data, _response, _requestMethod, _showConnectionError));

    }

    public void GetVersion(int _currentGameVersion, Action<int, Models.VersionResponse> _response)
    {

        Models.VersionRequest _requestForm = new Models.VersionRequest();

        _requestForm.version = _currentGameVersion;

        string _data = _requestForm.SerializeObjectToJSON();

        CallAPI("Version", _data, (_code, _versionResponseText) =>
        {

            _response(_code, _versionResponseText.DeserializeJsonToObject<Models.VersionResponse>());
            //_response(_code, new Models.VersionResponse());

        }, "POST", false);

    }

    public void Authenticate(string _token, string _login, string _password, Action<int, string> _response)
    {

        Models.Login.LoginRequest _requestForm = new Models.Login.LoginRequest();

        _requestForm.token = _token;
        _requestForm.login = _login;
        _requestForm.password = _password;


        string _data = _requestForm.SerializeObjectToJSON();

        CallAPI("Login", _data, _response, "POST", !string.IsNullOrWhiteSpace(_token));

    }

}
