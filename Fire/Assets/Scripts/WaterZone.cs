using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterZone : MonoBehaviour
{


    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Rag")
        {
            RagScripts rag = collider.gameObject.GetComponent<RagScripts>();
            if (!rag.ready)
            {
                rag.ChangeMokrost(1);
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Rag")
        {
            RagScripts rag = collider.gameObject.GetComponent<RagScripts>();
            if (rag.Mokrost > 20)
            {
                rag.ready = true;
            }

        }
    }

}
