using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomFunctions
{
	public class AI : MonoBehaviour
	{
        public static Vector3 GetRandomTargetPosition(Vector3 position, float distance)
		{
			Vector3 randomDirection = Random.insideUnitSphere * distance;
			randomDirection += position;
            UnityEngine.NavMeshHit navMeshHit;
            UnityEngine.NavMesh.SamplePosition(randomDirection, out navMeshHit, distance, -1);
			return navMeshHit.position;
        }

		public static float CheckTargetDistance(Transform fromThis, Transform toThis, float range)
	    {
	        Ray ray = new Ray(fromThis.position, toThis.position);
	        RaycastHit hit;

			if (Physics.Raycast(ray, out hit, range)) {
				return hit.distance;
			} else {
				return 0;
			}
        }
    }
}
