using Godot;
using GodotGame;
using GodotGame.PlayerBehaviour;
using System;

public class LoadingScreenUI : ColorRect
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
        tween.InterpolateProperty(this,
            "modulate",
            new Color(0, 0, 0, 0),
            new Color(0, 0, 0, 1),
            TRANSITION_SPEED * 1.5f);
        tween.Start();

        tween.Connect("tween_completed", this, "StartFadingOut");
    }

    public void StartFadingOut(Godot.Object _, NodePath __)
    {
        SceneManager.InvokeOnSceneInstance();

        tween.InterpolateProperty(this,
             "modulate",
             new Color(0, 0, 0, 1),
             new Color(0, 0, 0, 0),
             TRANSITION_SPEED * 1.5f);
        tween.Start();

        tween.Disconnect("tween_completed", this, "StartFadingOut");
        tween.Connect("tween_completed", this, "StopTransition");
    }

    public void StopTransition(Godot.Object _, NodePath __)
    {
        SceneManager.inTransition = false;
        tween.Disconnect("tween_completed", this, "StopTransition");
    }

    void StartTransition()
    {
        tween.InterpolateProperty(this,
            "modulate",
            new Color(0,0,0,0),
            new Color(0, 0, 0, 1),
            TRANSITION_SPEED);
        tween.Start();

        tween.Connect("tween_completed", this, "InstantiateScene");
    }

    public void InstantiateScene(Godot.Object _, NodePath __)
    {
        while (SceneManager.isInLoad) { /*wait*/ }

        SceneManager.ApplyChanges();

        tween.InterpolateProperty(this,
             "modulate",
             new Color(0, 0, 0, 1),
             new Color(0, 0, 0, 0),
             TRANSITION_SPEED);
        tween.Start();

        tween.Disconnect("tween_completed", this, "InstantiateScene");
        tween.Connect("tween_completed", this, "StopTransition");
    }
}
