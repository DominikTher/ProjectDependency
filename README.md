# Project Dependency
Project dependency diagram builder. Generate JSON from your solution and also HTML for visualization.

This is just for my personal purposes and it's very simple. Still in development. 

Tested only on a few basic solutions. I use this tool for diagrams of my solutions when I learning.

## Functionality
- [x] Export HTML ready diagram using [dagre-d3](https://github.com/dagrejs/dagre-d3)
- [x] Export JSON with dependencies between projects
- [x] Support for solution folders in HTML
- [x] Support for solution folders in JSON
- [X] .NET Core applications only

## Example of outputs

### Image of the final diagram
![Dependency diagram](https://github.com/DominikTher/ProjectDependency/blob/master/docs/dependencyDiagram-26-04-2020-12,20,45.PNG)

### HTML file
![dependencyDiagram-26-04-2020-12,20,45.html](https://github.com/DominikTher/ProjectDependency/blob/master/docs/dependencyDiagram-26-04-2020-12,20,45.html)

### JSON file
[dependencyDiagram-26-04-2020-12,20,45.json](https://github.com/DominikTher/ProjectDependency/blob/master/docs/dependencyDiagram-26-04-2020-12,20,45.json)

## Usage
1. Argument for the console app is path of the concrete solution
2. Then JSON and HTML files will be created automatically in the output directory
3. That's it, check your HTML chart :)
4. If you are not satisfied with the result, use JSON for further processing :)
