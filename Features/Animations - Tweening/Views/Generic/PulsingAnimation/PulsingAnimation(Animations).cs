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
using JovDK.Debugging;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

// from project
// ...


namespace JovDK.Animations.Tweening
{
    public partial class PulsingAnimation : TweenLoopAnimation
    {
        public override void StartAnimation()
        {
            // scale
            StartVerticalScaleLoopAnimation();
            StartHorizontalScaleLoopAnimation();
            // movement
            StartVerticalMovementLoopAnimation();
        }

        void StartHorizontalScaleLoopAnimation()
        {
            StartXScaleLoopAnimation();
            StartZScaleLoopAnimation();
        }

        // horizontal
        void StartXScaleLoopAnimation()
        {
            // set final position first, to a 
            // better loop where the sequence
            //starts with the final position
            transform.DOScaleX(_maxScale, 0);

            Sequence xScaleSequence = DOTween.Sequence();
            _sequencesList.Add(xScaleSequence);

            Tween xScaleIncreaseTween =
                transform
                    .DOScaleX(
                        _maxScale,
                        _horizontalScaleIncreaseDuration)
                    .SetEase(_fastEase);
            Tween xScaleDecreaseTween =
                transform
                    .DOScaleX(
                        _minScale,
                        _horizontalScaleDeacreaseDuration)
                    .SetEase(_slowEase);

            // DEcrease before INcrease 
            xScaleSequence.Append(xScaleDecreaseTween).Append(xScaleIncreaseTween);

            xScaleSequence.SetLoops(-1);
        }

        // horizontal
        void StartZScaleLoopAnimation()
        {
            // set final position first, to a 
            // better loop where the sequence
            //starts with the final position
            transform.DOScaleZ(_maxScale, 0);

            Sequence zScaleSequence = DOTween.Sequence();
            _sequencesList.Add(zScaleSequence);

            Tween zScaleIncreaseTween =
                transform
                    .DOScaleZ(
                        _maxScale,
                        _horizontalScaleIncreaseDuration)
                    .SetEase(_fastEase);
            Tween zScaleDecreaseTween =
                transform
                    .DOScaleZ(
                        _minScale,
                        _horizontalScaleDeacreaseDuration)
                    .SetEase(_slowEase);

            // DEcrease before INcrease
            zScaleSequence.Append(zScaleDecreaseTween).Append(zScaleIncreaseTween);

            zScaleSequence.SetLoops(-1);
        }

        void StartVerticalScaleLoopAnimation()
        {
            // set final position first, to a 
            // better loop where the sequence
            //starts with the final position
            transform.DOScaleY(_minScale, 0);

            Sequence yScaleSequence = DOTween.Sequence();
            _sequencesList.Add(yScaleSequence);

            Tween yScaleIncreaseTween =
                transform
                    .DOScaleY(
                        _maxScale,
                        _verticalScaleIncreaseDuration)
                    .SetEase(_fastEase);
            Tween yScaleDecreaseTween =
                transform
                    .DOScaleY(
                        _minScale,
                        _verticalScaleDeacreaseDuration)
                    .SetEase(_slowEase);

            // INcrease before DEcrease
            yScaleSequence.Append(yScaleIncreaseTween).Append(yScaleDecreaseTween);

            yScaleSequence.SetLoops(-1);
        }

        void StartVerticalMovementLoopAnimation()
        {
            float initialLocalPosition = transform.localPosition.y;

            // set final position first, to a 
            // better loop where the sequence
            //starts with the final position
            transform.DOLocalMoveY(initialLocalPosition, 0);

            Sequence yMoveSequence = DOTween.Sequence();
            _sequencesList.Add(yMoveSequence);


            Tween moveUpTween =
                transform
                    .DOLocalMoveY(
                        initialLocalPosition + _veritacalMoveDistance,
                        _verticalMoveUpDuration)
                    .SetEase(_fastEase);
            Tween moveDownTween =
                transform
                    .DOLocalMoveY(
                        initialLocalPosition,
                        _verticalMoveDowDuration)
                    .SetEase(_slowEase);

            // up before down
            yMoveSequence.Append(moveUpTween).Append(moveDownTween);

            yMoveSequence.SetLoops(-1);
        }
    }
}
