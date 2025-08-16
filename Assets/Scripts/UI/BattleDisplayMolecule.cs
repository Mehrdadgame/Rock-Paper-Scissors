using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Helpers.GameEnums;

public class BattleDisplayMolecule
{
    private ChoiceDisplayAtom botChoice;
    private ChoiceDisplayAtom playerChoice;
    private Label vsLabel;

    public BattleDisplayMolecule(VisualElement parent)
    {
        var container = new VisualElement();
        container.style.flexDirection = FlexDirection.Column;
        container.style.alignItems = Align.Center;
        container.style.flexGrow = 1;
        container.style.justifyContent = Justify.SpaceAround;

        // Bot choice (top)
        var botContainer = new VisualElement();
        botContainer.style.alignItems = Align.Center;
        botContainer.style.marginBottom = 30;
        botChoice = new ChoiceDisplayAtom(botContainer, "Bot's Choice");
        container.Add(botContainer);

        // VS label
        vsLabel = new Label("VS");
        vsLabel.style.color = Color.white;
        vsLabel.style.fontSize = 24;
        vsLabel.style.unityFontStyleAndWeight = FontStyle.Bold;
        vsLabel.style.marginTop = 20;
        vsLabel.style.marginBottom = 20;
        container.Add(vsLabel);

        // Player choice (bottom)
        var playerContainer = new VisualElement();
        playerContainer.style.alignItems = Align.Center;
        playerContainer.style.marginTop = 30;
        playerChoice = new ChoiceDisplayAtom(playerContainer, "Your Choice");
        container.Add(playerContainer);

        parent.Add(container);
    }

    public void UpdateChoices(Sprite playerSprite, Sprite botSprite)
    {
        playerChoice.UpdateChoice(playerSprite);
        botChoice.UpdateChoice(botSprite);

        // Only show VS when game is actually playing and both have made choices
        bool shouldShowVS = playerSprite != null && botSprite != null &&
                           playerSprite.name != "default" && botSprite.name != "default";

        if (shouldShowVS)
        {
            vsLabel.style.display = DisplayStyle.Flex;
            vsLabel.style.visibility = Visibility.Visible;
        }
        else
        {
            vsLabel.style.display = DisplayStyle.None;
            vsLabel.style.visibility = Visibility.Hidden;
        }
    }

    public void SetBattleResult(GameResult result)
    {
        Color playerColor = Color.white;
        Color botColor = Color.white;

        switch (result)
        {
            case GameResult.PlayerWin:
                playerColor = Color.green;
                botColor = Color.red;
                break;
            case GameResult.BotWin:
                playerColor = Color.red;
                botColor = Color.green;
                break;
            case GameResult.Draw:
                playerColor = Color.yellow;
                botColor = Color.yellow;
                break;
        }

        playerChoice.SetTitleColor(playerColor);
        botChoice.SetTitleColor(botColor);
    }

    public void Reset()
    {
        // Hide VS label on reset
        vsLabel.style.display = DisplayStyle.None;
        vsLabel.style.visibility = Visibility.Hidden;
        playerChoice.SetTitleColor(Color.white);
        botChoice.SetTitleColor(Color.white);
    }
}