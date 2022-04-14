using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateManager : MonoBehaviour
{
    [SerializeField] List<PressurePlate> plates = new List<PressurePlate>();
    int boolToWin;
    [SerializeField] GameObject removeObject;

    public void Check()
    {
        foreach (PressurePlate boolCheck in plates)
        {
            if (boolCheck.isActive == true)
            {
                boolToWin++;
            }
            else
            {
                boolToWin--;
            }
        }
        if (boolToWin == plates.Count)
        {
            removeObject.SetActive(false);
        }
    }
}
