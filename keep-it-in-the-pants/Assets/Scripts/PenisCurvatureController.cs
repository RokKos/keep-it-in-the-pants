using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenisCurvatureController : MonoBehaviour {

    [SerializeField] private Transform endPoint;
    private Vector3 startPosition;
    private Vector3 endPosition;
    [SerializeField] private Transform[] bones;
    [SerializeField] private AnimationCurve curvature;
    [SerializeField] private float finalOffset;

    private void Start() {
        startPosition = bones[0].position;
        EventManager.Instance.OnDirectionInputChanged.AddListener(Curve);
    }

    private void Curve(float x, float z) {
        x *= finalOffset;
        z *= finalOffset;
        startPosition = bones[0].position;
        for (int i = 1; i < bones.Length; i++) {
            var progress = (float)i / (float) bones.Length;
            var xCurved = startPosition.x + curvature.Evaluate(progress) * x;
            var zCurved = startPosition.z + curvature.Evaluate(progress) * z;

            bones[i].position = new Vector3(xCurved, bones[i].position.y, zCurved);
        }
    }

}
