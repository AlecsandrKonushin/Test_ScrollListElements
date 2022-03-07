using UnityEngine;
using UnityEngine.UI;

public class ElementScroll : MonoBehaviour
{
    [SerializeField] private Image iconImage, backgroundImage;
    [SerializeField] private Text nameText, numberText;

    [Space]
    [SerializeField] private Sprite greenBackground;

    public Sprite GetSpriteIcon { get => iconImage.sprite; }
    public string GetNameText { get => nameText.text; }
    public string GetNumberText { get => numberText.text; }

    public void SetElementScrollData(Sprite icon, string name, string number)
    {
        iconImage.sprite = icon;
        nameText.text = name;
        numberText.text = number;
    }

    public void SetElementScrollData(ElementScroll element)
    {
        iconImage.sprite = element.GetSpriteIcon;
        nameText.text = element.GetNameText;
        numberText.text = element.GetNumberText;
    }

    public void ChangeBackgroundOnGreen()
    {
        backgroundImage.sprite = greenBackground;
    }
}
