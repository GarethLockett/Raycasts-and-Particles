using System.Collections.Generic;
using UnityEngine;

/*
    Script: RaycastParticleSpawner
    Author: Gareth Lockett
    Version: 1.0
    Description:    Place this script on a game object with a camera component.
                    Creates raycasts from mouse clicks in the camera (Game View)
                    Instantiates a random particle systems at raycast collision points.

                    Make sure there is a self destruct component on the particle systems so they get destroyed after they have done their thing.
*/

[RequireComponent( typeof( Camera ) ) ]
public class RaycastParticleSpawner : MonoBehaviour
{
    // Properties
    public List<ParticleSystem> particleSystems;    // A list of particle systems to instantiate from.
    public float offsetFromSurface;                 // Distance to spawn the particle system away from the hit point.

    // Methods
    private void Start()
    {
        // Sanity check.
        if( this.particleSystems == null ){ Debug.LogWarning( "Did you forget to add particle systems to the RaycastParticleSpawner component?", this.gameObject ); return; }
        if( this.particleSystems.Count == 0 ) { Debug.LogWarning( "Did you forget to add particle systems to the RaycastParticleSpawner component?", this.gameObject ); return; }
    }

    private void Update()
    {
        // Check for left mouse button click.
        if( Input.GetMouseButtonDown( 0 ) == true )
        {
            // Create a 'ray' that travels away from the camera using the mouse position.
            Ray ray = this.GetComponent<Camera>().ScreenPointToRay( Input.mousePosition );

            // Create a 'RaycastHit' variable. This will contain data about what the ray hits (If anything) after the call to Physics.Raycast()
            RaycastHit hit;

            // Physics.Raycast() returns 'true' if the ray hit something.
            // NOTE: 'Mathf.Infinity' means the ray will be infinitely* long (eg this could be set to a shorter length for other raycast tests)
            if( Physics.Raycast( ray, out hit, Mathf.Infinity ) == true )
            {
                // If we are in here then it means the ray collided with *something*
                // The information about what it collided with, as well as where it collided, is stored in the RaycastHit 'hit' variable (See 'out' in the parameter)

                // Do a quick clean out of the particleSystems (In case some got removed for some reason?)
                this.particleSystems.RemoveAll( item => item == null );
                
                // Check if there are particle systems available.
                if( this.particleSystems.Count > 0 )
                {
                    // Choose one randomly and instantiate it.
                    GameObject pSystem = GameObject.Instantiate( this.particleSystems[ Random.Range( 0, this.particleSystems.Count ) ].gameObject );

                    // Add a ParticleSystemDestroyer component to the newly instantiated particle system game object, so that it will destroy itself after running.
                    pSystem.gameObject.AddComponent<ParticleSystemDestroyer>();

                    // Position the newly instantiated particle system at the hit point.
                    pSystem.transform.position = hit.point;

                    // Offset the newly instantiated particle system from the hit point.
                    pSystem.transform.position += hit.normal * this.offsetFromSurface;

                    // Orient the newly instantiated particle system so it points in the direction away from the hit point.
                    pSystem.transform.up = hit.normal;

                    // Output a cleaned particle system name to the console.
                    Debug.Log( "Instantiated particle system: " +pSystem.name.Replace( "(Clone)", "" ), pSystem );

                    // Show the ray in the Scene view (Handy for debugging)
                    Debug.DrawLine( ray.origin, hit.point, Color.yellow, 0.25f );
                    //Debug.DrawRay( ray.origin, ray.direction, Color.yellow, 0.25f );
                }
                else { Debug.LogWarning( "There are no particle systems to instantiate!" ); }
            }
        }
    }
}
