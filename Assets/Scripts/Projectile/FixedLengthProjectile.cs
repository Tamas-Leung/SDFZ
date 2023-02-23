using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedLengthProjectile : Projectile
{
    [SerializeField] private float duration;
    [SerializeField] private float distance;
    [SerializeField] private AnimationCurve moveCurve;


    private Vector3 direction;
    private Vector3 targetPosition;
    private Vector3 startPosition;
    private float elapsedTime = 0;

    public override void SetupDirection(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
        startPosition = transform.position;
        targetPosition = transform.position + direction * distance;
        Destroy(gameObject, duration);
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / duration;

        transform.position = Vector3.Lerp(startPosition, targetPosition, moveCurve.Evaluate(percentageComplete));
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
