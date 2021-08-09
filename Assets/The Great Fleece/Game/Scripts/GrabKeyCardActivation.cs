using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabKeyCardActivation : MonoBehaviour
{
    public GameObject _cutScene;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _cutScene.SetActive(true);
            GameManager.Instance.HasCard = true;
            this.gameObject.SetActive(false);
        }
    }
}
