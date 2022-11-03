using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Load : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level3");
        }
    }
}
