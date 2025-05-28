using UnityEngine;

public class NoteJudge : MonoBehaviour
{
    public enum LaneType { Top, Bottom }
    public LaneType laneType;

    private bool isPlayerIn = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerIn = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerIn = false;
        }
    }

    void Update()
    {
        if (!isPlayerIn) return;

        bool validInput = false;

        if (laneType == LaneType.Top)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.F))
                validInput = true;
        }
        else if (laneType == LaneType.Bottom)
        {
            if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.K))
                validInput = true;
        }

        if (validInput)
        {
            Debug.Log($"노트 처리 완료: {laneType}");
            Destroy(gameObject);
        }
    }
}