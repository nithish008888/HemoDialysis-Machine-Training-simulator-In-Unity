using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AvatarAnimationController : MonoBehaviour
{
    public Animator avatarAnimator; // The Animator for the avatar
    public AudioSource voiceOverAudio; // AudioSource to play the voice-over dialogues
    public AudioClip helloClip; // The 33-second welcome audio clip
    // UI Text to show the dialogue (optional)
void Start()
{
    StartInteraction(); // Start the sequence automatically
}

    // Start the interaction with animations and dialogue
    public void StartInteraction()
    {
        StartCoroutine(PlayAnimationAndVoiceOver());
    }

    // Play the formal bow animation followed by the talking animation
    private IEnumerator PlayAnimationAndVoiceOver()
    {
        // 1. Play the Formal Bow animation at the start
        avatarAnimator.SetTrigger("Bow");
        
        voiceOverAudio.clip = helloClip; // Assign the 33-second voice-over
        voiceOverAudio.Play();
        
        // Wait for the Formal Bow animation to finish (based on its length, assuming it's about 3-4 seconds)
        yield return new WaitForSeconds(4f); // Adjust if necessary based on the animation's duration

        // 2. After Formal Bow, play the Talking animation
        avatarAnimator.SetTrigger("Talking");
       // Adjust the dialogue accordingly
        yield return new WaitForSeconds(helloClip.length); // Wait for the 33-second audio to finish

        // 3. Continue any further animations or actions here
        // You can add another animation or transition here if required
    }
}
