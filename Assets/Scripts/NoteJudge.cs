using UnityEngine;

public class NoteJudge : MonoBehaviour
{
    public enum LaneType { Top, Bottom }
    public LaneType laneType;

    public double targetTime;

    private bool isPlayerIn = false;
    private bool judged = false;

    void Update()
    {
        if (judged || !isPlayerIn) return;

        if (InputMatched())
        {
            judged = true;

            Report("Perfect", GameManager.JudgeResult.Perfect);
            Destroy(gameObject);
        }
    }

    private bool InputMatched()
    {
        if (laneType == LaneType.Top)
            return Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.F);
        else
            return Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.K);
    }

    private void Report(string debugText, GameManager.JudgeResult result)
    {
        Debug.Log($"[{debugText}] - Lane: {laneType}");
        GameManager.ReportJudge(result);

        var ui = GameObject.FindObjectOfType<JudgementUIManager>();
        if (ui != null)
        {
            ui.ShowJudge(debugText);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (judged) return;

        if (other.CompareTag("Player"))
        {
            isPlayerIn = true;
        }
        else if (other.CompareTag("MissArea"))
        {
            judged = true;
            Report("Miss", GameManager.JudgeResult.Miss);
            Destroy(gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerIn = false;
        }
    }
}
