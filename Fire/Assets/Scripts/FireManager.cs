using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour, IGameManager {
    [SerializeField] GameObject fire;
    [SerializeField] Transform[] points;
    public ManagerStatus status { get; private set; }
    
    public void StartUp()
    {
        Debug.Log("Fire manager starting...");
        for (int i = 0; i < points.Length; i++)
        {
            Instantiate(fire, points[i].position, transform.rotation);
        }
        status = ManagerStatus.Started;
    }

}
