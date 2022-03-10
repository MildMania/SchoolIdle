using Pathfinding;
using UnityEngine;

namespace MildMania.IdleArcade.Player
{
    public class NavmeshMovementBehavior : MonoBehaviour,
        IMovementExecutor
    {
        [SerializeField] private float _navMeshSampleDistance = 1.0f;

        private Seeker _seeker;
        public Seeker Seeker
        {
            get
            {
                if (_seeker == null)
                    _seeker = GetComponent<Seeker>();

                return _seeker;
            }
        }

        private Rigidbody _rigidbody;
        public Rigidbody Rigidbody
        {
            get
            {
                if (_rigidbody == null)
                    _rigidbody = GetComponent<Rigidbody>();

                return _rigidbody;
            }
        }

        public float MovementSpeed { get; private set; }

        public void Move(
            Vector3 moveDirection,
            float speed)
        {
            // Move the controller
            if (Seeker.IsOnGraph(distTreshold: 0.1f, out Vector3 _))
            {
                MovementSpeed = 1.0f;
                
                Vector3 curPosition = Rigidbody.transform.position;
                Vector3 newPosition = curPosition + moveDirection * speed * Time.deltaTime;

                if (AStarExtensions.IsOnGraph(
                    newPosition,
                    _navMeshSampleDistance,
                    out Vector3 hitPosition))
                    Rigidbody.transform.position = hitPosition;
            }
        }

        public void Stop()
        {
            MovementSpeed = 0;
        }

        public void LookAt(Vector3 targetPos)
        {
            Rigidbody.transform.forward = targetPos - Rigidbody.transform.position;
        }
    }
}
