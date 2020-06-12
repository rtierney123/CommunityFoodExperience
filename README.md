# CommunityFoodExperience
## Release Notes
### Build 1
- New software features for this release:
  + Working game map with 3D bus moving to different locations.
  + Stores working: Community Kitchen, Corner Store, Food Tiger, and Food Pantry.
  + Social Services: SNAP Office, VITA Services, and WIC Clinic.
  + Wallet contains currency values, character information, and transportation ticket.
  + Nutrition information displays the current nutrition level and required nutrition.
  + Clock running, gameplay lasts 15 minutes.
- Bug fixes made since the last release:
  + None.
- Known bugs and defects:
  + Drag and drop can duplicate items in store.
  + Clock misbehaves after traveling to some locations.
- Future features:
  + End game scene after player finishing the game with the survey link to ask player for opinion.
  + Tutorial scene to walk players through most complicated game logic.
 
### Build 2 
- New software features for this release 
  + A new map design has been implemented. 
  + The tutorial page is now available to players. 
  + Community Kitchen functionalities are working correctly and are affected by the current time in the game. 
  + Pause and ending menus now have all the logic built in to allow the player to restart or end the game early if they are unable to progress. 
- Bug fixes made since the last release:
  + The drag and drop functionality at store no longer incorrectly duplicates elements when dragged on the wrong side of the shop. 
  + The bus animation has been cleaned up and does not clip through any other 3d models in the map scene. 
  + The clock will not pass its max orange fill color and continue on forever. With the addition of the ending screen, the clock stops at 6pm and lines up with the orange pie chart fill. 
- Known bugs and defects:
  + Some food card information is known to be incorrect or inconsistent. Certain elements like prices are important to match, but others like saturated fats or cholesterols are unimportant. 
  + The end-game information about nutrition is not as comprehensive as the data we were supplied with (ie. no breakdown of cholesterols, fats, etc… and does not handle special dietary and health cases like diabetes
  + The information and up to date accuracy of financial aid programs was not portrayed in as much detail as we expected. 
  + There will not be tracking of users of this game, other than an external survey that a user can elect to participate in at the end of the game.
  + The embedded video conclusion/debriefing was never created, but in the future, this could easily be linked to a webpage on the foodbank’s site.
  
   
### Build 3(current)
- New software features for this release 
  + A new map design has been implemented with buildings closer together.
  + New bus system.
  + New UI designs including new location screens and control panel.
- Bug fixes made since the last release:
  + Fixed food items to display the correct information.
  + Fixed random events to always to be displayed properly.
  + Fixed clock to subtract correct amount of time.
- Known bugs and defects -- you should also include here any functionality you promised the customer but is missing in the release:
  + Word text currently filling out in form continues even on pause.
  + Additional nutrition information such as sodium, cholesterol, etc not displayed.
  + No in game tutorial beyond the help screen.

## Install Guide  
### Pre-requisites: 
- Desktop:
  + OS: Windows 7 SP1+, macOS 10.12+, Ubuntu 12.04+, SteamOS+
  + Graphics card with DX10 (shader model 4.0) capabilities.
  + CPU: SSE2 instruction set support.
- iOS player requires iOS 9.0 or higher.
- Android: OS 4.1 or later; ARMv7 CPU with NEON support or Atom CPU; OpenGL ES 2.0 or later.
- WebGL: Any recent desktop version of Firefox, Chrome, Edge or Safari.
- Universal Windows Platform: Windows 10 and a graphics card with DX10 (shader model 4.0) capabilities.
### Dependent libraries that must be installed:
- For normal user: none.
- For developer: install Unity version 2019.2.8f1.
### Download instructions: 
- Link:  http://teaminfinity9138.000webhostapp.com
- Username: implementation
- Password: ACFB255
### Build instructions:
- Open Community Food Experience folder in Unity.
- Go to File->Build.
- Choose Webgl as a build option.
- Click on the Build button.
### Run instructions: 
- Once files are up on server and embedded in page, the user just needs to go to site and refresh the page to see the game.
### Troubleshooting:  
- Browser scaling? Ctrl + “-” or Ctrl + “+” to resize or Ctrl + “*” to view 100% if number keys available.
- There is a known problem in Google Chrome where you must enable webGL in the browser settings. Press the triple dashed line button in the corner of the browser and select settings and enable webGL.
- Instructions also available here : https://ccm.net/faq/40585-how-to-enable-webgl-on-google-chrome.

