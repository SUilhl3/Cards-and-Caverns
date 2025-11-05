using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private AudioSystem audioSystem;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            {
                Destroy(gameObject);
            }
            audioSystem = gameObject.GetComponent<AudioSystem>();
        }
    }

    public void playAudio()
    {
        audioSystem.ReturnAudio();
    }
}
