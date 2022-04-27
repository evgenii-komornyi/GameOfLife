using Files;
using UI;

FileController fileController = new FileController();
UserInterface userInterface = new UserInterface();

UIController uiController = new UIController(userInterface, fileController);

userInterface.Greatings();
uiController.RunApplication();