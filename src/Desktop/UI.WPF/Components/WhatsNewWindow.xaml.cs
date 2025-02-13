using System.Windows;
using UI.WPF.ViewModels;

namespace UI.WPF.Views
{
	/// <summary>
	/// Interaction logic for WhatsNewWindow.xaml
	/// </summary>
	public partial class WhatsNewWindow : Window
	{
		public WhatsNewWindow()
		{
			InitializeComponent();
			DataContext = new WhatsNewViewModel();
		}
	}
}
