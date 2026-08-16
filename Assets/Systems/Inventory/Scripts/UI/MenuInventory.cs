using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuInventory : MonoBehaviour
{
    bool tabOpen = false;
    bool paused = false;
    public UnityEvent onPause,onUnpause, onCloseTab;

    public Button quitButton;

    void Start()
    {
        quitButton.onClick.AddListener(Quit);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!tabOpen){
            Time.timeScale = paused? 1 : 0;
            paused = !paused;
            if (paused)
            {
                onPause.Invoke();
            }
            else
            {
                onUnpause.Invoke();
            }
            }
            else
            {
                onCloseTab.Invoke();
            }
        }
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit game");
    }
}