using Files;
using UI;

FileController fileController = new FileController();

Bootstrapper bootstrapper = new Bootstrapper(fileController);

UIController ui = new UIController(bootstrapper);
ui.start();