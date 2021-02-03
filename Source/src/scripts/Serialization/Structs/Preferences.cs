using Godot;
using System;

public struct Preferences 
{
    public string language;

    public Vector2 resolution;

    public bool fullscreen;
    public bool vsync;

    /// <param name="language"></param>
    /// <param name="resolution"></param>
    /// <param name="fullscreen"></param>
    /// <param name="vsync"></param>
    public Preferences(string language, Vector2 resolution, bool fullscreen, bool vsync)
    {
        this.language = language;
        this.resolution = resolution;
        this.fullscreen = fullscreen;
        this.vsync = vsync;
    }
}
