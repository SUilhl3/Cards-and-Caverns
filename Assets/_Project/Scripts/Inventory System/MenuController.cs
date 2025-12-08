using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    public GameObject menuCanvas;

    void Start()
    {
        menuCanvas.SetActive(false);
        Time.timeScale = 1f; 
    }

    void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            bool newState = !menuCanvas.activeSelf;
            menuCanvas.SetActive(newState);

            Time.timeScale = newState ? 0f : 1f;
        }
    }
}
