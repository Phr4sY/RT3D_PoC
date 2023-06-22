# Genetic Algorithm
Team Members: Jan Patzelt, Hannes Imschweiler, Susanne Werking

## How to run the project
You will find our project in [this repo](https://github.com/Phr4sY/RT3D_PoC/tree/textOnCanvas).

Our project structure consists of multiple Scripts:
* Car: Logic for a car and its gene information, 
* Evolution: for sorting, selecting, reproducing and mutating functions
* Population: defining parameters for genetic algorithm, creating and running generations of car Gameobject (individuals) by adding Car instance to Gameobject
* RoadGenerator: Generation of road with 'Wall' and 'Target' prefab 


## Summary of development/research
First steps were researching and learning with tutorials from Unity and YouTube how to use Unity itself. Behavior Trees, Genetic Algorithm and path finding algorithms were the second part of the research, how they work and how to apply them. 

Second step was to sketch out our application with individuals, population, general process and what parameters to use.

The implementation phase was the most time intensive process. Starting from the Car script, refining our skills in C# and working toward the genetic algorithm cycle with small steps and debugging. One of the biggest problems were to use GameObjects correctly and get a correct behavior from the Car individuals. Stackoverflow and YouTube tutorials were a good support.


## References
The following links served as inspiration and problem solving:
[Genetic algorithm in unity using C#](https://medium.com/analytics-vidhya/genetic-algorithm-in-unity-using-c-72f0fafb535c)
[Training Cars with Genetic Algorithms Part 2](https://www.youtube.com/watch?v=R4Ty165kMlo)
[Neural Networks and Genetic Algorithms for a Self Driving Car in Unity](https://www.youtube.com/watch?v=C6SZUU8XQQ0)



# TODO: Following things have to be in readme.txt:
   - Name of project
   - Name of team members (important)
   - Optional: How to run the project, project structure
   - Summary of development/research: How did you spend most of the time. What were challenges and how did you solve them? Any lessons learned?
   - References: Used Assets, Code, Inspiration. Everything you have not done yourself should be listet
    Link to a short video that shows the features of the project
    Link to project files if project is larger than 100MB (Google Drive, Dropbox, ...)
