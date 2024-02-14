namespace GeoFizik.Model
{
    public class Customer : PropChange
    {
        private int id;
        private string name;
        List<Project> projects;

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

        public List<Project> Projects
        {
            get { return projects; }
            set
            {
                projects = value; OnPropertyChanged(nameof(Projects));
            }
        }
    }
}