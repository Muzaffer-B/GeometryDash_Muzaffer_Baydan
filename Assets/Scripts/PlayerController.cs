using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Seetings")]
    [SerializeField] int speed = 1;
    [SerializeField] private float GroundCehckRadius;

    [Header("Elements")]
    [SerializeField] private Transform GroundCheckTransform;
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private LayerMask GroundMask;
    [SerializeField] private Transform sprite;

    public bool gravityZoneTouched;
    // Start is called before the first frame update
  
    Rigidbody2D rb;
    Camera camera;

    public static Action onMouseClick;
    
    void Start()
    {
        camera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        ObstacleCheck.ObstacleTouched += Respawn;
    }

    private void OnDestroy()
    {
        ObstacleCheck.ObstacleTouched -= Respawn;

    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameMode == GameMode.Move)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;

            Move();
        }
     
       
    }

    bool OnGround()
    {
        return Physics2D.OverlapBox(GroundCheckTransform.position, Vector2.one, GroundCehckRadius, GroundMask);
    }

    void Respawn()
    {
        transform.position = spawnLocation.position;

        GameManager.instance.ChangeGameStatus(gameStatus.Normal);
        if (gravityZoneTouched)
        {
            camera.backgroundColor = Color.Lerp(Color.black, Color.blue, 5);
        }
        

    }
    void Move()
    {

        if (Input.GetMouseButton(0) && GameManager.instance.inGameStatus == gameStatus.Gravity)
        {
            int i = 0;
            i++;
            Debug.Log("i: " + i);
            Debug.Log("HoldJumped");
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
            onMouseClick?.Invoke();
        }

        if (OnGround())
        {
            Vector3 Rotation = sprite.rotation.eulerAngles;
            Rotation.z = Mathf.Round(Rotation.z / 90) * 90;
            sprite.rotation = Quaternion.Euler(Rotation);

            if (Input.GetMouseButton(0) && GameManager.instance.inGameStatus == gameStatus.Normal)
            {
                Debug.Log("Jumped");
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * 26f, ForceMode2D.Impulse);
                onMouseClick?.Invoke();
            }
        }
        else
        {
            sprite.Rotate(Vector3.back * 1f);
        }
    }
  
    public void ChangeCameraColor()
    {
        gravityZoneTouched = true;
        camera.backgroundColor = Color.Lerp(Color.blue, Color.black, 5);
    }
}
