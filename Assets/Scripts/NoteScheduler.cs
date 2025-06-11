using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NoteScheduler : MonoBehaviour
{
    public AudioSource musicSource;
    public string patternFileName = "note_pattern"; // 확장자 제거

    private List<NoteData> notePattern = new List<NoteData>();
    private int nextNoteIndex = 0;

    private MapCreator mapCreator;
    private bool resultLoaded = false;

    void Start()
    {
        mapCreator = GameObject.Find("GameRoot").GetComponent<MapCreator>();
        LoadNotesFromFile(patternFileName);

        GameManager.StartSong(musicSource);
    }

    void Update()
    {
        if (nextNoteIndex < notePattern.Count)
        {
            double currentTime = AudioSettings.dspTime - GameManager.dspSongStartTime;

            while (nextNoteIndex < notePattern.Count && notePattern[nextNoteIndex].time <= currentTime)
            {
                SpawnNote(notePattern[nextNoteIndex]);
                nextNoteIndex++;
            }
        }

        // 음악 종료 후 결과 화면 이동
        if (!musicSource.isPlaying &&
            AudioSettings.dspTime - GameManager.dspSongStartTime >= musicSource.clip.length &&
            !resultLoaded)
        {
            resultLoaded = true;
            SceneManager.LoadScene("ResultScene");
        }
    }

    void SpawnNote(NoteData data)
    {
        Vector3 basePosition = mapCreator.GetPlayerPosition();
        basePosition.x += MapCreator.BLOCK_WIDTH * ((float)MapCreator.BLOCK_NUM_IN_SCREEN / 2.0f);

        float[] laneHeights = { 0.5f, 2.0f };
        basePosition.y = laneHeights[(int)data.lane];

        mapCreator.CreateNoteAt(basePosition, data.lane);
    }

    void LoadNotesFromFile(string filenameWithoutExtension)
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(filenameWithoutExtension.Replace(".json", ""));

        if (jsonFile == null)
        {
            Debug.LogError($" JSON 파일을 Resources에서 불러오지 못했습니다: {filenameWithoutExtension}");
            return;
        }

        NotePatternWrapper wrapper = JsonUtility.FromJson<NotePatternWrapper>(jsonFile.text);
        notePattern = wrapper.notes;
    }

}
