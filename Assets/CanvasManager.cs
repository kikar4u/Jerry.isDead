using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CanvasManager : MonoBehaviour
{
    [Header ("Sprite")]
    [SerializeField] Sprite muteSprite;
    [SerializeField] Sprite unMuteSprite;

    [Space]
    [Header ("Panel")]
    [SerializeField] GameObject settingPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopTheGame()
    {
        // the game stop running.
        Debug.Log("game stop");
        Time.timeScale = 0;
    }

    public void MuteTheGame()
    {
        // the game stop running.
        
        FindObjectOfType<AudioSource>().mute = !FindObjectOfType<AudioSource>().mute;
        if (FindObjectOfType<AudioSource>().mute)
        {
            Debug.Log("music mute");
            settingPanel.GetComponent<Image>().sprite = muteSprite;
        }
        else
        {
            Debug.Log("music unmute");
            settingPanel.GetComponent<Image>().sprite = unMuteSprite;
        }
    }

    public void ResumeTheGame()
    {
        // the game restart
        Debug.Log("game Run");
        Time.timeScale = 1;
    }

    public void QuitApplication()
    {
        Debug.Log("The game is stopped");
        Application.Quit();
    }
}
