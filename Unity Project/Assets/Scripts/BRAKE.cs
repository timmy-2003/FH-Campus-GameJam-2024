using UnityEngine;

public class BRAKE : MonoBehaviour
{
    public AudioClip audioClip; // Audio clip to play
    private AudioSource audioSource; // Reference to the AudioSource component

    void Start()
    {
        // Get the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();

        // Make sure an AudioClip is assigned
        if (audioClip == null)
        {
            Debug.LogError("No audio clip assigned!");
        }
    }

    void Update()
    {
        // Check if the 'S' key is pressed
        if (Input.GetKey(KeyCode.S))
        {
            Debug.LogError("SS");
            // Check if an audio clip and audio source are assigned
            if (audioClip != null && audioSource != null)
            {
                // Play the audio clip continuously
                if (!audioSource.isPlaying) // Ensure the audio is not already playing
                {
                    audioSource.clip = audioClip;
                    audioSource.Play();
                }
            }
            else
            {
                Debug.LogError("Audio clip or AudioSource component not assigned!");
            }
        }
        else
        {
            // Stop the audio if the 'W' key is released
            audioSource.Stop();
        }
    }
}
