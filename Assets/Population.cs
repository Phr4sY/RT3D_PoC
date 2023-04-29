using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population : MonoBehaviour
{
    public GameObject carPrefab;
	public List<GameObject> cars;
	public List<Car> carControllers;
	Car carController;
	int numberCars = 1;

	void Start(){
		for (int i = 0; i < numberCars; i++)
		{
			GameObject car = Instantiate(carPrefab, this.transform.position, this.transform.rotation);
			carController = car.GetComponent<Car>();
			cars.Add(car);
			carControllers.Add(carController);
		}
		Debug.Log(cars[0].GetComponent<Car>().distance);
	}

	void Update(){
		Debug.Log(carControllers[0].distance);
	}
}
