using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateManager : MonoBehaviour
{
    [SerializeField] List<PressurePlate> plates = new List<PressurePlate>();

    [SerializeField] GameObject removeObject;
    [SerializeField] GameObject replaceObject;

    [SerializeField] bool replace;

    int boolToWin;

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
            if (!replace)
            {
                removeObject.SetActive(false);
            }
            else
            {
                removeObject.SetActive(false);
                replaceObject.SetActive(true);
            }
        }
    }
}
