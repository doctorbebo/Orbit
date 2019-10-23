using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPicker : MonoBehaviour
{

    Scene scene;

    private void Start()
    {
        scene = SceneManager.GetActiveScene();

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(scene.buildIndex);
            PlanetBehavior.farthestXPosition = 0;
        }

        if (Input.GetMouseButtonDown(0) && ShipController.isPlayerDead)
        {
            SceneManager.LoadScene(scene.buildIndex);
            PlanetBehavior.farthestXPosition = 0;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (MainMenuUI.showMainMenu == true)
            {
                Application.Quit();
            }
            else
            {
                MainMenuUI.showMainMenu = true;
                SceneManager.LoadScene(scene.buildIndex);
            }
        }



    }
    public static void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
