using Files;
using UI;

FileController fileController = new FileController();
UserInterface userInterface = new UserInterface();

GameManager gameManager = new GameManager(userInterface, fileController);

gameManager.RunApplication();