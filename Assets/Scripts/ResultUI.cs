using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultUI : MonoBehaviour
{
    public Font font;
    void Start()
    {
        // Canvas 생성
        GameObject canvasObj = new GameObject("ResultCanvas");
        canvasObj.layer = LayerMask.NameToLayer("UI");

        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        canvasObj.AddComponent<GraphicRaycaster>();


        CreateText(canvas.transform, $"SCORE  {GameManager.score}", new Vector2(0, 100), font, 64, Color.white);
        CreateText(canvas.transform, $"MAX COMBO  {GameManager.maxCombo}", new Vector2(0, 0), font, 48, Color.cyan);

        string grade = GetGrade(GameManager.score);
        Color gradeColor = GetGradeColor(grade);
        CreateText(canvas.transform, grade, new Vector2(0, -150), font, 96, gradeColor);

        // 재시작 안내 텍스트 추가
        CreateText(canvas.transform, "Press Space To Restart", new Vector2(0, -280), font, 32, Color.white);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("TitleScene");
        }
    }

    void CreateText(Transform parent, string content, Vector2 anchoredPos, Font font, int fontSize, Color color)
    {
        GameObject go = new GameObject("Text_" + content);
        go.transform.SetParent(parent);

        Text text = go.AddComponent<Text>();
        text.text = content;
        text.font = font;
        text.fontSize = fontSize;
        text.alignment = TextAnchor.MiddleCenter;
        text.color = color;

        RectTransform rt = text.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(1000, 100);
        rt.anchoredPosition = anchoredPos;
        rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);
    }

    string GetGrade(int score)
    {
        if (score >= 10000) return "S";
        else if (score >= 7500) return "A";
        else if (score >= 3000) return "B";
        else return "F";
    }

    Color GetGradeColor(string grade)
    {
        switch (grade)
        {
            case "S": return new Color(1f, 0.84f, 0.1f); // Gold
            case "A": return Color.green;
            case "B": return Color.blue;
            case "F": return Color.red;
            default: return Color.white;
        }
    }
}
