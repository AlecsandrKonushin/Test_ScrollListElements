using System.Collections.Generic;
using UnityEngine;

public class CreatorController : MonoBehaviour
{
    [Range(1, 20)]
    [SerializeField] private int countElements;
    [SerializeField] private int numberMarkElement;
    [SerializeField] private GameObject contentObject;

    [Space]
    [SerializeField] private ElementScroll elementPrefab;

    [Space]
    [SerializeField] private Sprite[] icons;

    private const string elementName = "Element";
    private const float offsetElement = 45f;
    private float heightContent = 0;

    private List<ElementScroll> elements;

    private void Start()
    {
        CreateRandomContent();
        SetMarkElement();
    }

    public void CreateRandomContent()
    {
        elements = new List<ElementScroll>();

        for (int i = 0; i < countElements; i++)
        {
            ElementScroll element = CreateElementScroll(i);
            elements.Add(element);

            heightContent += element.GetComponent<RectTransform>().rect.height + offsetElement;
        }

        heightContent += offsetElement;

        RectTransform rtContent = contentObject.GetComponent<RectTransform>();
        rtContent.sizeDelta = new Vector2(rtContent.rect.width, heightContent);
    }

    private ElementScroll CreateElementScroll(int number)
    {
        Sprite icon = icons[Random.Range(0, icons.Length - 1)];
        string stringName = $"{elementName} {number}";
        string stringNumber = Random.Range(10, 1000).ToString();

        ElementScroll element = Instantiate(elementPrefab, contentObject.transform, false);
        element.transform.SetParent(contentObject.transform);
        element.SetElementScrollData(icon, stringName, stringNumber);

        return element;
    }

    private void SetMarkElement()
    {
        if (numberMarkElement > elements.Count - 1)
        {
            numberMarkElement = elements.Count - 1;
        }

        ScrollController.Instance.SetMarkElementData(elements[numberMarkElement]);

        elements[numberMarkElement].ChangeBackgroundOnGreen();
    }
}
