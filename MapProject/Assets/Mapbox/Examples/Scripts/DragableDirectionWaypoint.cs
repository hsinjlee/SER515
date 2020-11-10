using Mapbox.Unity.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Utils;
using UnityEngine.UI;

namespace Mapbox.Examples
{
	public class DragableDirectionWaypoint : MonoBehaviour
	{
		public Transform MoveTarget;
		private Vector3 screenPoint;
		private Vector3 offset;
		private Plane _yPlane;
		public CameraMovement _camera;
		private string _waypointType;
		public InputField startPoint;
		public InputField endPoint;
		public void Start()
		{
			_camera = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
			_yPlane = new Plane(Vector3.up, Vector3.zero);
			startPoint = GameObject.Find("StartPoint").GetComponent<InputField>();
			endPoint = GameObject.Find("EndPoint").GetComponent<InputField>();
		}

		void OnMouseDrag()
		{
			if(this.name == "pinpoint1" || this.name == "pinpoint2")
            {
				if(_waypointType != this.name)
                {
					_waypointType = this.name;
				}
            }
			
			_camera.moveFlag = false;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			float enter = 0.0f;
			if (_yPlane.Raycast(ray, out enter))
			{
				MoveTarget.position = ray.GetPoint(enter);
			}
		}

        private void OnMouseUp()
        {
			_camera.moveFlag = true;
			GameObject g = GameObject.FindGameObjectWithTag(_waypointType);
            DirectionsFactory theDirect = GameObject.Find("Directions(Clone)").GetComponent<DirectionsFactory>();
			Vector2d v = theDirect.TransferName(g.transform);
			if (_waypointType == "pinpoint1")
            {
				startPoint.text = v.x + " " + v.y;	
			}
			if (_waypointType == "pinpoint2")
            {
				endPoint.text = v.x + " " + v.y;
            }
        }
    }
}
