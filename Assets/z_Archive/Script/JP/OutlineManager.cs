using UnityEngine;
using UnityEngine.EventSystems;


[RequireComponent(typeof(Outline))]
public class OutlineManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Outline outline;

    public Color normal = Color.yellow;
    public Color selected = Color.green;

    public float selectedOutlineWidth = 5;
    private float normalOutlineWidth;

    private bool onHover = true;

    private void Awake()
    {
        outline = GetComponent<Outline>();
        outline.OutlineColor = normal;
        normalOutlineWidth = outline.OutlineWidth;
    }

    private void OnSelected()
    {
        outline.OutlineColor = selected;
        outline.OutlineWidth = selectedOutlineWidth;
    }

    private void OnDeselected()
    {
        outline.OutlineColor = normal;
        outline.OutlineWidth = normalOutlineWidth;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (onHover)
        {
            OnSelected();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (onHover)
        {
            OnDeselected();
        }
    }
}
