using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class NoteScheduler : MonoBehaviour
{
    public AudioSource musicSource;
    public string patternFileName = "note_pattern.json";

    private List<NoteData> notePattern = new List<NoteData>();
    private int nextNoteIndex = 0;

    private MapCreator mapCreator;

    void Start()
    {
        mapCreator = GameObject.Find("GameRoot").GetComponent<MapCreator>();
        LoadNotesFromFile(patternFileName);

        GameManager.StartSong(musicSource); // 음악 시작 및 시간 기준 설정
    }

    void Update()
    {
        if (nextNoteIndex >= notePattern.Count) return;

        double currentTime = AudioSettings.dspTime - GameManager.dspSongStartTime;

        while (nextNoteIndex < notePattern.Count && notePattern[nextNoteIndex].time <= currentTime)
        {
            SpawnNote(notePattern[nextNoteIndex]);
            nextNoteIndex++;
        }
    }

    void SpawnNote(NoteData data)
    {
        Vector3 basePosition = mapCreator.GetPlayerPosition(); // 플레이어 위치 기준
        basePosition.x += MapCreator.BLOCK_WIDTH * ((float)MapCreator.BLOCK_NUM_IN_SCREEN / 2.0f);

        float[] laneHeights = { 0.0f, 1.0f };
        basePosition.y = laneHeights[(int)data.lane];

        mapCreator.CreateNoteAt(basePosition, data.lane);
    }

    void LoadNotesFromFile(string filename)
    {
        string path = Path.Combine(Application.dataPath, filename);
        if (!File.Exists(path))
        {
            Debug.LogError($"Note pattern file not found: {path}");
            return;
        }

        string json = File.ReadAllText(path);
        NotePatternWrapper wrapper = JsonUtility.FromJson<NotePatternWrapper>(json);
        notePattern = wrapper.notes;
    }
}
