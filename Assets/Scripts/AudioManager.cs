using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--Bgm--")]
    public AudioClip bgmClip;
    public float bgmVolume;
    AudioSource bgmPlayer;

    [Header("--Sfx--")]
    int sfxChannels = 10;
    public AudioClip[] sfxClips;
    public float sfxVolume;
    AudioSource[] sfxPlayers;
    public Sfx sfx;
    public enum Sfx
    {
       DIE1,DIE2,DIE3,DIE4,DIE5,HORSE_DIE,FIRE1,FIRE2,FIRE3,FIRE4,FIRE5,FIRE6,PARABOLA,SHOTGUN,SKILL3,SPEAR,SWING0,SWING1,SWING2,SWING3,SWING4,SWING5,
       BOW,BLOOD,
    }

    // Start is called before the first frame update
 
    private void Start()
    {
        InitBgm();
        InitSfx();
    }
    void InitBgm()
    {
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;
    }

    void InitSfx()
    {
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[sfxChannels];

        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].volume = sfxVolume;
            sfxPlayers[i].bypassListenerEffects = true;
        }
    }

    public void PlayerBgm(bool islive)
    {
        bgmPlayer.clip = bgmClip;
        if (islive)
            bgmPlayer.Play();
        else
            bgmPlayer.Stop();
    }

    public void PlayerSfx(Sfx sfx)
    {
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            if (sfxPlayers[i].isPlaying)
                continue;

            sfxPlayers[i].clip = sfxClips[(int)sfx];
            sfxPlayers[i].Play();
            break;
        }
    }
}