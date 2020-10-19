using System;
using System.Runtime.InteropServices;

namespace core.Runner
{
    internal static class FFI
    {
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);


        internal static class SW_COMMANDS
        {
            /// <summary>
            /// Minimizes a window, even if the thread that owns the window is not responding. This flag should only be used when minimizing windows from a different thread. 
            /// </summary>
            internal const int SW_FORCEMINIMIZE = 11;
            /// <summary
            /// Hides the window and activates another window. 
            /// </summary>
            internal const int SW_HIDE = 0;
            /// <summary>
            /// Maximizes the specified window. 
            /// </summary>
            internal const int SW_MAXIMIZE = 3;
            /// <summary>
            /// Minimizes the specified window and activates the next top-level window in the Z order. 
            /// </summary>
            internal const int SW_MINIMIZE = 6;
            /// <summary>
            ///  Activates the window and displays it in its current size and position. 
            /// </summary>
            internal const int SW_SHOW = 5;
            /// <summary>
            ///  Activates and displays a window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when displaying the window for the first time. 
            /// </summary>
            internal const int SW_SHOWNORMAL = 1;
        }
    }
}
