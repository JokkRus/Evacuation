using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    [SerializeField] float TimeToSpawn = 10f;
    public float smokeKef = 0.005f;
    private float time = 0;
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
    public int maxCount = 3;
    private float Live = 1f;

    void Start()
    {

    }


    void Update()
    {
        time += Time.deltaTime;
        if (time >= TimeToSpawn && maxCount > 0)
        {
            time = 0;
            float xDir = Random.Range(-1,1);
            if (xDir >= 0) xDir = 1;
            else xDir = -1;
            float zDir = Random.Range(-1, 1);
            if (zDir >= 0) zDir = 1;
            else zDir = -1;
            float x = Random.Range(minX, maxX) * xDir + transform.position.x;
            float z = Random.Range(minZ, maxZ) * zDir + transform.position.z;
            Instantiate(gameObject, new Vector3(x, transform.position.y, z), transform.rotation);
            maxCount--;
            Messenger<float>.Broadcast(GameEvent.FireUpdated, smokeKef);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EndZone")
        {
            Messenger<float>.Broadcast(GameEvent.FireUpdated, -smokeKef);
            Destroy(gameObject);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Managers.Player.ChangeHealth(-1);
        }
        else if (other.gameObject.tag == "Supply")
        {
            Live -= Time.deltaTime;
            if (Live <= 0)
            {
                Messenger<float>.Broadcast(GameEvent.FireUpdated, -0.01f);
                Destroy(gameObject);      
            }
        }
    }
}