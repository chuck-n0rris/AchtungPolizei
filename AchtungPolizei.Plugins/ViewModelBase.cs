namespace AchtungPolizei.Plugins
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Base class for any view model. Note: pay attention at following example of usage.
    /// </summary>
    /// <example>
    /// class MyViewModel : ViewModelBase&lt;MyViewModel&gt;
    /// {
    ///     private string name;
    ///     public string Name
    ///     {
    ///         get { return name; }
    ///         set { ChangeProperty(x => x.Name, value); }
    ///     }
    ///
    ///     public string NameValidator()
    ///     {
    ///         return string.IsNullOrWhiteSpace(name) ? "Name can not be empty!" : null;
    ///     }
    /// }
    /// </example>
    /// <typeparam name="T">Type of view model.</typeparam>
    public abstract class ViewModelBase<T> : IDataErrorInfo, INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when property changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Contanins error message.
        /// </summary>
        public string Error { get; protected set; }

        /// <summary>
        /// Returns error message for given field.
        /// </summary>
        /// <param name="columnName">Name of the field.</param>
        /// <returns>Error message.</returns>
        public string this[string columnName]
        {
            get
            {
                var method = this.GetType().GetMethod(columnName + "Validator");
                if (method == null)
                {
                    return null;
                }

                var error = method.Invoke(this, new object[0]) as string;
                this.Error = this.Error ?? error;
                return error;
            }
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        /// <returns></returns>
        public bool Validate()
        {
            this.ForceValidation();
            return this.Error == null;
        }

        /// <summary>
        /// Changes the property.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="setter">The setter.</param>
        /// <param name="value">The value.</param>
        protected void ChangeProperty<TProperty>(Expression<Func<T, TProperty>> setter, TProperty value)
        {
            try
            {
                var expression = setter.Body as MemberExpression;
                var name = expression.Member.Name;

                // assumption: field names are in camel case without leading underscore
                var fieldName = char.ToLower(name[0]) + name.Substring(1);
                var field = this.GetType().GetField(
                    fieldName, 
                    BindingFlags.Instance | BindingFlags.NonPublic);

                var previousValue = (TProperty)field.GetValue(this);
                if (value.Equals(previousValue))
                {
                    return;
                }

                field.SetValue(this, value);

                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs(name));
                }
            }
            catch (Exception exception)
            {
                throw new ApplicationException("Can't set property value.", exception);
            }
        }

        public void RaisePropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Forces the validation.
        /// </summary>
        private void ForceValidation()
        {
            this.Error = this.Error ?? this.GetType()
                                 .GetMethods()
                                 .Where(x => x.Name.EndsWith("Validator"))
                                 .Select(x => x.Invoke(this, new object[0]) as string)
                                 .FirstOrDefault(x => x != null);
        }
    }
}