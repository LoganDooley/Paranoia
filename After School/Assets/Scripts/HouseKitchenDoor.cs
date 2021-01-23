using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseKitchenDoor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("touched");
        other.transform.position = new Vector3(-46.5f, 11.54f, -3f);
    }
}
