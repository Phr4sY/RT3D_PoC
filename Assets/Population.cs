using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population : MonoBehaviour
{
    public GameObject carPrefab;
	public List<GameObject> cars;
	public List<Car> carControllers;

	int numberCars = 5;
	int numberOfCarsCrashed = 0;

	void Start(){
		for (int i = 0; i < numberCars; i++)
		{
			GameObject car = Instantiate(carPrefab, this.transform.position, this.transform.rotation);
			Car carController = car.GetComponent<Car>();
			cars.Add(car);
			carControllers.Add(carController);
		}
		Debug.Log(cars[0].GetComponent<Car>().distance);
	}

	void Update(){
		Debug.Log(carControllers[0].distance);

		for (int i = 0; i < carControllers.Count; i++)
		{
			if (carControllers[i].crashed)
			{
				numberOfCarsCrashed++;
			}
		}

		if (numberOfCarsCrashed == cars.Count)
		{
			Debug.Log("All cars crashed. Yay");
			Debug.Log("DNA is " + carControllers[0].direction[1]);
		}
	}
}
