// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// third
// ...

// from company
using JovDK.Debugging;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

// from project
// ...


namespace JovDK.Generic.StateMachine
{
    public class Transition
    {
        public Func<bool> Condition { get; }
        public IState To { get; }

        public Transition(IState to, Func<bool> condition)
        {
            To = to;
            Condition = condition;
        }
    }
}
