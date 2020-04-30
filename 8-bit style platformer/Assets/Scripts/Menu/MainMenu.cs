using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Track Animation Component
    // Track animation clips
    // Function recieves animation events
    // functions play fade in/out animations

    [SerializeField] private Animation _mainMenuAnimator;
    [SerializeField] private AnimationClip _fadeOutAnimation;
    [SerializeField] private AnimationClip _fadeInAnimation;

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    public void OnFadeOutComplete()
    {

    }

    public void OnFadeInComplete()
    {
        UIManager.Instance.SetDummyCameraActive(true);
    }

    void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        if(previousState == GameManager.GameState.PREGAME && currentState == GameManager.GameState.RUNNING)
        {
            FadeOut();
        }
    }

    public void FadeOut()
    {
        _mainMenuAnimator.Stop();
        _mainMenuAnimator.clip = _fadeOutAnimation;
        _mainMenuAnimator.Play();
        UIManager.Instance.SetDummyCameraActive(false);
    }

    public void FadeIn()
    {
        _mainMenuAnimator.Stop();
        _mainMenuAnimator.clip = _fadeInAnimation;
        _mainMenuAnimator.Play();
    }
}
