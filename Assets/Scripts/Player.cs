using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] GameObject weapon;
    [SerializeField] float weaponSpeed;

    Coroutine shootingCoroutine;

    float minX;
    float maxX;
    float minY;
    float maxY;

    // Start is called before the first frame update
    void Start()
    {
        SetMoveBoundaries();
    }    

    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();
    }

    private void Shoot()
    {

        if (Input.GetButtonDown("Fire1")) {
            shootingCoroutine = StartCoroutine(ShootContinuously());
        }
        else if (Input.GetButtonUp("Fire1")) {
            StopCoroutine(shootingCoroutine);
        }
    }

    private IEnumerator ShootContinuously()
    {
        while (true) {
            GameObject energyBall = Instantiate(weapon, transform.position, Quaternion.identity);
            energyBall.GetComponent<Rigidbody2D>().velocity = new Vector2(0, weaponSpeed);

            yield return new WaitForSeconds(0.2f);
        }
    }    

    private void Move()
    {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, minX, maxX);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, minY, maxY);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void SetMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        minX = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        maxX = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        minY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        maxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }
}
