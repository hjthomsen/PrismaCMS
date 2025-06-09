namespace PrismaCMS.Domain.ValueObjects
{
    public class ContactInfo
    {
        public string? Email { get; private set; }
        public string? Phone { get; private set; }
        public string? Address { get; private set; }
        public string? City { get; private set; }
        public string? PostalCode { get; private set; }
        public string? Country { get; private set; }

        private ContactInfo() { }

        public ContactInfo(string? email, string? phone, string? address, string? city, string? postalCode, string? country)
        {
            Email = email;
            Phone = phone;
            Address = address;
            City = city;
            PostalCode = postalCode;
            Country = country;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not ContactInfo other)
                return false;

            return Email == other.Email &&
                   Phone == other.Phone &&
                   Address == other.Address &&
                   City == other.City &&
                   PostalCode == other.PostalCode &&
                   Country == other.Country;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Email, Phone, Address, City, PostalCode, Country);
        }
    }
}