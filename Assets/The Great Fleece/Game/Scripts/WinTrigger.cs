using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject _cutscene;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.HasCard == true)
            {
                _cutscene.SetActive(true);
            }
            else
            {
                Debug.Log("You need to have the keycard!");
            }
        }
    }
}
