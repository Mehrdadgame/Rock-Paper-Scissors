using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Helpers.GameEnums;

public class ChoiceButtonAtom
{
    private Button button;
    private VisualElement iconContainer;

    public event Action<GameChoice> OnChoiceSelected;

    public ChoiceButtonAtom(VisualElement parent, GameChoice choice, Sprite sprite)
    {
        button = new Button();
        button.AddToClassList("choice-button");

        // Style the button
        button.style.width = 90;
        button.style.height = 90;
        button.style.borderTopLeftRadius = 15;
        button.style.borderTopRightRadius = 15;
        button.style.borderBottomLeftRadius = 15;
        button.style.borderBottomRightRadius = 15;
        button.style.borderLeftWidth = 2;
        button.style.borderRightWidth = 2;
        button.style.borderTopWidth = 2;
        button.style.borderBottomWidth = 2;
        button.style.borderLeftColor = new Color(1f, 1f, 1f, 0.6f);
        button.style.borderRightColor = new Color(1f, 1f, 1f, 0.6f);
        button.style.borderTopColor = new Color(1f, 1f, 1f, 0.6f);
        button.style.borderBottomColor = new Color(1f, 1f, 1f, 0.6f);
        button.style.backgroundColor = new Color(1f, 1f, 1f, 0.1f);

        // Create icon container
        iconContainer = new VisualElement();
        iconContainer.style.width = Length.Percent(80);
        iconContainer.style.height = Length.Percent(80);
        iconContainer.style.alignSelf = Align.Center;
        iconContainer.style.justifyContent = Justify.Center;
        iconContainer.style.alignItems = Align.Center;

        // Set sprite if provided
        if (sprite != null)
        {
            iconContainer.style.backgroundImage = new StyleBackground(sprite);
        }

        button.Add(iconContainer);
        button.clicked += () => OnChoiceSelected?.Invoke(choice);

        parent.Add(button);
    }

    public void UpdateSprite(Sprite sprite)
    {
        if (sprite != null)
        {
            iconContainer.style.backgroundImage = new StyleBackground(sprite);
        }
    }

    public void SetEnabled(bool enabled)
    {
        button.SetEnabled(enabled);

        if (enabled)
        {
            button.style.opacity = 1.0f;
        }
        else
        {
            button.style.opacity = 0.5f;
        }
    }

    public void SetHighlight(bool highlight)
    {
        if (highlight)
        {
            button.style.backgroundColor = new Color(1f, 1f, 1f, 0.3f);
            button.style.borderLeftColor = Color.white;
            button.style.borderRightColor = Color.white;
            button.style.borderTopColor = Color.white;
            button.style.borderBottomColor = Color.white;
            button.style.scale = new Scale(new Vector3(1.05f, 1.05f, 1f));
        }
        else
        {
            button.style.backgroundColor = new Color(1f, 1f, 1f, 0.1f);
            button.style.borderLeftColor = new Color(1f, 1f, 1f, 0.6f);
            button.style.borderRightColor = new Color(1f, 1f, 1f, 0.6f);
            button.style.borderTopColor = new Color(1f, 1f, 1f, 0.6f);
            button.style.borderBottomColor = new Color(1f, 1f, 1f, 0.6f);
            button.style.scale = new Scale(Vector3.one);
        }
    }
}