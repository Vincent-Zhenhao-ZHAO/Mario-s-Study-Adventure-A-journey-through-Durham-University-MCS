using System.Collections;
using UnityEngine;

// code base on: https://github.com/zigurous/unity-super-mario-tutorial
// hit the different blocks
public class HitBlock : MonoBehaviour
{
    public GameObject item;
    public int maxHits = -1;
    public Sprite emptyBlock;

    private bool _animating;
    
    // if player hits the block
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!_animating && maxHits!=0 && col.gameObject.CompareTag("Player"))
        {
            if (col.transform.DotTest(transform,Vector2.up))
            {
                Hit();
            }
        }
    }

    // the block reduce the maxhit
    // if met maxhit, then be empty and not able to hit
    // if not, then make animnation
    private void Hit()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        
        maxHits--;

        if (maxHits == 0)
        {
            spriteRenderer.sprite = emptyBlock;
        }

        if (item != null)
        {
            Instantiate(item, transform.position,Quaternion.identity);
        }

        StartCoroutine(Animate());
    }

    // animation, move block up and down
    private IEnumerator Animate()
    {
        _animating = true;

        Vector3 restingPosition = transform.localPosition;
        Vector3 animatedPosition = restingPosition + Vector3.up * 0.5f;

        yield return Move(restingPosition, animatedPosition);
        yield return Move(animatedPosition, restingPosition);

        _animating = false;

    }

    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float elapsed = 0f;
        float duration = 0.125f;

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
