using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public float rotationDuration;
    public bool isGameActive;
    public bool isSlowedDown = true;
    public GameObject currentGameObjectDragged;
    [HideInInspector] public int levelChucks;

    private bool isGamePaused;

    public GameObject character;

    void Start()
    {
        SpaceWheel.Instance.rotationDuration = rotationDuration;
        //SpaceWheel.Instance.Init();

    }

    public void Init()
    {
        SpaceWheel.Instance.Init();

        isGameActive = true;
        levelChucks = SpaceWheel.Instance.levelToLoad.listSections.Count;
        UIManager.Instance.Init();
        character.GetComponent<PlayerMov>().Init();
    }

    public void GameOver()
    {
        isGameActive = false;
        Debug.Log("GameOver");
        SpaceWheel.Instance.breakRotation = true;

        //Faudra peut-être attendre que l'annim' de mort se termine.

        Invoke("RestartLevel",3f);
    }

    private void RestartLevel()
    {
        //Player Init()
        UIManager.Instance.Restart();
        character.transform.position = Vector3.zero;
        Init();
    }

    public void GameWin()
    {
        //Ouvrir l'écran de victoire

        PushNextLevel(); //Peut-être mettre cette fonction dans un bouton "nivo suivant"
    }

    private void PushNextLevel()
    {
        SpaceWheel wheel = SpaceWheel.Instance;


        if (wheel.indexCampagne < wheel.campagneToLoad.listLevels.Count - 1)
        {
            wheel.indexCampagne++;

            //Player Init()
            wheel.Init();
        }
        else
        {
            //finir campagne/Ecran de fin avec remerciements toussa   OU   Prochain campagne
            print("Jeu fini");
        }

    }

    public void PauseGame()
    {
        isGamePaused = !isGamePaused;
        UIManager.Instance.ShowHidePause();
        Time.timeScale = 0.0f;
    }   

    public void ResumeGame()
    {
        isGamePaused = !isGamePaused;
        UIManager.Instance.ShowHidePause(false);
        Time.timeScale = 1.0f;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isGamePaused)
            {
                ResumeGame();
                return;
            }
            else
            {
                PauseGame();  
                return;
            }
            
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

}
