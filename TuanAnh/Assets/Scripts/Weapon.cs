using System.Collections;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //
    public float damageRifle = 10f;
    public float damagePistol = 30f;

    public float rangeRifle = 30f;
    public float rangePistol = 10f;

    public float fireRateRifle = 8f;
    public float fireRatePistol = 1f;

    public float impactForce = 60f;

    //
    public TextMeshProUGUI ammoAmountRifle;
    public TextMeshProUGUI ammoAmountPistol;

    public int maxAmmoRifle = 25;
    public int maxAmmoPistol = 7;

    public int currentAmmoRifle = 0;
    public int currentAmmoPistol = 0;

    public float reloadTimeRifle = 1f;
    public float reloadTimePistol = 0.5f;

    private bool isReloadingRifle = false;
    private bool isReloadingPistol = false;

    private float nextTimeToFireRifle = 0f;
    private float nextTimeToFirePistol = 0f;

    public SwitchWeapon weapon;

    //
    public Camera fpsCam;

    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

  
    // Start is called before the first frame update
    void Start()
    {
        if (currentAmmoRifle == 0)
        {
            currentAmmoRifle = maxAmmoRifle;
        }

        if (currentAmmoPistol == 0)
        {
            currentAmmoPistol = maxAmmoPistol;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (weapon.selectedWeapon == 0)
        {
            SetAmmoRifle();
            if (isReloadingRifle)
            {
                return;
            }

            if (Input.GetKeyDown("r"))
            {
                StartCoroutine(ReloadRifle());
                return;
            }

            if (currentAmmoRifle > 0)
            {
                {
                    if (Input.GetButton("Fire1") && Time.time >= nextTimeToFireRifle)
                    {
                        damageRifle = 10f;
                        nextTimeToFireRifle = Time.time + 1f / fireRateRifle;
                        ShootRifle();
                    }
                }
            }
        }
        
        if (weapon.selectedWeapon == 1)
        {
            SetAmmoPistol();
            if (isReloadingPistol)
            {
                return;
            }

            if (Input.GetKeyDown("r"))
            {
                StartCoroutine(ReloadPistol());
                return;
            }

            if (currentAmmoPistol > 0)
            {
                {
                    if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFirePistol)
                    {
                        damagePistol = 30f;
                        nextTimeToFirePistol = Time.time + 1f / fireRatePistol;
                        ShootPistol();
                    }
                }
            }
        }
    }

    //

    void SetAmmoRifle()
    {
        ammoAmountRifle.text = currentAmmoRifle.ToString() + "/" + maxAmmoRifle;
    }

    void SetAmmoPistol()
    {
        ammoAmountPistol.text = currentAmmoPistol.ToString() + "/" + maxAmmoPistol;
    }


    //
    IEnumerator ReloadRifle()
    {
        isReloadingRifle = true;
        Debug.Log("Reloading Rifle");

        yield return new WaitForSeconds(reloadTimeRifle);

        currentAmmoRifle = maxAmmoRifle;
        isReloadingRifle = false;
    }

    IEnumerator ReloadPistol()
    {
        isReloadingPistol = true;
        Debug.Log("Reloading Pistol");

        yield return new WaitForSeconds(reloadTimePistol);

        currentAmmoPistol = maxAmmoPistol;
        isReloadingPistol = false;
    }


    //
    void ShootRifle()
    {
        muzzleFlash.Play();

        currentAmmoRifle--;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, rangeRifle))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
            {
                target.TakeDamage(damageRifle);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 0.5f);
        }
    }

    void ShootPistol()
    {
        muzzleFlash.Play();

        currentAmmoPistol--;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, rangePistol))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
            {
                target.TakeDamage(damagePistol);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 0.5f);
        }
    }
}
