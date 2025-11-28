using UnityEngine;
using UnityEngine.SceneManagement;  // Import this to use SceneManager

public class SceneLoader : MonoBehaviour
{
    // Method to load the "Emergencies" scene
    public void LoadEmergenciesScene()
    {
        SceneManager.LoadScene("Emergencies");
    }

    // Method to load the "Training" scene
    public void LoadTrainingScene()
    {
        SceneManager.LoadScene("Training");
    }

    // Method to quit the application
    public void QuitApplication()
    {
        // Check if the application is running in the editor or build
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in the editor
        #else
            Application.Quit(); // Quit the application when it's built
        #endif
    }
}
