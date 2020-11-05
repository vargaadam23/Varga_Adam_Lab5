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


using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;

namespace Varga_Adam_Lab5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    enum ActionState
    {
        New,
        Edit,
        Delete,
        Nothing
    }
    public partial class MainWindow : Window
    {
        ActionState action = ActionState.Nothing;
        PhoneNumbersDataSet phoneNumbersDataSet = new PhoneNumbersDataSet();
        PhoneNumbersDataSetTableAdapters.PhoneNumbersTableAdapter
        tblPhoneNumbersAdapter = new PhoneNumbersDataSetTableAdapters.PhoneNumbersTableAdapter();
        Binding txtPhoneNumberBinding = new Binding();
        Binding txtSubscriberBinding = new Binding();
        Binding txtValueBinding = new Binding();
        Binding txtDateBinding = new Binding();
        public MainWindow()
        {
            InitializeComponent();

            grdMain.DataContext = phoneNumbersDataSet.PhoneNumbers;
            txtPhoneNumberBinding.Path = new PropertyPath("Phonenum");
            txtSubscriberBinding.Path = new PropertyPath("Subscriber");
            txtValueBinding.Path = new PropertyPath("Value");
            txtDateBinding.Path = new PropertyPath("Date");
            txtPhoneNumber.SetBinding(TextBox.TextProperty, txtPhoneNumberBinding);
            txtSubscriber.SetBinding(TextBox.TextProperty, txtSubscriberBinding);
            txtValue.SetBinding(TextBox.TextProperty, txtValueBinding);
            txtDate.SetBinding(TextBox.TextProperty, txtDateBinding);

        }
        private void lstPhonesLoad()
        {
            tblPhoneNumbersAdapter.Fill(phoneNumbersDataSet.PhoneNumbers);
        }

        private void frmMain_Loaded(object sender, RoutedEventArgs e)
        {
            PhoneNumbersDataSet phoneNumbersDataSet =((PhoneNumbersDataSet)(this.FindResource("phoneNumbersDataSet")));
            System.Windows.Data.CollectionViewSource phoneNumbersViewSource =((System.Windows.Data.CollectionViewSource)(this.FindResource("phoneNumbersViewSource")));
            phoneNumbersViewSource.View.MoveCurrentToFirst();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Close Application?", "Question", MessageBoxButton.YesNo,MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void grdMain_Loaded(object sender, RoutedEventArgs e)
        {
            lstPhonesLoad();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
            btnNew.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;

            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;
            lstPhones.IsEnabled = false;
            btnPrevious.IsEnabled = false;
            btnNext.IsEnabled = false;
            txtPhoneNumber.IsEnabled = true;
            txtSubscriber.IsEnabled = true;
            txtValue.IsEnabled = true;
            txtDate.IsEnabled = true;
            BindingOperations.ClearBinding(txtPhoneNumber, TextBox.TextProperty);
            BindingOperations.ClearBinding(txtSubscriber, TextBox.TextProperty);
            BindingOperations.ClearBinding(txtValue, TextBox.TextProperty);
            BindingOperations.ClearBinding(txtDate, TextBox.TextProperty);
            txtPhoneNumber.Text = "";
            txtSubscriber.Text = "";
            txtValue.Text = "";
            txtDate.Text = "";
            Keyboard.Focus(txtPhoneNumber);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;
            string tempPhonenum = txtPhoneNumber.Text.ToString();
            string tempSubscriber = txtSubscriber.Text.ToString();
            string tempValue = txtValue.Text.ToString();
            string tempDate = txtDate.Text.ToString();

            btnNew.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;
            lstPhones.IsEnabled = false;
            btnPrevious.IsEnabled = false;
            btnNext.IsEnabled = false;
            txtPhoneNumber.IsEnabled = true;
            txtSubscriber.IsEnabled = true;
            txtValue.IsEnabled = true;
            txtDate.IsEnabled = true;
            BindingOperations.ClearBinding(txtPhoneNumber, TextBox.TextProperty);
            BindingOperations.ClearBinding(txtSubscriber, TextBox.TextProperty);
            BindingOperations.ClearBinding(txtValue, TextBox.TextProperty);
            BindingOperations.ClearBinding(txtDate, TextBox.TextProperty);

            txtPhoneNumber.Text = tempPhonenum;
            txtSubscriber.Text = tempSubscriber;
            txtValue.Text = tempValue;
            txtDate.Text = tempDate;
            Keyboard.Focus(txtPhoneNumber);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;
            string tempPhonenum = txtPhoneNumber.Text.ToString();
            string tempSubscriber = txtSubscriber.Text.ToString();
            string tempValue = txtValue.Text.ToString();
            string tempDate = txtDate.Text.ToString();
            btnNew.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;
            lstPhones.IsEnabled = false;
            btnPrevious.IsEnabled = false;
            btnNext.IsEnabled = false;
            BindingOperations.ClearBinding(txtPhoneNumber, TextBox.TextProperty);
            BindingOperations.ClearBinding(txtSubscriber, TextBox.TextProperty);
            BindingOperations.ClearBinding(txtValue, TextBox.TextProperty);
            BindingOperations.ClearBinding(txtDate, TextBox.TextProperty);
            txtPhoneNumber.Text = tempPhonenum;
            txtSubscriber.Text = tempSubscriber;
            txtValue.Text = tempValue;
            txtDate.Text = tempDate;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Nothing;
            btnNew.IsEnabled = true;
            btnEdit.IsEnabled = true;
            btnEdit.IsEnabled = true;
            btnSave.IsEnabled = false;
            btnCancel.IsEnabled = false;
            lstPhones.IsEnabled = true;
            btnPrevious.IsEnabled = true;
            btnNext.IsEnabled = true;
            txtPhoneNumber.IsEnabled = false;
            txtSubscriber.IsEnabled = false;
            txtValue.IsEnabled = false;
            txtDate.IsEnabled = false;
            txtPhoneNumber.SetBinding(TextBox.TextProperty, txtPhoneNumberBinding);
            txtSubscriber.SetBinding(TextBox.TextProperty, txtSubscriberBinding);
            txtValue.SetBinding(TextBox.TextProperty, txtValueBinding);
            txtDate.SetBinding(TextBox.TextProperty, txtDateBinding);

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (action == ActionState.New)
            {
                try
                {
                    DataRow newRow = phoneNumbersDataSet.PhoneNumbers.NewRow();
                    newRow.BeginEdit();
                    newRow["Phonenum"] = txtPhoneNumber.Text.Trim();
                    newRow["Subscriber"] = txtSubscriber.Text.Trim();
                    newRow["Value"] =Int32.Parse(txtValue.Text.Trim());
                    newRow["Date"] =DateTime.Parse(txtDate.Text.Trim());
                    newRow.EndEdit();
                    phoneNumbersDataSet.PhoneNumbers.Rows.Add(newRow);
                    tblPhoneNumbersAdapter.Update(phoneNumbersDataSet.PhoneNumbers);
                    phoneNumbersDataSet.AcceptChanges();
                }
                catch (Exception ex)
                {
                    phoneNumbersDataSet.RejectChanges();
                    MessageBox.Show(ex.Message);
                }
                btnNew.IsEnabled = true;
                btnEdit.IsEnabled = true;
                btnSave.IsEnabled = false;
                btnCancel.IsEnabled = false;
                lstPhones.IsEnabled = true;
                btnPrevious.IsEnabled = true;
                btnNext.IsEnabled = true;
                txtPhoneNumber.IsEnabled = false;
                txtSubscriber.IsEnabled = false;
                txtValue.IsEnabled = false;
                txtDate.IsEnabled = false;
            }
            else
            if (action == ActionState.Edit)
            {
                try
                {
                    DataRow editRow = phoneNumbersDataSet.PhoneNumbers.Rows[lstPhones.SelectedIndex];
                    editRow.BeginEdit();
                    editRow["Phonenum"] = txtPhoneNumber.Text.Trim();
                    editRow["Subscriber"] = txtSubscriber.Text.Trim();
                    editRow["Value"] = Int32.Parse(txtValue.Text.Trim());
                    editRow["Date"] = DateTime.Parse(txtDate.Text.Trim());
                    editRow.EndEdit();
                    tblPhoneNumbersAdapter.Update(phoneNumbersDataSet.PhoneNumbers);
                    phoneNumbersDataSet.AcceptChanges();
                }
                catch (Exception ex)
                {
                    phoneNumbersDataSet.RejectChanges();
                    MessageBox.Show(ex.Message);
                }
                btnNew.IsEnabled = true;
                btnEdit.IsEnabled = true;
                btnDelete.IsEnabled = true;
                btnSave.IsEnabled = false;
                btnCancel.IsEnabled = false;
                lstPhones.IsEnabled = true;
                btnPrevious.IsEnabled = true;
                btnNext.IsEnabled = true;
                txtPhoneNumber.IsEnabled = false;
                txtSubscriber.IsEnabled = false;
                txtValue.IsEnabled = false;
                txtDate.IsEnabled = false;
                txtPhoneNumber.SetBinding(TextBox.TextProperty, txtPhoneNumberBinding);
                txtSubscriber.SetBinding(TextBox.TextProperty, txtSubscriberBinding);
                txtValue.SetBinding(TextBox.TextProperty, txtValueBinding);
                txtDate.SetBinding(TextBox.TextProperty, txtDateBinding);
            }
            else
               if (action == ActionState.Delete)
               {
                try
                {
                    DataRow deleterow = phoneNumbersDataSet.PhoneNumbers.Rows[lstPhones.SelectedIndex];
                    deleterow.Delete();

                    tblPhoneNumbersAdapter.Update(phoneNumbersDataSet.PhoneNumbers);
                    phoneNumbersDataSet.AcceptChanges();
                }
                catch (DataException ex)
                {
                    phoneNumbersDataSet.RejectChanges(); MessageBox.Show(ex.Message);
                    MessageBox.Show(ex.Message);
                }
                btnNew.IsEnabled = true;
                btnEdit.IsEnabled = true;
                btnDelete.IsEnabled = true;
                btnSave.IsEnabled = false;
                btnCancel.IsEnabled = false;
                lstPhones.IsEnabled = true;
                btnPrevious.IsEnabled = true;
                btnNext.IsEnabled = true;
                txtPhoneNumber.IsEnabled = false;
                txtSubscriber.IsEnabled = false;
                txtValue.IsEnabled = false;
                txtDate.IsEnabled = false;
                txtPhoneNumber.SetBinding(TextBox.TextProperty, txtPhoneNumberBinding);
                txtSubscriber.SetBinding(TextBox.TextProperty, txtSubscriberBinding);
                txtValue.SetBinding(TextBox.TextProperty, txtValueBinding);
                txtDate.SetBinding(TextBox.TextProperty, txtDateBinding);
            }
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView navigationView = CollectionViewSource.GetDefaultView(phoneNumbersDataSet.PhoneNumbers);
            navigationView.MoveCurrentToPrevious();

        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView navigationView =CollectionViewSource.GetDefaultView(phoneNumbersDataSet.PhoneNumbers);
            navigationView.MoveCurrentToNext();
        }
    }
}
