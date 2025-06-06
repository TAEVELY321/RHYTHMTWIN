using UnityEngine;
using UnityEngine.UI;

public class JudgementUIManager : MonoBehaviour
{
    public GameObject player;
    public Font uiFont;

    private Canvas canvas;

    void Start()
    {
        // Canvas 생성
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
        // 텍스트 오브젝트 생성
        GameObject textObj = new GameObject("JudgeText");
        textObj.transform.SetParent(canvas.transform, false);

        Text uiText = textObj.AddComponent<Text>();
        uiText.font = uiFont;
        uiText.text = text;
        uiText.fontSize = 48;
        uiText.alignment = TextAnchor.MiddleCenter;
        uiText.color = Color.white;

        RectTransform rect = uiText.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(300, 100);

        // 플레이어 머리 위 위치를 화면 좌표로 변환
        Vector3 worldPos = player.transform.position + new Vector3(0, 2f, 0);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        rect.position = screenPos;

        Destroy(textObj, 1.0f);
    }
}
