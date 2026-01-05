// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
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


namespace JovDK.Core.Queues.Coroutines
{

    /// <summary>
    /// A async + coroutine queue for Unity:
    /// - Enqueue coroutines (IEnumerator) to run sequentially.
    /// - Enqueue async Tasks to run sequentially, yielding to Unity until completion.
    /// - Each enqueue returns a Task you can await for completion/errors.
    ///
    /// Notes:
    /// - Processing runs on the Unity main thread (via a host MonoBehaviour).
    /// - If a queued Task uses background threads, that's fine; we just "wait" from a coroutine.
    /// </summary>
    public partial class Coroutines_AsyncQueue : IDisposable
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // [SerializeField] bool _dependencies;


        [Space(5), Header("[ State ]"), Space(10)]

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

        private readonly MonoBehaviour _host;
        private readonly Queue<WorkItem> _queue = new();
        private readonly object _gate = new();
        private Coroutine _runner;
        private bool _disposed;
        private readonly CancellationTokenSource _disposeCts = new();



        // [Space(5), Header("[ Parts ]"), Space(10)]

        // [SerializeField] bool _parts;
        // [SerializeField] Button _mainButton;
        // [SerializeField] TextMeshProUGUI _mainText;
        // [SerializeField] Image _mainImage;
        // [SerializeField] Transform _mainContainer;


        // [Space(5), Header("[ Configs ]"), Space(10)]

        // [SerializeField] bool _configs;



        public Coroutines_AsyncQueue(MonoBehaviour host)
        {
            _host = host ? host : throw new ArgumentNullException(nameof(host));
        }



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
        // protected virtual void TryToKillBackgroundTween()
        // {
        //     if (_curretBackgroundTween.IsActive())
        //         _curretBackgroundTween.Kill();
        // }
        #endregion View


        /// <summary>Number of items currently waiting (not including the active one).</summary>
        public int PendingCount
        {
            get { lock (_gate) return _queue.Count; }
        }

        /// <summary>True while the queue coroutine is running.</summary>
        public bool IsRunning
        {
            get { lock (_gate) return _runner != null; }
        }

        /// <summary>
        /// Enqueue a coroutine. It will run after all previously enqueued items finish.
        /// </summary>
        public Task Enqueue(IEnumerator routine, CancellationToken ct = default)
        {
            if (routine == null) throw new ArgumentNullException(nameof(routine));
            ThrowIfDisposed();

            var tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var linked = CreateLinkedToken(ct);

            EnqueueInternal(new WorkItem(
                routine: WrapCoroutine(routine, linked, tcs),
                tcs: tcs,
                ct: linked
            ));

            return tcs.Task;
        }

        /// <summary>
        /// Enqueue an async function. We will start it when it reaches the front of the queue,
        /// and yield each frame until it completes. Exceptions/cancellation are propagated.
        /// </summary>
        public Task Enqueue(Func<CancellationToken, Task> asyncFunc, CancellationToken ct = default)
        {
            if (asyncFunc == null) throw new ArgumentNullException(nameof(asyncFunc));
            ThrowIfDisposed();

            var tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var linked = CreateLinkedToken(ct);

            IEnumerator Routine()
            {
                Task task = null;

                // Start the task at the moment it becomes active in the queue.
                try
                {
                    linked.ThrowIfCancellationRequested();
                    task = asyncFunc(linked);
                    if (task == null)
                        throw new InvalidOperationException("asyncFunc returned null Task.");
                }
                catch (OperationCanceledException oce)
                {
                    tcs.TrySetCanceled(oce.CancellationToken);
                    yield break;
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                    yield break;
                }

                // Wait for completion (Unity-friendly).
                while (!task.IsCompleted)
                {
                    if (linked.IsCancellationRequested)
                    {
                        // We cannot forcibly stop an arbitrary Task; we just mark queue item canceled.
                        tcs.TrySetCanceled(linked);
                        yield break;
                    }

                    yield return null;
                }

                // Propagate outcome.
                if (task.IsCanceled)
                {
                    tcs.TrySetCanceled(linked);
                    yield break;
                }

                if (task.IsFaulted)
                {
                    // Unwrap AggregateException for nicer stacks.
                    var ex = task.Exception?.InnerException ?? task.Exception;
                    tcs.TrySetException(ex ?? new Exception("Task faulted with unknown error."));
                    yield break;
                }

                tcs.TrySetResult(true);
            }

            EnqueueInternal(new WorkItem(
                routine: WrapCoroutine(Routine(), linked, tcs),
                tcs: tcs,
                ct: linked
            ));

            return tcs.Task;
        }

        /// <summary>
        /// Enqueue a synchronous action to run on the Unity main thread.
        /// </summary>
        public Task Enqueue(Action action, CancellationToken ct = default)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            return Enqueue(WrapActionAsCoroutine(action), ct);
        }

        /// <summary>
        /// Cancels everything pending. The currently running item cannot be forcibly stopped
        /// (Unity limitation), but pending items will complete as canceled.
        /// </summary>
        public void CancelPending()
        {
            ThrowIfDisposed();

            List<WorkItem> pending = null;

            lock (_gate)
            {
                if (_queue.Count == 0) return;

                pending = new List<WorkItem>(_queue.Count);
                while (_queue.Count > 0)
                    pending.Add(_queue.Dequeue());
            }

            foreach (var item in pending)
                item.Tcs.TrySetCanceled(item.Ct);
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;

            _disposeCts.Cancel();

            // Cancel pending work.
            CancelPending();

            // Stop runner if host still exists.
            if (_host && _runner != null)
                _host.StopCoroutine(_runner);

            _runner = null;
            _disposeCts.Dispose();
        }

        // -----------------------
        // Internals
        // -----------------------

        private void EnqueueInternal(WorkItem item)
        {
            lock (_gate)
            {
                _queue.Enqueue(item);

                // Start runner if not already running.
                if (_runner == null)
                    _runner = _host.StartCoroutine(RunLoop());
            }
        }

        private IEnumerator RunLoop()
        {
            while (true)
            {
                WorkItem item;

                lock (_gate)
                {
                    if (_queue.Count == 0)
                    {
                        _runner = null;
                        yield break;
                    }

                    item = _queue.Dequeue();
                }

                // Execute item coroutine.
                yield return item.Routine;
            }
        }

        private static IEnumerator WrapActionAsCoroutine(Action action)
        {
            action();
            yield break;
        }

        private static IEnumerator WrapCoroutine(IEnumerator routine, CancellationToken ct, TaskCompletionSource<bool> tcs)
        {
            // If the routine already manages completion itself (like our Task wrapper),
            // we don't want to double-complete. So: only complete if the TCS isn't finished.
            bool completed = false;

            try
            {
                while (true)
                {
                    if (ct.IsCancellationRequested)
                    {
                        tcs.TrySetCanceled(ct);
                        yield break;
                    }

                    if (!routine.MoveNext())
                        break;

                    yield return routine.Current;
                }

                completed = true;
                tcs.TrySetResult(true);
            }
            catch (OperationCanceledException oce)
            {
                tcs.TrySetCanceled(oce.CancellationToken);
            }
            catch (Exception ex)
            {
                tcs.TrySetException(ex);
            }
            finally
            {
                // If routine ended without exceptions but some inner wrapper already completed/canceled,
                // do nothing; TrySetResult is safe, but keeping a guard is clearer.
                if (!completed && tcs.Task.IsCompleted)
                {
                    // No-op: inner wrapper handled it.
                }
            }
        }

        private CancellationToken CreateLinkedToken(CancellationToken ct)
        {
            if (!ct.CanBeCanceled && !_disposeCts.Token.CanBeCanceled)
                return CancellationToken.None;

            // Linked tokens are helpful so Dispose() cancels all pending items.
            return CancellationTokenSource.CreateLinkedTokenSource(ct, _disposeCts.Token).Token;
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(Coroutines_AsyncQueue));
        }

        private readonly struct WorkItem
        {
            public readonly IEnumerator Routine;
            public readonly TaskCompletionSource<bool> Tcs;
            public readonly CancellationToken Ct;

            public WorkItem(IEnumerator routine, TaskCompletionSource<bool> tcs, CancellationToken ct)
            {
                Routine = routine;
                Tcs = tcs;
                Ct = ct;
            }
        }
    }
}
