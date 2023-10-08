using System.Collections;
using UnityEngine;

// Get rewards when hitting the mystery block
// code base on: https://github.com/zigurous/unity-super-mario-tutorial
public class GetRewards : MonoBehaviour
{
    // first get the credits
    void Start()
    {
        MajorGameManager.Instance.GainCredits();

        StartCoroutine(Animate());
    }
    
    // make animation -> let player move jump and down
    // also make block move jump and down
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
