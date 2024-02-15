using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using JovDK.Debug;

public class DebugPanel : MonoBehaviour
{
    public static DebugPanel instance;

    [Space(10)]
    public bool isDebugging = false;

    [Space(25)]
    [SerializeField]
    bool isShowingPanel = false;
    [Space(10)]

    [SerializeField] GameObject panelContainer;

    private void Awake()
    {

#if !(UNITY_EDITOR || DEVELOPMENT_BUILD)
        DebugExtension.DevLog("Destroing Debug Panel".ToColor(GoodColors.Red));
        Destroy(gameObject);
#endif
        DebugExtension.DevLog("Keeping Debug Panel".ToColor(GoodColors.Green));



        SetupInstance();



        if (isDebugging)
        {

            ShowPanel();

        }
        else
        {

            HidePanel();

        }

    }
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {

            if (Input.GetKey(KeyCode.LeftShift))
            {

                PanelShowSwitch();

            }


        }

    }
    public virtual void SetupInstance()
    {

        DebugExtension.DevLogWarning("setting...");

        if (DebugPanel.instance == null)
        {

            instance = this;

        }
        else
        {

            DebugExtension.DevLogWarning("one or more Debug Panels instances has been detected!");
            Destroy(this);

        }

    }

    private void ShowPanel()
    {

        if (panelContainer != null)
        {

            panelContainer.SetActive(true);

            isShowingPanel = true;

        }

    }
    private void HidePanel()
    {

        if (panelContainer != null)
        {

            panelContainer.SetActive(false);

            isShowingPanel = false;

        }

    }

    private void PanelShowSwitch()
    {

        if (isShowingPanel)
        {

            HidePanel();

        }
        else
        {

            ShowPanel();

        }

    }


}
