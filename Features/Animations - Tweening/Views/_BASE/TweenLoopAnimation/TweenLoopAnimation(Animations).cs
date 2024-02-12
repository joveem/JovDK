// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// third
using DG.Tweening;
using TMPro;

// from company
using JovDK.Debug;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

// from project
// ...


namespace JovDK.Animations.Tweening
{
    public abstract partial class TweenLoopAnimation : LoopAnimation
    {
        public abstract override void StartAnimation();

        public override void StopAnimation()
        {
            transform.DOKill();
            KillAllSequences();
        }

        void KillAllSequences()
        {
            foreach (Sequence sequence in _sequencesList)
                sequence.Kill();

            _sequencesList = new List<Sequence>();
        }
    }
}
