using System.Collections;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private bool isCameraFlipped = false;
    private float[] triggerTimes = { 15f, 30f, 60f };
    private int[] comboThresholds = { 15, 20, 50 };

    private int currentComboTarget = 0;
    private float flipTriggerTime = 0f;
    private float timeSinceStart = 0f;

    private bool waitingNextEvent = false;

    void Start()
    {
        ScheduleNextFlip();
    }

    void Update()
    {
        timeSinceStart += Time.deltaTime;

        // 조건 1: 카메라 회전 예정 시점 도달
        if (!isCameraFlipped && timeSinceStart >= flipTriggerTime)
        {
            Camera.main.GetComponent<CameraEffectController>().RotateCamera();
            isCameraFlipped = true;
        }

        // 조건 2: 회전 중이고 콤보 목표 달성 시 → 복원
        if (isCameraFlipped && GameManager.combo >= currentComboTarget)
        {
            Camera.main.GetComponent<CameraEffectController>().ResetCamera();
            isCameraFlipped = false;
            StartCoroutine(WaitBeforeNextFlip());
        }
    }

    void ScheduleNextFlip()
    {
        int index = Random.Range(0, 3);
        flipTriggerTime = timeSinceStart + triggerTimes[index];
        currentComboTarget = comboThresholds[index];
        Debug.Log($"[BattleManager] 화면전환 예정: {triggerTimes[index]}초 후, 콤보 {currentComboTarget} 달성 시 복귀");
    }

    IEnumerator WaitBeforeNextFlip()
    {
        waitingNextEvent = true;
        Debug.Log("[BattleManager] 화면 복원됨. 다음 상태까지 20초 대기...");
        yield return new WaitForSeconds(20f);
        waitingNextEvent = false;
        ScheduleNextFlip();
    }
}
