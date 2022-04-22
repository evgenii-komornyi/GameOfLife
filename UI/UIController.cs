using Files;
using GameOfLifeEngine;

namespace UI
{
    /// <summary>
    /// The class is contained all settings 
    /// to start a game from UI.
    /// </summary>
    public class UIController
    {
        private Bootstrapper bootstrapper;

        /// <summary>
        /// Constructor for Dependency Injection.
        /// </summary>
        /// <param name="bootstrapper">
        /// Bootstrapper service.
        /// </param>
        public UIController(Bootstrapper bootstrapper)
        {
            this.bootstrapper = bootstrapper;
        }

        /// <summary>
        /// Method starts the UI.
        /// </summary>
        public void start()
        {
            bootstrapper.Greatings();
            bootstrapper.StartMenu();
        }
    }
}