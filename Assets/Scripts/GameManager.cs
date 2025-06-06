using UnityEngine;

public static class GameManager
{
    public static double dspSongStartTime;

    public enum JudgeResult { Perfect, Good, Miss, Bad }

    public static System.Action<JudgeResult> OnJudge;

    public static void StartSong(AudioSource musicSource)
    {
        dspSongStartTime = AudioSettings.dspTime;
        musicSource.Play();
    }

    public static void ReportJudge(JudgeResult result)
    {
        OnJudge?.Invoke(result);
    }
}
