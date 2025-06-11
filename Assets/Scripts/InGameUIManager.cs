using UnityEngine;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    private Text scoreText;
    private Text comboText;

    public Font font;
    void Start()
    {
        // Canvas ����
        GameObject canvasObj = new GameObject("InGameCanvas");
        canvasObj.layer = LayerMask.NameToLayer("UI");

        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);  // FHD ����
        scaler.matchWidthOrHeight = 1.0f;

        canvasObj.AddComponent<GraphicRaycaster>();

 

        // ���� �ؽ�Ʈ
        scoreText = CreateUIText(canvas.transform, new Vector2(0, 100), font, 36, Color.white);
        // �޺� �ؽ�Ʈ
        comboText = CreateUIText(canvas.transform, new Vector2(0, 50), font, 28, Color.cyan);
    }

    Text CreateUIText(Transform parent, Vector2 anchoredPos, Font font, int fontSize, Color color)
    {
        GameObject go = new GameObject("UIText");
        go.transform.SetParent(parent);

        Text text = go.AddComponent<Text>();
        text.font = font;
        text.fontSize = fontSize;
        text.alignment = TextAnchor.MiddleCenter;
        text.color = color;

        RectTransform rt = text.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(800, 80);
        rt.anchoredPosition = anchoredPos;
        rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.7f); // ȭ�� ��� �߾� ����
        rt.pivot = new Vector2(0.5f, 0.5f); // ���� ����

        return text;
    }

    void Update()
    {
        scoreText.text = $"SCORE: {GameManager.score}";
        comboText.text = $"{GameManager.combo} COMBO";
    }
}