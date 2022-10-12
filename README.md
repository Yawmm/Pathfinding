# Pathfinding
 A library implementing the A* algorithm and Dijkstra's algorithm.

 In the Pathfinding directory you can find the core systems related to pathfinding combined with a few demos.
 This library implements multiple algorithms, both Dijkstra's algorithm and the A* algorithm. You can switch between these algorithms by changing the `appsettings.json` file.

 Here you can change a few things:
 `Settings.Pathfinding.Type`: the pathfinding algorithm used, value: `Dijkstra` or `AStar` or null
 `Settings.Map`: change which map the pathfinding algorithm will use and how it should display and parse the given maps.

 Besides this main directory, there is also a Benchmarks directory, which contains a number of benchmarks you can run.
 There is also a very basic unit test class which tests some basic functionality, like "is the map parsed correctly", or "does the program created a correct graph from the given map"

 If you want to run the benchmarks you will need to run the project in Release mode.