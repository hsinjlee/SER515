using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Unity.Utilities;
using System.Collections.Generic;

public class SpawnObject : MonoBehaviour
{
	[SerializeField]
	AbstractMap _map;

	[SerializeField]
	[Geocode]
	string[] _locationStrings;
	Vector2d[] _locations;
    Vector2d _coordinate;

	[SerializeField]
	float _spawnScale = 100f;

	[SerializeField]
	GameObject _markerPrefab;

	List<GameObject> _spawnedObjects;
    // Start is called before the first frame update
    void Start()
    {
       	_locations = new Vector2d[_locationStrings.Length];
		_spawnedObjects = new List<GameObject>();
		for (int i = 0; i < _locationStrings.Length; i++)
		{
            var locationString = _locationStrings[i];
			//var latLon = _locationStrings[i].Split(',');
            //var latLon = searchString.Split(',');
			//_coordinate.x = double.Parse(latLon[0]);
			//_coordinate.y = double.Parse(latLon[1]);
			_locations[i] = Conversions.StringToLatLon("33.404304, -111.93902");
			var instance = Instantiate(_markerPrefab);
			instance.transform.localPosition = _map.GeoToWorldPosition(_locations[i], true);
			instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
			_spawnedObjects.Add(instance);
		} 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
