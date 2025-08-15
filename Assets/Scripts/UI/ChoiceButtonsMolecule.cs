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

    public ChoiceButtonsMolecule(VisualElement parent, Dictionary<GameChoice, Sprite> spriteMap)
    {
        var container = new VisualElement();
        container.AddToClassList("buttons-container");
        container.style.flexDirection = FlexDirection.Row;
        container.style.justifyContent = Justify.SpaceAround;
        container.style.width = Length.Percent(100);
        container.style.marginTop = 20;
        container.style.paddingLeft = 20;
        container.style.paddingRight = 20;

        // Create buttons with sprites
        rockButton = new ChoiceButtonAtom(container, GameChoice.Rock,
            spriteMap.ContainsKey(GameChoice.Rock) ? spriteMap[GameChoice.Rock] : null);
        paperButton = new ChoiceButtonAtom(container, GameChoice.Paper,
            spriteMap.ContainsKey(GameChoice.Paper) ? spriteMap[GameChoice.Paper] : null);
        scissorsButton = new ChoiceButtonAtom(container, GameChoice.Scissors,
            spriteMap.ContainsKey(GameChoice.Scissors) ? spriteMap[GameChoice.Scissors] : null);

        // Wire up events
        rockButton.OnChoiceSelected += (choice) => OnChoiceSelected?.Invoke(choice);
        paperButton.OnChoiceSelected += (choice) => OnChoiceSelected?.Invoke(choice);
        scissorsButton.OnChoiceSelected += (choice) => OnChoiceSelected?.Invoke(choice);

        parent.Add(container);
    }

    public void UpdateSprites(Dictionary<GameChoice, Sprite> spriteMap)
    {
        if (spriteMap.ContainsKey(GameChoice.Rock))
            rockButton.UpdateSprite(spriteMap[GameChoice.Rock]);

        if (spriteMap.ContainsKey(GameChoice.Paper))
            paperButton.UpdateSprite(spriteMap[GameChoice.Paper]);

        if (spriteMap.ContainsKey(GameChoice.Scissors))
            scissorsButton.UpdateSprite(spriteMap[GameChoice.Scissors]);
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