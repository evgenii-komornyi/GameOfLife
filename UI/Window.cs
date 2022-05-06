using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class Window
    {
        private bool _isCursorVisible = true;

        /// <summary>
        /// Method sets optimal window size.
        /// </summary>
        public void WindowConfiguration()
        {
            SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
        }

        /// <summary>
        /// Method shows/hides blinking console's cursor.
        /// </summary>
        public void ShowHideCursor()
        {
            _isCursorVisible = !_isCursorVisible;
            Console.CursorVisible = _isCursorVisible;
        }

        /// <summary>
        /// Method clears console window.
        /// </summary>
        public void ClearConsole()
        {
            Console.Clear();
        }

        /// <summary>
        /// Method sets console window's title.
        /// </summary>
        /// <param name="title">Title.</param>
        public void SetTitle(string title)
        {
            Console.Title = title;
        }

        /// <summary>
        /// Method sets beginning cursor's position. 
        /// </summary>
        /// <param name="left">Shift from left.</param>
        /// <param name="top">Shift from top.</param>
        public void SetCursorPosition(int left, int top)
        {
            Console.SetCursorPosition(left, top);
        }

        /// <summary>
        /// Method sets window's size.
        /// </summary>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        private void SetWindowSize(int width, int height)
        {
            #pragma warning disable CA1416 // Validate platform compatibility
            Console.SetWindowSize(width, height);
            #pragma warning restore CA1416 // Validate platform compatibility
        }
    }
}
