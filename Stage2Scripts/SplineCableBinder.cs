using UnityEngine;
using UnityEngine.Splines;
using System.Collections.Generic;

public class SplineCableBinder : MonoBehaviour
{
    [Header("연결 설정")]
    public SplineContainer splineContainer;
    public List<Transform> cableSegments = new List<Transform>();

    private SplineExtrude splineExtrude;
    private Spline targetSpline;          

    void Start()
    {
        if (splineContainer == null || cableSegments == null || cableSegments.Count < 2) return;

        splineExtrude = splineContainer.GetComponent<SplineExtrude>();
        targetSpline = splineContainer.Spline;

        InitializeSplineKnots();
    }

    private void InitializeSplineKnots()
    {
        targetSpline.Clear();
        for (int i = 0; i < cableSegments.Count; i++)
        {
            BezierKnot newKnot = new BezierKnot(Vector3.zero)
            {
                Rotation = Quaternion.identity
            };
            targetSpline.Add(newKnot);
            targetSpline.SetTangentMode(i, TangentMode.Linear);
        }
    }

    void Update()
    {
        if (targetSpline == null || splineExtrude == null) return;

        int count = cableSegments.Count;
        Vector3 upReference = Vector3.up;
        Transform containerTransform = splineContainer.transform;

        for (int i = 0; i < count; i++)
        {
            Transform segment = cableSegments[i];
            if (segment == null) continue;

            Vector3 localPos = containerTransform.InverseTransformPoint(segment.position);
            BezierKnot knot = targetSpline[i];
            knot.Position = localPos;
            Vector3 forwardDirection = Vector3.forward;

            if (i < count - 1 && cableSegments[i + 1] != null)
            {
                forwardDirection = (cableSegments[i + 1].position - segment.position).normalized;
            }
            else if (i > 0 && cableSegments[i - 1] != null)
            {
                forwardDirection = (segment.position - cableSegments[i - 1].position).normalized;
            }

            if (forwardDirection != Vector3.zero)
            {
                if (Mathf.Abs(Vector3.Dot(forwardDirection, Vector3.up)) > 0.95f)
                {
                    upReference = Vector3.forward;
                }
                else
                {
                    upReference = Vector3.up;
                }

                knot.Rotation = Quaternion.LookRotation(forwardDirection, upReference);
            }

            targetSpline[i] = knot;
        }

        splineExtrude.Rebuild();
    }
}