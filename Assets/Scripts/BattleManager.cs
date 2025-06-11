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

        // ���� 1: ī�޶� ȸ�� ���� ���� ����
        if (!isCameraFlipped && timeSinceStart >= flipTriggerTime)
        {
            Camera.main.GetComponent<CameraEffectController>().RotateCamera();
            isCameraFlipped = true;
        }

        // ���� 2: ȸ�� ���̰� �޺� ��ǥ �޼� �� �� ����
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
        Debug.Log($"[BattleManager] ȭ����ȯ ����: {triggerTimes[index]}�� ��, �޺� {currentComboTarget} �޼� �� ����");
    }

    IEnumerator WaitBeforeNextFlip()
    {
        waitingNextEvent = true;
        Debug.Log("[BattleManager] ȭ�� ������. ���� ���±��� 20�� ���...");
        yield return new WaitForSeconds(20f);
        waitingNextEvent = false;
        ScheduleNextFlip();
    }
}
