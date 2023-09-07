using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    [SerializeField] GameObject screen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        screen.SetActive(true);
        Time.timeScale = 0;
    }

    private void Reset()
    {
        if(Time.timeScale == 0 && Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}