using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class HitCheckpoint : MonoBehaviour
{
    private int Checkpoints = 0;
    private bool GamePassed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "NeonBlueCircle")
        {
            print("Trigger Checkpoint");
            Destroy(other.gameObject);
            Checkpoints++;
        }

        if (Checkpoints == 2)
        {
            GamePassed = true;
            print("Game Passed");
        }
    }
}
