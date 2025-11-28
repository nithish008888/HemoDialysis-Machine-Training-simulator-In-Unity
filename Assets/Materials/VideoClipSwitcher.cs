using UnityEngine;
using UnityEngine.Video;

public class VideoClipSwitcher : MonoBehaviour
{
    // Reference to the prefab (which should have the VideoPlayer component)
    public GameObject videoPrefab;

    // Video clips to assign to the VideoPlayer
    public VideoClip T1testClip;
    public VideoClip startClip;
    public VideoClip primeClip;

    // VideoPlayer component attached to the prefab
    private VideoPlayer videoPlayer;

    void Start()
    {
        // Ensure we have a valid videoPrefab and VideoPlayer component
        if (videoPrefab != null)
        {
            videoPlayer = videoPrefab.GetComponent<VideoPlayer>();
            if (videoPlayer == null)
            {
                Debug.LogError("No VideoPlayer component found on the prefab!");
            }
        }
        else
        {
            Debug.LogError("VideoPrefab is not assigned!");
        }
    }

    // Function to assign the T1test video clip
    public void T1test()
    {
        if (videoPlayer != null && T1testClip != null)
        {
            videoPlayer.clip = T1testClip; // Assign the T1test clip
            videoPlayer.Play(); // Play the video
        }
        else
        {
            Debug.LogWarning("VideoPlayer or T1testClip is missing!");
        }
    }

    // Function to assign the start video clip
    public void StartClip()
    {
        if (videoPlayer != null && startClip != null)
        {
            videoPlayer.clip = startClip; // Assign the start clip
            videoPlayer.Play(); // Play the video
        }
        else
        {
            Debug.LogWarning("VideoPlayer or StartClip is missing!");
        }
    }

    // Function to assign the prime video clip
    public void PrimeClip()
    {
        if (videoPlayer != null && primeClip != null)
        {
            videoPlayer.clip = primeClip; // Assign the prime clip
            videoPlayer.Play(); // Play the video
        }
        else
        {
            Debug.LogWarning("VideoPlayer or PrimeClip is missing!");
        }
    }
}
