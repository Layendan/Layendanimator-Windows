using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
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
