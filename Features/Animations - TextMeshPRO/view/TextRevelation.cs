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
using static JovDK.Animations.Tools.AnimationTools;

// from project
// ...


namespace JovDK.Animations.TextMeshPro
{
    public static partial class TextRevelation
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


        // [Space(5), Header("[ Parts ]"), Space(10)]

        // [SerializeField] bool _parts;
        // [SerializeField] Button _mainButton;
        // [SerializeField] TMP_Text _mainText;
        // [SerializeField] Image _mainImage;
        // [SerializeField] Transform _mainContainer;


        // [Space(5), Header("[ Configs ]"), Space(10)]

        // [SerializeField] bool _configs;



        #region MonoBehaviour
        // void Awake()
        // {
        //     // SetInitialState();
        // }

        // void OnEnable()
        // {
        //     // // TODO: review this!
        //     // SubscribeAllListenersOnEnable();
        // }

        // void Start()
        // {
        //     // // TODO: review this!
        //     // SubscribeAllListenersOnStart();
        //     // SetupButtons();
        // }

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

        // void OnDestroy()
        // {
        //     // // TODO: review this!
        //     // UnsubscribeAllListenersOnDestroy();
        // }
        #endregion MonoBehaviour

        #region Callbacks
        // // void OnIdk(bool value)
        // void OnIdk()
        // {
        //     // DebugExtension.DefaultCallbackLog();

        //     // OnIdkCallback?.Invoke(value);
        //     OnIdkCallback?.Invoke();
        // }
        #endregion Callbacks

        #region Buttons
        // void SetupButtons()
        // {
        //     _mainButton.SetOnClickIfNotNull(MainButton);
        // }

        // void MainButton()
        // {
        //     DebugExtension.DefaultButtonLog();


        // }
        #endregion Buttons

        #region Subscriptions
        // // AWAKE/START <-> destroy
        // // inverse of UnsubscribeAllListenersOnDestroy
        // void SubscribeAllListenersOnStart()
        // {
        //     // ! REVIEW THIS
        //     // ! start / destroy

        //     // _randomDataBus.DoIfNotNull(() =>
        //     // {
        //     //     // this scripts -> other script
        //     //     // _randomDataBus.IdkProperty.AsObservable().TakeUntilDestroy(gameObject).Subscribe(OnIdkPropertyUpdate);
        //     //     _onStartSubscriptions.Register(
        //     //         () => _randomDataBus.OnRandonActionCallback += OnRandonAction,
        //     //         () => _randomDataBus.OnRandonActionCallback += OnRandonAction);
        //     //     _onStartSubscriptions.RegisterFrom(_randomDataBus.RandonProperty, OnRandonPropertyUpdate);
        //     // });
        // }

        // // awake/start <-> DESTROY
        // // inverse of SubscribeAllListenersOnStart
        // void UnsubscribeAllListenersOnDestroy()
        // {
        //     // ! REVIEW THIS
        //     // ! start / destroy

        //     // // this scripts -> other script
        //     // _onStartSubscriptions.UnsubscribeAllAndClear();
        //     // // this scripts -> external
        //     // _externalOnStartSubscriptions.UnsubscribeAllAndClear();
        // }

        // // ENABLE <-> disable
        // // inverse of UnsubscribeAllListenersOnDisable
        // void SubscribeAllListenersOnEnable()
        // {
        //     // ! REVIEW THIS
        //     // ! enable / disable

        //     // _randomDataBus.DoIfNotNull(() =>
        //     // {
        //     //     // this scripts -> other script
        //     //     // _randomDataBus.IdkProperty.AsObservable().TakeUntilDestroy(gameObject).Subscribe(OnIdkPropertyUpdate);
        //     //     _onEnableSubscriptions.Register(
        //     //         () => _randomDataBus.OnRandonActionCallback += OnRandonAction,
        //     //         () => _randomDataBus.OnRandonActionCallback += OnRandonAction);
        //     //     _onEnableSubscriptions.RegisterFrom(_randomDataBus.RandonProperty, OnRandonPropertyUpdate);
        //     // });
        // }

        // // enable <-> DISABLE
        // // inverse of SubscribeAllListenersOnEnable
        // void UnsubscribeAllListenersOnDisable()
        // {
        //     // ! REVIEW THIS
        //     // ! enable / disable

        //     // // this scripts -> other script
        //     // _onEnableSubscriptions.UnsubscribeAllAndClear();
        //     // // this scripts -> external
        //     // _externalOnEnableSubscriptions.UnsubscribeAllAndClear();
        // }

        // void OnIdkPropertyUpdate(int newValue)
        // {
        //     // DebugExtension.DefaultSubscriptionLog();
        //     // DebugExtension.DefaultSubscriptionLog(
        //     //     "newValue = ", newValue.SerializeObjectToJSON(), "\n",
        //     //     "");


        // }
        #endregion Subscriptions

        #region Controller
        // void SetInitialState()
        // {
        //     DebugExtension.DefaultGenericLog();


        // }
        #endregion Controller

        #region View

        // TODO: REVIEW THIS
        // TODO: review visual bug on TextMeshProUGUI.RevelationAnimation
        // TODO: with empty text example (on matchmaking profile cards
        // TODO: at matchmaking service showcase scene)
        // TODO: search globaly: #RevelationAnimation-bug-093

        // TODO: REVIEW THIS
        // TODO: fix wrong behavior on really fast
        // TODO: animationDuration, based on this
        // TODO: changing:e9ed10aa5bf265e16e0700abd1263324fe888ab1
        public static async void RevelationAnimation(
            this TMP_Text textComponent,
            float animationDuration,
            string text = null,
            Color32 disabledColor = default)
        {
            try
            {
                RevelationAnimationUnsafe(
                    textComponent,
                    animationDuration,
                    text,
                    disabledColor
                );
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogException(e);
            }
        }

        // TODO: REVIEW THIS
        // TODO: fix wrong behavior on really fast
        // TODO: animationDuration, based on this
        // TODO: changing:e9ed10aa5bf265e16e0700abd1263324fe888ab1
        public static async void RevelationAnimationUnsafe(
            this TMP_Text textComponent,
            float animationDuration,
            string text = null,
            Color32 disabledColor = default)
        {
            List<Color32> _startCharactersColorsList = new List<Color32>();
            List<Color32> resetedColorList = new List<Color32>();

            if (disabledColor.Equals(default(Color32)))
                disabledColor = new Color32(0, 0, 0, 0);

            if (textComponent is null)
                return;

            if (text != null)
                textComponent.text = text;

            textComponent.ForceMeshUpdate();


            TMP_TextInfo textInfo = textComponent.textInfo;

            Color32[] vertexColors;
            int charactersAmount = 0;

            for (int characterIndex = 0; characterIndex < textInfo.characterInfo.Length; characterIndex++)
            {
                if (textInfo.characterInfo[characterIndex].isVisible)
                {
                    int materialIndex = textInfo.characterInfo[characterIndex].materialReferenceIndex;
                    int vertexIndex = textInfo.characterInfo[characterIndex].vertexIndex;

                    vertexColors = textInfo.meshInfo[materialIndex].colors32;

                    for (int i = vertexIndex; i < vertexIndex + 4; i++)
                    {
                        _startCharactersColorsList.Add(vertexColors[i]);
                        vertexColors[i] = disabledColor;
                    }

                    charactersAmount++;
                }
            }

            textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

            float characterAnimationDuration = animationDuration / charactersAmount;
            int characterAnimationDurationMillisecond = MillisecondsBySeconds(characterAnimationDuration);

            int characterNumber = 0;

            for (int characterIndex = 0; characterIndex < textInfo.characterInfo.Length; characterIndex++)
            {
                if (textInfo.characterInfo[characterIndex].isVisible)
                {
                    int materialIndex = textInfo.characterInfo[characterIndex].materialReferenceIndex;
                    int vertexIndex = textInfo.characterInfo[characterIndex].vertexIndex;

                    vertexColors = textInfo.meshInfo[materialIndex].colors32;

                    string debugText = null;

                    for (int i = characterNumber; i < vertexIndex + 4; i++)
                    {

                        if (i < _startCharactersColorsList.Count)
                        {
                            Color32 _startCharactersColor = _startCharactersColorsList[i];
                            vertexColors[i] = _startCharactersColor;
                        }
                        else
                        {
                            debugText =
                                "index is greater than list! | " +
                                "i = " + i + " | " +
                                "list.Count = " + _startCharactersColorsList.Count + " | " +
                                "[ REVIEW THIS! ]".ToColor(GoodCollors.pink);
                        }

                    }

                    if (debugText != null)
                        DebugExtension.DevLogWarning(debugText);

                    if (null == textComponent)
                        continue;

                    textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

                    characterNumber += 4;
                    await Task.Delay(characterAnimationDurationMillisecond);
                }
            }
        }

        public static IEnumerator RevelationAnimationCoroutine(
            this TMP_Text textComponent,
            float animationDuration,
            string text = null,
            Color32 disabledColor = default,
            Action onFinishCallback = null)
        {
            List<Color32> _startCharactersColorsList = new List<Color32>();
            List<Color32> resetedColorList = new List<Color32>();

            if (disabledColor.Equals(default(Color32)))
                disabledColor = new Color32(0, 0, 0, 0);

            if (text != null)
                textComponent.text = text;

            textComponent.ForceMeshUpdate();


            TMP_TextInfo textInfo = textComponent.textInfo;

            Color32[] vertexColors;
            int charactersAmount = 0;

            if (textInfo == null)
            {
                DebugExtension.DevLogWarning("textInfo IS NULL!");
                textComponent.text = text;
                yield break;
            }

            if (textInfo.characterInfo == null)
            {
                DebugExtension.DevLogWarning("textInfo.characterInfo IS NULL!");
                textComponent.text = text;
                yield break;
            }

            for (int characterIndex = 0; characterIndex < textInfo.characterInfo.Length; characterIndex++)
            {
                if (textInfo.characterInfo[characterIndex].isVisible)
                {
                    int materialIndex = textInfo.characterInfo[characterIndex].materialReferenceIndex;
                    int vertexIndex = textInfo.characterInfo[characterIndex].vertexIndex;

                    vertexColors = textInfo.meshInfo[materialIndex].colors32;

                    for (int i = vertexIndex; i < vertexIndex + 4; i++)
                    {
                        _startCharactersColorsList.Add(vertexColors[i]);
                        vertexColors[i] = disabledColor;
                    }

                    charactersAmount++;
                }
            }

            // textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

            float characterAnimationDuration = animationDuration / charactersAmount;

            float expectedPassedTimeSum = 0;
            float realPassedTimeSum = 0;
            int characterNumber = 0;

            for (int characterIndex = 0; characterIndex < textInfo.characterInfo.Length; characterIndex++)
            {
                if (textInfo.characterInfo[characterIndex].isVisible)
                {
                    float startTime = Time.time;

                    int materialIndex = textInfo.characterInfo[characterIndex].materialReferenceIndex;
                    int vertexIndex = textInfo.characterInfo[characterIndex].vertexIndex;

                    vertexColors = textInfo.meshInfo[materialIndex].colors32;

                    string debugText = null;

                    for (int i = characterNumber; i < vertexIndex + 4; i++)
                    {

                        if (i < _startCharactersColorsList.Count)
                        {
                            Color32 _startCharactersColor = _startCharactersColorsList[i];
                            vertexColors[i] = _startCharactersColor;
                        }
                        else
                        {
                            debugText =
                                "index is greater than list! | " +
                                "i = " + i + " | " +
                                "list.Count = " + _startCharactersColorsList.Count + " | " +
                                "[ REVIEW THIS! ]".ToColor(GoodCollors.pink);
                        }

                    }

                    if (debugText != null)
                        DebugExtension.DevLogWarning(debugText);

                    expectedPassedTimeSum += characterAnimationDuration;

                    if (realPassedTimeSum < expectedPassedTimeSum)
                    {
                        textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
                        yield return new WaitForSeconds(expectedPassedTimeSum - realPassedTimeSum);
                    }

                    float elapsedTime = Time.time - startTime;
                    realPassedTimeSum += elapsedTime;

                    characterNumber += 4;
                }
            }

            textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

            onFinishCallback?.Invoke();
        }

        // public static IEnumerator RevelationAnimationCoroutine(
        //     this TextMeshProUGUI[] textComponents,
        //     float animationDuration,
        //     string text = null,
        //     Color32 disabledColor = default)
        // {
        //     List<Color32> _startCharactersColorsList = new List<Color32>();
        //     List<Color32> resetedColorList = new List<Color32>();

        //     if (disabledColor.Equals(default(Color32)))
        //         disabledColor = new Color32(0, 0, 0, 0);

        //     TMP_Text firstTextComponent = textComponents[0];

        //     if (text != null)
        //         firstTextComponent.text = text;

        //     // firstTextComponent.ForceMeshUpdate();
        //     foreach (var textComponent in textComponents)
        //         textComponent.ForceMeshUpdate();


        //     TMP_TextInfo textInfo = firstTextComponent.textInfo;

        //     Color32[] vertexColors;
        //     int charactersAmount = 0;

        //     if (textInfo == null)
        //     {
        //         DebugExtension.DevLogWarning("textInfo IS NULL!");
        //         // firstTextComponent.text = text;
        //         foreach (var textComponent in textComponents)
        //             textComponent.text = text;
        //         yield break;
        //     }

        //     if (textInfo.characterInfo == null)
        //     {
        //         DebugExtension.DevLogWarning("textInfo.characterInfo IS NULL!");
        //         // firstTextComponent.text = text;
        //         foreach (var textComponent in textComponents)
        //             textComponent.text = text;
        //         yield break;
        //     }

        //     for (int characterIndex = 0; characterIndex < textInfo.characterInfo.Length; characterIndex++)
        //     {
        //         if (textInfo.characterInfo[characterIndex].isVisible)
        //         {
        //             int materialIndex = textInfo.characterInfo[characterIndex].materialReferenceIndex;
        //             int vertexIndex = textInfo.characterInfo[characterIndex].vertexIndex;

        //             vertexColors = textInfo.meshInfo[materialIndex].colors32;

        //             for (int i = vertexIndex; i < vertexIndex + 4; i++)
        //             {
        //                 _startCharactersColorsList.Add(vertexColors[i]);
        //                 vertexColors[i] = disabledColor;
        //             }

        //             charactersAmount++;
        //         }
        //     }

        //     // textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

        //     float characterAnimationDuration = animationDuration / charactersAmount;

        //     float expectedPassedTimeSum = 0;
        //     float realPassedTimeSum = 0;
        //     int characterNumber = 0;

        //     for (int characterIndex = 0; characterIndex < textInfo.characterInfo.Length; characterIndex++)
        //     {
        //         if (textInfo.characterInfo[characterIndex].isVisible)
        //         {
        //             float startTime = Time.time;

        //             int materialIndex = textInfo.characterInfo[characterIndex].materialReferenceIndex;
        //             int vertexIndex = textInfo.characterInfo[characterIndex].vertexIndex;

        //             vertexColors = textInfo.meshInfo[materialIndex].colors32;

        //             string debugText = null;

        //             for (int i = characterNumber; i < vertexIndex + 4; i++)
        //             {

        //                 if (i < _startCharactersColorsList.Count)
        //                 {
        //                     Color32 _startCharactersColor = _startCharactersColorsList[i];
        //                     vertexColors[i] = _startCharactersColor;
        //                 }
        //                 else
        //                 {
        //                     debugText =
        //                         "index is greater than list! | " +
        //                         "i = " + i + " | " +
        //                         "list.Count = " + _startCharactersColorsList.Count + " | " +
        //                         "[ REVIEW THIS! ]".ToColor(GoodCollors.pink);
        //                 }

        //             }

        //             if (debugText != null)
        //                 DebugExtension.DevLogWarning(debugText);

        //             expectedPassedTimeSum += characterAnimationDuration;

        //             if (realPassedTimeSum < expectedPassedTimeSum)
        //             {
        //                 // firstTextComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
        //                 foreach (var textComponent in textComponents)
        //                     textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
        //                 yield return new WaitForSeconds(expectedPassedTimeSum - realPassedTimeSum);
        //             }

        //             float elapsedTime = Time.time - startTime;
        //             realPassedTimeSum += elapsedTime;

        //             characterNumber += 4;
        //         }
        //     }

        //     // firstTextComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
        //     foreach (var textComponent in textComponents)
        //         textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
        // }

        // TODO: REVIEW THIS
        // TODO: review this naming

        // TODO: REVIEW THIS
        // TODO: fix wrong behavior on really fast
        // TODO: animationDuration, based on this
        // TODO: changing:e9ed10aa5bf265e16e0700abd1263324fe888ab1
        public static async void ReverseRevelationAnimation(
            this TMP_Text textComponent,
            float animationDuration,
            string text = null,
            Color32 disabledColor = default)
        {
            List<Color32> _startCharactersColorsList = new List<Color32>();
            List<Color32> resetedColorList = new List<Color32>();

            if (disabledColor.Equals(default(Color32)))
                disabledColor = new Color32(0, 0, 0, 0);

            if (text != null)
                textComponent.text = text;

            textComponent.ForceMeshUpdate();


            TMP_TextInfo textInfo = textComponent.textInfo;

            Color32[] vertexColors;
            int charactersAmount = 0;

            for (int characterIndex = 0; characterIndex < textInfo.characterInfo.Length; characterIndex++)
            {
                if (textInfo.characterInfo[characterIndex].isVisible)
                    charactersAmount++;
            }

            textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

            float characterAnimationDuration = animationDuration / charactersAmount;
            int characterAnimationDurationMillisecond = MillisecondsBySeconds(characterAnimationDuration);

            int characterNumber = 0;

            for (int characterIndex = 0; characterIndex < textInfo.characterInfo.Length; characterIndex++)
            {
                if (textInfo.characterInfo[characterIndex].isVisible)
                {
                    int materialIndex = textInfo.characterInfo[characterIndex].materialReferenceIndex;
                    int vertexIndex = textInfo.characterInfo[characterIndex].vertexIndex;

                    vertexColors = textInfo.meshInfo[materialIndex].colors32;

                    for (int i = characterNumber; i < vertexIndex + 4; i++)
                        vertexColors[i] = disabledColor;

                    textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

                    characterNumber += 4;
                    await Task.Delay(characterAnimationDurationMillisecond);
                }
            }
        }

        public static IEnumerator ReverseRevelationAnimationCoroutine(
            this TMP_Text textComponent,
            float animationDuration,
            string text = null,
            Color32 disabledColor = default,
            bool leftToRight = false,
            Action onFinishCallback = null)
        {
            List<Color32> _startCharactersColorsList = new List<Color32>();
            List<Color32> resetedColorList = new List<Color32>();

            if (disabledColor.Equals(default(Color32)))
                disabledColor = new Color32(0, 0, 0, 0);

            if (text != null)
                textComponent.text = text;

            textComponent.ForceMeshUpdate();


            TMP_TextInfo textInfo = textComponent.textInfo;

            Color32[] vertexColors;
            int charactersAmount = 0;

            for (int characterIndex = 0; characterIndex < textInfo.characterInfo.Length; characterIndex++)
            {
                if (textInfo.characterInfo[characterIndex].isVisible)
                    charactersAmount++;
            }

            textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

            float characterAnimationDuration = animationDuration / charactersAmount;

            int characterNumber = 0;
            int characterIndexStart = leftToRight ? 0 : textInfo.characterInfo.Length - 1;
            int characterIndexEnd = leftToRight ? textInfo.characterInfo.Length : -1;
            int characterIndexStep = leftToRight ? 1 : -1;

            for (int characterIndex = characterIndexStart; characterIndex != characterIndexEnd; characterIndex += characterIndexStep)
            {
                if (textInfo.characterInfo[characterIndex].isVisible)
                {
                    int materialIndex = textInfo.characterInfo[characterIndex].materialReferenceIndex;
                    int vertexIndex = textInfo.characterInfo[characterIndex].vertexIndex;

                    vertexColors = textInfo.meshInfo[materialIndex].colors32;

                    for (int i = vertexIndex; i < vertexIndex + 4; i++)
                        vertexColors[i] = disabledColor;

                    textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

                    yield return new WaitForSeconds(characterAnimationDuration);
                }
            }

            onFinishCallback?.Invoke();
        }

        // public static IEnumerator F(
        //     this TextMeshProUGUI[] textComponents,
        //     float animationDuration,
        //     string text = null,
        //     Color32 disabledColor = default)
        // {
        //     List<Color32> _startCharactersColorsList = new List<Color32>();
        //     List<Color32> resetedColorList = new List<Color32>();

        //     if (disabledColor.Equals(default(Color32)))
        //         disabledColor = new Color32(0, 0, 0, 0);

        //     TMP_Text firstTextComponent = textComponents[0];

        //     if (text != null)
        //     {
        //         // firstTextComponent.text = text;
        //         foreach (var textComponent in textComponents)
        //             textComponent.text = text;
        //     }

        //     // firstTextComponent.ForceMeshUpdate();
        //     foreach (var textComponent in textComponents)
        //         textComponent.ForceMeshUpdate();

        //     TMP_TextInfo textInfo = firstTextComponent.textInfo;

        //     Color32[] vertexColors;
        //     int charactersAmount = 0;

        //     for (int characterIndex = 0; characterIndex < textInfo.characterInfo.Length; characterIndex++)
        //     {
        //         if (textInfo.characterInfo[characterIndex].isVisible)
        //             charactersAmount++;
        //     }

        //     // firstTextComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
        //     foreach (var textComponent in textComponents)
        //         textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

        //     float characterAnimationDuration = animationDuration / charactersAmount;

        //     int characterNumber = 0;

        //     for (int characterIndex = 0; characterIndex < textInfo.characterInfo.Length; characterIndex++)
        //     {
        //         if (textInfo.characterInfo[characterIndex].isVisible)
        //         {
        //             int materialIndex = textInfo.characterInfo[characterIndex].materialReferenceIndex;
        //             int vertexIndex = textInfo.characterInfo[characterIndex].vertexIndex;

        //             vertexColors = textInfo.meshInfo[materialIndex].colors32;

        //             for (int i = characterNumber; i < vertexIndex + 4; i++)
        //                 vertexColors[i] = disabledColor;

        //             // firstTextComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
        //             foreach (var textComponent in textComponents)
        //                 textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

        //             characterNumber += 4;
        //             yield return new WaitForSeconds(characterAnimationDuration);
        //         }
        //     }
        // }
        #endregion View
    }
}
