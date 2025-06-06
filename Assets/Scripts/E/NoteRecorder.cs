using UnityEngine;
using System.Collections.Generic;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

public class NoteRecorder : MonoBehaviour
{
    public AudioSource musicSource;
    private double startDSPTime;

    public List<NoteData> recordedNotes = new List<NoteData>();

    void Start()
    {
        startDSPTime = AudioSettings.dspTime;
        musicSource.Play();
    }

    void Update()
    {
        double currentTime = AudioSettings.dspTime - startDSPTime;

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.F))
        {
            recordedNotes.Add(new NoteData { time = currentTime, lane = Lane.Top });
            UnityEngine.Debug.Log($"[RECORD] {currentTime:F3}s - Top");
        }
        else if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.K))
        {
            recordedNotes.Add(new NoteData { time = currentTime, lane = Lane.Bottom });
            UnityEngine.Debug.Log($"[RECORD] {currentTime:F3}s - Bottom");
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SaveNotesToFile("note_pattern.json");
        }
    }

    void SaveNotesToFile(string filename)
    {
        string json = JsonUtility.ToJson(new NotePatternWrapper { notes = recordedNotes }, true);
        string path = System.IO.Path.Combine(UnityEngine.Application.dataPath, filename);
        System.IO.File.WriteAllText(path, json);
        UnityEngine.Debug.Log($"[SAVE] Notes saved to {path}");
    }
}
