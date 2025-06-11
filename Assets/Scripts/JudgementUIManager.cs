using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class JudgementUIManager : MonoBehaviour
{
    public GameObject player;
    public Font uiFont;

    private Canvas canvas;

    void Start()
    {
        GameObject canvasObj = new GameObject("JudgementCanvas");
        canvasObj.layer = LayerMask.NameToLayer("UI");

        canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.pixelPerfect = true;

        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);

        canvasObj.AddComponent<GraphicRaycaster>();
    }

    public void ShowJudge(string text)
    {
        GameObject textObj = new GameObject("JudgeText");
        textObj.transform.SetParent(canvas.transform, false);

        Text uiText = textObj.AddComponent<Text>();
        uiText.font = uiFont;
        uiText.text = text;
        uiText.fontSize = 48;
        uiText.alignment = TextAnchor.MiddleCenter;
        uiText.color = GetColorByJudgement(text);

        RectTransform rect = uiText.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(300, 100);

        Vector3 worldPos = player.transform.position + new Vector3(0, 2f, 0);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        rect.position = screenPos;

        Destroy(textObj, 1.0f);
    }

    Color GetColorByJudgement(string text)
    {
        if (text.ToUpper().Contains("MISS"))
            return Color.red;

        if (text.ToUpper().Contains("PERFECT"))
        {
            List<Color> possibleColors = new List<Color>
            {
                Color.yellow,
                Color.green,
                Color.cyan,
                Color.magenta,
                new Color(1.0f, 0.5f, 0.0f), // orange
                new Color(0.6f, 0.4f, 1.0f)  // violet
            };

            return possibleColors[Random.Range(0, possibleColors.Count)];
        }

        return Color.white;
    }
}

