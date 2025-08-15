using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreDisplayMolecule
{
    private ScoreAtom playerScore;
    private ScoreAtom botScore;

    public ScoreDisplayMolecule(VisualElement parent)
    {
        var container = new VisualElement();
        container.AddToClassList("score-container");
        container.style.flexDirection = FlexDirection.Row;
        container.style.justifyContent = Justify.SpaceBetween;
        container.style.width = Length.Percent(100);
        container.style.marginBottom = 20;

        botScore = new ScoreAtom(container, "Bot Score");
        botScore.SetTitleColor(new Color(1f, 0.42f, 0.42f)); // #ff6b6b

        playerScore = new ScoreAtom(container, "Player Score");
        playerScore.SetTitleColor(new Color(0.31f, 0.8f, 0.77f)); // #4ecdc4

        parent.Add(container);
    }

    public void UpdateScores(int playerScoreValue, int botScoreValue)
    {
        playerScore.UpdateScore(playerScoreValue);
        botScore.UpdateScore(botScoreValue);
    }
}