// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;

namespace HoloToolkit.Unity.Tests
{
    public class RotateMinMax : MonoBehaviour
    {
        [SerializeField] private float _minAngle;
        [SerializeField] private float _maxAngle;
        [SerializeField] private float _step;
        [SerializeField] private bool _completed = false;

        private void Update()
        {
            if (_completed)
                return;

            transform.Rotate(Vector3.left, _step);
            if (transform.localRotation.eulerAngles.x > _maxAngle)
            {
                _step *= -1;
            }
            else if (transform.localRotation.eulerAngles.x == _maxAngle)
            {
                _completed = true;
            }
        }
    }
}
