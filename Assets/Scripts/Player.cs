using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 0.6f;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserSpeed = 100f;
    [SerializeField] float firingPeriod = 2f;

    [Header("Life and Death")]
    [SerializeField] float playerHealth = 1000f;

    Coroutine firingCoroutine;

    float xMinimum;
    float xMaximum;
    float yMinimum;
    float yMaximum;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
        StartCoroutine(YieldSeconds());
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Fire() {
        if (Input.GetButtonDown("Fire1")) {
        firingCoroutine = StartCoroutine(FireContinously());
        }
        if (Input.GetButtonUp("Fire1")) {
            StopCoroutine(firingCoroutine);
        }

    }

    IEnumerator FireContinously() {
        while (true) {
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
        yield return new WaitForSeconds(firingPeriod);
        }
    }


    private void Move() {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newYPosition = Mathf.Clamp(transform.position.y + deltaY, yMinimum, yMaximum);
        var newXPosition = Mathf.Clamp(transform.position.x + deltaX, xMinimum, xMaximum);
        transform.position = new Vector2(newXPosition, newYPosition);
    }


    private void SetUpMoveBoundaries() {
        Camera gameCamera = Camera.main;
        xMinimum = gameCamera.ViewportToWorldPoint(new Vector3(0,0,0)).x + padding;
        xMaximum = gameCamera.ViewportToWorldPoint(new Vector3(1,0,0)).x - padding;
        yMinimum = gameCamera.ViewportToWorldPoint(new Vector3(0,0,0)).y + padding;
        yMaximum = gameCamera.ViewportToWorldPoint(new Vector3(0,1,0)).y - padding;
    }

    IEnumerator YieldSeconds() {
        Debug.Log("HELLO WORLD");
        yield return new WaitForSeconds(3);
        Debug.Log("OHBOY");
    }

    private void ProcessHit(DamageDealer damageDealer) {
        playerHealth -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (playerHealth <= 0) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }




}


    