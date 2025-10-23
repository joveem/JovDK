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
using System.Linq;

// from project
// ...


namespace JovDK.Generic.Extensions.R3
{
    public class ReactiveProperty_Extended<T> : ReactiveProperty<T>
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // [SerializeField] bool _dependencies;


        // state
        public override T Value
        {
            get => base.Value;
            set
            {
                if (HasPendingValues())
                {
                    DebugExtension.DevLogError(
                        "$$$> ".ToColor(GoodColors.Red),
                        "Applying pending values before setting new value.", "\n",
                        "");

                    ApplyAllPendingValue();
                }
                base.Value = value;
            }
        }

        protected Queue<T> _pendingValues = new Queue<T>();


        // [Space(5), Header("[ Parts ]"), Space(10)]

        // [SerializeField] bool _parts;


        // [Space(5), Header("[ Configs ]"), Space(10)]

        // [SerializeField] bool _configs;



        public ReactiveProperty_Extended(T initialValue) : base(initialValue) { }



        #region Controller
        public bool HasPendingValues()
        {
            return _pendingValues.Count > 0;
        }

        public void SetPendingValueWithoutNotify(T newValue)
        {
            _pendingValues.Enqueue(newValue);
        }

        public void ApplyAllPendingValue()
        {
            while (HasPendingValues())
            {
                T _pendingValue = _pendingValues.Dequeue();
                this.Value = _pendingValue;
            }
        }

        /// <summary>
        /// Returns the last pending value. If there are
        /// no pending values, returns the current value
        /// instead.
        /// </summary>
        /// <returns></returns>
        public T GetLastPendingValueOrValue()
        {
            if (HasPendingValues())
                return _pendingValues.Last();

            return this.Value;
        }
        #endregion Controller
    }
}
