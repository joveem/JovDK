using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using JovDK.Debug;

public class PanelsManager : MonoBehaviour
{
    public static PanelsManager instance;


    [SerializeField]
    private GameObject loadingPanel;

    [Space(20)]

    [SerializeField]
    private GameObject informationPopupPrefab;
    [SerializeField]
    private GameObject decisionPopupPrefab;

    [Space(20)]
    public GameObject popupPivot;


    [Space(20)]
    public bool isShowingLoading = false;
    [SerializeField]
    private Slider progressBar;
    public float progress = 0;




    // Start is called before the first frame update
    void Awake()
    {

        if (PanelsManager.instance == null)
        {

            instance = this;
        }
        else
        {

            DebugExtension.DevLogWarning("one or more Panels Managers instaces has been detected!");
            Destroy(this);

        }

    }

    private void Update()
    {

        if (isShowingLoading)
        {

            if (progressBar != null)
            {

                progressBar.value = progress;

            }
            else
            {

                DebugExtension.DevLogWarning("progressBar IS NULL!");

            }

        }

    }

    public PopUp ShowInformationPopup(string _title, string _description)
    {

        PopUp _instance = Instantiate(informationPopupPrefab, popupPivot.transform).GetComponent<PopUp>();

        _instance.SetTexts(_title, _description);

        _instance.SetConfirmationAction(() => { });

        return _instance;

    }

    public PopUp ShowConfirmationPopup(string _title, string _description, Action _onConfirm = null, Action _onCancel = null, Action _onClose = null)
    {

        PopUp _instance = Instantiate(decisionPopupPrefab, popupPivot.transform).GetComponent<PopUp>();

        _instance.SetTexts(_title, _description);

        if (_onConfirm != null)
        {

            _instance.SetConfirmationAction(_onConfirm);

        }

        if (_onCancel != null)
        {

            _instance.SetCancelAction(_onCancel);

        }

        if (_onClose != null)
        {

            _instance.SetCloseAction(_onClose);

        }

        return _instance;

    }


    public void ShowLoadingPanel()
    {
        isShowingLoading = true;
        progress = 0;

        loadingPanel.SetActive(true);

    }
    public void HideLoadingPanel()
    {

        isShowingLoading = false;

        loadingPanel.SetActive(false);

    }

}
