using Godot;
using GodotGame.General;
using System;

namespace GodotGame.UI
{

    public class TextTyper : RichTextLabel, IUIElement
    {
        string currentSentence = string.Empty;
        int currentSentenceCharCount = 0;
        int lastCharIndex = 0;
        char[] chars;

        readonly char[] tagBoundary = new char[2] { '[', ']' };
        readonly char[] seperator = new char[2] { '=', ' ' };

        public Action StopedTyping;

        Timer timer;

        bool isActive = true;
        public bool IsActive
        {
            get => isActive;
            set
            {
                Visible = value;

                if (!value) Stop();
            }
        }

        public override void _Ready()
        {
            timer = GetNode<Timer>("Timer");
        }

        public void Reset()
        {
            Stop();
            BbcodeText = string.Empty;
        }

        public void Stop()
        {
            BbcodeText = currentSentence;

            lastCharIndex = 0;

            timer.Stop();

            StopedTyping?.Invoke();
        }

        public void TypeSentence(string text, float timeBetweenCharacters)
        {
            if (string.IsNullOrEmpty(text))
            { currentSentence = "..."; }
            else
            { currentSentence = text; }

            currentSentenceCharCount = currentSentence.Length;

            chars = currentSentence.ToCharArray();

            Text = string.Empty;

            timer.WaitTime = timeBetweenCharacters;
            timer.Start(-1);
        }

        public void OnTimerTimeout()
        {
            if (chars[lastCharIndex] == '[')
            {
                string newSentence = currentSentence.Remove(0, lastCharIndex);

                GD.Print(": " + newSentence);

                string tag = newSentence.Split(tagBoundary, StringSplitOptions.RemoveEmptyEntries)[0];
                string keyWord = tag.Split(seperator, StringSplitOptions.RemoveEmptyEntries)[0];
                GD.Print("tag: " + tag);

                if (keyWord.StartsWith("/")) keyWord = keyWord.Remove(0, 1);
                GD.Print("keyword: " + keyWord);

                int length = tag.Length() + 2; //'[', ']'
                lastCharIndex += length;

                if (tag == "PlayerName") AppendBbcode($"[{GameManager.СurrentSaveFile}]");

                AppendBbcode($"[{tag}]");

                currentSentenceCharCount -= length;
                /*
                            GD.Print(lastCharIndex);
                            GD.Print($"{Text.Length}/{currentSentenceCharCount}");
                */
                if (currentSentenceCharCount == Text.Length) Stop();
                if (BbcodeText == currentSentence) Stop();

                OnTimerTimeout();
            }

            AppendBbcode(chars[lastCharIndex].ToString());

            lastCharIndex++;

            if (currentSentenceCharCount == Text.Length) Stop();
            if (BbcodeText == currentSentence) Stop();
        }

        /*		bool isBBcodeTag(string input)
            {
                switch (input)
                {
                    case "i":
                        return true;
                    case "b":
                        return true;
                    case "u":
                        return true;
                    case "s":
                        return true;
                    case "code":
                        return true;
                    case "center":
                        return true;
                    case "right":
                        return true;
                    case "left":
                        return true;
                    case "fill":
                        return true;
                    case "indent":
                        return true;
                    case "url":
                        return true;
                    case "img":
                        return true;
                    case "font":
                        return true;
                    case "color":
                        return true;
                    case "table":
                        return true;
                    case "cell":
                        return true;
                    case "shake":
                        return true;
                    case "wave":
                        return true;
                    case "tornado":
                        return true;
                    case "fade":
                        return true;
                    case "rainbow":
                        return true;
                    default:
                        return false;
                }
            }*/
    }


}