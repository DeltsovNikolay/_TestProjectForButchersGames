using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public void OnRestartButton(string nameScene)
    {
        LoadScene(nameScene);
    }

    public void OnNextLevelButton(string nameScene)
    {
        LoadScene(nameScene);
    }

    private void LoadScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
}
