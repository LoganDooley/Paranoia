using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMirrorMvt : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 adjust;
        adjust.x = player.transform.position.x - 0.3f;
        adjust.y = 21.975f + (21.975f - player.transform.position.y);
        adjust.z = 1f;
        transform.position = adjust;
    }
}
