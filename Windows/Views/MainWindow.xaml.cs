using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Navigation;
using Windows.Views;


namespace Windows
{
    public partial class MainWindow : NavigationWindow
    {
        public bool isFullScreen { get; private set; } = false;
        public WindowState lastWindowState { get; private set; } = WindowState.Maximized;
        public static MainMenu mainMenu { get; private set; } = new MainMenu();

        public MainWindow()
        {
            InitializeComponent();

            Application.Current.MainWindow = this;

            Width = SystemParameters.WorkArea.Width / 1.5;
            Height = SystemParameters.WorkArea.Height / 1.5;

            mainMenu.KeepAlive = true;
            mainMenu.InitializeComponent();

            string[] args = Environment.GetCommandLineArgs();
            if (args.Count() >= 2)
            {
                foreach (string item in args)
                {
                    Console.WriteLine($"Arguments: { item }");
                    switch (item)
                    {
                        case "--debug":
                            Console.WriteLine("Entering Debug Mode");
                            break;
                        default:
                            break;
                    }
                }
            } 
            else
            {
                
            }

            Content = mainMenu;
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            var source = PresentationSource.FromVisual(this);
            ((HwndSource)source)?.AddHook(Hook);
        }

        const int WM_MOUSEHWHEEL = 0x020E;

        private IntPtr Hook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case WM_MOUSEHWHEEL:
                    int tilt = (short)HIWORD(wParam);
                    OnMouseTilt(tilt);
                    return (IntPtr)1;
            }
            return IntPtr.Zero;
        }

        /// <summary>
        /// Gets high bits values of the pointer.
        /// </summary>
        private static int HIWORD(IntPtr ptr)
        {
            var val32 = ptr.ToInt32();
            return ((val32 >> 16) & 0xFFFF);
        }

        /// <summary>
        /// Gets low bits values of the pointer.
        /// </summary>
        private static int LOWORD(IntPtr ptr)
        {
            var val32 = ptr.ToInt32();
            return (val32 & 0xFFFF);
        }

        private void OnMouseTilt(int tilt)
        {
            foreach (var al in FindVisualChildren<Assets.UserControls.AnimeList>(this))
            {
                if(al.IsMouseOver)
                {
                    al.MouseTilt(tilt);
                    break;
                }
            }
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null)
                yield return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                if (child != null && child is T)
                    yield return (T)child;

                foreach (T childOfChild in FindVisualChildren<T>(child))
                    yield return childOfChild;
            }
        }

        private void NavigationWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    if (isFullScreen)
                    {   //Not using method for unnecessary if statements
                        StateChanged -= NavigationWindow_StateChanged;
                        WindowState = WindowState.Normal;
                        WindowStyle = WindowStyle.SingleBorderWindow;
                        WindowState = lastWindowState;
                        isFullScreen = false;
                        StateChanged += NavigationWindow_StateChanged;

                        e.Handled = true;
                        break;
                    }
                    if (NavigationService.CanGoBack)
                    {
                        NavigationService.GoBack();
                        e.Handled = true;
                        break;
                    }
                    else
                        Console.WriteLine("ERROR: Cannot go back");
                    break;
                case Key.R:
                    if (Keyboard.IsKeyDown(Key.LeftCtrl))
                    {
                        NavigationService.Refresh();
                        e.Handled = true;
                    }
                    break;
                case Key.LeftCtrl:
                    if (Keyboard.IsKeyDown(Key.R))
                    {
                        NavigationService.Refresh();
                        e.Handled = true;
                    }
                    break;
                case Key.F:
                    ChangeFullscreen();
                    e.Handled = true;
                    break;
            }
        }

        private void ChangeFullscreen()
        {
            StateChanged -= NavigationWindow_StateChanged;
            if (isFullScreen)
            {
                WindowState = WindowState.Normal;
                WindowStyle = WindowStyle.SingleBorderWindow;
                WindowState = lastWindowState;
                isFullScreen = false;
            }
            else
            {
                WindowState = WindowState.Normal;
                WindowStyle = WindowStyle.None;
                WindowState = WindowState.Maximized;
                isFullScreen = true;
            }
            StateChanged += NavigationWindow_StateChanged;
        }

        private void NavigationWindow_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void NavigationWindow_StateChanged(object sender, EventArgs e)
        {
            lastWindowState = WindowState;
        }

        //Mediaelement Freeze when switching screen fix
        private void Mediaelmtfix(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Screen.AllScreens.Length > 1)
            {
                LocationChanged -= Mediaelmtfix;
                HwndSource hwndSource = PresentationSource.FromVisual(this) as HwndSource;
                HwndTarget hwndTarget = hwndSource.CompositionTarget;
                hwndTarget.RenderMode = RenderMode.SoftwareOnly;
            }
        }
    }
}
