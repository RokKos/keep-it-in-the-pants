using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour {

    [SerializeField] private new SphereCollider collider;

    public void SetRadius(float radius_) {
        collider.radius = radius_;
    }
}
