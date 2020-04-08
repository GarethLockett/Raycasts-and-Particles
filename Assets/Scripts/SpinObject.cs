using UnityEngine;

/*
    Script: SpinObject
    Author: Gareth Lockett
    Version: 1.0
    Description:    Super simple script for rotating a game object around its' XYZ axis (Euler)
*/ 

public class SpinObject : MonoBehaviour
{
	// Properties
	public bool xAxis = false;      // Toggle X axis rotation on/off.
	public float xSpeed = 1f;       // Speed which to rotate around the X axis.
	public bool yAxis = true;       // Toggle Y axis rotation on/off.
    public float ySpeed = 1f;       // Speed which to rotate around the Y axis.
    public bool zAxis = false;      // Toggle Z axis rotation on/off.
    public float zSpeed = 1f;       // Speed which to rotate around the Z axis.
    	
    // Methods
	void Update()
	{
        // Rotate around the axis if set true.
        if( this.xAxis == true ) { this.transform.Rotate( this.xSpeed * Time.deltaTime, 0f, 0f ); }
        if( this.yAxis == true ) { this.transform.Rotate( 0f, this.ySpeed * Time.deltaTime, 0f ); }
        if( this.zAxis == true ) { this.transform.Rotate( 0f, 0f, this.zSpeed * Time.deltaTime ); }
    }
}
