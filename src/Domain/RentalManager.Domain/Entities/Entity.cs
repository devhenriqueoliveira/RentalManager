namespace RentalManager.Domain.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public void PersisteId(Guid id)
        {
            if (id == null || id == default)
                throw new ArgumentException("Id is empty or null", nameof(id));

            Id = id;
        }
    }
}
