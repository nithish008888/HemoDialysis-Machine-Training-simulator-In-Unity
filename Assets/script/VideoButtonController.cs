using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoButtonController : MonoBehaviour
{
    // Expose these fields to the Inspector to assign the videos
    public VideoClip t1TestVideo; // Video for T1 Test
    public VideoClip startVideo;  // Video for Start
    public VideoClip primeVideo;  // Video for Prime

    // Reference to the VideoPlayer component (assigned from the Inspector)
    public VideoPlayer videoPlayer;

    // Reference to the RawImage that will display the video
    public RawImage rawImage;

    // Function to play the T1 Test video
    public void PlayT1TestVideo()
    {
        PlayVideo(t1TestVideo); // Play the T1 Test video
    }

    // Function to play the Start video
    public void PlayStartVideo()
    {
        PlayVideo(startVideo); // Play the Start video
    }

    // Function to play the Prime video
    public void PlayPrimeVideo()
    {
        PlayVideo(primeVideo); // Play the Prime video
    }

    // Helper function to assign and play a video on the VideoPlayer
    private void PlayVideo(VideoClip videoClip)
    {
        if (videoPlayer != null && rawImage != null)
        {
            // Set the video clip to the VideoPlayer
            videoPlayer.clip = videoClip;

            // Prepare and play the video
            videoPlayer.Prepare();
            videoPlayer.prepareCompleted += (source) =>
            {
                // Once the video is prepared, make sure the RawImage is visible and play the video
                rawImage.gameObject.SetActive(true);
                videoPlayer.Play();
            };
        }
        else
        {
            Debug.LogWarning("VideoPlayer or RawImage not assigned!");
        }
    }
}
