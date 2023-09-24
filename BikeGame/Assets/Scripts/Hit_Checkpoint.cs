using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HitCheckpoint : MonoBehaviour
{
    private int Checkpoints = 0;
    private bool GamePassed = false;

    private TextMeshProUGUI counter;

    private void Start()
    {
        counter = GameObject.Find("CheckpointCounter").GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "NeonBlueCircle")
        {
            print("Trigger Checkpoint");
            Destroy(other.gameObject);
            Checkpoints++;

            counter.text = Checkpoints.ToString() + "/5";
        }

        if (Checkpoints == 5)
        {
            PlayerPrefs.SetString("endText", "You win!");
            SceneManager.LoadScene("GameOverScreen");
        }
    }
}