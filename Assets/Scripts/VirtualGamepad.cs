using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class VirtualGamepad : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private bool staticGamepad;
    [SerializeField] private bool touchPad;
    [SerializeField] private RectTransform pointer;

    [HideInInspector] public Vector2 Value = Vector2.zero;

    private Vector2 prevPos;
    private Vector2 originPos;
    private Vector2 initialPos;
    private RectTransform rectTransform;
    private float width => rectTransform.rect.width;

    [SerializeField] private float threshold;

    public UnityEvent OnClick;
    public UnityEvent<Vector2> OnHover;
    public UnityEvent OnUp;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        initialPos = rectTransform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var val = (eventData.position - prevPos) / (width / 2);
        Value = Vector2.ClampMagnitude(val, 1.0f);

        SetPointerPosition(eventData.position);

        if (touchPad)
        {
            prevPos = eventData.position;
        }
        OnHover.Invoke(Value);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        originPos = eventData.position;
        prevPos = originPos;

        if (staticGamepad)
        {
            prevPos = gameObject.transform.position;

            var val = (eventData.position - prevPos) / (width / 2);
            Value = Vector2.ClampMagnitude(val, 1.0f);
        }
        else
        {
            rectTransform.position = eventData.position;
        }

        SetPointerPosition(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnUp.Invoke();
        Value = Vector2.zero;

        if ((originPos - eventData.position).sqrMagnitude < threshold)
        {
            OnClick.Invoke();
        }

        rectTransform.position = initialPos;

        SetPointerPosition(initialPos);
    }

    private void OnDisable()
    {
        Value = Vector2.zero;
    }

    private void SetPointerPosition(Vector2 position)
    {
        pointer.anchoredPosition = Vector2.ClampMagnitude((position - (Vector2)rectTransform.position), width / 2);
    }
}
