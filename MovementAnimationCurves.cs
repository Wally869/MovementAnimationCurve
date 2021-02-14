using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnimationCurves : MonoBehaviour
{
    [Header("Cycle Durations (Seconds)")]
    public float mCycleDurationX;
    public float mCycleDurationY;
    public float mCycleDurationZ;

    [Header("Animation Curves")]
    public AnimationCurve mCurveX;
    public AnimationCurve mCurveY;
    public AnimationCurve mCurveZ;

    [Header("Rotations Axis Allowed")]
    public bool bAxisX = true;
    public bool bAxisY = true;
    public bool bAxisZ = true;

    private float _mElapsed = 0f;
    private Vector3 _kReferencePosition;

    private Vector3 _mTempPosition;
    private Vector3 _mTempForward; 

    private void Awake()
    {
        _kReferencePosition = transform.localPosition;


        // ensure non-zero to avoid division by zero error
        if (mCycleDurationX == 0f)
        {
            mCycleDurationX = 0.001f;
        }
        if (mCycleDurationY == 0f)
        {
            mCycleDurationY = 0.001f;
        }
        if (mCycleDurationZ == 0f)
        {
            mCycleDurationZ = 0.001f;
        }
    }

    private void Update()
    {
        _mElapsed += Time.deltaTime;

        _mTempPosition = _kReferencePosition;

        _mTempPosition.x += mCurveX.Evaluate(Evaluate(mCycleDurationX));
        _mTempPosition.y += mCurveX.Evaluate(Evaluate(mCycleDurationY));
        _mTempPosition.z += mCurveX.Evaluate(Evaluate(mCycleDurationZ));

        _mTempForward = _mTempPosition - transform.localPosition;
        if (bAxisX == false)
        {
            _mTempForward.x = 0f;
        }
        if (bAxisY == false)
        {
            _mTempForward.y = 0f;
        }
        if (bAxisZ == false)
        {
            _mTempForward.z = 0f;
        }
        _mTempForward = _mTempForward.normalized;
    }

    private void LateUpdate()
    {
        transform.forward = _mTempForward;
        transform.localPosition = _mTempPosition;

    }

    private float Evaluate(float cycleDuration)
    {
        return Mathf.Sin(2f * Mathf.PI * _mElapsed * (1f / cycleDuration));
            
    }

}
