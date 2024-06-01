using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        public bool isLooping;
    }

    public List<Sound> sounds;
    public List<Sound> musicTracks;

    private AudioSource soundSource;
    private AudioSource musicSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        soundSource = gameObject.AddComponent<AudioSource>();
        musicSource = gameObject.AddComponent<AudioSource>();

        musicSource.loop = true;
    }

    public void PlaySound(string name)
    {
        Sound sound = sounds.Find(s => s.name == name);
        if (sound != null)
        {
            soundSource.PlayOneShot(sound.clip);
        }
    }

    public void PlayMusic(string name)
    {
        Sound music = musicTracks.Find(m => m.name == name);
        if (music != null)
        {
            if (musicSource.clip != music.clip || !musicSource.isPlaying)
            {
                musicSource.clip = music.clip;
                musicSource.loop = music.isLooping;
                musicSource.Play();
            }
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PauseMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Pause();
        }
    }

    public void ResumeMusic()
    {
        if (!musicSource.isPlaying)
        {
            musicSource.UnPause();
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
}
