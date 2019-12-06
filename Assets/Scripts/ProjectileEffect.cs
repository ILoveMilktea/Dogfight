using UnityEngine;
using System.Collections;

public class ProjectileEffect : MonoBehaviour
{
    public GameObject impactParticle; // Effect spawned when projectile hits a collider
    public GameObject projectileParticle; // Effect attached to the gameobject as child
    public GameObject muzzleParticle; // Effect instantly spawned when gameobject is spawned

    private void OnEnable()
    {
        projectileParticle.SetActive(true);

        if (muzzleParticle)
        {
            muzzleParticle.SetActive(true);
            StartCoroutine(OffParticle(muzzleParticle, 1.5f));
        }
    }
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
        //ParticleSystem[] trails = GetComponentsInChildren<ParticleSystem>(); // Gets a list of particle systems, as we need to detach the trails
        //                                                                     //Component at [0] is that of the parent i.e. this object (if there is any)
        //for (int i = 1; i < trails.Length; i++) // Loop to cycle through found particle systems
        //{
        //    ParticleSystem trail = trails[i];

        //    if (trail.gameObject.name.Contains("Trail"))
        //    {
        //        trail.transform.SetParent(null); // Detaches the trail from the projectile
        //        Destroy(trail.gameObject, 2f); // Removes the trail after seconds
        //    }
        //}

        impactParticle.transform.position = hitPoint;
        impactParticle.SetActive(true);
        projectileParticle.SetActive(false);

        //StartCoroutine(OffParticle(projectileParticle, 3f));
        StartCoroutine(OffParticle(impactParticle, 3.5f));
        //Destroy(gameObject); // Removes the projectile
    }
}