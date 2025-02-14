using System.Windows;

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
		}

		private void OKBUTTON_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
