using Godot;
using GodotGame;
using GodotGame.PlayerBehavior;
using System;

public partial class LoadingScreenUI : ColorRect
{
    const float TRANSITION_SPEED = 0.5f;

    Tween tween;

    public override void _Ready()
    {
        tween = GetNode<Tween>("Tween");

        SceneManager.OnSceneStartedLoading += StartTransition;
        SceneManager.FadeScreen += StartFading;
    }

    private void StartFading()
    {
        tween.TweenProperty(this,
            "modulate",
            new Color(0, 0, 0, 1),
            TRANSITION_SPEED * 1.5f);
        tween.Play();

        tween.Connect("tween_completed", new Callable(this, "StartFadingOut"));
    }

    public void StartFadingOut(GodotObject _, NodePath __)
    {
        SceneManager.InvokeOnSceneInstance();

        tween.TweenProperty(this,
             "modulate",
             new Color(0, 0, 0, 0),
             TRANSITION_SPEED * 1.5f);
        tween.Play();

        tween.Disconnect("tween_completed", new Callable(this, "StartFadingOut"));
        tween.Connect("tween_completed", new Callable(this, "StopTransition"));
    }

    public void StopTransition(GodotObject _, NodePath __)
    {
        SceneManager.inTransition = false;
        tween.Disconnect("tween_completed", new Callable(this, "StopTransition"));
    }

    void StartTransition()
    {
        tween.TweenProperty(this,
            "modulate",
            new Color(0, 0, 0, 1),
            TRANSITION_SPEED);
        tween.Play();

        tween.Connect("tween_completed", new Callable(this, "InstantiateScene"));
    }

    public void InstantiateScene(GodotObject _, NodePath __)
    {
        while (SceneManager.isInLoad) { /*wait*/ }

        SceneManager.ApplyChanges();

        tween.TweenProperty(this,
             "modulate",
             new Color(0, 0, 0, 0),
             TRANSITION_SPEED);
        tween.Play();

        tween.Disconnect("tween_completed", new Callable(this, "InstantiateScene"));
        tween.Connect("tween_completed", new Callable(this, "StopTransition"));
    }
}
