using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportarObjeto : MonoBehaviour
{
    public string tagDelObjetoDetonador; // Tag del objeto con el que al colisionar se debe teletransportar
    private Transform teletransportarDestino; // Transform del objeto de destino

    
    void Start()
    {
        // Buscar el objeto de destino por su nombre y obtener su Transform
        GameObject almacenPiezas = GameObject.Find("AlmacenPiezas");
        if (almacenPiezas != null)
        {
            teletransportarDestino = almacenPiezas.transform;
        }
        else
        {
            Debug.LogError("No se encontró el objeto de destino 'AlmacenPiezas'.");
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
    
        if (other.CompareTag(tagDelObjetoDetonador))
        {
    
            if (teletransportarDestino != null)
            {
    
                transform.position = teletransportarDestino.position;
            }
            else
            {
                Debug.LogWarning("El objeto destino para la teletransportación no se ha encontrado.");
            }
        }
    }
}
