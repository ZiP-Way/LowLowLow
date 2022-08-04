using Data;
using SignalsFramework;
using UnityEngine;
using UnityEngine.Profiling;
using UpdateSys;
using UniRx;

public class Swipe : MonoBehaviour, IUpdatable
{
    #region "Signals"

    public static readonly Signal<float> SwipingValueChanged = new Signal<float>();
    public static readonly Signal<bool> SwipingToggle = new Signal<bool>();
    public static readonly Signal<float> Reset = new Signal<float>();

    #endregion

    #region "Fields"

    private Vector2 startTapPosition = default,
            currentTapPosition = default;

    private float maxSwipeValue = 0f;
    private float currentSwipeValue = 0f;
    private float tapUpSwipeValue = 0f;

    #endregion

    private void Awake()
    {
        maxSwipeValue = Screen.height * 0.1f; // 10% percent of the screen size

        Hub.LevelComplete.Subscribe(_ => Swipe.SwipingToggle.Fire(false)).AddTo(this);
        Hub.LevelFailed.Subscribe(_ => Swipe.SwipingToggle.Fire(false)).AddTo(this);

        Swipe.Reset.Subscribe(value =>
        {
            tapUpSwipeValue = value; // set middle pos
            currentSwipeValue = tapUpSwipeValue;
        }).AddTo(this);

        Swipe.SwipingToggle.Subscribe(state =>
        {
            if (state)
            {
                this.StartUpdate();
            }
            else
            {
                this.StopUpdate();
            }
        }).AddTo(this);
    }

    private void OnDisable()
    {
        this.StopUpdate();
    }

    public void OnSystemUpdate(float deltaTime)
    {
        Profiler.BeginSample("Swiping");
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            startTapPosition = Input.mousePosition;
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (startTapPosition == Vector2.zero) startTapPosition = Input.mousePosition;

            currentTapPosition = Input.mousePosition;

            currentSwipeValue = (currentTapPosition.y - startTapPosition.y) / maxSwipeValue + tapUpSwipeValue;
            currentSwipeValue = Mathf.Clamp(currentSwipeValue, 0.0f, 1.0f);
            SwipingValueChanged.Fire(currentSwipeValue);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            tapUpSwipeValue = currentSwipeValue;
        }
        Profiler.EndSample();
    }
}
