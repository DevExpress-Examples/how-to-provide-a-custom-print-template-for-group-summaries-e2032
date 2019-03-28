using DevExpress.Mvvm.POCO;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace E2032 {

    public class MainWindowViewModel {
        Random r;
        protected MainWindowViewModel() {
            r = new Random();
            Items = new ObservableCollection<Item>();
            for (int i = 0; i < 50; i++) {
                Items.Add(Item.Create(i, "Item " + i, r.Next(0, 50), (Category)r.Next(0, 3)));
            }
        }

        public virtual ObservableCollection<Item> Items { get; set; }
    }
    public class Item {
        protected Item() { }
        public static Item Create(int id, string name, double value, Category category) {
            return ViewModelSource.Create(() => new Item() { ID = id, Name = name, Value = value, Category = category });
        }
        public virtual int ID { get; set; }

        public virtual string Name { get; set; }

        public virtual double Value { get; set; }

        public virtual Category Category { get; set; }
    }

    public enum Category {
        Deferred, Normal, Urgent
    }
}
