using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    Scene scene;
    private void Start()
    {
        scene = SceneManager.GetActiveScene();
  
    }
    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R pressed");
            SceneManager.LoadScene(scene.buildIndex);
        }
    }
}
