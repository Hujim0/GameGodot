using Godot;
using GodotGame;
using GodotGame.PlayerBehaviour;
using System;

public class LoadingScreenUI : ColorRect
{
    Tween tween;

    public override void _Ready()
    {
        tween = GetNode<Tween>("Tween");

        SceneManager.SceneStartedLoading += StartTransition;

        tween.Connect("tween_completed", null, "LoadScene");
    }

    void StartTransition()
    {
        Player.Instance.SetPause(true);

        Material.Set("shader_param/reverse", false);

        tween.InterpolateProperty(this,
            "shader_param/progress",
            0,
            0.7f,
            10f);
        tween.Start();
    }

    public void LoadScene()
    {
        while (SceneManager.isInLoad) { /*wait*/ }

        SceneManager.ApplyChanges();

        Player.Instance.SetPause(false);

        Material.Set("shader_param/reverse", true);

        tween.InterpolateProperty(this,
            "shader_param/progress",
            1,
            0.3f,
            10f);
        tween.Start();
    }
}
