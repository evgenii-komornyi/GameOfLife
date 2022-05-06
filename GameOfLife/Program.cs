using Files;
using UI;

FileController fileController = new FileController();
Window window = new Window();
UserInterface userInterface = new UserInterface(window);

GameManager gameManager = new GameManager(userInterface, fileController, window);

gameManager.RunApplication();