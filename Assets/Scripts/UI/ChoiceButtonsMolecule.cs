using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Helpers.GameEnums;

public class ChoiceButtonsMolecule
{
    private ChoiceButtonAtom rockButton;
    private ChoiceButtonAtom paperButton;
    private ChoiceButtonAtom scissorsButton;

    public event Action<GameChoice> OnChoiceSelected;

    public ChoiceButtonsMolecule(VisualElement parent)
    {
        var container = new VisualElement();
        container.AddToClassList("buttons-container");
        container.style.flexDirection = FlexDirection.Row;
        container.style.justifyContent = Justify.SpaceAround;
        container.style.width = Length.Percent(100);
        container.style.marginTop = 20;

        rockButton = new ChoiceButtonAtom(container, GameChoice.Rock, "🪨");
        paperButton = new ChoiceButtonAtom(container, GameChoice.Paper, "📄");
        scissorsButton = new ChoiceButtonAtom(container, GameChoice.Scissors, "✂️");

        rockButton.OnChoiceSelected += (choice) => OnChoiceSelected?.Invoke(choice);
        paperButton.OnChoiceSelected += (choice) => OnChoiceSelected?.Invoke(choice);
        scissorsButton.OnChoiceSelected += (choice) => OnChoiceSelected?.Invoke(choice);

        parent.Add(container);
    }

    public void SetButtonsEnabled(bool enabled)
    {
        rockButton.SetEnabled(enabled);
        paperButton.SetEnabled(enabled);
        scissorsButton.SetEnabled(enabled);
    }

    public void HighlightChoice(GameChoice choice)
    {
        rockButton.SetHighlight(choice == GameChoice.Rock);
        paperButton.SetHighlight(choice == GameChoice.Paper);
        scissorsButton.SetHighlight(choice == GameChoice.Scissors);
    }
}