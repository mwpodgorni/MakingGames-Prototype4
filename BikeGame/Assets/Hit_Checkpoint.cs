using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HitCheckpoint : MonoBehaviour
{
    private int Checkpoints = 0;
    

    public TextMeshProUGUI Counter;

    private void Start()
    {
        // Find the Text UI element by its name in the scene
        Counter = GameObject.Find("CheckpointCounter").GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "NeonBlueCircle")
        {
            print("Trigger Checkpoint");
            Destroy(other.gameObject);
            Checkpoints++;

            Counter.text = Checkpoints.ToString() + "/5";
        }

        if (Checkpoints == 5)
        {
            SceneManager.LoadScene("GameOverScreen");
        }
    }
}