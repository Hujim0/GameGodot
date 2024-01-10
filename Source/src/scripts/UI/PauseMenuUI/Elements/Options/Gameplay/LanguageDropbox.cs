using Godot;
using GodotGame.General;
using System;

public partial class LanguageDropbox : OptionButton
{
    public override void _Ready()
    {
        for (int i = 0; i < GameManager.AvalibleLanguages.Length; i++)
            AddItem(GameManager.AvalibleLanguages[i], i);
        
        Select(0);
    }

    public override void _Toggled(bool buttonPressed)
    {
        GameManager.ChangeLanguage(GameManager.AvalibleLanguages[Selected]);
    }
}
