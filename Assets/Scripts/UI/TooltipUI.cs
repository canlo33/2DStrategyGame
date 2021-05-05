using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI Instance { get; private set; }
    [SerializeField] private RectTransform canvasRectTransform;
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private RectTransform backGround;
    [SerializeField] private RectTransform rectTransform;
    private ToolTipTimer toolTipTimer;
    private void Awake()
    {
        Instance = this;
        Hide();
    }
    private void Update()
    {
        FollowMousePosition();
        //Check if the tooltip has a timer, if so then hide it once the timer runs out.
        if (toolTipTimer != null)
        {
            toolTipTimer.timer -= Time.deltaTime;
            if (toolTipTimer.timer <= 0)
                Hide();
        }
    }

    private void FollowMousePosition()
    {
        //Check if the Tooltip is out of screen, if so then shift it back to the screen limits.
        Vector2 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;
        if (anchoredPosition.x + backGround.rect.width > canvasRectTransform.rect.width)
            anchoredPosition.x = canvasRectTransform.rect.width - backGround.rect.width;
        if (anchoredPosition.y + backGround.rect.height > canvasRectTransform.rect.height)
            anchoredPosition.y = canvasRectTransform.rect.height - backGround.rect.height;
        rectTransform.anchoredPosition = anchoredPosition;
    }
    private void SetText(string tooltipText)
    {       
        textMeshPro.SetText(tooltipText);
        textMeshPro.ForceMeshUpdate();
        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 textPadding = new Vector2(8, 8);
        backGround.sizeDelta = textSize + textPadding;
    }
    public void Display(string tooltipText, ToolTipTimer toolTipTimer = null)
    {
        gameObject.SetActive(true);
        SetText(tooltipText);
        this.toolTipTimer = toolTipTimer;
        FollowMousePosition();
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public class ToolTipTimer
    {
        public float timer;
    }
}
