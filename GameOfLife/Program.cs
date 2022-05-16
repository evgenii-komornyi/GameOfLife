using Files;
using UI;

FileService fileService = new FileService();
Window window = new Window();
UserInterface userInterface = new UserInterface(window);

FileManager fileManager = new FileManager(fileService);
GameManager gameManager = new GameManager(userInterface, fileManager, window);

gameManager.RunApplication();