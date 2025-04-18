using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker
{
    internal class AddExpenseItemViewModel : INotifyPropertyChanged // should notify ui when property is changed
    {
        // expense form properties
        public string _ExpenseName="";
        public string _ExpenseAmount = "";
        public string _SelectedExpenseType;
        public DateTime _SelectedDate = DateTime.Now;   

        public event PropertyChangedEventHandler PropertyChanged;

        public List<string> ExpenseTypes { get; } = new List<string> //creates a list as well as gets ExpenseType from user
        {
            "Food", "Transportation", "Entertainment", "Utilities", "Shopping"
        };

        public string ExpenseName //was last addition to the properties
        {
            get { return _ExpenseName; }
            set
            {
                if (_ExpenseName != value)
                {
                    _ExpenseName = value;
                    OnPropertyChanged(nameof(ExpenseName));
                }
            }
        }

        public string ExpenseAmount
        {
            get { return _ExpenseAmount; }
            set
            {
                if (_ExpenseAmount != value) //if new value doesn't match old value
                {
                    _ExpenseAmount = value; // then set new value
                    OnPropertyChanged(nameof(ExpenseAmount)); //notify property has changed
                }
            }
        }

        //im on this step 
        public string SelectedExpenseType
        {
            get { return _SelectedExpenseType; }
            set
            {
                if (_SelectedExpenseType != value) //if new value doesn't match old value
                {
                    _SelectedExpenseType = value; // then set new value
                    OnPropertyChanged(nameof(SelectedExpenseType));
                }
            }
        }

        public DateTime SelectedDate
        {
            get { return _SelectedDate; }
            set
            {
                if (_SelectedDate != value) //if new value doesn't match old value
                {
                    _SelectedDate = value; // then set new value
                    OnPropertyChanged(nameof(SelectedDate));
                }
            }
        }


        public AddExpenseItemViewModel()
        {
            // Defaults; if needed
            _ExpenseAmount = "";
            _SelectedExpenseType = "Food";
            _SelectedDate = DateTime.Now;
        }
        protected virtual void OnPropertyChanged(string propertyName) //part of INotify
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
