using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public GameObject AudioBGM;
    //public GameObject AudioMouse;
    AudioSource audioSE;
    AudioSource audioBGM;
    //AudioSource audioMouse;

    // Start is called before the first frame update
    void Start()
    {
      Instance = this;
      audioSE = GetComponent<AudioSource>();
      audioBGM = AudioBGM.GetComponent<AudioSource>();
      //audioMouse = AudioMouse.GetComponent<AudioSource>();
    }

    //<summary>
    //効果音を出す
    //</summary>
    //<param>音源ファイル名</param>
    public void SoundSE(string SEName)
    {
      audioSE.PlayOneShot( Resources.Load("03_SE/" + SEName ,typeof(AudioClip) ) as AudioClip );
    }

    //<summary>
    //BGMを流す
    //</summary>
    public void SoungBGM()
    {
      audioBGM.Play();
    }

    /// <summary>
    /// BGMの音量変更
    /// </summary>
    /// <param name="volume"></param>
    public void ChangeVolumeBGM()
    {
        audioBGM.volume = SaveLoadSystem.Instance.gameData.VolumeBGM;
    }

    /// <summary>
    /// SEの音量変更
    /// </summary>
    /// <param name="volume"></param>
    public void ChangeVolumeSE()
    {
        audioSE.volume = SaveLoadSystem.Instance.gameData.VolumeSE;
    }

    //===アプリ固有=================================

    //<summary>
    //ネズミロボットの音を流す
    //</summary>
    //public void MousePlay()
    //{
    //    if(SaveLoadSystem.Instance.gameData.VolumeSE != 0)
    //        audioMouse.Play();
    //}

    //<summary>
    //ネズミロボットの音を流す
    //</summary>
    //public void MouseStop()
    //{
    //    if (SaveLoadSystem.Instance.gameData.VolumeSE != 0)
    //        audioMouse.Stop();
    //}
}
