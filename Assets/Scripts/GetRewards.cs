using System.Collections;
using UnityEngine;

public class GetRewards : MonoBehaviour
{
    void Start()
    {
        MajorGameManager.Instance.GainCredits();

        StartCoroutine(Animate());
    }
    
    private IEnumerator Animate()
    {
        Vector3 restingPosition = transform.localPosition;
        Vector3 animatedPosition = restingPosition + Vector3.up * 2f;

        yield return Move(restingPosition, animatedPosition);
        yield return Move(animatedPosition, restingPosition);
        
        Destroy(gameObject);
    }

    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float elapsed = 0f;
        float duration = 0.40f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            transform.localPosition = Vector3.Lerp(from,to,t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = to;

    }
}