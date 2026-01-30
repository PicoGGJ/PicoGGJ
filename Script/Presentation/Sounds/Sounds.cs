using System;
using UnityEngine;


[Serializable]
public class Sounds : MonoBehaviour
{
    [SerializeField] private GameObject gameObject;
    [SerializeField] private AudioListener listener;
    [SerializeField] private AudioSource[] audioSource;
    private bool Mute;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        audioSource = GetComponents<AudioSource>();
        if(Mute)
        {
            audioSource[0].volume = 0;
            audioSource[0].Stop();
        }
        else
        {
            audioSource[0].volume = 100;
            audioSource[0].Play();
        }
    }

    public void PlaySfx(int indexMusic)
    {
        audioSource[indexMusic].loop = false;
        audioSource[indexMusic].Play();
    }
}
