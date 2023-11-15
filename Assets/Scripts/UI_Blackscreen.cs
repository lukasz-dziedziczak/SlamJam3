using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class UI_Blackscreen : MonoBehaviour
{
    [SerializeField] float fadeInTime = 1f;
    [SerializeField] float fadeOutTime = 1f;
    [SerializeField] Image image;

    State state = State.Black;
    float timer;

    public event Action FadeInComplete;
    public event Action FadeOutComplete;

    public enum State
    {
        FadingIn,
        FadingOut,
        Transparent,
        Black
    }

    private void Start()
    {
        SetBlack();
        StartFadeIn();
    }

    private void Update()
    {
        if (state == State.FadingIn)
        {
            timer += Time.deltaTime;
            float newAlpha = Mathf.Lerp(1, 0, timer/fadeInTime);
            SetAlpha(newAlpha);
            if (timer > fadeOutTime)
            {
                SetTransparent();
                FadeInComplete?.Invoke();
                FadeInComplete = null;
            }
        }

        else if (state == State.FadingOut)
        {
            timer += Time.deltaTime;
            float newAlpha = Mathf.Lerp(0, 1, timer / fadeOutTime);
            SetAlpha(newAlpha);
            if (timer > fadeOutTime)
            {
                SetBlack();
                FadeOutComplete?.Invoke();
                FadeOutComplete = null;
            }
        }
    }

    public void SetBlack()
    {
        SetAlpha(1f);
        state = State.Black;
    }

    public void SetTransparent()
    {
        SetAlpha(0f);
        state = State.Transparent;
    }

    public void StartFadeIn()
    {
        state = State.FadingIn;
        timer = 0;
    }

    public void StartFadeOut()
    {
        state = State.FadingOut;
        timer = 0;
    }

    public void SetAlpha(float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }
}
