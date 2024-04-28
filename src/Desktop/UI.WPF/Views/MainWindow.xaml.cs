using System.Windows;
using System.Windows.Input;

namespace UI.WPF.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
	}

	// Dragging window. It should probably not be in code behind, but it is very simple and I do not care.
	private void TopRow_MouseDown(object sender, MouseButtonEventArgs e)
	{
		if (e.ChangedButton == MouseButton.Left)
		{
			this.DragMove();
		}
	}

}