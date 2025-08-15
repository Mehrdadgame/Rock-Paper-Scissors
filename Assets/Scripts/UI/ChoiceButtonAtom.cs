using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Helpers.GameEnums;

public class ChoiceButtonAtom
{
    private Button button;
    private Label iconLabel;

    public event Action<GameChoice> OnChoiceSelected;

    public ChoiceButtonAtom(VisualElement parent, GameChoice choice, string icon)
    {
        button = new Button();
        button.AddToClassList("choice-button");

        iconLabel = new Label(icon);
        iconLabel.style.fontSize = 30;
        iconLabel.style.unityTextAlign = TextAnchor.MiddleCenter;

        button.Add(iconLabel);
        button.clicked += () => OnChoiceSelected?.Invoke(choice);

        parent.Add(button);
    }

    public void SetEnabled(bool enabled)
    {
        button.SetEnabled(enabled);
    }

    public void SetHighlight(bool highlight)
    {
        button.style.backgroundColor = highlight ?
            new Color(1f, 1f, 1f, 0.3f) :
            new Color(1f, 1f, 1f, 0.1f);
    }
}
