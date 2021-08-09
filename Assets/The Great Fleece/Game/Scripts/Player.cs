using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject _coin;
    [SerializeField]
    private AudioClip _coinSFX;
    private Animator _anim;
    private NavMeshAgent _agent;
    private Vector3 _target;
    private bool _coinTossed;
    


    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                _agent.SetDestination(hitInfo.point);
                _anim.SetBool("Walk", true);
                _target = hitInfo.point;
            }
        }

        float distance = Vector3.Distance(transform.position, _target);

        if (distance < 1.0f)
        {
            _anim.SetBool("Walk", false);
        }

        if (Input.GetMouseButtonDown(1) && _coinTossed == false)
        {
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                if (_coin != null)
                {
                    _anim.SetTrigger("Throw");
                    Instantiate(_coin, hitInfo.point, Quaternion.identity);
                    AudioSource.PlayClipAtPoint(_coinSFX, Camera.main.transform.position, 1.5f);
                    _coinTossed = true;
                    SendAIToCoin(hitInfo.point);
                }
            }
        }
    }

    void SendAIToCoin(Vector3 coinPosition)
    {
        GameObject[] guards = GameObject.FindGameObjectsWithTag("Guard1");

        foreach (var guard in guards)
        {
            NavMeshAgent currentAgent = guard.GetComponent<NavMeshAgent>();
            GuardAI currentGuard = guard.GetComponent<GuardAI>();
            Animator currentAnim = guard.GetComponent<Animator>();

            currentGuard.coinTossed = true;
            currentAgent.SetDestination(coinPosition);
            currentAnim.SetBool("Walk", true);
            currentGuard.coinPosition = coinPosition;
        }
    }
}
