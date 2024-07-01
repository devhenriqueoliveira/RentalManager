namespace RentalManager.Domain.Entities
{
    public class Motorcycle : Entity
    {
        public int Year { get; private set; }
        public string Plate { get; private set; } = default!;
        public string Model { get; private set; } = default!;

        public Motorcycle(int year, string plate, string model)
        {
            if (year < 1885 || year > DateTime.Now.Year)
                throw new ArgumentException("Year is out of valid range.", nameof(year));

            if (string.IsNullOrWhiteSpace(plate))
                throw new ArgumentException("Plate cannot be empty or null.", nameof(plate));

            if (string.IsNullOrWhiteSpace(model))
                throw new ArgumentException("Model cannot be empty or null.", nameof(model));

            Year = year;
            Plate = plate;
            Model = model;
        }

        public Motorcycle()
        {
            
        }

        public void ChangeYear(int newYear)
        {
            if (newYear < 1885 || newYear > DateTime.Now.Year)
                throw new ArgumentException("Year is out of valid range.", nameof(newYear));

            Year = newYear;
        }

        public void ChangePlate(string newPlate)
        {
            if (string.IsNullOrWhiteSpace(newPlate))
                throw new ArgumentException("New plate cannot be empty or null.", nameof(newPlate));

            Plate = newPlate;
        }

        public void ChangeModel(string newModel)
        {
            if (string.IsNullOrWhiteSpace(newModel))
                throw new ArgumentException("New model cannot be empty or null.", nameof(newModel));

            Model = newModel;
        }

        public int GetAge()
        {
            return DateTime.Now.Year - Year;
        }

        public override string ToString()
        {
            return $"{Year} {Model} (Plate: {Plate})";
        }
    }
}
