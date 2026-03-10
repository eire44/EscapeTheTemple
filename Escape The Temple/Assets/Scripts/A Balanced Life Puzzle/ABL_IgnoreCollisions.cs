using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABL_IgnoreCollisions : MonoBehaviour
{
    public Collider terrainCollider;
    public Collider floorCollider;

    void Start()
    {
        Physics.IgnoreCollision(this.GetComponent<Collider>(), terrainCollider);
        Physics.IgnoreCollision(this.GetComponent<Collider>(), floorCollider);
    }
}
