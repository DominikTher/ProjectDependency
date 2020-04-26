# Project Dependency
Project dependency diagram builder. Generate JSON from your solution and also XML for [draw.io](https://app.diagrams.net/).

This is just for my personal purposes and it's very simple. Tested only on a few basic solutions. I use this tool for diagrams of my solutions when I learning.

## Functionality
- [x] Export XML ready diagram for importing to [draw.io](https://app.diagrams.net/)
- [x] Export JSON with dependencies between projects
- [x] .NET Core applications only

## Example of outputs
### Final diagram
![Dependency diagram](https://github.com/DominikTher/ProjectDependency/blob/basic/docs/dependencyDiagram-20-04-2020-20%2C19%2C52.png)

### XML file
[dependencyDiagram-20-04-2020-20,19,52.xml](https://github.com/DominikTher/ProjectDependency/blob/basic/docs/dependencyDiagram-20-04-2020-20%2C19%2C52.xml)
### JSON file
[dependencyDiagram-20-04-2020-20,19,52.json](https://github.com/DominikTher/ProjectDependency/blob/basic/docs/dependencyDiagram-20-04-2020-20%2C19%2C52.json)

## Usage
1. As an argument for the console app, paste the path of the concrete solution
2. JSON and XML files will be created
3. If you want a nice result, import your XML to [draw.io](https://app.diagrams.net/)
    - for the best result, select all elements on the diagram. Go to Arrange -> Layout -> Vertical Flow
    - then you can modify your diagrams for presentations, documents, etc.
