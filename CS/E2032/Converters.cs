using System;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace E2032 {
    public class RemoveParenthesesConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var str = value as string;
            if (str == null)
                return null;
            return str.Replace("(", string.Empty).Replace(")", string.Empty);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public class PrintRowInfoToImageSourceConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            Category category;
            if (Enum.TryParse(value.ToString(), out category)) {
                switch (category) {
                    case Category.Deferred:
                        return "Images/Deffered_32x32.png";
                    case Category.Normal:
                        return "Images/Normal_32x32.png";
                    case Category.Urgent:
                        return "Images/Urgent_32x32.png";
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
