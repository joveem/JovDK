// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

// third
using TMPro;
using DG.Tweening;

// from company
using JovDK.Debugging;
using JovDK.SafeActions;
using JovDK.SerializingTools.Bson;
using JovDK.SerializingTools.Json;

// from project
// ...


public partial class BasePanel : MonoBehaviour
{

    // [Space(5), Header("[ Dependencies ]"), Space(10)]

    // bool _dependencies;


    [Space(5), Header("[ State ]"), Space(10)]

    protected bool _isShowing = false;
    public bool IsShowing { get => _isShowing; }


    [Space(5), Header("[ Parts ]"), Space(10)]

    [SerializeField] Image _fadeBackground;
    [SerializeField] RectTransform _bodyContainer;


    [Space(5), Header("[ Configs ]"), Space(10)]

    [SerializeField] Ease _backgroundPanelShowAnimationEase = Ease.OutBack;
    [SerializeField] Ease _backgroundPanelHideAnimationEase = Ease.OutExpo;
    [SerializeField] float _maxFadeOpacity = 0.4f;
    [SerializeField] float _showAnimationDelay = 0.35f;


    // void Awake()
    // {

    // }

    // void Start()
    // {

    // }

    // void Update()
    // {

    // }

    // void FixedUpdate()
    // {

    // }

}

#if UNITY_EDITOR
[CustomEditor(typeof(BasePanel), true)]
public class BasePanelEditor : UnityEditor.Editor
{
    BasePanel _basePanel;

    public void OnEnable()
    {
        _basePanel = (BasePanel)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.Space(20);

        if (GUILayout.Button("> ShowPanelInstantaneously()"))
        {
            DebugExtension.DevLog("> ShowPanelInstantaneously()");
            _basePanel.ShowPanelInstantaneously();
        }

        if (GUILayout.Button("> HidePanelInstantaneously()"))
        {
            DebugExtension.DevLog("> HidePanelInstantaneously()");
            _basePanel.HidePanelInstantaneously();
        }
    }
}
#endif
