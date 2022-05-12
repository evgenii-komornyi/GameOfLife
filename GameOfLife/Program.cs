using Files;
using UI;

FileController fileController = new FileController();
Window window = new Window();
UserInterface userInterface = new UserInterface(window);

FileManager fileManager = new FileManager(fileController);
GameManager gameManager = new GameManager(userInterface, fileManager, window);

gameManager.RunApplication();