namespace Library.Domain.Models
{
    public abstract class Entity<T>
    {
        protected readonly string _id;
        protected T props;

        protected Entity(T props, string? id)
        {
            this._id = !string.IsNullOrEmpty(id) ? id : Guid.NewGuid().ToString();
            this.props = props;
        }

        public bool equals(Entity<T>? obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }

            return _id == obj._id;
        }
    }
}
