using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterPipes : MonoBehaviour
{
    public Transform connection;
    public KeyCode enterKeyCode = KeyCode.S;
    public Vector3 enterDirection = Vector3.down;
    public Vector3 exitDirection = Vector3.zero;
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (connection != null && other.CompareTag("Player"))
        {
            if (Input.GetKey(enterKeyCode))
            {
                StartCoroutine(Enter(other.transform));
            }
        }
    }

    private IEnumerator Enter(Transform player)
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        
        Vector3 enteredPos = transform.position + enterDirection;
        Vector3 enterScale = Vector3.one * 1f;

        yield return Move(player, enteredPos, enterScale);
        yield return new WaitForSeconds(1f);
        
        Camera.main.GetComponent<CameraFollowing>().UnderGround(connection.position.y <= 0f);
        
        if (exitDirection != Vector3.zero)
        {
            player.position = connection.position - exitDirection;
            yield return Move(player, connection.position + exitDirection, Vector3.one);
        }
        else
        {
            player.position = connection.position;
            player.localScale = Vector3.one;
        }

        player.GetComponent<PlayerMovement>().enabled = true;

    }

    private IEnumerator Move(Transform player, Vector3 endPos, Vector3 endScale)
    {
        float elapsed = 0f;
        float duration = 1.2f;

        Vector3 startPos = player.position;
        Vector3 startScale = player.localScale;

        while (elapsed < duration)
        {
            float time = elapsed / duration;

            player.position = Vector3.Lerp(startPos, endPos, time);
            player.localScale = Vector3.Lerp(startScale, endScale, time);
            elapsed += Time.deltaTime;

            yield return null;
        }

        player.position = endPos;
        player.localScale = endScale;

    }
}
