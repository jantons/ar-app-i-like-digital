using UnityEngine;
using ARExample;

public class AudioController : MonoBehaviour
{
    AudioSource _myAudio;
    [SerializeField] AudioClip howl, breath_low, breath_high;
    AudioClip step;
    int myAudioId;
    // Start is called before the first frame update
    void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
    }

    public void PlayAnimAudio(int id)
    {
        if (myAudioId != id)
        {
            myAudioId = id;
            _myAudio.Stop();
            switch (id)
            {
                case 0:
                    {
                        _myAudio.clip = breath_high; // StandingIdle
                        break;
                    }
                case 1:
                    {
                        _myAudio.clip = breath_low; // Lying Idle
                        break;
                    }
                case 2:
                    {
                        _myAudio.clip = breath_high; // Sitting Idle
                        break;
                    }
                case 3:
                    {
                        _myAudio.clip = howl; // Sitting Idle Howl
                        break;
                    }
            }
            _myAudio.Play();
        }
    }
}
