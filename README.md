# BattleShip

The task is to implement a Battleship state tracking API for a single player that must support the following logic: 
<br>
* Create a board 
* Add a battleship to the board 
* Take an “attack” at a given position, and report back whether the attack resulted in a hit or a miss.

## Deployed To Azure
* URL - https://battleship20210503115708.azurewebsites.net/
* SwaggerUrl - https://battleship20210503115708.azurewebsites.net/swagger/index.html

### Endpoints
| api | Type | Description |
| --- | --- |  --- |
| `\` | GET | Welcome message |
| `/Game` | GET | Welcome message |
| `/Game/CreateBoard` | POST | Create board |
| `/Game/AddBattleShip` | POST | Add a battleship to the board |
| `/Game/Attack` | GET | Check for attack |

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Prerequisites
* Visual Studio 2019
* .Net Core 3.1

### Build
* Open "BattleShip.sln" in Visual Studio IDE to build the application
* In the toolbar, click "Build" -> "Build Solution"

### Running the Application
* In the toolbar, click "Debug" -> "Start without Debugging"
* "https://localhost:44398/swagger/index.html" page will be displayed in the browser

### Running the Tests
* In the toolbar, click "Test" -> "Run All Tests"
* Test results will be dispayed in the "Test Explorer"

### Publishing to Azure
* In the toolbar, click "Build" -> "Publish Battleship"
* Click on "Publish" button to publish to Azure. (Might require Azure credentials)






