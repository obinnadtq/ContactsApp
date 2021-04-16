﻿using DesktopContactsApp.Classes;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopContactsApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			ReadDatabase();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			NewContactWindow newContactWindow = new NewContactWindow();
			newContactWindow.ShowDialog();

			ReadDatabase();
		}

		void ReadDatabase()
		{
			List<Contact> contacts;
			using(SQLiteConnection conn = new SQLiteConnection(App.databasePath))
			{
				conn.CreateTable<Contact>();
				contacts = (conn.Table<Contact>().ToList()).OrderBy(c => c.Name).ToList();
			}

			if(contacts != null)
			{
				contactsListView.ItemsSource = contacts;
			}
		}

		private void contactsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Contact selectedContact = (Contact)contactsListView.SelectedItem;

			if(selectedContact != null)
			{
				ContactDetailsWindow contactDetailsWindow = new ContactDetailsWindow(selectedContact);
				contactDetailsWindow.ShowDialog();
				ReadDatabase();
			}
		}
	}
}
