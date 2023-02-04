using UnityEngine.Audio;
using System;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using System.Collections.Generic;

public class AudioManager : Singleton<AudioManager> {

    public AudioMixer masterAudioMixer;
    public AudioMixerGroup musicMixerGroup;
    public AudioMixerGroup sfxMixerGroup;

    public Sound[] sounds;

    //[SerializeField] private bool isMusicMuted = false;
    //private float unmutedMusicVolume;
    private const float mutedVolume = -80f;


    private void Start() {
        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.outputAudioMixerGroup = s.mixerGroup;
        }

        PlayMusic("mainMusic");
    }

    public void PlayMusic(string sound) {
        Sound msc = Array.Find(sounds, item => item.soundType == Sound.SOUND_TYPE.MUSIC && item.name == sound);

        if (msc == null) {
            Debug.LogWarning("Sound: " + sound + " not found!");
            return;
        }

        Sound otherPlaying = Array.Find(sounds, item => item.soundType == Sound.SOUND_TYPE.MUSIC && item.source.isPlaying);

        Sequence seq = DOTween.Sequence();
        if (otherPlaying != null) {
            seq.Append(otherPlaying.source.DOFade(0f, 2f).OnComplete(() => otherPlaying.source.Stop()));
        }
        seq.Join(msc.source.DOFade(msc.volume, 2f).From(0).OnStart(() => msc.source.Play()));
    }

    public void PlaySFX(string sound) {
        //Sound s = Array.Find(sounds, item => item.name == sound && item.soundType == Sound.SOUND_TYPE.SFX);

        List<Sound> listOfSounds = sounds.Where(w => w.name == sound && w.soundType == Sound.SOUND_TYPE.SFX).ToList();



        Sound s = listOfSounds[UnityEngine.Random.Range(0, listOfSounds.Count)];
        print("Playing sound: " + s.name);

        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = s.volume;
        s.source.pitch = s.pitch;

        //s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        //s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

        s.source.Play();
    }

    public void PlaySFXFadeInAndOut(string sound) {
        List<Sound> listOfSounds = sounds.Where(w => w.name == sound && w.soundType == Sound.SOUND_TYPE.SFX).ToList();
        Sound s = listOfSounds[UnityEngine.Random.Range(0, listOfSounds.Count)];

        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = s.volume;
        s.source.pitch = s.pitch;

        s.source.Play();
        s.source.DOFade(s.volume, 0.25f).From(0);
        s.source.DOFade(0f, 0.25f).SetDelay(1f);
    }

    public void PlaySFXDetached(string sound) {
        List<Sound> listOfSounds = sounds.Where(w => w.name == sound && w.soundType == Sound.SOUND_TYPE.SFX).ToList();
        Sound s = listOfSounds[UnityEngine.Random.Range(0, listOfSounds.Count)];

        GameObject obj = new GameObject("audio_" + s.name);
        AudioSource src = obj.AddComponent<AudioSource>();
        src.volume = s.volume;
        src.pitch = s.pitch;
        src.clip = s.clip;
        src.outputAudioMixerGroup = s.mixerGroup;

        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        //AudioSource.PlayClipAtPoint(s.clip, Vector2.zero, s.volume);
        src.Play();
        Destroy(obj, src.clip.length);
    }

    public Sound GetSFX(string sound) {
        List<Sound> listOfSounds = sounds.Where(w => w.name == sound && w.soundType == Sound.SOUND_TYPE.SFX).ToList();

        Sound s = listOfSounds[UnityEngine.Random.Range(0, listOfSounds.Count)];

        if (s == null) {
            Debug.LogWarning("Sound GET CLIP: " + name + " not found!");
            return null;
        }

        return s;
    }

    public bool IsMusicMuted() {
        masterAudioMixer.GetFloat("musicVolume", out float musicVolume);
        return musicVolume == mutedVolume;
    }

    public bool IsSFXMuted() {
        masterAudioMixer.GetFloat("sfxVolume", out float sfxVolume);
        return sfxVolume == mutedVolume;
    }

    public void ToggleMusicMute() {
        if (IsMusicMuted()) masterAudioMixer.ClearFloat("musicVolume");
        else masterAudioMixer.SetFloat("musicVolume", mutedVolume);
    }

    public void ToggleSFXMute() {
        if (IsSFXMuted()) masterAudioMixer.ClearFloat("sfxVolume");
        else masterAudioMixer.SetFloat("sfxVolume", mutedVolume);
    }

    private void Update() {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.B)) {
            PlayMusic("mainMusic");
        } else if (Input.GetKeyDown(KeyCode.N)) {
            PlaySFXDetached("woosh");
        }
#endif
    }
}
