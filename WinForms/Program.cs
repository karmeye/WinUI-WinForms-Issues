using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.XamlTypeInfo;

namespace WinForms;

internal class Program : Microsoft.UI.Xaml.Application, IXamlMetadataProvider
{
	private static XamlControlsXamlMetaDataProvider xamlMetaDataProvider = null;

	public Program()
	{
		System.Windows.Forms.Application.Run(new Form1());
	}

	/// <summary>
	/// Invoked when the application is launched. Override this method to perform application
	//  initialization and to display initial content in the associated Window.
	/// </summary>
	/// <param name="args">Event data for the event.</param>
	protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
	{
		if (xamlMetaDataProvider == null)
		{
			XamlControlsXamlMetaDataProvider.Initialize();
			xamlMetaDataProvider = new();
			this.Resources.MergedDictionaries.Add(new Microsoft.UI.Xaml.Controls.XamlControlsResources());
		}
		else
		{
			throw new Exception("OnLaunched was called a second time");
		}
	}

	/// <summary>
	///  The main entry point for the application.
	/// </summary>
	[STAThread]
	static void Main()
	{
		// To customize application configuration such as set high DPI settings or default font,
		// see https://aka.ms/applicationconfiguration.
		ApplicationConfiguration.Initialize();

		Microsoft.UI.Xaml.Application.Start((p) =>
		{
			var syncContext = new DispatcherQueueSynchronizationContext(DispatcherQueue.GetForCurrentThread());
			SynchronizationContext.SetSynchronizationContext(syncContext);

			new Program();

			var currentApp = Microsoft.UI.Xaml.Application.Current;
			if (currentApp is not null)
				currentApp.Exit();
		});
	}

	public IXamlType GetXamlType(Type type)
	{
		return xamlMetaDataProvider.GetXamlType(type);
	}

	public IXamlType GetXamlType(string fullName)
	{
		return xamlMetaDataProvider.GetXamlType(fullName);
	}

	public XmlnsDefinition[] GetXmlnsDefinitions()
	{
		return xamlMetaDataProvider.GetXmlnsDefinitions();
	}
}