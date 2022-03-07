using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
[RequireComponent(typeof(RectMask2D))]
public class ScrollController : Singleton<ScrollController>
{
    [SerializeField] private GameObject upYLimitation, downYLimitation;
    [SerializeField] private ElementScroll upperMarkElement;

    private bool isInit;

    private ScrollRect scroll;
    private RectMask2D maskContent;

    private ElementScroll markElement;
    private Transform elementTransform;
    private float maxY, minY;

    private bool upperMarkIsShow;
    private PositionMarkElement positionMark;

    private const float minValueMask = 50, maxValueMask = 100;

    private void Init()
    {
        scroll = GetComponent<ScrollRect>();
        maskContent = GetComponent<RectMask2D>();

        scroll.onValueChanged.AddListener(ChangeValueScroll);

        maxY = upYLimitation.transform.position.y;
        minY = downYLimitation.transform.position.y;
    }

    public void SetMarkElementData(ElementScroll element)
    {
        if (!isInit)
        {
            Init();
        }

        markElement = element;
        elementTransform = markElement.transform;

        upperMarkElement.SetElementScrollData(markElement);
        Invoke(nameof(SetStartPositionMarkElement), 0.05f); // Задержка, что бы элементы заняли свои позиции в content
    }

    private void SetStartPositionMarkElement()
    {
        float posYMark = elementTransform.position.y;

        if (posYMark > maxY)
        {
            positionMark = PositionMarkElement.OverScreen;
            ShowUpperMarkElement(true);
        }
        else if (posYMark < minY)
        {
            positionMark = PositionMarkElement.UnderScreen;
            ShowUpperMarkElement(false);
        }
        else
        {
            positionMark = PositionMarkElement.OnScreen;
        }
    }

    private void ChangeValueScroll(Vector2 valueChange)
    {
        if (positionMark == PositionMarkElement.OnScreen)
        {
            if (elementTransform.position.y > maxY)
            {
                ShowUpperMarkElement(true);
                positionMark = PositionMarkElement.OverScreen;
            }
            else if (elementTransform.position.y < minY)
            {
                ShowUpperMarkElement(false);
                positionMark = PositionMarkElement.UnderScreen;
            }
        }
        else if (positionMark == PositionMarkElement.OverScreen)
        {
            if (elementTransform.position.y < maxY)
            {
                HideUpperMarkElement();
            }
        }
        else if (positionMark == PositionMarkElement.UnderScreen)
        {
            if (elementTransform.position.y > minY)
            {
                HideUpperMarkElement();
            }
        }
    }

    private void ShowUpperMarkElement(bool positionIsUp)
    {
        upperMarkIsShow = true;
        Vector3 newPos = upperMarkElement.transform.position;

        if (positionIsUp)
        {
            newPos.y = maxY;

            maskContent.padding = new Vector4(0, minValueMask, 0, maxValueMask);
        }
        else
        {
            newPos.y = minY;

            maskContent.padding = new Vector4(0, maxValueMask, 0, minValueMask);
        }

        upperMarkElement.transform.position = newPos;
        upperMarkElement.gameObject.SetActive(true);
    }

    private void HideUpperMarkElement()
    {
        upperMarkIsShow = false;
        upperMarkElement.gameObject.SetActive(false);

        maskContent.padding = new Vector4(0, minValueMask, 0, minValueMask);
        positionMark = PositionMarkElement.OnScreen;
    }
}

public enum PositionMarkElement
{
    OverScreen,
    OnScreen,
    UnderScreen
}
