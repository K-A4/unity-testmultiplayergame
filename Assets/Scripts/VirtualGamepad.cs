using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualGamepad : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public bool StaticGamepad;
    public bool TouchPad;

    [HideInInspector]
    public Vector2 Value = Vector2.zero;

    private Vector2 prevPos;
    private Vector2 originPos;

    [SerializeField]
    private float threshold;

    public UnityEvent OnClick;
    public UnityEvent<Vector2> OnHover;
    public UnityEvent OnUp;
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        var val = (eventData.position - prevPos) / (rectTransform.rect.width / 2);
        Value = Vector2.ClampMagnitude(val, 1.0f);

        if (TouchPad)
        {
            prevPos = eventData.position;
        }
        OnHover.Invoke(Value);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        originPos = eventData.position;
        prevPos = originPos;
        if (StaticGamepad)
        {
            prevPos = gameObject.transform.position;

            var val = (eventData.position - prevPos) / (rectTransform.rect.width / 2);
            Value = Vector2.ClampMagnitude(val, 1.0f);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnUp.Invoke();
        Value = Vector2.zero;
        if ((originPos - eventData.position).sqrMagnitude < threshold)
        {
            OnClick.Invoke();
        }
    }

    private void OnDisable()
    {
        Value = Vector2.zero;
    }
}
