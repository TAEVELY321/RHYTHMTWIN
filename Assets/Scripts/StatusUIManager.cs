using UnityEngine;
using UnityEngine.UI;

public class StatusUIManager : MonoBehaviour
{
    private Text hpText;
    private Text spText;
    public Font font;
    void Start()
    {
        // Canvas 생성
        GameObject canvasObj = new GameObject("StatusCanvas");
        canvasObj.layer = LayerMask.NameToLayer("UI");

        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        scaler.matchWidthOrHeight = 1f;

        canvasObj.AddComponent<GraphicRaycaster>();

 

        hpText = CreateUIText(canvas.transform, new Vector2(-800, -50), font, 32, Color.red);
        spText = CreateUIText(canvas.transform, new Vector2(-800, -120), font, 28, Color.yellow);
    }

    Text CreateUIText(Transform parent, Vector2 anchoredPos, Font font, int fontSize, Color color)
    {
        GameObject go = new GameObject("UIText");
        go.transform.SetParent(parent);

        Text text = go.AddComponent<Text>();
        text.font = font;
        text.fontSize = fontSize;
        text.alignment = TextAnchor.MiddleLeft;
        text.color = color;

        RectTransform rt = text.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(600, 80);
        rt.anchoredPosition = anchoredPos;
        rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 1f);
        rt.pivot = new Vector2(0f, 1f); // 왼쪽 위 기준

        return text;
    }

    void Update()
    {
        hpText.text = $"HP: {GameManager.hp} / {GameManager.maxHp}";

        // 스킬포인트를 ●●○ 형태로 표시
        int sp = GameManager.skillPoint;
        string dots = new string('●', sp) + new string('○', Mathf.Max(0, 3 - sp));
        spText.text = $"SP: {dots}";
    }
}
