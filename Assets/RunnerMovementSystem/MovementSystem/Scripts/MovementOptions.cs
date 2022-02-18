using System;
using UnityEngine;

namespace RunnerMovementSystem
{
    [Serializable]
    public class MovementOptions
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _borderOffset;
/*        [SerializeField] private float _accelerate;*/

        public float MoveSpeed => _moveSpeed;
        public float RotationSpeed => _rotationSpeed;
        public float BorderOffset => _borderOffset;
/*        public float Accelerate => _accelerate;

        public void SetAccelerate(float accelerate)
        {
            _accelerate = accelerate;
        }*/
    }
}