using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using WinUI.Library;

namespace WinForms;

public partial class Form1 : Form
{
	public Form1()
	{
		InitializeComponent();
		Load += OnLoad;
	}

	public Microsoft.UI.Xaml.Hosting.DesktopWindowXamlSource Dwxs { get; private set; }
	public Microsoft.UI.Windowing.AppWindow FormAppWindow { get; set; }

	private void OnLoad(object? sender, EventArgs e)
	{
		WindowId windowId = Win32Interop.GetWindowIdFromWindow(Handle);
		FormAppWindow = AppWindow.GetFromWindowId(windowId);

		Microsoft.UI.Xaml.Hosting.WindowsXamlManager.InitializeForCurrentThread();

		Dwxs = new Microsoft.UI.Xaml.Hosting.DesktopWindowXamlSource();
		Dwxs.Initialize(FormAppWindow.Id);
		Dwxs.SiteBridge.MoveAndResize(new Windows.Graphics.RectInt32(0, 0, 0, 0));
	}

	private Window? libraryWindow;

	private void button1_Click(object sender, EventArgs e)
	{
		libraryWindow = new WinUI.Library.LibraryWindow();
		libraryWindow.Activate();
		libraryWindow.AppWindow.Closing += (_, _) => {
			libraryWindow = null;
		};
	}
}
