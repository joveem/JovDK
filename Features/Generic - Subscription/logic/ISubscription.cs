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
using TMPro;

// from company
using JovDK.Debugging;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

// from project
// ...


namespace JovDK.Core.Subscription
{
    public interface ISubscription
    {
        public void Unsubscribe();
    }

    public static class SubscriptionExtension
    {
        public static void Register(
            this List<ISubscription> baseSubscriptionsList,
            Action subscriptionCallback,
            Action disposeCallback)
        {
            DefaultSubscription subscription = new DefaultSubscription(subscriptionCallback, disposeCallback);
            baseSubscriptionsList.Add(subscription);
        }

        public static void RegisterFrom<T>(
            this List<ISubscription> baseSubscriptionsList,
            ReactiveProperty<T> reactiveProperty,
            Action<T> relativeCallback)
        {
            R3Subscription<T> subscription = R3Subscription.From(reactiveProperty, relativeCallback);
            baseSubscriptionsList.Add(subscription);
        }

        public static void RegisterFrom<T>(
            this List<ISubscription> baseSubscriptionsList,
            ReadOnlyReactiveProperty<T> reactiveProperty,
            Action<T> relativeCallback)
        {
            R3ReadOnlySubscription<T> subscription = R3ReadOnlySubscription.From(reactiveProperty, relativeCallback);
            baseSubscriptionsList.Add(subscription);
        }

        public static void RegisterFrom(
            this List<ISubscription> baseSubscriptionsList,
            Delegate target,
            Delegate relativeCallback)
        {
            DebugExtension.DevLog("80-01");
            Delegate.Combine(target, relativeCallback);
            // DelegateSubscription subscription = new DelegateSubscription(target, relativeCallback);
            // baseSubscriptionsList.Add(subscription);
        }

        public delegate T2 ConvertionGetter<T1, T2>(T1 value);
        public delegate T DefaultValueGetter<T>();

        public static ReactiveProperty<T2> RegisterFromConvertion<T1, T2>(
            this List<ISubscription> baseSubscriptionsList,
            ReactiveProperty<T1> reactiveProperty,
            ConvertionGetter<T1, T2> convertionGetter,
            DefaultValueGetter<T2> defaultValueGetter = null)
        {
            ReactiveProperty<T2> t2 = new ReactiveProperty<T2>(default);

            Action<T1> callback = (t1Value) =>
            {
                T2 t2Value = default;

                if (defaultValueGetter is not null)
                    t2Value = defaultValueGetter();

                if (t1Value is not null && t1Value != null)
                    t2Value = convertionGetter(t1Value);

                t2.Value = t2Value;
            };

            baseSubscriptionsList.RegisterFrom(reactiveProperty, callback);

            return t2;
        }

        public static void RegisterFromInputFieldValue(
            this List<ISubscription> baseSubscriptionsList,
            TMP_InputField inputField,
            Action<string> relativeCallback)
        {
            inputField.DoIfNotNull(() =>
            {
                UnityEngine.Events.UnityAction<string> listener = (value) => relativeCallback?.Invoke(value);

                inputField.onValueChanged.AddListener(listener);
                Action customDispose = () => inputField.onValueChanged.RemoveListener(listener);

                CustomSubscription subscription = new CustomSubscription(customDispose);
                baseSubscriptionsList.Add(subscription);
            });
        }

        public static void UnsubscribeAll(this List<ISubscription> baseSubscriptionsList)
        {
            foreach (ISubscription subscription in baseSubscriptionsList)
            {
                if (subscription is not null)
                    subscription.Unsubscribe();
            }
        }

        public static void UnsubscribeAllAndClear(this List<ISubscription> baseSubscriptionsList)
        {
            baseSubscriptionsList.UnsubscribeAll();
            baseSubscriptionsList = new List<ISubscription>();
        }
    }

    public class DefaultSubscription : ISubscription
    {
        Action _disposeCallback;

        public DefaultSubscription(Action subscriptionCallback, Action disposeCallback)
        {
            subscriptionCallback?.Invoke();
            _disposeCallback = disposeCallback;
        }

        void ISubscription.Unsubscribe()
        {
            try
            {
                _disposeCallback?.Invoke();
            }
            catch (Exception exception)
            {
                DebugExtension.DevLogError(
                    "$$> ".ToColor(GoodColors.Red),
                    "exception = ", "\n",
                    exception.ToString());

                // throw;
            }
        }
    }

    public class DelegateSubscription : ISubscription
    {
        Delegate _target;
        Delegate _callback;

        public DelegateSubscription(Delegate target, Delegate callback)
        {
            _target = target;
            _callback = callback;

            Subscribe();
        }

        void Subscribe()
        {
            Delegate.Combine(_target, _callback);
        }

        public void Unsubscribe()
        {
            _target = Delegate.Remove(_target, _callback);
        }
    }

    public class CustomSubscription : ISubscription
    {
        Action _dispose;

        public CustomSubscription(Action customDispose)
        {
            _dispose = customDispose;
        }

        void ISubscription.Unsubscribe()
        {
            try
            {
                _dispose?.Invoke();
            }
            catch (Exception exception)
            {
                DebugExtension.DevLogError(
                    "$$> ".ToColor(GoodColors.Red),
                    "exception = ", "\n",
                    exception.ToString());

                // throw;
            }
        }
    }

    public static class R3Subscription
    {
        public static R3Subscription<T> From<T>(
            ReactiveProperty<T> reactiveProperty,
            Action<T> relativeCallback)
        {
            return new R3Subscription<T>(reactiveProperty, relativeCallback);
        }
    }

    public static class R3ReadOnlySubscription
    {
        public static R3ReadOnlySubscription<T> From<T>(
            ReadOnlyReactiveProperty<T> reactiveProperty,
            Action<T> relativeCallback)
        {
            return new R3ReadOnlySubscription<T>(reactiveProperty, relativeCallback);
        }
    }

    public class R3Subscription<T> : ISubscription
    {
        ReactiveProperty<T> _reactiveProperty;
        Action<T> _relativeCallback;
        IDisposable _disposable;

        public R3Subscription(
            ReactiveProperty<T> reactiveProperty,
            Action<T> relativeCallback)
        {
            _reactiveProperty = reactiveProperty;
            _relativeCallback = relativeCallback;

            Subscribe();
        }

        void Subscribe()
        {
            _disposable = _reactiveProperty.Subscribe(_relativeCallback);
        }

        void ISubscription.Unsubscribe()
        {
            try
            {
                _disposable.Dispose();
            }
            catch (Exception exception)
            {
                DebugExtension.DevLogError(
                    "$$> ".ToColor(GoodColors.Red),
                    "exception = ", "\n",
                    exception.ToString());

                // throw;
            }
        }
    }

    public class R3ReadOnlySubscription<T> : ISubscription
    {
        ReadOnlyReactiveProperty<T> _reactiveProperty;
        Action<T> _relativeCallback;
        IDisposable _disposable;

        public R3ReadOnlySubscription(
            ReadOnlyReactiveProperty<T> reactiveProperty,
            Action<T> relativeCallback)
        {
            _reactiveProperty = reactiveProperty;
            _relativeCallback = relativeCallback;

            Subscribe();
        }

        void Subscribe()
        {
            _disposable = _reactiveProperty.Subscribe(_relativeCallback);
        }

        void ISubscription.Unsubscribe()
        {
            try
            {
                _disposable.Dispose();
            }
            catch (Exception exception)
            {
                DebugExtension.DevLogError(
                    "$$> ".ToColor(GoodColors.Red),
                    "exception = ", "\n",
                    exception.ToString());

                // throw;
            }
        }
    }
}
