using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace E2032 {
    public class ViewModelBase : INotifyPropertyChanged {
        public ViewModelBase() { }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e) {
            PropertyChanged?.Invoke(sender, e);
        }

        protected void RaisePropertyChanged(string propertyName) {
            this.OnPropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class MainWindowViewModel : ViewModelBase {
        Random r;
        public MainWindowViewModel() {
            r = new Random();
            Items = new ObservableCollection<Item>();
            for (int i = 0; i < 50; i++) {
                Items.Add(new Item() { ID = i, Name = "Item " + i, Value = r.Next(0, 50), Category = (Category)r.Next(0, 3) });
            }
        }

        ObservableCollection<Item> items;
        public ObservableCollection<Item> Items {
            get {
                return items;
            }
            set {
                if (items == value) {
                    return;
                }

                items = value;
                RaisePropertyChanged(nameof(Items));
            }
        }
    }
    public class Item : ViewModelBase {
        public Item() { }

        int iD;
        public int ID {
            get {
                return iD;
            }
            set {
                if (iD == value) {
                    return;
                }

                iD = value;
                RaisePropertyChanged(nameof(ID));
            }
        }
        string name;
        public string Name {
            get {
                return name;
            }
            set {
                if (name == value) {
                    return;
                }

                name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }
        double _value;
        public double Value {
            get {
                return _value;
            }
            set {
                if (_value == value) {
                    return;
                }

                _value = value;
                RaisePropertyChanged(nameof(Value));
            }
        }
        Category category;
        public Category Category {
            get {
                return category;
            }
            set {
                if (category == value) {
                    return;
                }

                category = value;
                RaisePropertyChanged(nameof(Category));
            }
        }
    }
    public enum Category {
        Deferred, Normal, Urgent
    }
}
