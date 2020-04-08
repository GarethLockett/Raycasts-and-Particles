using UnityEngine;

/*
    Script: ParticleSystemDestroyer
    Author: Gareth Lockett
    Version: 1.0
    Description:    Simple script for auto destroying a particle system game object after the particle system has finished.
*/

[ RequireComponent( typeof( ParticleSystem ) ) ]
public class ParticleSystemDestroyer : MonoBehaviour
{
    private ParticleSystem pSystem;      // Reference to the particle system on this game object.

    private void Start()
    {
        // Get a reference to the particle system on this game object (eg So don't have to keep using this.GetComponent<ParticleSystem>() every Update())
        this.pSystem = this.GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        // Check to see if the particle system is still running (eg 'alive')
        if( this.pSystem.IsAlive( true ) == false ){ Destroy( this.gameObject ); }
    }
}
