using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyShip : Ship
{

    public override void Update()
    {
        List<Collider2D> nearbyIslands = new List<Collider2D>(Physics2D.OverlapCircleAll(transform.position, 10f));
        nearbyIslands.Sort(delegate (Collider2D i1, Collider2D i2)
        {
            float dist1 = (i1.transform.position - transform.position).sqrMagnitude;
            float dist2 = (i2.transform.position - transform.position).sqrMagnitude;
            if (dist1 > dist2)
            {
                return -1;
            }
            else if (dist2 > dist1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        });

        int maxIslands = 5;
        Vector3 averageDirection = Vector3.zero;
        int count = 0;
        for (int i = 0; i < maxIslands && i < nearbyIslands.Count; i++, count++)
        {
            Vector3 diff = (nearbyIslands[i].transform.position - transform.position);
            Vector3 dir = diff.normalized * diff.sqrMagnitude;
            averageDirection += dir;
        }
        averageDirection /= count;

        velocity = spriteRenderer.transform.up * movementSpeed;

        float angle = Vector3.Angle(velocity, averageDirection);
        steering = Mathf.Min(Mathf.Abs(angle), turningSpeed) * Mathf.Sign(angle);

        base.Update();
    }
}
