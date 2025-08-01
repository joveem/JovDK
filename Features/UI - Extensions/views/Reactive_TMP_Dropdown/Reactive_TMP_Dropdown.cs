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


namespace JovDK.UI.Extensions.Reactive
{
    public partial class Reactive_TMP_Dropdown<T> : MonoBehaviour
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // [SerializeField] bool _dependencies;


        [Space(5), Header("[ State ]"), Space(10)]

        public ReactiveProperty<T> CurrentValue = new ReactiveProperty<T>(default);
        T _previousValue;

        List<ReactiveOptionData<T>> _currentPossibleOptionsList = new List<ReactiveOptionData<T>>();
        Dictionary<T, ReactiveOptionData<T>> _currentPossibleOptionsByValue = new Dictionary<T, ReactiveOptionData<T>>();


        [Space(5), Header("[ Parts ]"), Space(10)]

        public TMP_Dropdown BaseDropdown;


        // [Space(5), Header("[ Configs ]"), Space(10)]

        // [SerializeField] bool _configs;


        #region MonoBehaviour
        void Awake()
        {
            // SetInitialState();
        }

        // void OnEnable()
        // {
        //     // // TODO: review this!
        //     // SubscribeAllListenersOnEnable();
        // }

        void Start()
        {
            SubscribeAllListenersOnStart();
        }

        // void FixedUpdate()
        // {

        // }

        // void Update()
        // {

        // }

        // void OnDisable()
        // {
        //     // // TODO: review this!
        //     // UnsubscribeAllListenersOnDisable();
        // }

        void OnDestroy()
        {
            UnsubscribeAllListenersOnDestroy();
        }
        #endregion MonoBehaviour

        #region Buttons
        // void SetupButtons()
        // {
        //     _mainButton.SetOnClickIfNotNull(MainButton);
        // }

        // void MainButton()
        // {

        // }
        #endregion Buttons

        #region Subscriptions
        // AWAKE/START <-> destroy
        // inverse of UnsubscribeAllListenersOnDestroy
        void SubscribeAllListenersOnStart()
        {
            // reactive Dropdown -> reactive value
            CurrentValue.TakeUntil(this.destroyCancellationToken).Subscribe(OnCurrentValueUpdate);

            // reactive Dropdown -> Dropdown
            BaseDropdown.DoIfNotNull(() => BaseDropdown.onValueChanged.AddListener(OnDropdownValueChanged));
        }

        // awake/start <-> DESTROY
        // inverse of SubscribeAllListenersOnStart
        void UnsubscribeAllListenersOnDestroy()
        {
            // this scripts -> other script
            // reactive Dropdown -> reactive value            
            // CurrentValue <- OnCurrentValueUpdate is auto unsubscribed on destroy

            // reactive Dropdown -> Dropdown
            BaseDropdown.DoIfNotNull(() => BaseDropdown.onValueChanged.RemoveListener(OnDropdownValueChanged));
        }

        // // ENABLE <-> disable
        // // inverse of UnsubscribeAllListenersOnDisable
        // void SubscribeAllListenersOnEnable()
        // {
        //     // this scripts -> other script            
        //     // ! disable
        //     // _randomDataBus.IdkProperty.AsObservable().TakeUntilDisable(gameObject).Subscribe(OnIdkPropertyUpdate);
        // }

        // // enable <-> DISABLE
        // // inverse of SubscribeAllListenersOnEnable
        // void UnsubscribeAllListenersOnDisable()
        // {
        //     // this scripts -> other script
        //     // ! disable
        //     // REVIEW THIS IdkProperty <- OnIdkPropertyUpdate is unsubscribed on disable automatically
        // }

        void OnCurrentValueUpdate(T value)
        {
            // DebugExtension.DefaultSubscriptionLog("value = " + value.ToString());

            bool isAlreadyRegistered = _currentPossibleOptionsByValue.ContainsKey(value);

            if (isAlreadyRegistered)
            {
                _previousValue = value;
                ReactiveOptionData<T> optionData = _currentPossibleOptionsByValue[value];

                BaseDropdown.DoIfNotNull(() => BaseDropdown.SetValueWithoutNotify(optionData.RelativeIndex));
            }
            else
            {
                DebugExtension.DevLogWarning(
                    "$> ".ToColor(GoodColors.Red) +
                    "Unexpected value!" + "\n" +
                    "value = " + value.ToString() + "\n" +
                    "_previousValue = " + _previousValue.ToString() + "\n" +
                    "");

                CurrentValue.Value = _previousValue;
            }
        }

        void OnDropdownValueChanged(int index)
        {
            // DebugExtension.DefaultSubscriptionLog("index = " + index.ToString());

            if (index < _currentPossibleOptionsList.Count)
            {
                T relativeValue = _currentPossibleOptionsList[index].RelativeValue;
                CurrentValue.Value = relativeValue;
            }
        }
        #endregion Subscriptions

        #region Controller
        // void SetInitialState()
        // {

        // }

        public void SetOptions(List<ReactiveOptionData<T>> optionsList)
        {
            _currentPossibleOptionsList = optionsList;

            foreach (ReactiveOptionData<T> option in optionsList)
            {
                bool isAlreadyRegistered = _currentPossibleOptionsByValue.ContainsKey(option.RelativeValue);

                if (!isAlreadyRegistered)
                    _currentPossibleOptionsByValue[option.RelativeValue] = option;
            }

            BaseDropdown.DoIfNotNull(() =>
            {
                List<TMP_Dropdown.OptionData> finalDropdownOptions = new List<TMP_Dropdown.OptionData>();

                foreach (ReactiveOptionData<T> baseOption in optionsList)
                {
                    finalDropdownOptions.Add(baseOption.RelativeOptionData);
                }

                BaseDropdown.options = finalDropdownOptions;
            });
        }
        #endregion Controller

        #region View
        // public void ShowPanel()
        // {

        // }
        #endregion View
    }

    public class ReactiveOptionData<T>
    {
        public T RelativeValue;
        public int RelativeIndex = -1;
        public TMP_Dropdown.OptionData RelativeOptionData = null;
    }
}
