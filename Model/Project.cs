namespace GeoFizik.Model
{
    public class Project : PropChange
    {
        private int id;
        private string name;
        Customer customer_;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string? Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public Customer Customer_
        {
            get { return customer_; }
            set
            {
                customer_ = value;
                OnPropertyChanged(nameof(Customer));
            }
        }
    }
}