using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Audio_MusicManager : MonoBehaviour
{
    //Play one audio clip with Wwise
    public List<name_MusicEvent> _nameEventsMusic = new List<name_MusicEvent>();
    public Dictionary<string, AK.Wwise.Event> _eventsMusic = new Dictionary<string, AK.Wwise.Event>();

    private string _currentSceneName, _previousSceneName;
    private List<name_MusicEvent> _currentEventsPlaying = new List<name_MusicEvent>();

    public void Awake()
    {
        //Initialize
        foreach (name_MusicEvent _name in _nameEventsMusic)
        {
            _eventsMusic.Add(_name._name, _name._event);
        }
    }

    private void Update()
    {
        //Manage
        ManageCurrentMainMusic(SceneManager.GetActiveScene().name);
    }

    public void PlayWwiseEventMusic(AK.Wwise.Event _event)
    {
        _event.Post(gameObject);
    }

    public void StopWwiseEventMusic(AK.Wwise.Event _event)
    {
        _event.Stop(gameObject);
    }

    public void ManageCurrentMainMusic(string _currentScene)
    {
        //Check if change the music
        if (_currentScene != _previousSceneName)
        {
            //Stop music if playing & reset
            if (_currentEventsPlaying.Count != 0)
            {
                foreach (name_MusicEvent _event in _currentEventsPlaying)
                {
                    StopWwiseEventMusic(_event._event);
                }
            }

            _currentEventsPlaying = new List<name_MusicEvent>();

            //Find correct music for this scene
            List<name_MusicEvent> musicToPlay = new List<name_MusicEvent>();

            foreach (name_MusicEvent _event in _nameEventsMusic)
            {
                foreach (string sceneName in _event._sceneMusic)
                {
                    if (sceneName == _currentScene)
                    {
                        musicToPlay.Add(_event);
                    }
                }
            }

            if (musicToPlay.Count == 0)
            {
                Debug.Log("No music for this scene");

                //Play correct music
                foreach (name_MusicEvent _event in _nameEventsMusic)
                {
                    //Check if music is excluded when needed
                    bool isExcluded = false;

                    foreach (string _excludeMusic in _event._excludeSceneMusic)
                    {
                        if (_excludeMusic == _currentScene)
                        {
                            isExcluded = true;
                            Debug.Log("Exclude default music");
                            break;
                        }
                    }

                    //Play default when needed
                    if (_event._defaultMusicIfNone && !isExcluded)
                    {
                        Debug.Log("Play this music instead");

                        //Play music
                        PlayWwiseEventMusic(_eventsMusic[_event._name]);

                        //Add it to current list of music playing played
                        _currentEventsPlaying.Add(_event);
                    }
                }
            }
            else
            {
                //Play correct music
                foreach (name_MusicEvent _event in musicToPlay)
                {
                    //Play music
                    PlayWwiseEventMusic(_eventsMusic[_event._name]);

                    //Add it to current list of music playing played
                    _currentEventsPlaying.Add(_event);

                }
            }

            //Reset
            _currentSceneName = _currentScene;
            _previousSceneName = _currentScene;
        }
    }
}

[System.Serializable]
public struct name_MusicEvent
{
    public string _name;
    public AK.Wwise.Event _event;
    public List<string> _sceneMusic;
    public List<string> _excludeSceneMusic;
    public bool _defaultMusicIfNone;
}
