using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenButtons : MonoBehaviour
{
    
    public void BUTTON_PLAY_AGAIN()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.stopCompletly = false;
        GameManager.AbleToMove = false;
    }

    public void BUTTON_MAIN_MENU() 
    {

    }

}
