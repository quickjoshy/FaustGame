using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void SurvivalButton() {

        SceneManager.LoadScene("SurvivalScene");
    
    }


    public void QuitButton()
    {
        Application.Quit();
    }
}
