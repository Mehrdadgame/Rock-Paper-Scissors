
using UnityEngine;
using UnityEngine.UIElements;

public class ChoiceDisplayAtom
{
    private VisualElement imageElement;
    private Label titleLabel;

    public ChoiceDisplayAtom(VisualElement parent, string title)
    {
        var container = new VisualElement();
        container.style.alignItems = Align.Center;

        titleLabel = new Label(title);
        titleLabel.style.color = Color.white;
        titleLabel.style.fontSize = 18;
        titleLabel.style.marginBottom = 10;
        titleLabel.style.unityFontStyleAndWeight = FontStyle.Bold;

        imageElement = new VisualElement();
        imageElement.AddToClassList("choice-image");
        imageElement.style.width = 120;
        imageElement.style.height = 120;
        imageElement.style.borderTopLeftRadius = 10;
        imageElement.style.borderTopRightRadius = 10;
        imageElement.style.borderBottomLeftRadius = 10;
        imageElement.style.borderBottomRightRadius = 10;
        imageElement.style.borderLeftWidth = 2;
        imageElement.style.borderRightWidth = 2;
        imageElement.style.borderTopWidth = 2;
        imageElement.style.borderBottomWidth = 2;
        imageElement.style.borderLeftColor = Color.white;
        imageElement.style.borderRightColor = Color.white;
        imageElement.style.borderTopColor = Color.white;
        imageElement.style.borderBottomColor = Color.white;

        container.Add(titleLabel);
        container.Add(imageElement);
        parent.Add(container);
    }

    public void UpdateChoice(Sprite sprite)
    {
        if (sprite != null)
        {
            imageElement.style.backgroundImage = new StyleBackground(sprite);
        }
        else
        {
            imageElement.style.backgroundImage = StyleKeyword.None;
        }
    }

    public void SetTitleColor(Color color)
    {
        titleLabel.style.color = color;
    }
}