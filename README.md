# WinUI WinForms Issues


Running the WinUI project Unpackaged.

- HotReload 
  - works when modifying XAML of WinUI/MainWindow (but only in Debug build)	
  - works when modifying XAML of WinUI.Library/LibraryWindow (but only in Debug build)
- Still need to add `<UseRidGraph>true</UseRidGraph>` when targeting .NET 8 even though referencing Microsoft.WindowsAppSDK 1.4.240211001 (see https://github.com/microsoft/microsoft-ui-xaml/issues/8599#issuecomment-1875744583). To avoid “error NETSDK1083: The specified RuntimeIdentifier 'win10-x64' is not recognized. See https://aka.ms/netsdk1083 for more information.”



Running the WinForms project

- Opening the WinUI `Window`
  1. When `Window` is closed the `Form` is also closed and therefore the whole application quits.
  2. The `Microsoft.UI.XAML.Controls.TextBox` in the `Window` does not respond to keyboard input.
- When publishing the WinForms project and clicking the button to open the WinUI `Window` a `Microsoft.UI.Xaml.Markup.XamlParseException: XAML parsing failed.` is thrown.
  - This can be fixed manually by first building in Release and then copying the `\WinForms\bin\Release\net8.0-windows10.0.22621.0\WinUI.Library` folder and pasting it into the publishing folder.
- Most of the times, to get changes made to the the `LibaryWindow.xaml` XAML in the WinUI.Library to be included in the build, one has to clean the obj and bin folders. Just cleaning the solution is not enough. 
  - Seems like unloading the WinForms project does help.. make it work.
  - Most of the times HotReload changes to `LibaryWindow.xaml` does not work. Probably related to the above.