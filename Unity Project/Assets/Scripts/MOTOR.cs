using UnityEngine;

public class MOTOR : MonoBehaviour
{
    public AudioClip audioClipW; // Audio clip for 'W' key
    public AudioClip audioClipS; // Audio clip for 'S' key
    private AudioSource audioSource; // Reference to the AudioSource component

    void Start()
    {
        // Get the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();

        // Make sure audio clips are assigned
        if (audioClipW == null || audioClipS == null)
        {
            Debug.LogError("One or more audio clips not assigned!");
        }
    }

    void Update()
    {
        // Check if the 'W' key is pressed
        if (Input.GetKeyDown(KeyCode.W))
        {
            // Check if the 'W' audio clip and audio source are assigned
            if (audioClipW != null && audioSource != null)
            {
                // Play the 'W' audio clip
                audioSource.clip = audioClipW;
                audioSource.Play();
            }
            else
            {
                Debug.LogError("Audio clip or AudioSource component not assigned for 'W' key!");
            }
        }

        // Check if the 'S' key is pressed
        if (Input.GetKeyDown(KeyCode.S))
        {
            // Check if the 'S' audio clip and audio source are assigned
            if (audioClipS != null && audioSource != null)
            {
                // Play the 'S' audio clip
                audioSource.clip = audioClipS;
                audioSource.Play();
            }
            else
            {
                Debug.LogError("Audio clip or AudioSource component not assigned for 'S' key!");
            }
        }
    }
}
