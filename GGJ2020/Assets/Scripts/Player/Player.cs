using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController m_controller;
    public int playerNumber;
    public float playerSpeed = 12f;
    private float gravity = -9.81f;

    private Vector3 m_velocity;

    public Transform playerMesh;

    public float dashSpeed = 5f;
    public float dashTime = 0.2f;
    private bool m_isDash = false;
    private bool m_isCooldownDash = true;
    public float timeDashCooldown = 1f;
    private float m_lastX;
    private float m_lastZ;

    [SerializeField]
    private Transform m_spawnExtinguisher;
    [SerializeField]
    private GameObject m_extinguisher;

    public PipeBreakManager pipeBreakManager;

    void Start()
    {
        m_controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Dash();
    }

    private void OnTriggerStay(Collider other)
    {

        if (Input.GetButtonDown("Interact2Player" + playerNumber))
        {
            if (other.CompareTag("Puddle") && GetComponent<Catch>().GetObjectInHand() == Catch.ObjectInHand.Mop)
            {
                Destroy(other.gameObject);
                GetComponent<Catch>().DestroyPickable();
            }
            else if (other.gameObject.GetComponent<Flammable>() && GetComponent<Catch>().GetObjectInHand() == Catch.ObjectInHand.Extinguisher)
            {
                other.gameObject.GetComponent<Flammable>().SetIsOnFire(false);
                GetComponent<Catch>().DestroyPickable();
                GameObject extinguisher = Instantiate<GameObject>(m_extinguisher, m_spawnExtinguisher.position, Quaternion.Euler(-90.0f, 0.0f, 0.0f));
            }
            else if (other.gameObject.GetComponent<LeverLight>())
            {
                other.gameObject.GetComponent<LeverLight>().PlayerActivate(playerNumber);
            }
            else if (other.gameObject.GetComponent<ActiveConvoyer>())
            {
                Debug.Log("Active Convoyer");
                other.gameObject.GetComponent<ActiveConvoyer>().SpawnItem();
            }
            else if (other.gameObject.GetComponent<RepairConvoyer>())
            {
                other.gameObject.GetComponent<RepairConvoyer>().PlayerActivate(playerNumber);
            }
            else if (other.CompareTag("Pipe"))
            {
                pipeBreakManager.FixPipe(other.name);
            }
            else if (other.CompareTag("Valve"))
            {
                pipeBreakManager.FixValve(other.name);
            }
        }
    }

    void Move()
    {
        float x = 0, z = 0;

        if (m_isDash)
        {
            Vector3 move = transform.right * m_lastX + transform.forward * m_lastZ;
            m_controller.Move(move * playerSpeed * dashSpeed * Time.deltaTime);
        }
        else
        {
            if (Input.GetJoystickNames().Length > playerNumber - 1 && !string.IsNullOrEmpty(Input.GetJoystickNames()[playerNumber - 1]))
            {
                x = Input.GetAxis("JoyXPlayer" + (playerNumber));
                z = Input.GetAxis("JoyYPlayer" + (playerNumber));
            }
            else
            {
                x = Input.GetAxis("HorizontalPlayer" + (playerNumber));
                z = Input.GetAxis("VerticalPlayer" + (playerNumber));
            }

            if (x != 0.0f || z != 0.0f)
            {
                if (GetComponent<Animator>().GetFloat("Speed") == 0.0f)
                {
                    GetComponent<Animator>().SetFloat("Speed", playerSpeed);
                }
                playerMesh.eulerAngles = new Vector3(0, (Mathf.Atan2(x, z) * 180 / Mathf.PI), 0);
            }
            else if(GetComponent<Animator>().GetFloat("Speed") != 0.0f)
            {

                GetComponent<Animator>().SetFloat("Speed", 0.0f);
            }

            Vector3 move = transform.right * x + transform.forward * z;
            m_controller.Move(move * playerSpeed * Time.deltaTime);

            m_lastX = x;
            m_lastZ = z;

            m_velocity.y += gravity * Time.deltaTime;
            m_controller.Move(m_velocity * Time.deltaTime);
        }
    }

    void Dash()
    {
        if (Input.GetButtonDown("DashPlayer" + playerNumber) && m_isCooldownDash)
        {

            m_isDash = true;
            m_isCooldownDash = false;
            StartCoroutine(MyDash());
            
        }
    }

    IEnumerator MyDash()
    {
        yield return new WaitForSeconds(dashTime);
        m_isDash = false;
        yield return new WaitForSeconds(timeDashCooldown);
        m_isCooldownDash = true;
    }

    
}