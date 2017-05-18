using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour {

    // Controls
    public enum Mode { Semi, Auto };
    public Mode mode;
    public float rpm;
    public float damage = 25;
    public LayerMask collisionMask;

	private float shotInterval;
    private float nextShotTime;

    // Components
    public PlayerCharacter player;
    public Transform bulletSpawn;
    public Transform extractor;
    public Shell shell;
    public Explosion explosion;
    private LineRenderer bulletTracer;

    void Start ()
	{
        shell.CreatePool();
        explosion.CreatePool();
    	shotInterval = 60 / rpm; // Set the time between shots
        if (GetComponent<LineRenderer>()) {
            bulletTracer = GetComponent<LineRenderer>();
        }
	}

	void Update ()
	{
		// Switch between fire modes
		if (Input.GetButtonDown("ChangeFireMode")) {
			if (mode == Mode.Auto) {
				mode = Mode.Semi;
			} else if (mode == Mode.Semi) {
				mode = Mode.Auto;
			}
		}
	}

    /// <summary>
    /// Fires a ray from the gun and checks for collisions. If it hits something it executes appropriate functions
    /// </summary>
    public void Shoot ()
    {
		if (CanShoot()) {
	        Ray ray = new Ray(bulletSpawn.position, bulletSpawn.forward);
			RaycastHit hit;

			float shotRange = 20;

			if (Physics.Raycast(ray, out hit, shotRange, collisionMask)) {
                shotRange = hit.distance; // If we hit something make sure the bullet doesnt go through

                Explosion newExplosion = explosion.Spawn(hit.point, Random.rotation);

                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemies")) {
                    newExplosion.SetColour(Color.red);
                } else {
                    newExplosion.SetColour(Color.grey);
                }

                // If a character entity is hit
                if (hit.collider.GetComponent<Character>()) {
					hit.collider.gameObject.GetComponent<Zombie> ().SetNewPosition(transform.parent.position); //
					hit.collider.GetComponent<Character>().Damage(damage, hit.point, transform.forward); // Damage enemy hit
                    player.AddScore(10);
                }
			}

            nextShotTime = Time.time + shotInterval;

            GetComponent<AudioSource>().Play();

            shell.Spawn(extractor.position, extractor.rotation);

            if (bulletTracer) {
                StartCoroutine(BulletTracer(ray.direction * shotRange));
            }
		}
    }

    public void ShootAuto ()
	{
		if (mode == Mode.Auto) {
			Shoot();
		}
    }

    /// <summary>
    /// This runs every time shoot is called. It sets true initially and then waits for the time between shots
    /// </summary>
    private bool CanShoot()
	{
        bool canShoot = true;

		if (Time.time < nextShotTime) {
			canShoot = false;
		}
		return canShoot;
    }

    IEnumerator BulletTracer (Vector3 bulletEnd)
    {
        bulletTracer.enabled = true;
        bulletTracer.SetPosition(0, bulletSpawn.position);
        bulletTracer.SetPosition(1, bulletSpawn.position + bulletEnd);
        yield return null;
        bulletTracer.enabled = false;
    }
}
