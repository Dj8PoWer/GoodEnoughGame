using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeManager : MonoBehaviour
{
    AudioSource[] sources;

    public float volume = 1;
    float _volume = 1;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        sources = FindObjectsOfType<AudioSource>();
        ChangeVolume();
    }

    // Update is called once per frame
    void Update()
    {
        if (volume != _volume)
        {
            ChangeVolume();
            _volume = volume;
        }
    }

    void ChangeVolume()
    {
        foreach (AudioSource source in sources)
        {
            AudioListener.volume = volume;
        }
    }

}
