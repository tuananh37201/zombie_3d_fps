using System.Collections;
using TMPro;
using UnityEngine;

public class WeaponAdvanced : MonoBehaviour
{
    public SwitchWeapon weapon;
    //public GameObject impactEffect;
    public float impactForce = 60f;

    //-- Rifle
    public Camera RifleCam;
    public TextMeshProUGUI ammoAmountRifle;
    public Animator rifleAnimation;
    public GameObject rifleFireSound;
    public ParticleSystem muzzleFlashRifle;
    public float damageRifle = 10f;
    public float rangeRifle = 30f;
    public float fireRateRifle = 8f;
    public int maxAmmoRifle = 200;
    public int magazineRifle = 25;
    public int currentAmmoRifle = 0;
    public float reloadTimeRifle = 1f;
    private bool isReloadingRifle = false;
    private float nextTimeToFireRifle = 0f;
    //-- Pistol
    public Camera PistolCam;
    public TextMeshProUGUI ammoAmountPistol;
    public Animator pistolAnimation;
    public GameObject pistolFireSound;
    public ParticleSystem muzzleFlashPistol;
    public float damagePistol = 30f;
    public float rangePistol = 20f;
    public float fireRatePistol = 3f;
    public int maxAmmoPistol = 50;
    public int magazinePistol = 7;
    public int currentAmmoPistol = 0;
    public float reloadTimePistol = 0.5f;
    private bool isReloadingPistol = false;
    private float nextTimeToFirePistol = 0f;

    /*
    //-- Snipe
    public Camera SnipeCam;
    public TextMeshProUGUI ammoAmountSnipe;
    public float damageSnipe = 100f;
    public float rangeSnipe = 100f;
    public float fireRateSnipe = 1f;
    public int maxAmmoSnipe = 20;
    public int magazineSnipe = 5;
    public int currentAmmoSnipe = 0;
    public float reloadTimeSnipe = 1.5f;
    private bool isReloadingSnipe = false;
    private float nextTimeToFireSnipe = 0f;
    */

    // Start is called before the first frame update
    void Start()
    {
        rifleFireSound.SetActive(false);
        if (currentAmmoRifle == 0)
        {
            currentAmmoRifle = magazineRifle;
        }
        if (currentAmmoPistol == 0)
        {
            currentAmmoPistol = magazinePistol;
        }
        /*
        if (currentAmmoSnipe == 0)
        {
            currentAmmoSnipe = magazineSnipe;
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (weapon.selectedWeapon == 0)
        {
            pistolFireSound.SetActive(false);
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
                if (Time.time >= nextTimeToFireRifle)
                {
                    rifleAnimation.SetInteger("Fire", 0);
                }
                if (Input.GetButton("Fire1") && Time.time >= nextTimeToFireRifle)
                {
                    //damageRifle = 10f;
                    rifleAnimation.SetInteger("Fire", 1);
                    nextTimeToFireRifle = Time.time + 1f / fireRateRifle;
                    ShootRifle();
                }
                if (Input.GetButtonUp("Fire1"))
                {
                    rifleFireSound.SetActive(false);
                }
            }
            else
            {
                rifleFireSound.SetActive(false);
                rifleAnimation.SetInteger("Fire", 0);
            }
        }
        
        if (weapon.selectedWeapon == 1)
        {
            rifleFireSound.SetActive(false);
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
                if (Time.time >= nextTimeToFirePistol)
                {
                    pistolAnimation.SetInteger("Fire", 0);
                }
                if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFirePistol)
                {
                    //damagePistol = 30f;
                    pistolAnimation.SetInteger("Fire", 1);
                    nextTimeToFirePistol = Time.time + 1f / fireRatePistol;
                    ShootPistol();
                }
                if (Input.GetButtonUp("Fire1"))
                {
                    pistolFireSound.SetActive(false);
                }
            }
            else
            {
                pistolFireSound.SetActive(false);
                pistolAnimation.SetInteger("Fire", 0);
            }
        }

        /*
        if (weapon.selectedWeapon == 2)
        {
            SetAmmoSnipe();
            if (isReloadingSnipe)
            {
                return;
            }

            if (Input.GetKeyDown("r"))
            {
                StartCoroutine(ReloadSnipe());
                return;
            }

            if (currentAmmoSnipe > 0)
            {
                {
                    if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFireSnipe)
                    {
                        //damageSnipe = 100f;
                        nextTimeToFireSnipe = Time.time + 1f / fireRateSnipe;
                        ShootSnipe();
                    }
                }
            }
        }
        */
    }

    //========== HIỂN THỊ SỐ LƯỢNG ĐẠN RA UI ==========
    void SetAmmoRifle()
    {
        ammoAmountRifle.text = currentAmmoRifle.ToString() + "/" + maxAmmoRifle;
    }

    void SetAmmoPistol()
    {
        ammoAmountPistol.text = currentAmmoPistol.ToString() + "/" + maxAmmoPistol;
    }

    /*
    void SetAmmoSnipe()
    {
        ammoAmountSnipe.text = currentAmmoSnipe.ToString() + "/" + maxAmmoSnipe;
    }
    */

    //========== RELOAD ==========
    IEnumerator ReloadRifle()
    {
        rifleFireSound.SetActive(false);
        isReloadingRifle = true;
        Debug.Log("Reloading Rifle");
        rifleAnimation.SetInteger("Reload", 1);
        yield return new WaitForSeconds(reloadTimeRifle);
        rifleAnimation.SetInteger("Reload", 0);
        maxAmmoRifle -= (magazineRifle - currentAmmoRifle);
        currentAmmoRifle = magazineRifle;
        isReloadingRifle = false;
    }

    IEnumerator ReloadPistol()
    {
        pistolFireSound.SetActive(false);
        isReloadingPistol = true;
        Debug.Log("Reloading Pistol");
        pistolAnimation.SetInteger("Reload", 1);
        yield return new WaitForSeconds(reloadTimePistol);
        pistolAnimation.SetInteger("Reload", 0);
        maxAmmoPistol -= (magazinePistol - currentAmmoPistol);
        currentAmmoPistol = magazinePistol;
        isReloadingPistol = false;
    }

    /*
    IEnumerator ReloadSnipe()
    {
        isReloadingSnipe = true;
        Debug.Log("Reloading Snipe");

        yield return new WaitForSeconds(reloadTimeSnipe);

        maxAmmoSnipe -= (magazineSnipe - currentAmmoSnipe);
        currentAmmoSnipe = magazineSnipe;
        isReloadingSnipe = false;
    }
    */

    //========== SHOOT ==========
    void ShootRifle()
    {
        muzzleFlashRifle.Play();
        rifleFireSound.SetActive(true);
        currentAmmoRifle--;

        RaycastHit hit;
        if (Physics.Raycast(RifleCam.transform.position, RifleCam.transform.forward, out hit, rangeRifle))
        {
            Debug.Log(hit.transform.name);

            Target1 target = hit.transform.GetComponent<Target1>();

            if (target != null)
            {
                target.TakeDamage(damageRifle);
                
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            //GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            //Destroy(impactGO, 0.5f);
        }
    }

    void ShootPistol()
    {
        muzzleFlashPistol.Play();
        pistolFireSound.SetActive(true);
        currentAmmoPistol--;

        RaycastHit hit;
        if (Physics.Raycast(PistolCam.transform.position, PistolCam.transform.forward, out hit, rangePistol))
        {
            Debug.Log(hit.transform.name);

            Target1 target = hit.transform.GetComponent<Target1>();

            if (target != null)
            {
                target.TakeDamage(damagePistol);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            //GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            //Destroy(impactGO, 0.5f);
        }
    }

    /*
    void ShootSnipe()
    {
        muzzleFlash.Play();

        currentAmmoSnipe--;

        RaycastHit hit;
        if (Physics.Raycast(SnipeCam.transform.position, SnipeCam.transform.forward, out hit, rangeSnipe))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
            {
                target.TakeDamage(damageSnipe);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 0.5f);
        }
    }
    */
}
