using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Button reSetGame;
    // Start is called before the first frame update
    void Start()
    {
        reSetGame = GetComponentInChildren<Button>();
        reSetGame.onClick.AddListener(ResetScene);
    }

    private void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
