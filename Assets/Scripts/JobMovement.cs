using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDownObject : MonoBehaviour
{

    public float amplitude = 0.25f;
    public float frequency = 1f;
    public Vector3 m_Speed = new Vector3(0, 1/2, 0);
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * frequency) * amplitude;

        // On met à jour la position Y de l’objet
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
        transform.rotation = transform.rotation * Quaternion.Euler(m_Speed);


    }
}
        
