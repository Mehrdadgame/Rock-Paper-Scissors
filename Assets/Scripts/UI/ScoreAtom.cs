using UnityEngine;
using UnityEngine.UIElements;
using System;

// ====================
// ATOMS - Basic UI Components
// ====================

// Score display atom
public class ScoreAtom
{
    private Label scoreLabel;
    private Label titleLabel;

    public ScoreAtom(VisualElement parent, string title, string initialScore = "0")
    {
        var container = new VisualElement();
        container.AddToClassList("score-atom");
        container.style.alignItems = Align.Center;

        titleLabel = new Label(title);
        titleLabel.style.color = Color.white;
        titleLabel.style.fontSize = 16;
        titleLabel.style.marginBottom = 5;

        scoreLabel = new Label(initialScore);
        scoreLabel.AddToClassList("score-label");
        scoreLabel.style.fontSize = 24;
        scoreLabel.style.color = Color.white;

        container.Add(titleLabel);
        container.Add(scoreLabel);
        parent.Add(container);
    }

    public void UpdateScore(int score)
    {
        scoreLabel.text = score.ToString();
    }

    public void SetTitleColor(Color color)
    {
        titleLabel.style.color = color;
    }
}
