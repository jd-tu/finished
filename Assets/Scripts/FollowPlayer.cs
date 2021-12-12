using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
        private GameManager gameManager;
        // private AudioSource playerAudio;
        [SerializeField] Transform player;
        Vector3 offset;

        private void Start () {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        offset = transform.position - player.position;
        }

        private void Update () {
        if(! gameManager.isGameActive) return;

        Vector3 targetPos = player.position + offset;
        targetPos.x = 200;
        transform.position = targetPos;
        }
}
