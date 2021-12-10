using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public PlayerControl thePlayer;

    private Vector3 lastPlayerPostition;
    private float distanceToMove;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerControl>();
        
    }

    // Update is called once per frame
    void Update()
    {
        distanceToMove = thePlayer.transform.position.x - lastPlayerPostition.x;
        transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);
        lastPlayerPostition = thePlayer.transform.position;
    }
}
