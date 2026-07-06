// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using SystemRandom = System.Random;
using UnityRandom = UnityEngine.Random;

// third
using DG.Tweening;
using R3;
using TMPro;

// from company
using JovDK.Debugging;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

// from project
// ...


namespace PackageName.MajorContext.MinorContext
{
    public partial class UiFocusProxyRect : MonoBehaviour, IPointerClickHandler
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // [SerializeField] bool _dependencies;


        // [Space(5), Header("[ State ]"), Space(10)]

        // [SerializeField] bool _state1;
        // ReactiveProperty<bool> _state2 = new ReactiveProperty<bool>(false);
        // public ReactiveProperty<bool> State => _state2;
        // Tween _curretBackgroundTween = null;
        // // getters
        // public Func<bool> State3Getter = null;
        // // callbacks
        // public Action OnIdkCallback = null;
        // public Action<bool> OnIdkCallback = null;
        // // subscriptions
        // List<ISubscription> _onStartSubscriptions = new List<ISubscription>();
        // List<ISubscription> _externalOnStartSubscriptions = new List<ISubscription>();
        // public List<ISubscription> ExternalOnStartSubscriptions => _externalOnStartSubscriptions;
        // List<ISubscription> _onEnableSubscriptions = new List<ISubscription>();
        // List<ISubscription> _externalOnEnableSubscriptions = new List<ISubscription>();
        // public List<ISubscription> ExternalOnEnableSubscriptions => _externalOnEnableSubscriptions;


        [Space(5), Header("[ Parts ]"), Space(10)]

        [SerializeField] TMP_InputField _baseInputField;


        // [Space(5), Header("[ Configs ]"), Space(10)]

        // [SerializeField] bool _configs;



        #region MonoBehaviour
        public void OnPointerClick(PointerEventData eventData)
        {
            // DebugExtension.DefaultButtonLog();

            TryToForceInputFieldFocus();
        }
        #endregion MonoBehaviour

        #region Callbacks
        #endregion Callbacks

        #region Controller
        void TryToForceInputFieldFocus()
        {
            // DebugExtension.DefaultGenericLog();

            _baseInputField.DoIfNotNull(() =>
            {
                _baseInputField.Select();
                _baseInputField.ActivateInputField();
            });
        }
        #endregion Controller
    }
}
