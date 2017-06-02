using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour {

	public Transform player;
	public float yOffset;

	void Update () {
        float yPosition;
        if (player.position.y <= 0)
        {
			yPosition = transform.position.y;
        }
        else
        {
            yPosition = player.position.y + yOffset;
            
        }
		transform.position = new Vector3(player.position.x, yPosition, transform.position.z);
	}
}
