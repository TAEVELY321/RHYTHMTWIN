using UnityEngine;

public class BlockControl : MonoBehaviour
{
    public MapCreator map_creator = null;

    private Vector3 originalPos;
    private bool isAnimating = true;
    private float animTimer = 0f;
    private float animDuration = 0.3f;

    private bool isExiting = false;

    void Start()
    {
        map_creator = GameObject.Find("GameRoot").GetComponent<MapCreator>();

        originalPos = transform.position;
        transform.position = originalPos + new Vector3(0, -2f, 0); // 아래에서 시작
    }

    void Update()
    {
        if (isAnimating)
        {
            animTimer += Time.deltaTime;
            float t = Mathf.Clamp01(animTimer / animDuration);
            transform.position = Vector3.Lerp(originalPos + new Vector3(0, -2f, 0), originalPos, EaseOut(t));

            if (t >= 1f)
            {
                isAnimating = false;
            }
        }

        if (!isExiting && map_creator != null && map_creator.isDelete(this.gameObject))
        {
            isExiting = true;
            animTimer = 0f;
        }

        if (isExiting)
        {
            animTimer += Time.deltaTime;
            float t = Mathf.Clamp01(animTimer / animDuration);
            transform.position = Vector3.Lerp(originalPos, originalPos + new Vector3(0, -2f, 0), EaseIn(t));

            if (t >= 1f)
            {
                Destroy(gameObject);
            }
        }
    }

    float EaseOut(float t)
    {
        return 1 - Mathf.Pow(1 - t, 3); // 부드러운 시작
    }

    float EaseIn(float t)
    {
        return t * t * t; // 부드러운 끝
    }
}
