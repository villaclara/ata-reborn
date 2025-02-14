namespace UI.WPF.Services.Abstracts;

/// <summary>
/// Interface to create a new Window.
/// It does not specify the Window to create Type, the implementation does this.
/// </summary>
public interface IWindowCreator
{
	/// <summary>
	/// Create and display new Window.
	/// </summary>
	void CreateWindow();
}
