using UnityEngine;
using UnityEngine.SceneManagement;

public class UINavigator : MonoBehaviour
{
    public string startGameScene;

    public void OnPressStartGame()
    {
        SceneManager.LoadScene(startGameScene);
    }
}