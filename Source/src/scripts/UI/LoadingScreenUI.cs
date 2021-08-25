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

        SceneManager.SceneStartedLoading += StartTransition;
    }

/*    void StartTransition()
    {
        Player.Instance.SetPause(true);

        Material.Set("shader_param/reverse", false);

        Material.Set("shader_param/progress", 0f);

        tween.InterpolateProperty(Material,
            "shader_param/progress",
            0,
            0.75f,
            TRANSITION_SPEED);
        tween.Start();

        tween.Connect("tween_completed", this, "InstantiateScene");
    }

    public void InstantiateScene(Godot.Object _, NodePath __)
    {
        while (SceneManager.isInLoad) { *//*wait*//* }

        SceneManager.ApplyChanges();

        Player.Instance.SetPause(false);

        Material.Set("shader_param/reverse", true);

        Material.Set("shader_param/progress", 1f);

        tween.InterpolateProperty(Material,
            "shader_param/progress",
            1,
            0.25f,
            TRANSITION_SPEED);
        tween.Start();

        tween.Disconnect("tween_completed", this, "InstantiateScene");
        tween.Connect("tween_completed", this, "StopTransition");
    }*/

    public void StopTransition(Godot.Object _, NodePath __)
    {
        SceneManager.inTransition = false;
        tween.Disconnect("tween_completed", this, "StopTransition");
    }

    void StartTransition()
    {
        Player.Instance.SetPause(true);

        tween.InterpolateProperty(this,
            "modulate",
            new Color(0,0,0,0),
            new Color(0, 0, 0, 1),
            TRANSITION_SPEED - 0.1f);
        tween.Start();

        tween.Connect("tween_completed", this, "InstantiateScene");
    }

    public void InstantiateScene(Godot.Object _, NodePath __)
    {
        while (SceneManager.isInLoad) { /*wait*/ }

        SceneManager.ApplyChanges();

        Player.Instance.SetPause(false);

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
