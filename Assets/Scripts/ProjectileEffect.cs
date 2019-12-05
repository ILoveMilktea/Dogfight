using UnityEngine;
using System.Collections;

public class ProjectileEffect : MonoBehaviour
{
    public GameObject impactParticle; // Effect spawned when projectile hits a collider
    public GameObject projectileParticle; // Effect attached to the gameobject as child
    public GameObject muzzleParticle; // Effect instantly spawned when gameobject is spawned
    [Header("Adjust if not using Sphere Collider")]
    public float colliderRadius = 1f;
    [Range(0f, 1f)] // This is an offset that moves the impact effect slightly away from the point of impact to reduce clipping of the impact effect
    public float collideOffset = 0.15f;


    void Start()
    {
        projectileParticle.SetActive(true);
        
        if (muzzleParticle)
        {
            muzzleParticle.SetActive(true);
            StartCoroutine(OffParticle(muzzleParticle, 1.5f));
        }
    }

    private IEnumerator OffParticle(GameObject particle, float time)
    {
        yield return new WaitForSeconds(time);
        particle.SetActive(false);
    }
    public void HitEffect(Vector3 hitPoint)
    {
        GameObject impactP = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, hitPoint)) as GameObject; // Spawns impact effect

        ParticleSystem[] trails = GetComponentsInChildren<ParticleSystem>(); // Gets a list of particle systems, as we need to detach the trails
                                                                             //Component at [0] is that of the parent i.e. this object (if there is any)
        for (int i = 1; i < trails.Length; i++) // Loop to cycle through found particle systems
        {
            ParticleSystem trail = trails[i];

            if (trail.gameObject.name.Contains("Trail"))
            {
                trail.transform.SetParent(null); // Detaches the trail from the projectile
                Destroy(trail.gameObject, 2f); // Removes the trail after seconds
            }
        }

        StartCoroutine(OffParticle(projectileParticle, 3f));
        StartCoroutine(OffParticle(impactP, 3.5f));
        //Destroy(gameObject); // Removes the projectile
    }
}