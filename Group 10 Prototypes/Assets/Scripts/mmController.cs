using UnityEngine;
using UnityEngine.SceneManagement;
public class mmController : MonoBehaviour
{
    public GameObject controlScreen;
    bool controlScreenOpen = false;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            controlScreenOpen = false;
        }
        controlScreen.SetActive(controlScreenOpen);
    }
    public void play()
    {
        if (!controlScreenOpen)
        {
            SceneManager.LoadScene(1);
        }
    }
    public void controls()
    {
        controlScreenOpen = true;
    }
    public void quit()
    {
        Application.Quit();
    }
}