// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using SystemRandom = System.Random;
using UnityRandom = UnityEngine.Random;

// third
using DG.Tweening;
using R3;
using R3.Collections;
using TMPro;

// from company
using JovDK.Debugging;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

// from project
// ...


namespace JovDK.UI.Reactive
{
    public partial class Reactive_NumberSettingField<T> : MonoBehaviour
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // [SerializeField] bool _dependencies;


        [Space(5), Header("[ State ]"), Space(10)]

        public ReactiveProperty<T> CurrentValue = new ReactiveProperty<T>(default);
        T _previousValue;


        [Space(5), Header("[ Parts ]"), Space(10)]

        [SerializeField] TextMeshProUGUI _mainText;
        [SerializeField] Button _minusButton;
        [SerializeField] Button _plusButton;


        [Space(5), Header("[ Configs ]"), Space(10)]

        [SerializeField] int _minValue = 0;


        #region MonoBehaviour
        // void Awake()
        // {
        //     // SetInitialState();
        // }

        void OnEnable()
        {
            SubscribeAllListenersOnEnable();
        }

        void Start()
        {
            // // TODO: review this!
            // SubscribeAllListenersOnStart();
            SetupButtons();
        }

        // void FixedUpdate()
        // {

        // }

        // void Update()
        // {

        // }

        void OnDisable()
        {
            UnsubscribeAllListenersOnDisable();
        }

        // void OnDestroy()
        // {
        //     // // TODO: review this!
        //     // UnsubscribeAllListenersOnDestroy();
        // }
        #endregion MonoBehaviour

        #region Callbacks
        // void OnIdk()
        // {
        //        // DebugExtension.DevLog(">".ToColor(GoodColors.Orange));
        // }
        #endregion Callbacks

        #region Buttons
        void SetupButtons()
        {
            _minusButton.SetOnClickIfNotNull(MinusButton);
            _plusButton.SetOnClickIfNotNull(PlusButton);
        }

        void MinusButton()
        {
            // DebugExtension.DevLog("#>".ToColor(GoodColors.Orange));

            double currentValue = Convert.ToDouble(CurrentValue.Value);

            if (currentValue - 1 >= _minValue)
                currentValue -= 1;

            CurrentValue.Value = (T)Convert.ChangeType(currentValue, typeof(T));
        }

        void PlusButton()
        {
            // DebugExtension.DevLog("#>".ToColor(GoodColors.Orange));

            double currentValue = Convert.ToDouble(CurrentValue.Value);
            currentValue += 1;
            CurrentValue.Value = (T)Convert.ChangeType(currentValue, typeof(T));
        }
        #endregion Buttons

        #region Subscriptions
        // // AWAKE/START <-> destroy
        // // inverse of UnsubscribeAllListenersOnDestroy
        // void SubscribeAllListenersOnStart()
        // {
        //     // this scripts -> other script            
        //     // ! destroy
        //     // _randomDataBus.IdkProperty.AsObservable().TakeUntilDestroy(gameObject).Subscribe(OnIdkPropertyUpdate);
        // }

        // // awake/start <-> DESTROY
        // // inverse of SubscribeAllListenersOnStart
        // void UnsubscribeAllListenersOnDestroy()
        // {
        //     // this scripts -> other script
        //     // ! destroy
        //     // REVIEW THIS IdkProperty <- OnIdkPropertyUpdate is unsubscribed on destroy automatically
        // }

        // ENABLE <-> disable
        // inverse of UnsubscribeAllListenersOnDisable
        void SubscribeAllListenersOnEnable()
        {
            // reactive number field -> reactive value
            CurrentValue.TakeUntil(this.destroyCancellationToken).Subscribe(OnCurrentValueUpdate);
        }

        // enable <-> DISABLE
        // inverse of SubscribeAllListenersOnEnable
        void UnsubscribeAllListenersOnDisable()
        {
            // this scripts -> other script
            // ! disable
            // REVIEW THIS IdkProperty <- OnIdkPropertyUpdate is unsubscribed on disable automatically
            // reactive number field -> reactive value
            // CurrentValue <- OnCurrentValueUpdate is unsubscribed on disable automatically
        }

        void OnCurrentValueUpdate(T value)
        {
            // DebugExtension.DevLog("#> ".ToColor(GoodColors.Pink) + "value = " + value.ToString());

            double currentValue = Convert.ToDouble(value);

            if (currentValue >= _minValue)
            {
                _previousValue = (T)Convert.ChangeType(currentValue, typeof(T));
                string valueAsText = currentValue.ToString();
                _mainText.SetTextIfNotNull(valueAsText);
            }
            else
                CurrentValue.Value = _previousValue;
        }
        #endregion Subscriptions

        #region Controller
        // void SetInitialState()
        // {
        //     // DebugExtension.DevLog(">".ToColor(GoodColors.Orange));
        // }
        #endregion Controller

        #region View
        // public void ShowPanel()
        // {

        // }
        #endregion View
    }
}
