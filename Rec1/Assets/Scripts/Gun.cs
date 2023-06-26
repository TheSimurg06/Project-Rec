using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public float dmg = 10f;
    public float range = 100f;
    public Camera fpscam;
    public float fireRate = 15f;
    public ParticleSystem muzzleFlash;
    public GameObject impact;
    public float impactForce = 30f;
    private float nextTimeToFire = 0f;

    [Header("Reload")]
    public float reloadTime=1f;
    public int maxAmmo = 10;
    public int currentAmmo;
    private bool isReloading = false;
    public Animator animator;


    void Start() 
    {
        currentAmmo = maxAmmo;
    }
    //gameobject her aktif olduğunda tekrar çağrılır. 
    //Böylelikle şarjör değiştirirken silah değiştirince oluşan bug çözülür.
    private void OnEnable() 
    {
        isReloading = false;
        animator.SetBool("Reloading",false);
    }
    void Update()
    {
        if(isReloading)
        {
            //update devamındaki kodları çalıştırmaz.
            return;
        }
        if(currentAmmo <=0)
        {
            StartCoroutine(Reload());
            return;
        }

        if(Input.GetButton("Fire1") && Time.time >= nextTimeToFire){
            nextTimeToFire = Time.time + 1f/fireRate;
            Shoot();       
        }
    }
    IEnumerator Reload() 
    {
        animator.SetBool("Reloading",true);
        isReloading = true;
        Debug.Log("Reloading");
        //fonksiyonu dondurur.
        yield return  new WaitForSeconds(reloadTime - 0.25f);
        currentAmmo = maxAmmo;
        animator.SetBool("Reloading",false);
        yield return  new WaitForSeconds(0.25f);
        isReloading = false;

    }
    void Shoot()
    {
        // Raycast'in vurduğu nesne hakkında bilgi içerir. 
        RaycastHit hit;
        muzzleFlash.Play();

        currentAmmo--;

        if(Physics.Raycast(fpscam.transform.position,fpscam.transform.forward,out hit, range))
        {
            //Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if(target != null) 
            {
            target.TakeDamage(dmg);
            }
            
            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactEffect = Instantiate(impact,hit.point,Quaternion.LookRotation(hit.normal));
            Destroy(impactEffect,2f);
        }
    }
}
