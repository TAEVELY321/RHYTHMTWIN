using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    public static double dspSongStartTime;

    public static int score = 0;
    public static int combo = 0;
    public static int maxCombo = 0;

    public static int hp = 20;
    public static int maxHp = 20;

    public static int skillPoint = 0;

    public enum JudgeResult { Perfect, Good, Bad, Miss }

    public static System.Action<JudgeResult> OnJudge;

    public static void StartSong(AudioSource musicSource)
    {
        dspSongStartTime = AudioSettings.dspTime;

        score = 0;
        combo = 0;
        maxCombo = 0;
        hp = maxHp;
        skillPoint = 0;

        musicSource.Play();
    }

    public static void ReportJudge(JudgeResult result)
    {
        if (result == JudgeResult.Miss)
        {
            combo = 0;
            hp -= 1;
            CheckHP();
        }
        else
        {
            score += 10;
            combo += 1;

            if (combo > maxCombo)
                maxCombo = combo;

            if (combo % 10 == 0 && skillPoint < 3)
                skillPoint += 1;
        }

        OnJudge?.Invoke(result);
    }

    public static void CheckHP()
    {
        if (hp <= 0)
        {
            Debug.Log("[GAME OVER] HP 0 → F 처리");
            SceneManager.LoadScene("ResultScene");
        }
    }

    public static void TryUseSkill()
    {
        if (skillPoint >= 3)
        {
            int recover = maxHp / 2;
            hp = Mathf.Min(hp + recover, maxHp);
            skillPoint -= 3;

            Debug.Log($"[SKILL] 회복 사용! 체력 +{recover} → 현재 HP: {hp}");
        }
        else
        {
            Debug.Log("[SKILL] 포인트 부족");
        }
    }
}
